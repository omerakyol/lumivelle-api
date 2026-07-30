using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Wardrobe.Queries.GetOutfitSuggestions;

public class GetOutfitSuggestionsQueryHandler(
    IWardrobeItemRepository wardrobeItemRepository,
    IBeautyProfileRepository beautyProfileRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<GetOutfitSuggestionsQueryRequest, IDataResult<OutfitSuggestionsResult>>
{
    private const int MaxCombos = 3;

    public async Task<IDataResult<OutfitSuggestionsResult>> Handle(
        GetOutfitSuggestionsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var items = await wardrobeItemRepository.GetByAccountIdAsync(accountId, null);
        var profile = await beautyProfileRepository.GetLatestByAccountIdAsync(accountId);

        var anchors = items
            .Where(i => i.Category is "Tops" or "Dresses")
            .OrderByDescending(i => i.PaletteMatchScore)
            .Take(MaxCombos)
            .ToList();

        var byCategory = items
            .GroupBy(i => i.Category)
            .ToDictionary(g => g.Key, g => g.OrderByDescending(i => i.PaletteMatchScore).ToList());

        var usedByCategory = new Dictionary<string, HashSet<ObjectId>>();
        var combos = new List<OutfitComboResult>();

        foreach (var anchor in anchors)
        {
            var comboItems = new List<WardrobeItemDocument> { anchor };
            var fillCategories = anchor.Category == "Dresses"
                ? new[] { "Shoes", "Accessories" }
                : new[] { "Bottoms", "Shoes", "Accessories" };

            foreach (var category in fillCategories)
            {
                if (!byCategory.TryGetValue(category, out var candidates) || candidates.Count == 0)
                    continue;

                if (!usedByCategory.TryGetValue(category, out var used))
                {
                    used = [];
                    usedByCategory[category] = used;
                }

                var pick = candidates.FirstOrDefault(c => !used.Contains(c.Id)) ?? candidates[0];
                used.Add(pick.Id);
                comboItems.Add(pick);
            }

            var score = (int)Math.Round(comboItems.Average(i => i.PaletteMatchScore));
            combos.Add(new OutfitComboResult
            {
                Items = comboItems.Select(i => new OutfitComboItemResult
                {
                    Category = i.Category,
                    WardrobeItemId = i.Id.ToString(),
                    Name = i.Name,
                    ImageUrl = i.ImageUrl
                }).ToList(),
                Score = score,
                Why = BuildWhy(profile)
            });
        }

        var ordered = combos.OrderByDescending(c => c.Score).ToList();

        return new SuccessDataResult<OutfitSuggestionsResult>(new OutfitSuggestionsResult { Combos = ordered });
    }

    private static string BuildWhy(BeautyProfileDocument profile)
    {
        return !string.IsNullOrEmpty(profile?.Season)
            ? $"Pieces from your {profile.Season} palette, ranked by match"
            : "Pieces from your wardrobe, ranked by match";
    }
}
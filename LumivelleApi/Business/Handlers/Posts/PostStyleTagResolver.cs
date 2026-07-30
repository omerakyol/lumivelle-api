using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using MongoDB.Bson;

namespace Business.Handlers.Posts;

public static class PostStyleTagResolver
{
    public static async Task<Dictionary<ObjectId, HashSet<string>>> GetTagsByPostIdAsync(
        List<PostDocument> posts,
        IWardrobeItemRepository wardrobeItemRepository,
        IOutfitRepository outfitRepository)
    {
        var wardrobeItemIds = posts.Where(p => p.WardrobeItemId.HasValue)
            .Select(p => p.WardrobeItemId!.Value).Distinct().ToList();
        var outfitIds = posts.Where(p => p.OutfitId.HasValue)
            .Select(p => p.OutfitId!.Value).Distinct().ToList();

        var wardrobeItemsById = (await wardrobeItemRepository.GetByIdsAsync(wardrobeItemIds))
            .ToDictionary(i => i.Id);
        var outfits = await outfitRepository.GetByIdsAsync(outfitIds);
        var outfitsById = outfits.ToDictionary(o => o.Id);

        var outfitMemberItemIds = outfits.SelectMany(o => o.ItemIds).Distinct().ToList();
        var outfitMemberItemsById = (await wardrobeItemRepository.GetByIdsAsync(outfitMemberItemIds))
            .ToDictionary(i => i.Id);

        var result = new Dictionary<ObjectId, HashSet<string>>();

        foreach (var post in posts)
        {
            var tags = new HashSet<string>();

            if (post.WardrobeItemId.HasValue
                && wardrobeItemsById.TryGetValue(post.WardrobeItemId.Value, out var item))
                foreach (var tag in item.StyleTags)
                    tags.Add(tag);

            if (post.OutfitId.HasValue && outfitsById.TryGetValue(post.OutfitId.Value, out var outfit))
                foreach (var memberId in outfit.ItemIds)
                    if (outfitMemberItemsById.TryGetValue(memberId, out var memberItem))
                        foreach (var tag in memberItem.StyleTags)
                            tags.Add(tag);

            if (tags.Count > 0)
                result[post.Id] = tags;
        }

        return result;
    }
}
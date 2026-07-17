using System.Text.RegularExpressions;
using Business.Handlers.Recommendations.Queries.GetDailyEdit;
using FluentValidation;

namespace Business.Handlers.Recommendations.ValidationRules;

public class GetDailyEditValidator : AbstractValidator<GetDailyEditQueryRequest>
{
    public GetDailyEditValidator()
    {
        RuleFor(x => x.LocalDate)
            .NotEmpty()
            .Must(date => date != null && Regex.IsMatch(date, @"^\d{4}-\d{2}-\d{2}$"))
            .WithMessage("LocalDate must be in yyyy-MM-dd format");
    }
}

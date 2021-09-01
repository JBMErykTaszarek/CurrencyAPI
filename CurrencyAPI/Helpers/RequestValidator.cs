using CurrencyAPI.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyAPI.Helpers
{
    public class RequestValidator : AbstractValidator<CurrencyQuery>
    {
        public RequestValidator()
        {


            RuleFor(query => query.currencyCodes).Cascade(CascadeMode.Stop)
                                                 .NotEmpty().WithMessage("Please fill currency codes first")
                                                 .Must((query, currencyCodes) => { return ValidateCodesLength(currencyCodes); })
                                                 .WithMessage("Bad currency codes. Please type 3 charcters codes.)")
                                                 .Must((query, currencyCodes) => { return ValidateCodesCapital(currencyCodes); })
                                                 .WithMessage("Bad currency codes. Currency codes have to be written with capital letters.)");

            RuleFor(query => query.startDate).NotEmpty().WithMessage("Please fill start date first")
                                             .Must((query, startDate) => { return ValidateDateOrder(query.startDate, query.endDate); })
                                             .WithMessage("Bad date order. Please check if start date is earlier or equal to end date")
                                             .Must((query, startDate) => { return ValidateFutureDate(query.endDate); })
                                             .WithMessage("Bad end date. Please check if end date does not contain date from the future");

        }


        private bool ValidateCodesLength(IDictionary<string, string> currencyCodes)
        {
            if (currencyCodes.Keys.Any(key => key.Length != 3) || currencyCodes.Values.Any(value => value.Length != 3))
            {
                return false;
            }
            return true;
        }

        private bool ValidateCodesCapital(IDictionary<string, string> currencyCodes)
        {
            if (currencyCodes.Keys.Any(key => key.ToUpper() != key) || currencyCodes.Values.Any(value => value.ToUpper() != value))
            {
                return false;
            }
            return true;
        }

        private bool ValidateDateOrder (DateTime startDate, DateTime endDate)
        {
            return DateTime.Compare(startDate, endDate) <= 0;
        }

        private bool ValidateFutureDate (DateTime endDate)
        {
            return DateTime.Compare(endDate, DateTime.Now) < 0;
        }


    }
}

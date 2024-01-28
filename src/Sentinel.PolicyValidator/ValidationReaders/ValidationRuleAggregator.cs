using Microsoft.Extensions.Logging;
using Sentinel.PolicyValidator.Extensions;
using Sentinel.PolicyValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sentinel.PolicyValidator.ValidationReaders
{
    public class ValidationRuleAggregator
    {
        public IDictionary<string, ValidationModel> MergedValidations = new Dictionary<string, ValidationModel>();
        public ValidationRuleAggregator(IEnumerable<IValidationReader> readers, ILogger<ValidationRuleAggregator> logger)
        {
            foreach (var reader in readers)
            {
                var validations = reader.Read();
                foreach (var validation in validations)
                {
                    if (MergedValidations.ContainsKey(validation.Key))
                    {
                        MergedValidations[validation.Key].Validations.AddRange(validation.Value.Validations);
                    }
                    else
                    {
                        MergedValidations.Add(validation);
                    }

                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sentinel.PolicyValidator.Models;

namespace Sentinel.PolicyValidator.ValidationReaders;

public interface IValidationReader
{
    IDictionary<string, ValidationModel> Read();

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sentinel.PolicyValidator.ValidationReaders;

public class JsonValidationOptions
{
    public static string JsonValidationLocation = "JsonValidationLocation";

    public List<string> Locations { get; set; } = new List<string>();
}
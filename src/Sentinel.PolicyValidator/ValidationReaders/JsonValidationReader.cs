using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Sentinel.PolicyValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sentinel.PolicyValidator.ValidationReaders;

public class JsonValidationReader : IValidationReader
{
    List<string> _locations = new List<string>();
    public JsonValidationReader(IOptions<JsonValidationOptions> JsonValidationOptions)
    {
        if (JsonValidationOptions?.Value?.Locations != null)
        {
            _locations = JsonValidationOptions.Value.Locations;
        }

        JsonValidationOptions.Value.Locations.ForEach(x => Console.WriteLine(x));
    }

    public IDictionary<string, ValidationModel> Read()
    {
        IDictionary<string, ValidationModel> validations = new Dictionary<string, ValidationModel>();

        var Jsons = ExtractJtokenFromFile("./Validation.json");
        var headers = Jsons?.Children();
        if (headers != null)
        {
            foreach (var header in headers)
            {
                if (header is JProperty item)
                {
                    var val = item.Value.ToObject<ValidationModel>();
                    val.Name = item.Name;
                    validations.Add(item.Name, val);
                }
            }
        }

        return validations;
        // var Jsons = ExtractJtokenFromFile("./Validation.json");
        // var headers = Jsons?.Children();
        // if (headers != null)
        // {
        //     foreach (var header in headers)
        //     {
        //         if (header is JProperty item)
        //         {

        //             Console.WriteLine(item.Name);
        //             // item.Value.ToObject<ValidationModel>();
        //             //var content = header.SelectToken("$." + item.Name);
        //             var val = item.Value.ToObject<ValidationModel>();
        //             val.Name = item.Name;
        //             Console.WriteLine(val.K8sResource.KubeKind);
        //         }
        //     }
        // }

    }

    private JToken? ExtractJtokenFromFile(string fileLocation)
    {
        var fileContent = System.IO.File.ReadAllText(fileLocation);
        var o1 = JObject.Parse(fileContent);
        var valition = o1.SelectToken("$.Validate");
        return valition;
    }

}

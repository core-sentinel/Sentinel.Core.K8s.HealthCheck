using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sentinel.PolicyValidator.Models;
public class PolicyItemValidationResult
{
    public bool Isvalid { get; init; }
    public string ResourceName { get; init; } = string.Empty;
    public string ResourceNamespace { get; init; } = string.Empty;
    public string ResourceType { get; init; } = string.Empty;
    public string Error { get; init; } = string.Empty;
    public string? CapturedString { get; init; } = string.Empty;
    public ValidationRuleModel Rule { get; init; } = default!;
}
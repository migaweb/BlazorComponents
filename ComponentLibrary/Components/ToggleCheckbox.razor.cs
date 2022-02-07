using ComponentLibrary.Components.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;

namespace ComponentLibrary.Components;

public partial class ToggleCheckbox : InputBase<bool>
{
  [CascadingParameter] protected Theme Theme { get; set; } = null!;

  private const string _cssScope = "togglecb-component";
  [Parameter] public string Id { get; set; } = $"{nameof(ToggleCheckbox)}_{DateTime.Now.Ticks}";
  [Parameter] public string? Label { get; set; }

  protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out bool result, [NotNullWhen(false)] out string? validationErrorMessage)
  {
    if (bool.TryParse(value, out var resultBool))
    {
      result = resultBool;
      validationErrorMessage = null;
      return true;
    }
    else
    {
      result = default;
      validationErrorMessage = "The chosen value is not a valid boolean.";
      return false;
    }
  }
}

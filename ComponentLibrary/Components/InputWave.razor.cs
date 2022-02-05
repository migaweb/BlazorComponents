using ComponentLibrary.Components.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ComponentLibrary.Components
{
  public partial class InputWave : InputBase<string>
  {
    [Parameter]
    public string? Label { get; set; }

    [Parameter]
    public string Id { get; set; } = "wave_input" + DateTime.Now.Ticks;

    protected override bool TryParseValueFromString(string? value, out string result, out string validationErrorMessage)
    {
      result = value!;
      validationErrorMessage = "";
      return true;
    }

    [CascadingParameter] protected Theme Theme { get; set; } = null!;

    private async Task OnInputChange(ChangeEventArgs args)
    {
      Value = (string?)args.Value;
      await ValueChanged.InvokeAsync(Value);
    }

    private const string _cssScope = "wave-component";

    public string LabelMarkupString
    {
      get
      {
        if (String.IsNullOrWhiteSpace(Label)) return string.Empty;

        var chars = Label.ToArray();
        var htmlLabel = string.Empty;
        for (int i = 0; i < chars.Length; i++)
        {
          htmlLabel += $"<span style=\"transition-delay:{ i * 50}ms\" {_cssScope}>{chars[i]}</span>";
        }

        return htmlLabel;
      }
    }
  }
}

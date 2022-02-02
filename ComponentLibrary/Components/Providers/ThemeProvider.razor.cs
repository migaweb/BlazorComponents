using Microsoft.AspNetCore.Components;

namespace ComponentLibrary.Components.Providers
{
  public partial class ThemeProvider
  {
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public Theme Theme { get; set; } = new ();
  }
}

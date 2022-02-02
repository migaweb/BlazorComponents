using ComponentLibrary.Components.Providers;
using Microsoft.AspNetCore.Components;

namespace ComponentLibrary.Components
{
  public abstract class BaseComponent : ComponentBase
  {
    [CascadingParameter] protected Theme Theme { get; set; } = null!;
  }
}

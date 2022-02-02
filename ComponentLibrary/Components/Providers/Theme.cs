namespace ComponentLibrary.Components.Providers;

public class Theme
{
  public Theme()
  {
    Colors = new Colors();
  }

  public Colors Colors { get; set; }
}

public class Colors
{
  public string Primary { get; set; } = "#3498db";
  public string Secondary { get; set; } = "#3498db";
  public string Gray { get; set; } = "#e0e0e0";
}

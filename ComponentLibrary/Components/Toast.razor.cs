using Microsoft.AspNetCore.Components;

namespace ComponentLibrary.Components;
public partial class Toast : BaseComponent
{
  private const int _defaultVisibleDuration = 3;
  private int _visibleDuration = 3;

  /// <summary>
  /// Sets the position of the toast container.
  /// Values: BottomRight, BottomLeft
  /// </summary>
  [Parameter] public ToastPosition Position { get; set; } = ToastPosition.BottomRight;

  /// <summary>
  /// Sets how long the toast should be visible.
  /// Value between 2 and 10 seconds.
  /// </summary>
  [Parameter] public int VisibleDuration 
  { 
    get => _visibleDuration;
    set {
      if (value >= 2 || value <= 10)
        _visibleDuration = value;
      else
        _visibleDuration = _defaultVisibleDuration;
    } 
  }
  private List<ToastMessage> ToastMessages { get; set; } = new List<ToastMessage>();

  public void CreateNotification(string message, ToastType type)
  {
    var toast = new ToastMessage { Message = message, Type = type, Timestamp = DateTime.Now };
    ToastMessages.Add(toast);
    StateHasChanged();

    _ = RemoveNotification(toast);
  }

  private async Task RemoveNotification(ToastMessage toast)
  {
    await Task.Delay(VisibleDuration * 1000);
    ToastMessages.Remove(toast);
    StateHasChanged();
  }

  private string GetColorByType(ToastType type) => type switch
  {
    ToastType.Info => Theme.Colors.Primary,
    ToastType.Success => Theme.Colors.Success,
    ToastType.Error => Theme.Colors.Error,
    _ => Theme.Colors.Primary
  };

  private string GetToastPosition() => Position switch
  {
    ToastPosition.BottomLeft => "toasts-bottom-left",
    ToastPosition.BottomRight => "toasts-bottom-right",
    _ => "toasts-bottom-right"
  };
}

internal class ToastMessage
{
  public string Message { get; set; } = string.Empty;
  public ToastType Type { get; set; } = ToastType.Info;
  public DateTime Timestamp { get; set; }
}

public enum ToastType
{
  Info,
  Success,
  Error
}

public enum ToastPosition
{
  BottomRight,
  BottomLeft
}

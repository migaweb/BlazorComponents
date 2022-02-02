using Microsoft.AspNetCore.Components;

namespace ComponentLibrary.Components;
public partial class ProgressSteps : BaseComponent
{
  private int _value;

  [Parameter] public int Steps { get; set; }
  [Parameter] public EventCallback<int> ValueChanged { get; set; }

  [Parameter] public int CircleDiameter { get; set; } = 30;
  [Parameter] public int ProgressLineHeight { get; set; } = 4;

  [Parameter]
  public int Value
  {
    get => _value;
    set
    {
      if (_value != value)
      {
        _value = value;
        SetActiveStep(_value);
      }
    }
  }

  private List<Step> AllSteps { get; set; } = new List<Step>();

  private double ProgressWidth
  {
    get
    {
      var actives = AllSteps.Where(e => e.Id < AllSteps.First(s => s.Active).Id).Count();
      return ((double)actives / ((double)AllSteps.Count - 1)) * 100;
    }
  }

  private int ActiveStep { get => AllSteps.First(e => e.Active).Id; }

  protected override void OnInitialized()
  {
    int i = 0;
    do
    {
      AllSteps.Add(new Step { Id = ++i, Active = i == Value });
    } while (i < Steps);

    SetActiveStep(Value);
  }

  public void SetActiveStep(int step)
  {
    if (!AllSteps.Any(e => e.Id == step)) return;

    var activeStep = AllSteps.Where(e => e.Id == step).First();
    ResetActiveSteps();
    activeStep.Active = true;
    UpdateValue(activeStep);
  }

  public void Previous()
  {
    var activeStep = AllSteps.Where(e => e.Active).FirstOrDefault();
    if (activeStep == null)
    {
      AllSteps.First().Active = true;
      return;
    }
    var nextActiveStep = AllSteps.Where(e => e.Id == activeStep.Id - 1).FirstOrDefault();

    if (nextActiveStep != null)
    {
      ResetActiveSteps();
      nextActiveStep.Active = true;
      UpdateValue(nextActiveStep);
    }
  }

  public void Next()
  {
    var activeStep = AllSteps.Where(e => e.Active).FirstOrDefault();
    if (activeStep == null)
    {
      AllSteps.First().Active = true;
      return;
    }
    var nextActiveStep = AllSteps.Where(e => e.Id == activeStep.Id + 1).FirstOrDefault();

    if (nextActiveStep != null)
    {
      ResetActiveSteps();
      nextActiveStep.Active = true;
      UpdateValue(nextActiveStep);
    }
  }

  private void UpdateValue(Step nextActiveStep)
  {
    Value = nextActiveStep.Id;
    ValueChanged.InvokeAsync(nextActiveStep.Id);
    StateHasChanged();
  }

  private void ResetActiveSteps()
  {
    AllSteps.ForEach(e => e.Active = false);
  }

  private bool IsActivated(int currentStep)
  {
    return currentStep <= ActiveStep;
  }

  private class Step
  {
    internal int Id { get; set; }
    public bool Active { get; set; }
  }
}

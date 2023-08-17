namespace Ui.Shared.Containers.Step;

public class StepEventArgs : EventArgs
{
    public StepElement Step { get; set; }

    public StepEventArgs(StepElement step) => Step = step;    
}

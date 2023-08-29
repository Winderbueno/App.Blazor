using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Ui.Shared.Containers.Form.Abstract;

public abstract class CustomRequiredField<TValue> : Field<TValue>
{
    [Parameter]
    public bool Required { get; set; } = false;
    [Parameter]
    public string RequiredMessage { get; set; } = "Field is required.";

    protected ValidationMessageStore? store;
    protected bool invalid = false;

    protected override void OnInitialized()
    {
        if (Required) EditContext!.OnValidationRequested += OnValidationRequested;
        store = new(EditContext!);
        base.OnInitialized();
    }

    protected void OnValidationRequested(object? sender = null, ValidationRequestedEventArgs? e = null) 
        => ValidateField(Value);

    protected virtual void ValidateField(TValue? Val)
    {
        store?.Clear();
        if (Val == null)
        {
            invalid = true;
            store?.Add(() => Value!, RequiredMessage);
        }
        else invalid = false;
    }
}
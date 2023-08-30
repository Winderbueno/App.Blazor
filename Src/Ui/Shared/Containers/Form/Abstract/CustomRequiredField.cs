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
    protected virtual bool requiredValueUnset => Value == null;

    protected override void OnInitialized()
    {
        if (Required) EditContext!.OnValidationRequested += OnValidationRequested;
        store = new(EditContext!);
    }

    protected void OnValidationRequested(object? sender = null, ValidationRequestedEventArgs? e = null) => ValidateField();

    protected virtual void ValidateField()
    {
        if(Required)
        {
            store?.Clear();
            if (requiredValueUnset)
            {
                invalid = true;
                store?.Add(() => Value!, RequiredMessage);
            }
            else invalid = false;
        }
    }
}
using K.Blazor.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace K.Blazor.Components.Containers.Form.Abstract;

public abstract class SelectField<TValue, TOption> : Field<TValue>
{
    /* Options */
    [EditorRequired, Parameter]
    public IEnumerable<TOption>? Options { get; set; }
    [Parameter]
    public (Func<TOption?, string?>? Name, Func<TOption?, string?>? Value)? OptionGetters { get; set; }


    [Parameter]
    public bool Required { get; set; } = false;

   
    [Inject] private KConfService? conf { get; set; }

    protected SelectFieldOptions selectConf => conf!.SelectFieldOptions;
    protected IEnumerable<Option>? options;
    protected ValidationMessageStore? store;
    protected bool invalid = false, optionsVisible = false;

    protected virtual bool requiredValueUnset => Value == null;

    protected override void OnInitialized()
    {
        // Validation
        if (Required) {
            EditContext!.OnValidationRequested += OnValidationRequested;
            store = new(EditContext!);
        }

        options = Options!.ToOption(
            selectConf.DefaultOptionName!,
            OptionGetters?.Name,
            OptionGetters?.Value);
    }

    protected void OnValidationRequested(
        object? sender = null, ValidationRequestedEventArgs? e = null) => ValidateField();

    protected virtual void ValidateField()
    {
        if(Required)
        {
            store?.Clear();
            if (requiredValueUnset)
            {
                invalid = true;
                store?.Add(() => Value!, selectConf.RequiredMessage());
            }
            else invalid = false;
        }
    }
}
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

   
    [Inject] private KConfService? Conf { get; set; }

    protected IEnumerable<Option>? options;
    protected ValidationMessageStore? store;
    protected bool invalid = false, optionsVisible = false;

    protected SelectFieldOptions SelectConf => Conf!.SelectFieldOptions;
    protected virtual bool RequiredValueUnset => Value == null;

    protected override void OnInitialized()
    {
        // Validation
        if (Required) {
            EditContext!.OnValidationRequested += OnValidationRequested;
            store = new(EditContext!);
        }

        options = Options!.ToOption(
            SelectConf.DefaultOptionName!,
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
            if (RequiredValueUnset)
            {
                invalid = true;
                store?.Add(() => Value!, SelectConf.RequiredMessage());
            }
            else invalid = false;
        }
    }
}
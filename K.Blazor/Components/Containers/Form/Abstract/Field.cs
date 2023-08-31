using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System.Linq.Expressions;

namespace K.Blazor.Components.Containers.Form.Abstract;

public abstract class Field<TValue> : ComponentBase
{
    [CascadingParameter]
    protected EditContext? EditContext { get; set; }

    [Parameter] public string? Label { get; set; }
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public bool Disabled { get; set; } = false;

    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public Expression<Func<TValue>>? ValueExpression { get; set; }

    [Parameter] public EventCallback<ChangeEventArgs> OnChange { get; set; }
    [Parameter] public EventCallback<ChangeEventArgs> OnInput { get; set; }
    [Parameter] public EventCallback<FocusEventArgs> OnBlur { get; set; }

    // This method allows to access the Field's FieldName from its ValueExpression parameter
    // See. https://stackoverflow.com/questions/74040748/blazor-binding-get-underlying-field-name
    // This mecanism is also used natively by blazor's InputBase class
    // https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.forms.inputbase-1?view=aspnetcore-7.0
    // Its can be useful to NotifyFieldChanged on EditContext (Ex. SelectField)
    protected string ParseFieldName()
    {
        if (this.ValueExpression is null)
            throw new ArgumentException($"You must provide a ValueExpression for this component.");

        var accessorBody = this.ValueExpression.Body;

        if (accessorBody is UnaryExpression unaryExpression
            && unaryExpression.NodeType == ExpressionType.Convert
            && unaryExpression.Type == typeof(object))
        {
            accessorBody = unaryExpression.Operand;
        }

        if (!(accessorBody is MemberExpression memberExpression))
            throw new ArgumentException($"The provided expression is not supported.");

        return memberExpression.Member.Name;
    }
}
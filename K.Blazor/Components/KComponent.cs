using K.Blazor.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace K.Blazor.Components;

public abstract class KComponent : ComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? AdditionalAttributes { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter]
    public Color? Color { get; set; }

    [Parameter]
    public string? CssClass { get; set; }

    protected string? cssClass => CssClass += Enums.Color.Light.Equals(Color) ? " dark" : null;
}
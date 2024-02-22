using Microsoft.JSInterop;

namespace Presentation.Layout;

// https://stackoverflow.com/questions/63135574/how-do-i-subscribe-to-onscroll-event-in-blazor
public class ScrollInfoService : IScrollInfoService
{
    public event Action? OnScroll;
    public int YValue { get; private set; }
    public bool MobileNavHide { get; private set; }

    // Invoke function declared in scroll-observer.js
    public ScrollInfoService(IJSRuntime js)
        => js.InvokeVoidAsync("RegisterScrollInfoService", DotNetObjectReference.Create(this));

    [JSInvokable("OnScroll")]
    public void JsOnScroll(int yValue)
    {
        if (yValue == 0 || yValue < 56 || YValue > yValue)
            MobileNavHide = false;
        else if (YValue < yValue)
            MobileNavHide = true;

        OnScroll?.Invoke();
        YValue = yValue;
    }
}

public interface IScrollInfoService
{
    event Action? OnScroll;
    int YValue { get; }
    bool MobileNavHide { get; }
}
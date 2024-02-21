using Microsoft.JSInterop;

namespace Presentation.Layout;

// https://stackoverflow.com/questions/63135574/how-do-i-subscribe-to-onscroll-event-in-blazor
public class ScrollInfoService : IScrollInfoService
{
    public event EventHandler<Tuple<int, int>>? OnScroll;
    public int YValue { get; private set; }

    // Invoke function declared in scroll-observer.js
    public ScrollInfoService(IJSRuntime js)
        => js.InvokeVoidAsync("RegisterScrollInfoService", DotNetObjectReference.Create(this));
    

    [JSInvokable("OnScroll")]
    public void JsOnScroll(int yValue)
    {
        OnScroll?.Invoke(this, new (YValue, yValue));
        YValue = yValue;
    }
}

public interface IScrollInfoService
{
    event EventHandler<Tuple<int, int>> OnScroll;
    int YValue { get; }
}
using Microsoft.JSInterop;

namespace Presentation.Layout;

// https://stackoverflow.com/questions/63135574/how-do-i-subscribe-to-onscroll-event-in-blazor
public class ScrollInfoService : IScrollInfoService
{
    public event Action? OnDisplayChange, OnShow, OnHide;
    public int YValue { get; private set; }
    public bool MobileNavHide { get; private set; }

    // Invoke function declared in scroll-observer.js
    public ScrollInfoService(IJSRuntime js)
        => js.InvokeVoidAsync("RegisterScrollInfoService", DotNetObjectReference.Create(this));

    [JSInvokable("OnScroll")]
    public void JsOnScroll(int yValue)
    {
        bool prevMobileNavHide = MobileNavHide;
        decimal diff = YValue - yValue;
        if (Math.Abs(diff) > 5)
        {
            if (yValue == 0 || yValue < 56 || YValue > yValue)
            {
                MobileNavHide = false;
                if(YValue > yValue) OnShow?.Invoke();
            }
            else if (YValue < yValue)
            {
                MobileNavHide = true;
                OnHide?.Invoke();
            }   
        }

        if(prevMobileNavHide != MobileNavHide)
            OnDisplayChange?.Invoke();

        YValue = yValue;
    }
}

public interface IScrollInfoService
{
    event Action? OnDisplayChange, OnShow, OnHide;
    int YValue { get; }
    bool MobileNavHide { get; }
}
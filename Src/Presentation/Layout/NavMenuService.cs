using Microsoft.JSInterop;

namespace Presentation.Layout;

// https://stackoverflow.com/questions/63135574/how-do-i-subscribe-to-onscroll-event-in-blazor
public class NavMenuService : INavMenuService
{
    public event Action? OnDisplayChange, OnShow, OnHide;
    private int YValue;
    public bool Display { get; private set; }

    // Invoke function declared in scroll-observer.js
    public NavMenuService(IJSRuntime js)
        => js.InvokeVoidAsync("RegisterNavMenuService", DotNetObjectReference.Create(this));

    [JSInvokable("OnScroll")]
    public void JsOnScroll(int yValue)
    {
        bool prevDisplay = Display;
        decimal diff = YValue - yValue;
        if (Math.Abs(diff) > 5)
        {
            if (yValue == 0 || yValue < 56 || YValue > yValue)
            {
                Display = false;
                if(YValue > yValue) OnShow?.Invoke();
            }
            else if (YValue < yValue)
            {
                Display = true;
                OnHide?.Invoke();
            }   
        }

        if(prevDisplay != Display)
            OnDisplayChange?.Invoke();

        YValue = yValue;
    }
}

public interface INavMenuService
{
    bool Display { get; }
    event Action? OnDisplayChange, OnShow, OnHide;
}
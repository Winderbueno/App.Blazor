using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace K.Blazor.Events;

// Necessary to have @onmouseleave & @onmouseenter accessible in components
// https://stackoverflow.com/questions/72886638/how-to-polyfill-onmouseenter-onmouseleave-in-blazor-6
[EventHandler("onmouseleave", typeof(MouseEventArgs), true, true)]
[EventHandler("onmouseenter", typeof(MouseEventArgs), true, true)]
public static class EventHandlers { }
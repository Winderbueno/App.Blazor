using K.Blazor.Enums;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Presentation.Shared.Indicators.Toast;

public class ToasterService : IDisposable
{
    private readonly List<Toast> _toastList = new List<Toast>();
    private Timer _timer = new Timer();
    public event EventHandler? ToasterChanged;
    public event EventHandler? ToasterTimerElapsed;
    public bool HasToasts => _toastList.Count > 0;

    public ToasterService()
    {
        _timer.Interval = 5000;
        _timer.AutoReset = true;
        _timer.Elapsed += TimerElapsed;
        _timer.Start();
    }

    public List<Toast> GetToasts()
    {
        ClearBurntToast();
        return _toastList;
    }

    public void AddSuccess(string msg)
        => AddToast(Toast.NewToast(msg, Color.Success));

    public void AddError(string? msg = null)
        => AddToast(Toast.NewToast(msg != null ? msg : "Unknown Error", Color.Danger));

    public void AddToast(Toast toast)
    {
        // To only manage one toast
        _toastList.Clear();

        _toastList.Add(toast);

        // only raise the ToasterChanged event if it hasn't already been raised by ClearBurntToast
        if (!ClearBurntToast())
            ToasterChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ClearToast(Toast toast)
    {
        if (_toastList.Contains(toast))
        {
            _toastList.Remove(toast);
            // only raise the ToasterChanged event if it hasn't already been raised by ClearBurntToast
            if (!ClearBurntToast())
                ToasterChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Dispose()
    {
        if (_timer is not null)
        {
            _timer.Elapsed += TimerElapsed;
            _timer.Stop();
        }
    }

    private void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        ClearBurntToast();
        ToasterTimerElapsed?.Invoke(this, EventArgs.Empty);
    }

    private bool ClearBurntToast()
    {
        var toastsToDelete = _toastList.Where(item => item.IsBurnt).ToList();
        if (toastsToDelete is not null && toastsToDelete.Count > 0)
        {
            toastsToDelete.ForEach(toast => _toastList.Remove(toast));
            ToasterChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
        return false;
    }
}
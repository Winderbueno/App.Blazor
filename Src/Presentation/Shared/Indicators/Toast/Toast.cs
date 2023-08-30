using K.Blazor.Enums;

namespace Presentation.Shared.Indicators.Toast;

public record Toast
{
    public Guid Id = Guid.NewGuid();
    public string Title { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public Color? Color { get; init; }
    public readonly DateTimeOffset Posted = DateTimeOffset.Now;
    public DateTimeOffset TimeToBurn { get; set; } = DateTimeOffset.Now.AddSeconds(8);
    public bool IsBurnt => TimeToBurn < DateTimeOffset.Now;
    private TimeSpan elapsedTime => Posted - DateTimeOffset.Now;

    public string ElapsedTimeText =>
        elapsedTime.Seconds > 60
        ? $"posted {-elapsedTime.Minutes} mins ago"
        : $"posted {-elapsedTime.Seconds} secs ago";


    public static Toast NewToast( 
        string message, 
        Color? color = null, 
        double? secsToLive = null)
    {
        Toast toast = new Toast
        {
            Message = message,
            Color = color,
        };

        if (secsToLive != null)
            toast.TimeToBurn = DateTimeOffset.Now.AddSeconds((double)secsToLive);

        return toast;
    }
}
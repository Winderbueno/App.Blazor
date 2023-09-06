namespace Presentation.Middlewares.Globalization;

public class LanguageEvents
{
    public event EventHandler<string>? LanguageChanged;

    internal void InvokeLanguageChanged(string newLanguage, object? sender = null)
        => LanguageChanged?.Invoke(sender ?? this, newLanguage);
}

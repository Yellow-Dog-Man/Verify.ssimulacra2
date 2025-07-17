using ssimulacra2.NET;
using System.Diagnostics.CodeAnalysis;

namespace Verify.ssimulacra2;

// Largely based uppon Verify.Phash by Simon Cropp
public static class SettingsTasksExtensions {
    public static SettingsTask WithSsimulacra2(this SettingsTask settings, double threshold = (int)Ssimulacra2Quality.VisuallyLossless, bool round = true)
    {
        settings.UseStreamComparer(VerifySsimulacra2.CompareStreams);
        Apply(settings.CurrentSettings.Context, new SsimulacraSettings(threshold));
        return settings;
    }

    public static SettingsTask WithSsimulacra2(this SettingsTask settings, Ssimulacra2Quality minimumQuality = Ssimulacra2Quality.VisuallyLossless, bool round = true)
    {
        return WithSsimulacra2(settings, (int)minimumQuality, round);
    }

    internal static void Apply(Dictionary<string, object> context, SsimulacraSettings settings)
    {
        context[SsimulacraSettings.SettingsKey] = settings;
    }
}
public static class VerifySettingsExtensions
{
    public static VerifySettings WithSsimulacra2(this VerifySettings settings, Ssimulacra2Quality minimumQuality = Ssimulacra2Quality.VisuallyLossless, bool round = true)
    {
        return WithSsimulacra2(settings, (int)minimumQuality, round);
    }
    public static VerifySettings WithSsimulacra2(this VerifySettings settings, double threshold = (int)Ssimulacra2Quality.VisuallyLossless, bool round = true)
    {
        SettingsTasksExtensions.Apply(settings.Context, new SsimulacraSettings(threshold, round));
        settings.UseStreamComparer(VerifySsimulacra2.CompareStreams);

        return settings;
    }
}

internal static class IReadonlyDictionaryExtensions
{
    internal static bool GetSettings<T>(
    this IReadOnlyDictionary<string, object> context, string key,
    [NotNullWhen(true)] out T? settings) where T : class
    {
        if (context.TryGetValue(key, out var value))
        {
            settings = (T)value;
            return true;
        }

        settings = null;
        return false;
    }
}

// TODO: Alpha
public class SsimulacraSettings(double threshold = (int)Ssimulacra2Quality.VisuallyLossless, bool round = true)
{
    public static string SettingsKey => "Ssimulacra2";
    public double Threshold { get; } = threshold;
    public bool Round { get; } = round;
}

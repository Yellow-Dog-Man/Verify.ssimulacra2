using ssimulacra2.NET;
using Verify.ssimulacra2;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace VerifyTests;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class VerifySsimulacra2
{
    public static bool Initialized { get; private set; }

    public static void Initialize()
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        RegisterComparers();
    }

    public static void RegisterComparers()
    {
        InnerVerifier.ThrowIfVerifyHasBeenRun();
        VerifierSettings.RegisterStreamComparer("png", CompareStreams);
        VerifierSettings.RegisterStreamComparer("jpg", CompareStreams);
        VerifierSettings.RegisterStreamComparer("jpeg", CompareStreams);
        VerifierSettings.RegisterStreamComparer("gif", CompareStreams);
    }
    public static Task<CompareResult> CompareStreams(
        Stream received,
        Stream verified,
        IReadOnlyDictionary<string, object> context)
    {
        double threshold = (int) Ssimulacra2Quality.VisuallyLossless;
        bool round = true;
        if (context.GetSettings(SsimulacraSettings.SettingsKey, out SsimulacraSettings? settings))
        {
            threshold = settings.Threshold;   
            round = settings.Round;
        }

        byte[] receivedBuffer = ConvertStream(received);
        byte[] verifiedBuffer = ConvertStream(verified);

        double score = Ssimulacra2.ComputeFromMemory(verifiedBuffer, receivedBuffer);
        if (round)
            score = Math.Round(score);

        var compare = score > threshold;
        if (compare)
            return Task.FromResult(CompareResult.Equal);

        return Task.FromResult(CompareResult.NotEqual($"diff > threshold. threshold: {threshold}, score: {score}"));
    }

    // Largely based on: https://stackoverflow.com/a/2630539
    private static byte[] ConvertStream(Stream stream)
    {
        if (stream.Position != 0)
            throw new InvalidOperationException($"Expected stream to be at start");

        if (stream is MemoryStream ms)
            return ms.ToArray();
        return ReadFully(stream);
    }

    private static byte[] ReadFully(Stream stream)
    {
        using MemoryStream ms = new();
        stream.CopyTo(ms);
        return ms.ToArray();
    }
}
using ssimulacra2.NET;
using System.Runtime.CompilerServices;

namespace Verify.ssimulacra2.Tests;

[UsesVerify]
[TestClass]
public partial class Ssimulacra2Tests
{
    [ModuleInitializer]
    public static void Init()
    {
        //DiffRunner.Disabled = true;
        //VerifySsimulacra2.Initialize();
    }

    [TestMethod]
    public async Task TestIdentical()
    {
        await VerifyFile("carrots.png").WithSsimulacra2(Ssimulacra2Quality.VisuallyLossless);
    }

    [DataRow("carrots_8bit.png")]
    [TestMethod]
    public async Task TestDistorted(string filepath)
    {
        // This one should fail
        await Verifier.ThrowsTask(async () =>
            await VerifyFile(filepath)
                .WithSsimulacra2(Ssimulacra2Quality.VisuallyLossless)
                .DisableDiff()
        )
        .IgnoreStackTrace()
        .UseMethodName("Distored_Ex")
        .ScrubEmptyLines();
    }
}

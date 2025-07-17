# Verify.ssimulacra2

Enables you to use [Ssimulacra2.Net](https://github.com/YellowDogMan/Ssimulacra2.Net) as a [Verify](https://github.com/VerifyTests/Verify), [Image Comparer](https://github.com/VerifyTests/Verify/blob/main/docs/comparer.md).

## Status
WIP, see [issue list](https://github.com/VerifyTests/Verify.ssimulacra2/issues).

# Setup
If you are new to [Verify](https://github.com/VerifyTests/Verify) we recommend following [Verify's Setup Guide](https://github.com/VerifyTests/Verify/blob/main/docs/getting-started.md).

Once you're setup and comfortable, you'll need to register the Ssimulacra2 comparer. There's a couple of ways to do this depending on your needs.

## Statically
If you want to use the Ssimulacra2 comparer statically, you can use the following code:

```csharp
[ModuleInitializer]
public static void Init()
{
    VerifySsimulacra2.Initialize();
}
```

Which will automatically register the comparer.

TODO: This might also occur if you use, `VerifierSettings.InitializePlugins()`, e.g.
```cs
[ModuleInitializer]
    public static void InitOther() =>
        VerifierSettings.InitializePlugins();
```

We need to double check.

## Dynamically when you need it

```cs
await VerifyFile("carrots.png")
        .WithSsimulacra2(90);
```

```cs
    await VerifyFile("carrots.png")
        .WithSsimulacra2(Ssimulacra2Quality.VisuallyLossless);
```

## Scores

Scoring is based on the original [Ssimulacra2 tool](https://github.com/cloudinary/ssimulacra2)'s Readme and is formally defined in the [Ssimulacra2.Net Enum Ssimulacra2Quality](https://github.com/YellowDogMan/Ssimulacra2.Net/blob/main/Ssimulacra2.Net/Ssimulacra2Quality.cs). You can always use it as a number(e.g. 50), or an enum value such as `Ssimulacra2Quality.VisuallyLossless`.

By default the threshold is set to `Ssimulacra2Quality.VisuallyLossless` or 90.

## Test Images

Test images are from [TestImages.Org](https://testimages.org/).

## Contributing

If you have any suggestions or improvements, please feel free to open an issue or submit a pull request. We're new to making extensions for Verify and would love to improve things.
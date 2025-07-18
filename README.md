# Verify.ssimulacra2

[![Nuget downloads](https://img.shields.io/nuget/v/YellowDogMan.Verify.ssimulacra2.svg)](https://www.nuget.org/packages/YellowDogMan.Verify.ssimulacra2)
[![Nuget](https://img.shields.io/nuget/dt/YellowDogMan.Verify.ssimulacra2)](https://www.nuget.org/packages/YellowDogMan.Verify.ssimulacra2)
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/Yellow-Dog-Man/Verify.ssimulacra2/test.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](/LICENSE.txt)


Enables you to use [Ssimulacra2.Net](https://github.com/YellowDogMan/Ssimulacra2.Net) as a [Verify](https://github.com/VerifyTests/Verify), [Image Comparer](https://github.com/VerifyTests/Verify/blob/main/docs/comparer.md).

## Status
WIP, see [issue list](https://github.com/VerifyTests/Verify.ssimulacra2/issues).

# Setup

### Setup Verify
If you are new to [Verify](https://github.com/VerifyTests/Verify) we recommend following [Verify's Setup Guide](https://github.com/VerifyTests/Verify/blob/main/docs/getting-started.md).

### Install Package
Install [Verify.ssimulacra2 with NuGet](https://www.nuget.org/packages/YellowDogMan.Verify.ssimulacra2):

```
Install-Package YellowDogMan.Verify.ssimulacra2
```

### Register Comparer
Once you're setup and comfortable, you'll need to register the Ssimulacra2 comparer. There's a couple of ways to do this depending on your needs.

#### Statically
If you want to use the Ssimulacra2 comparer statically, you can use the following code:

```csharp
[ModuleInitializer]
public static void Init()
{
    VerifySsimulacra2.Initialize();
}
```

Which will automatically register the comparer.

Or, you can register it automatically with:
```cs
[ModuleInitializer]
    public static void InitOther() =>
        VerifierSettings.InitializePlugins();
```
TODO: We need to double check this works, See [this issue](https://github.com/Yellow-Dog-Man/Verify.ssimulacra2/issues/1)

#### Dynamically when you need it

```cs
// Defaults to 90
await VerifyFile("carrots.png")
    .WithSsimulacra2(); 

// Specified Numerically
await VerifyFile("carrots.png")
    .WithSsimulacra2(90);

// Specified using Enums
await VerifyFile("carrots.png")
    .WithSsimulacra2(Ssimulacra2Quality.VisuallyLossless);
```

## Scores

Scoring is based on the original [Ssimulacra2 tool](https://github.com/cloudinary/ssimulacra2)'s Readme and is formally defined in the [Ssimulacra2.Net Enum Ssimulacra2Quality](https://github.com/Yellow-Dog-Man/Ssimulacra2.NET/blob/main/ssimulacra2.NET/Ssimulacra2Quality.cs). You can always use it as a number(e.g. 50), or an enum value such as `Ssimulacra2Quality.VisuallyLossless`.

By default the threshold is set to `Ssimulacra2Quality.VisuallyLossless` or 90.

## Test Images

Test images are from [TestImages.Org](https://testimages.org/).

## Contributing

If you have any suggestions or improvements, please feel free to open an issue or submit a pull request. We're new to making extensions for Verify and would love to improve things.

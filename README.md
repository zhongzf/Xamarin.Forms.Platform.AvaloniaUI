# Xamarin.Forms.Platform.AvaloniaUI
Xamarin Forms platform implemented with Avalonia (A multi-platform .NET UI framework) http://avaloniaui.net




# Xamarin.Forms #

Xamarin.Forms provides a way to quickly build native apps for iOS, Android, Windows and macOS, completely in C#.

Read more about the platform at https://www.xamarin.com/forms.

## Build Status ##

![Azure DevOps](https://devdiv.visualstudio.com/DevDiv/_apis/build/status/Xamarin/XamarinForms/Xamarin%20Forms?branchName=master "Azure Pipelines")

## Packages ##

Platform/Feature               | Package name                              | Stable (3.0.0 branch)     |Nightly Feed [MyGet](https://www.myget.org/F/xamarinforms-ci/api/v2)  (master branch)
-----------------------|-------------------------------------------|-----------------------------|-------------------------
Core             | `Xamarin.Forms` | [![NuGet](https://img.shields.io/nuget/v/Xamarin.Forms.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/Xamarin.Forms/)| [![MyGet](https://img.shields.io/myget/xamarinforms-ci/vpre/Xamarin.Forms.svg?style=flat-square&label=myget)](https://myget.org/feed/xamarinforms-ci/package/nuget/Xamarin.Forms)
Maps                 | `Xamarin.Forms.Maps`    | [![NuGet](https://img.shields.io/nuget/v/Xamarin.Forms.Maps.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/Xamarin.Forms.Maps/) | [![MyGet](https://img.shields.io/myget/xamarinforms-ci/vpre/Xamarin.Forms.Maps.svg?style=flat-square&label=myget)](https://myget.org/feed/xamarinforms-ci/package/nuget/Xamarin.Forms.Maps)
Pages  | `Xamarin.Forms.Pages`  | [![NuGet](https://img.shields.io/nuget/v/Xamarin.Forms.Pages.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/Xamarin.Forms.Pages/) | [![MyGet](https://img.shields.io/myget/xamarinforms-ci/vpre/Xamarin.Forms.Pages.svg?style=flat-square&label=myget)](https://myget.org/feed/xamarin.forms-ci/package/nuget/Xamarin.Forms.Pages)

If you want to use the latest dev build then you should read [this blog post](https://blog.xamarin.com/try-the-latest-in-xamarin-forms-with-nightly-builds):

- Add the nightly feed to your NuGet sources or add a NuGet.Config to your app (placing it in the same directory where your solution file is) with the following content:

  ```xml
  <?xml version="1.0" encoding="utf-8"?>
  <configuration>
    <packageSources>
      <clear />
      <add key="xamarin-ci" value="https://www.myget.org/F/xamarinforms-ci/api/v2" />
      <add key="NuGet.org" value="https://api.nuget.org/v3/index.json" />
    </packageSources>
  </configuration>
  ```

  *NOTE: This NuGet.Config should be with your application unless you want nightly packages to potentially start being restored for other apps on the machine.*

- Change your application's dependencies to have a `*` to get the latest version.

## Getting Started ##

##### Install Visual Studio 2017 #####

VS 2017 is required for developing Xamarin.Forms. If you do not already have it installed, you can download it [here](https://www.visualstudio.com/downloads/download-visual-studio-vs). VS 2017 Community is completely free. If you are installing VS 2017 for the first time, select the "Custom" installation type and select the following from the features list to install:

- .NET desktop development - In the `Summary > Optional select .NET Framework 4.7 SDK, .NET Framework 4.7 targeting pack`. 
- Universal Windows Platform Development - In the `Summary > Optional select the Windows 10 Mobile Emulator`.
- Mobile Development with .NET - In the `Summary > Optional select Xamarin Remoted Simulator, Xamarin SDK Manager, Intel Hardware Accelerated Execution Manager (HAXM)`

The Android 7.0 Nougat API 24 SDK is required for developing Xamarin.Forms. It can be installed by using the [Xamarin Android SDK Manager](https://docs.microsoft.com/xamarin/android/get-started/installation/android-sdk).

We also recommend installing [Xamarin Android Device Manager](https://developer.xamarin.com/guides/android/getting_started/installation/android-emulator/xamarin-device-manager/) This will use the HAXM tools installed above and allow you to configure Android Virtual Devices (AVDs) that emulate Android devices.
If you already have VS 2017 installed, you can verify that these features are installed by modifying the VS 2017 installation via the Visual Studio Installer.


<img src='https://avatars2.githubusercontent.com/u/14075148?s=200&v=4' width='100' />

# Avalonia

| Gitter Chat | Build Status (Win, Linux, OSX) | Open Collective |
|---|---|---|
|  [![Gitter](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/AvaloniaUI/Avalonia?utm_campaign=pr-badge&utm_content=badge&utm_medium=badge&utm_source=badge) | [![Build Status](https://dev.azure.com/AvaloniaUI/AvaloniaUI/_apis/build/status/AvaloniaUI.Avalonia)](https://dev.azure.com/AvaloniaUI/AvaloniaUI/_build/latest?definitionId=4) | [![Backers on Open Collective](https://opencollective.com/Avalonia/backers/badge.svg)](#backers) [![Sponsors on Open Collective](https://opencollective.com/Avalonia/sponsors/badge.svg)](#sponsors) |

## About

Avalonia is a WPF-inspired cross-platform XAML-based UI framework providing a flexible styling system and supporting a wide range of OSs: Windows (.NET Framework, .NET Core), Linux (GTK), MacOS, Android and iOS.

**Avalonia is currently in beta** which means that the framework is generally usable for writing applications, but there may be some bugs and breaking changes as we continue development.

| Control catalog | Desktop platforms | Mobile platforms |
|---|---|---|
| <a href='https://youtu.be/wHcB3sGLVYg'><img width='300' src='http://avaloniaui.net/images/screen.png'></a> | <a href='https://www.youtube.com/watch?t=28&v=c_AB_XSILp0' target='_blank'><img width='300' src='http://avaloniaui.net/images/avalonia-video.png'></a> | <a href='https://www.youtube.com/watch?v=NJ9-hnmUbBM' target='_blank'><img width='300' src='https://i.ytimg.com/vi/NJ9-hnmUbBM/hqdefault.jpg'></a> |

## Getting Started

Avalonia [Visual Studio Extension](https://marketplace.visualstudio.com/items?itemName=AvaloniaTeam.AvaloniaforVisualStudio) contains project and control templates that will help you get started. After installing it, open "New Project" dialog in Visual Studio, choose "Avalonia" in "Visual C#" section, select "Avalonia .NET Core Application" and press OK (<a href="http://avaloniaui.net/docs/quickstart/images/new-project-dialog.png">screenshot</a>). Now you can write code and markup that will work on multiple platforms!

For those without Visual Studio, starter guide for .NET Core CLI can be found [here](http://avaloniaui.net/docs/quickstart/create-new-project#net-core).

Avalonia is delivered via <b>NuGet</b> package manager. You can find the packages here: ([stable(ish)](https://www.nuget.org/packages/Avalonia/), [nightly](https://github.com/AvaloniaUI/Avalonia/wiki/Using-nightly-build-feed))

Use these commands in Package Manager console to install Avalonia manually:
```
Install-Package Avalonia
Install-Package Avalonia.Desktop
```

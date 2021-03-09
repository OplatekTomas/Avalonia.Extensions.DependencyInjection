# Extension for C# [Avalonia UI](https://github.com/AvaloniaUI/Avalonia) framework to enable ASP.Net Core like DI.
I'm not affiliated with the devs of Avalonia UI. This project was created, because I was not quite happy with the existing solutions and I just kinda wanted to try it.

## Features:
 - Easy setup
 - Decent speed. Startup is slower, since thats when some data is generated and cached, but after that the variable values are simply set using reflection. (This also means that once services are added in the startup, you can't add more of them later)
 - Services can contain other services, altough I would not combine transient and singleton services since it does make a transient services singleton.

## Build:
Building the project should be as simple as running `dotnet build`

## Usage:
 Add a `UseDependencyInjection(Action<IServiceCollection>)` extension method call in Avalonia UI builder.

For example:

![Imgur](https://i.imgur.com/vRy6L2C.jpg)

 Once that is done, change any class that inherits `Window` or `UserControl` you have to inherit `DIWindow` or `DIUserControl` instead.
 That should enable dependency injection on that class.
 
 Last step is to add `[Inject]` attribute to any private/protected field like this and the service should be injected as soon as the constructor of `DIWindow/DIUserControl` is called.

![Imgur](https://i.imgur.com/eVxu9IX.png)

## Disclaimer:
I wrote this on a tuesday in like 3.5 hours and did not test it nearly enough.

# MauiApp25

This experimental .NET MAUI project is a cross-platform starter app intended to run on Android, iOS, Mac Catalyst, and Windows. The goal is to explore a single codebase that targets all supported devices using .NET 10 and MAUI.

## Status
- Experimental — early prototype.
- Targets: Android, iOS, Mac Catalyst, Windows (via .NET 10 / MAUI workloads).

## Prerequisites
- .NET SDK 10 (or later) with MAUI workloads installed.
- Visual Studio with .NET MAUI support (recommended) or the `dotnet` CLI.

## Common commands
From the repository root:

- Restore and install workloads (if needed):
  `dotnet workload restore`

- Build the solution:
  `dotnet build`

- Run the app (desktop target example):
  `dotnet run --project ./MauiApp25/MauiApp25.csproj -f net10-windows10.0.19041.0`

- Clean caches and shutdown build servers (if build hangs):
  `dotnet build-server shutdown`
  `dotnet nuget locals all --clear`

## Using Visual Studio
- Open the solution in Visual Studio.
- Set `MauiApp25` as the startup project.
- Choose a run target (Windows Machine, Android emulator, or a connected device) and press F5.

## Project structure
- `MauiApp25/` — MAUI application project.
- Consider adding separate projects for data models and services (e.g., `MauiApp25.DataObjects`).

## Notes
- This is an experimental project; expect breaking changes as you prototype cross-platform features.
- If builds are cancelled because another MAUI build operation is running, stop any `dotnet`/`msbuild` processes, run `dotnet build-server shutdown`, and remove `bin`/`obj` and the `.vs` folder before rebuilding.

## License
Add license information as needed.

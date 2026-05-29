# zero410-NTLogin

Proprietary fork of [DeepSignalTech/NinjaTraderAutoLogin V1.0](https://github.com/DeepSignalTech/NinjaTraderAutoLogin/releases/tag/V1.0) with a fix for high-DPI environments.

Part of the **NT Auto Export** pipeline (private project; this fork is the auto-login component).

## Difference from upstream

Upstream V1.0 focuses the password field by clicking at fixed pixel offsets (`Left+30, Top+210` from the window's top-left corner). On high-DPI displays (e.g. notebooks at 150% scaling) those offsets are not rescaled, the click misses the password field, and auto-login fails silently.

**Fix:** the coordinate-based click is replaced with three `SendKeys.SendWait("{TAB}")` calls, focusing the password field via keyboard navigation. The result is DPI-independent, multi-monitor-safe, and independent of window size or position. Tab order was verified empirically on NinjaTrader 8:

- TAB 1 → initial focus moves into the form
- TAB 2 → username field (typically already filled)
- TAB 3 → password field (target)

## Changes to upstream source

Single file modified: `NTLogin/Program.cs`, password injection section. Tagged with comment `zero410 fix (D13)` in the source.

Dead code was also removed (no longer referenced after the fix): `ButtonClick`, `SetCursorPos`, `mouse_event` (and its `MOUSEEVENTF_*` constants), `GetWindowRect`, the `RECT` struct. Only `SetForegroundWindow` is retained from the original Win32 interop, since it is still used to bring the NinjaTrader window to the foreground.

No other source file was modified.

## Usage

Identical to upstream:NTLogin.exe "LOGIN_NAME" "PASSWORD"

Optionally, override the NinjaTrader executable path (default: `C:\Program Files\NinjaTrader 8\bin\NinjaTrader.exe`):NTLogin.exe "LOGIN_NAME" "PASSWORD" "C:\custom\path\NinjaTrader.exe"

The `LOGIN_NAME` argument is a placeholder for forward compatibility; the application currently assumes the login name is already remembered by the NinjaTrader login dialog and only injects the password.

See upstream README for details on creating a Windows shortcut with embedded credentials.

## Build

Open `NTLogin.sln` in Visual Studio, build in Release mode. The compiled binary will be placed at `NTLogin/bin/Release/NTLogin.exe`.

## License

MIT (inherited from upstream). See `LICENSE`.

## References

- Upstream: https://github.com/DeepSignalTech/NinjaTraderAutoLogin
- Tab-based approach originally suggested on NinjaTrader Support Forum: [thread](https://forum.ninjatrader.com/forum/ninjatrader-8/platform-technical-support-aa/1237868-starting-up-ninjatrader-desktop-8-1-without-typing-username-and-password-every-time)
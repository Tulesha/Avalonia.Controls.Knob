using System;
using Avalonia.Input;

namespace Tulesha.Avalonia.Controls.Helpers;

internal static class XyFocusHelpers
{
    internal static bool IsAllowedXyNavigationMode(this InputElement visual, KeyDeviceType? keyDeviceType)
    {
        return IsAllowedXyNavigationMode(XYFocus.GetNavigationModes(visual), keyDeviceType);
    }

    private static bool IsAllowedXyNavigationMode(XYFocusNavigationModes modes, KeyDeviceType? keyDeviceType)
    {
        return keyDeviceType switch
        {
            null => !modes.Equals(XYFocusNavigationModes
                .Disabled), // programmatic input, allow any subtree except Disabled.
            KeyDeviceType.Keyboard => modes.HasFlag(XYFocusNavigationModes.Keyboard),
            KeyDeviceType.Gamepad => modes.HasFlag(XYFocusNavigationModes.Gamepad),
            KeyDeviceType.Remote => modes.HasFlag(XYFocusNavigationModes.Remote),
            _ => throw new ArgumentOutOfRangeException(nameof(keyDeviceType), keyDeviceType, null)
        };
    }
}
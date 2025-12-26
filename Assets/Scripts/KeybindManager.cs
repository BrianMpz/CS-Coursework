using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class KeybindManager
{
    private static readonly Dictionary<KeyCode, string> Map = new()
    {
        // Mouse
        { KeyCode.Mouse0, "Left Mouse" },
        { KeyCode.Mouse1, "Right Mouse" },
        { KeyCode.Mouse2, "Middle Mouse" },

        // Letters
        { KeyCode.A, "A" }, { KeyCode.B, "B" }, { KeyCode.C, "C" }, { KeyCode.D, "D" }, { KeyCode.E, "E" }, { KeyCode.F, "F" },
        { KeyCode.G, "G" }, { KeyCode.H, "H" }, { KeyCode.I, "I" }, { KeyCode.J, "J" }, { KeyCode.K, "K" }, { KeyCode.L, "L" },
        { KeyCode.M, "M" }, { KeyCode.N, "N" }, { KeyCode.O, "O" }, { KeyCode.P, "P" }, { KeyCode.Q, "Q" }, { KeyCode.R, "R" },
        { KeyCode.S, "S" }, { KeyCode.T, "T" }, { KeyCode.U, "U" }, { KeyCode.V, "V" }, { KeyCode.W, "W" }, { KeyCode.X, "X" },
        { KeyCode.Y, "Y" }, { KeyCode.Z, "Z" },

        // Number Row
        { KeyCode.Alpha1, "1" },
        { KeyCode.Alpha2, "2" },
        { KeyCode.Alpha3, "3" },
        { KeyCode.Alpha4, "4" },
        { KeyCode.Alpha5, "5" },
        { KeyCode.Alpha6, "6" },
        { KeyCode.Alpha7, "7" },
        { KeyCode.Alpha8, "8" },
        { KeyCode.Alpha9, "9" },
        { KeyCode.Alpha0, "0" },

        // Modifiers (safe alone)
        { KeyCode.LeftShift, "Shift" },
        { KeyCode.RightShift, "Shift" },
        { KeyCode.LeftControl, "Ctrl" },
        { KeyCode.RightControl, "Ctrl" },
        { KeyCode.LeftAlt, "Alt" },
        { KeyCode.RightAlt, "Alt" },

        // Common Gameplay Keys
        { KeyCode.Space, "Space" },
        { KeyCode.Tab, "Tab" },
        { KeyCode.Backspace, "Backspace" },
        { KeyCode.Return, "Enter" },

        // Arrow Keys
        { KeyCode.UpArrow, "Up Arrow" },
        { KeyCode.DownArrow, "Down Arrow" },
        { KeyCode.LeftArrow, "Left Arrow" },
        { KeyCode.RightArrow, "Right Arrow" },

        // Numpad
        { KeyCode.Keypad0, "Numpad 0" },
        { KeyCode.Keypad1, "Numpad 1" },
        { KeyCode.Keypad2, "Numpad 2" },
        { KeyCode.Keypad3, "Numpad 3" },
        { KeyCode.Keypad4, "Numpad 4" },
        { KeyCode.Keypad5, "Numpad 5" },
        { KeyCode.Keypad6, "Numpad 6" },
        { KeyCode.Keypad7, "Numpad 7" },
        { KeyCode.Keypad8, "Numpad 8" },
        { KeyCode.Keypad9, "Numpad 9" },
        { KeyCode.KeypadPlus, "Numpad +" },
        { KeyCode.KeypadMinus, "Numpad -" },
        { KeyCode.KeypadMultiply, "Numpad *" },
        { KeyCode.KeypadDivide, "Numpad /" },
    };

    public static List<KeyCode> GetLegalKeybinds()
    {
        return Map.Keys.ToList();
    }

    public static string KeyCodeToString(KeyCode key)
    {
        return Map.ContainsKey(key) ? Map[key] : "null";
    }

    public static bool HasALegalKeybindBeenPressed()
    {
        foreach (KeyValuePair<KeyCode, string> entry in Map)
        {
            if (Input.GetKeyDown(entry.Key))
            {
                return true;
            }
        }
        return false;
    }
}

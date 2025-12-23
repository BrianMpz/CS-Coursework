using UnityEngine;

// Provides an accessible API for my client and I to store persistent data about the player, such as their high score via the Unity PlayerPrefs library
// Fulfils Criterion 1, 8
public static class DataManager
{
    public static string GetStringFromSave(string _name) // returns string
    {
        return PlayerPrefs.GetString(_name);
    }

    public static float GetFloatFromSave(string _name) // returns float
    {
        return PlayerPrefs.GetFloat(_name);
    }

    public static int GetIntFromSave(string _name) // returns int
    {
        return PlayerPrefs.GetInt(_name);
    }

    public static bool GetBoolFromSave(string _name) // returns bool (bools cant be stored directly so is converted to/from string)
    {
        string value = PlayerPrefs.GetString(_name);

        if (value == "True")
            return true;
        else
            return false;
    }

    public static void SaveString(string _name, string _newValue) // saves string
    {
        PlayerPrefs.SetString(_name, _newValue);
    }

    public static void SaveFloat(string _name, float _newValue) // saves float
    {
        PlayerPrefs.SetFloat(_name, _newValue);
    }

    public static void SaveInt(string _name, int _newValue) // saves int
    {
        PlayerPrefs.SetInt(_name, _newValue);
    }

    public static void SaveBool(string _name, bool _newValue) // saves bool (bools cant be stored directly so is converted to/from string)
    {
        PlayerPrefs.SetString(_name, _newValue ? "True" : "False");
    }

    public static void ClearAllData() // clears all data
    {
        PlayerPrefs.DeleteAll();
    }
}
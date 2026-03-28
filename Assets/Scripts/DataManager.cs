using UnityEngine;

// Provides an accessible API for my client and I to store persistent data about the player, such as their high score via the Unity PlayerPrefs library
// Fulfils Criterion 1, 8
public static class DataManager
{
    public static string GetStringFromSave(string _name) // returns string using unity's playerprefs sysem
    {
        return PlayerPrefs.GetString(_name);
    }

    public static float GetFloatFromSave(string _name) // returns float using unity's playerprefs sysem
    {
        return PlayerPrefs.GetFloat(_name);
    }

    public static int GetIntFromSave(string _name) // returns int using unity's playerprefs sysem
    {
        return PlayerPrefs.GetInt(_name);
    }

    public static bool GetBoolFromSave(string _name) // returns bool (bools cant be stored directly so is converted to/from string)
    {
        string _value = PlayerPrefs.GetString(_name);

        if (_value == "True")
            return true;
        else
            return false;
    }

    public static void SaveString(string _name, string _newValue) // saves string using unity's playerprefs sysem
    {
        PlayerPrefs.SetString(_name, _newValue);
    }

    public static void SaveFloat(string _name, float _newValue) // saves float using unity's playerprefs sysem
    {
        PlayerPrefs.SetFloat(_name, _newValue);
    }

    public static void SaveInt(string _name, int _newValue) // saves int using unity's playerprefs sysem
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
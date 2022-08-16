using UnityEngine;

public static class SaveSystem
{
    private const string levelSave = "level";
    public static int Level
    {
        get => PlayerPrefs.GetInt(levelSave);

        set => PlayerPrefs.SetInt(levelSave, value);
    }
}

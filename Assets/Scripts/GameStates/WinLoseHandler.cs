using System;
public static class WinLoseHandler
{
    public static event Action<bool> LevelCompleted;

    public static void OnLevelEnd(bool success)
    {
        LevelCompleted?.Invoke(success);

        if (success)
        {
            SaveSystem.Level++;
        }
        else
        {
        }
    }
}

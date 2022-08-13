using System;
using UnityEngine;

public static class GameStateHandler
{
    public static event Action<State> OnGameStateChange;

    public enum State
    {
        WaitsForStart = 0,
        InLoop = 1,
        WaitsForEnd = 2
    }

    private static State _gameState = 0;

    public static State GameState
    {
        get => _gameState;

        set
        {
            _gameState = value;
            OnGameStateChange?.Invoke(value);
        }
    }
}

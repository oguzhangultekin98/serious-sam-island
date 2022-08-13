using System;
using UnityEngine;

public class CharacterActionScheduler : ActionSchedulerBase
{
    private void OnEnable()
    {
        GameStateHandler.OnGameStateChange += GameStateHandlerOnGameStateChange;
    }

    private void OnDisable()
    {
        GameStateHandler.OnGameStateChange -= GameStateHandlerOnGameStateChange;
    }

    private void GameStateHandlerOnGameStateChange(GameStateHandler.State obj)
    {
        if (obj == GameStateHandler.State.InLoop)
        {
            StartDefaultActions();
        }
        else if (obj == GameStateHandler.State.WaitsForEnd)
            StopActiveAction();
    }

    public override void StartDefaultActions()
    {
        StartAction<PlayerJoystickMovement>();

        //Activate AI Here
        //if (CurrentAction == null)
            //StartAction<AINavigationMovement>();
    }
}

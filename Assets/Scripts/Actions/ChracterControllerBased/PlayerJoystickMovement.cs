using UnityEngine;
using System.Collections;

public class PlayerJoystickMovement : CharacterControllerMovementBase
{
    private Joystick _joystick;

    protected override void Awake()
    {
        base.Awake();

        _joystick = FindObjectOfType<Joystick>(true);
    }
    protected override void Update()
    {
        if (!Activated)
            return;
        var joyStickInput = GetInput();
        MoveTo(joyStickInput);
        base.Update();
    }

    public override void Activate()
    {
        base.Activate();

        if (!_joystick)
            _joystick = FindObjectOfType<VariableJoystick>();
    }

    private Vector3 GetInput()
    {
        return  new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
    }
}
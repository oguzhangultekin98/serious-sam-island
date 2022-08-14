using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehaviour : MonoBehaviour, IState
{
	public bool canTransitionItSelf { get { return false; } }

	private AIEye _eye;
	private AINavigationMovement _aiMovement;

	public WanderBehaviour(AIEye eye, AINavigationMovement aiNavigationMovement)
	{
		_eye = eye;
		_aiMovement = aiNavigationMovement;
	}

	public void Tick()
	{
		if (_aiMovement.IsMoving)
			return;

		_aiMovement.GoToRandomLocation();
	}

	public void OnEnter()
	{
	}

	public void OnExit()
	{
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehaviour : IState
{
	public bool canTransitionItSelf { get { return false; } }

	private Eye eye;
	private AINavigationMovement aiMovement;

	public WanderBehaviour(Eye eye, AINavigationMovement aiNavigationMovement)
	{
		this.eye = eye;
		aiMovement = aiNavigationMovement;
	}

	public void Tick()
	{
		if (aiMovement.IsMoving)
			return;

		aiMovement.GoToRandomLocation();
	}

	public void OnEnter()
	{
	}

	public void OnExit()
	{
	}
}

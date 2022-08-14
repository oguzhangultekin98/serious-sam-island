using UnityEngine;

public class AIBehaviourHandler : MonoBehaviour
{
	[SerializeField] private Eye eye;
	[Space]
	[SerializeField] private string DEBUG_currentBehaviour;

	private StateMachine stateMachine;
	private AINavigationMovement aiNavigationMovement;

	void Start()
	{
		aiNavigationMovement = GetComponent<AINavigationMovement>();

		stateMachine = new StateMachine();

		var wanderState = new WanderBehaviour(eye, aiNavigationMovement);

		stateMachine.SetState(wanderState);
	}

	private void Update()
	{
		if (GameStateHandler.GameState != GameStateHandler.State.InLoop ) { return; }
		stateMachine.Tick();
	}
}
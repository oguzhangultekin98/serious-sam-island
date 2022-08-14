using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(Animator))]
public class AINavigationMovement : MonoBehaviour
{

	private NavMeshAgent agent;
	private Animator animator;
	public Vector3 Destination { get { return agent.destination; } }

	[SerializeField] private float movementSpeed;
	[SerializeField] private float runAwaySpeed;
	[Space]
	[SerializeField] private float maxAnimatorSpeed;
	[Tooltip("Based on the max movement/chase speed")]
	[SerializeField] private AnimationCurve movementAnimatorSpeedMultiplier;

	private RandomMover randomMover;

	public bool IsMoving { get => isMoving; }
	private bool isMoving = false;
	private float maxSpeed;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
		agent.updateRotation = false;
		agent.speed = movementSpeed;
		maxSpeed = Mathf.Max(movementSpeed, runAwaySpeed);

		randomMover = AINavigation.Instance.GetRandomMover();
	}

	private void OnDie(IAiTarget obj)
	{
		agent.isStopped = true;
	}

	private void Update()
	{
		animator.SetFloat("Speed", agent.velocity.magnitude, 0.1f, Time.deltaTime);

		float animationSpeedX = agent.velocity.magnitude / maxSpeed;
		float animationSpeedY = movementAnimatorSpeedMultiplier.Evaluate(animationSpeedX);
		animator.SetFloat("MoveAnimationMultiplier", animationSpeedY);

		if (agent.velocity != Vector3.zero)
		{
			Vector3 lookDir = agent.velocity;
			lookDir.y = 0f;
			transform.rotation = Quaternion.LookRotation(lookDir);
		}

		if (agent.pathPending) { return; }

		if (agent.remainingDistance <= agent.stoppingDistance)
		{
			isMoving = false;
		}
	}

	public void GoToDestination(Vector3 loc, bool isGettingChased = false)
	{
		SetAgentDestination(loc);
		agent.speed = isGettingChased ? runAwaySpeed : movementSpeed;
	}

	public void GoToRandomLocation()
	{
		randomMover.SetNewRandomLocation();
		SetAgentDestination(randomMover.TargetPosition);
		agent.speed = movementSpeed;
	}

	public void Stop()
	{
		agent.isStopped = true;
		isMoving = false;
	}

	private void SetAgentDestination(Vector3 position)
	{
		agent.isStopped = false;
		agent.SetDestination(position);
		isMoving = true;
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (!AINavigation.DRAW_GIZMOS) { return; }
		if (agent == null) { return; }
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, agent.destination);
	}
#endif

}

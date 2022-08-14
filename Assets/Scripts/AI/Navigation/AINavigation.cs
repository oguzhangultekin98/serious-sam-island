using UnityEngine;

public class AINavigation : MonoBehaviour
{

	public const bool DRAW_GIZMOS = true;

	public static AINavigation Instance { get; private set; }

	[SerializeField] private Transform[] randomTargetLocations;

	private void Awake()
	{
		Instance = this;
	}

	public RandomMover GetRandomMover()
	{
		return new RandomMover(randomTargetLocations);
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (!DRAW_GIZMOS) { return; }
		Gizmos.color = Color.red;
		foreach(Transform loc in randomTargetLocations)
		{
			Gizmos.DrawWireSphere(loc.position, 0.5f);
		}
	}
#endif

}
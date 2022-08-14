using System.Collections.Generic;
using UnityEngine;

public class RandomMover
{

	private Transform[] allOptions;
	private List<Transform> previousRandomLocations;
	private Transform currentTarget;

	public Vector3 TargetPosition { get { return currentTarget.position; } }

	public RandomMover(Transform[] options)
	{
		allOptions = options;
		previousRandomLocations = new List<Transform>();
	}

	public void SetNewRandomLocation()
	{
		// Make sure we're not chasing a location that we have chased before
		if (currentTarget != null)
		{
			previousRandomLocations.Add(currentTarget);
		}

		List<Transform> remainingOptions = new List<Transform>(allOptions);
		foreach (Transform previousLoc in previousRandomLocations)
		{
			remainingOptions.Remove(previousLoc);
		}

		// Reset the list of remaining options if there is no option left
		if (remainingOptions.Count == 0)
		{
			previousRandomLocations.Clear();
			remainingOptions = new List<Transform>(allOptions);
			remainingOptions.Remove(currentTarget);
		}

		// Set the new target location
		int randomIndex = Random.Range(0, remainingOptions.Count);
		currentTarget = remainingOptions[randomIndex];
	}

}

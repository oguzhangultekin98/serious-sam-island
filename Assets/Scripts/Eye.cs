using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{

	private List<ITargetFaction> targets;
	private SphereCollider sphereCollider;

	public IReadOnlyList<ITargetFaction> Targets
	{
		get
		{
			return targets;
		}
	}

	private void Awake()
	{
		targets = new List<ITargetFaction>();
		sphereCollider = GetComponent<SphereCollider>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out ITargetFaction target) && !targets.Contains(target) && target.IsAlive)
		{
			targets.Add(target);
			target.OnRemoved += Target_OnRemoved;
		}
	}

	private void Target_OnRemoved(ITargetFaction _aiTarget)
	{
		_aiTarget.OnRemoved -= Target_OnRemoved;
		targets.Remove(_aiTarget);
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent(out ITargetFaction target) && targets.Contains(target))
		{
			target.OnRemoved -= Target_OnRemoved;
			targets.Remove(target);
		}
	}

	/// <summary>
	/// Set the Look range to the minLookRange if the minLookRange is greater than the SphereCollider radius
	/// </summary>
	/// <param name="minLookRange"> Minimum look range </param>
	/// <returns></returns>
	public void SetLookRange(float minLookRange)
	{
		if (sphereCollider.radius < minLookRange)
		{
			sphereCollider.radius = minLookRange;
		}
	}

	public List<T> GetTargetsOfType<T>() where T : Component
	{
		List<T> targets = new List<T>();
		for (int i = 0; i < this.targets.Count; i++)
		{
			if (this.targets[i].GetType().IsAssignableFrom(typeof(T)))
			{
				T target = (T)this.targets[i];
				targets.Add(target);
			}
		}
		return targets;
	}

	private List<ITargetFaction> GetTargetsOfFaction(Faction faction)
	{
		List<ITargetFaction> targets = new List<ITargetFaction>();
		for (int i = 0; i < this.targets.Count; i++)
		{
			if (this.targets[i].Faction == faction)
				targets.Add(this.targets[i]);
		}
		return targets;
	}

	public bool TargetInRange(Faction faction, float range, out ITargetFaction target)
	{
		target = GetClosestTargetOfFaction(faction);

		if (target != null &&
			Vector3.Distance(target.Pos, transform.position) < range)
		{
			return true;
		}

		target = null;
		return false;
	}

	public ITargetFaction GetClosestTargetOfFaction(Faction faction)
	{
		var targets = GetTargetsOfFaction(faction);
		float closestDistanceToPlayer = float.MaxValue;
		int closestIndex = -1;

		for (int i = 0; i < targets.Count; i++)
		{
			var distanceToPlayer = Vector3.Distance(targets[i].Pos, transform.position);
			if (distanceToPlayer < closestDistanceToPlayer)
			{
				closestDistanceToPlayer = distanceToPlayer;
				closestIndex = i;
			}
		}

		return closestIndex == -1 ? null : targets[closestIndex];
	}

}

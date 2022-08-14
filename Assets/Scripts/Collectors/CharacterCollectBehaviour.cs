using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollectBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectible collectibleBehaviour))
        {
            //Collect(collectibleBehaviour);
        }
    }
}

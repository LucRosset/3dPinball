using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Progression
{
    public class BallDespawner : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // No need to verify if `other´ is a Ball. Only Balls will interact with this trigger.
            Destroy(other.gameObject);
        }
    }
}
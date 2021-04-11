using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Progression
{
    public class PlayVolume : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}
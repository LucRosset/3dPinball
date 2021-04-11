using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Props
{
    public class Bumper : MonoBehaviour
    {
        [Tooltip("Force applied to the ball when it collides with the bumper.")]
        [SerializeField] private float force = 300f;
        [Tooltip("Display prop used to identify a bump.")]
        [SerializeField] private Display display = null;

        private void OnCollisionEnter(Collision other)
        {
            Vector3 appliedForce = other.transform.position - transform.position;
            appliedForce.y = 0f;
            appliedForce = appliedForce.normalized * force;
            other.gameObject.GetComponent<Rigidbody>().AddForce(appliedForce);
            display.SetDisplayState(true);
        }
    }
}

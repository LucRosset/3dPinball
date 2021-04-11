using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinball.Progression;

namespace Pinball.Props
{
    public class SideBumper : MonoBehaviour
    {
        [Tooltip("Force applied to the ball when it collides with the bumper.")]
        [SerializeField] private float force = 300f;
        [Tooltip("Display prop used to identify a bump.")]
        [SerializeField] private Display display = null;

        private Vector3 appliedForce;

        // Start is called before the first frame update
        void Start()
        {
            float facingAngle = transform.rotation.eulerAngles.y;
            appliedForce = new Vector3(
                Mathf.Cos(-facingAngle*Mathf.Deg2Rad),
                0f,
                Mathf.Sin(-facingAngle*Mathf.Deg2Rad)
            ) * force;
        }

        private void OnCollisionEnter(Collision other)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(appliedForce);
            display.SetDisplayState(true);
        }
    }
}
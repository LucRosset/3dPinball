using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinball.Progression;

namespace Pinball.Props
{
    public class PhysicsSideBumper : MonoBehaviour
    {
        [Tooltip("Distance the bumper will travel when activated.")]
        [SerializeField] private float activeAnchorDistance = .5f;

        private Vector3 startingPosition;
        private Vector3 activePosition;
        private SpringJoint springJoint;
        private bool active = false;

        // Start is called before the first frame update
        void Start()
        {
            float facingAngle = transform.rotation.eulerAngles.y;
            Vector3 springAnchorAxis = new Vector3(
                Mathf.Cos(-facingAngle*Mathf.Deg2Rad),
                0f,
                Mathf.Sin(-facingAngle*Mathf.Deg2Rad)
            );
            startingPosition = transform.position;
            activePosition = startingPosition + activeAnchorDistance * springAnchorAxis;
            springJoint = GetComponent<SpringJoint>();
            springJoint.axis = Vector3.forward;
            springJoint.connectedAnchor = startingPosition;
        }

        private void FixedUpdate()
        {
            if (active && Vector3.Distance(startingPosition, transform.position) >= activeAnchorDistance)
            {
                active = false;
                springJoint.connectedAnchor = startingPosition;
            }
        }

        private void OnCollisionEnter(Collision other) {
            // Not checking what is colliding against the side bumper because
            // anything that can actually bump into it should activate the bumper.
            if (!active)
            {
                active = true;
                springJoint.connectedAnchor = activePosition;
            }
        }
    }
}
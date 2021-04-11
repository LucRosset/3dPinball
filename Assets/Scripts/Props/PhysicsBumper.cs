using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinball.Progression;

namespace Pinball.Props
{
    public class PhysicsBumper : MonoBehaviour
    {
        [Tooltip("Bumper ring's travel direction and length when activated.")]
        [SerializeField] private float travel = -.5f;

        private SpringJoint springJoint;
        private Transform bumperRingTransform;
        private Vector3 restConnectedAnchor;
        private bool actuating;
        private bool returning;

        // Start is called before the first frame update
        void Start()
        {
            springJoint = GetComponentInChildren<SpringJoint>();
            bumperRingTransform = springJoint.transform;
            restConnectedAnchor = springJoint.connectedAnchor;
            actuating = false;
            returning = false;
        }

        private void FixedUpdate()
        {
            UpdateActuation();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!(actuating || returning))
            {
                actuating = true;
                springJoint.connectedAnchor = restConnectedAnchor + new Vector3(0f, travel, 0f);
            }
        }

        ///<summary>
        /// Check the bumper spring's state and set it's state accordingly.
        ///</summary>
        private void UpdateActuation()
        {
            if (actuating && distanceToConnectedAnchor() < .1f)
            {
                actuating = false;
                springJoint.connectedAnchor = restConnectedAnchor;
                returning = true;
            }
            else if (returning && distanceToConnectedAnchor() < .1f)
            {
                returning = false;
            }
        }

        ///<summary>
        /// Calculate the distance between the bumper ring and the spring attachment.
        ///</summary>
        ///<return>
        /// The spring's displacement.
        ///</return>
        private float distanceToConnectedAnchor()
        {
            // return Vector3.Distance(
            //     bumperRingTransform.localPosition,
            //     springJoint.connectedAnchor
            // );
            return Mathf.Abs(
                bumperRingTransform.localPosition.y
                - springJoint.connectedAnchor.y
            );
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinball.Props;

namespace Pinball.Progression
{
    public class PointsOnSpin : MonoBehaviour
    {
        [Tooltip("Angular velocity threshold above which the spinner awards points, in radians per second.")]
        [SerializeField] private float threshold = 2.5f;
        [Tooltip("Array of point rates added when the spinner is spun. Leave it blank for no points.")]
        [SerializeField] private int[] pointRates = null;
        [Tooltip("If an Action component is referenced here, will perform it when a collision happens.")]
        [SerializeField] private Action action = null;

        private int currentPointsIndex = 0;
        private Rigidbody myRigidbody;

        void Start()
        {
            myRigidbody = transform.GetComponentInChildren<Rigidbody>();
        }

        void Update()
        {
            if (action != null && pointRates != null &&  myRigidbody.angularVelocity.magnitude > threshold)
                action.DoAction(pointRates[currentPointsIndex]);
        }

        ///<summary>
        /// Set the points to the next tier.
        ///</summary>
        public void NextPoints()
        {
            if (pointRates != null && currentPointsIndex < pointRates.Length - 1)
            {
                currentPointsIndex++;
            }
        }

        ///<summary>
        /// Set the points tier.
        ///</summary>
        ///<param name="index">
        /// Index of the point tier.
        ///</param>
        public void SetPoints(int index)
        {
            if (pointRates != null && index < pointRates.Length)
            {
                currentPointsIndex = index;
            }
        }
    }
}

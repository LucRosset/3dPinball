using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinball.Props;

namespace Pinball.Progression
{
    public class PointsOnContact : MonoBehaviour
    {
        [Tooltip("Array of points added when the bumper is hit. Leave it blank for no points.")]
        [SerializeField] private int[] points = null;
        [Tooltip("If an Action component is referenced here, will perform it when a collision happens.")]
        [SerializeField] private Action action = null;

        private BlinkLamp blinkLamp = null;
        private int currentPointsIndex = 0;

        void Start()
        {
            blinkLamp = GetComponentInChildren<BlinkLamp>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (action != null && points != null)
                action.DoAction(points[currentPointsIndex]);
        }

        ///<summary>
        /// Set the points to the next tier.
        ///</summary>
        public void NextPoints()
        {
            if (points != null && currentPointsIndex < points.Length - 1)
            {
                currentPointsIndex++;
                blinkLamp.NextPoints();
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
            if (points != null && index < points.Length)
            {
                currentPointsIndex = index;
                if (blinkLamp)
                    blinkLamp.SetColor(index);
            }
        }
    }
}

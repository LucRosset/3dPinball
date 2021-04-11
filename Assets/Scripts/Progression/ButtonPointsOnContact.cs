using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinball.Props;

namespace Pinball.Progression
{
    public class ButtonPointsOnContact : MonoBehaviour
    {
        [Tooltip("Array of points added when the button is activated.")]
        [SerializeField] private int[] activatePoints = null;
        [Tooltip("Array of points added when the button is deactivated.")]
        [SerializeField] private int[] deactivatePoints = null;
        [Tooltip("Array of points added when the button is reactivated.")]
        [SerializeField] private int[] reactivatePoints = null;
        [Tooltip("If an IAction component is referenced here, will perform it when a collision happens.")]
        [SerializeField] private Action action = null;

        private bool previousState;
        private int currentPointsIndex = 0;
        private IButton button;

        void Start()
        {
            if (activatePoints.Length != deactivatePoints.Length || activatePoints.Length != reactivatePoints.Length)
                Debug.LogError("Error: component's points arrays must be the same length!");
            button = GetComponent<IButton>();
            previousState = button.GetButtonState();
        }

        private void OnCollisionEnter(Collision other)
        {
            bool isActive = button.GetButtonState();
            if (activatePoints != null && action != null)
            {
                if (isActive && !previousState)
                    action.DoAction(activatePoints[currentPointsIndex]);
                else if (!isActive)
                    action.DoAction(deactivatePoints[currentPointsIndex]);
                else //if (isActive && previousState)
                    action.DoAction(reactivatePoints[currentPointsIndex]);
            }
            previousState = isActive;
        }

        ///<summary>
        /// Set the points to the next tier.
        ///</summary>
        public void NextPoints()
        {
            if (activatePoints != null && currentPointsIndex < activatePoints.Length - 1)
                currentPointsIndex++;
        }

        ///<summary>
        /// Set the points tier.
        ///</summary>
        public void SetPoints(int index)
        {
            if (activatePoints != null && index < activatePoints.Length)
                currentPointsIndex = index;
        }
    }
}
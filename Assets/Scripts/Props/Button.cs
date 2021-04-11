using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinball.Progression;

namespace Pinball.Props
{
    public class Button : MonoBehaviour, IButton
    {
        [Tooltip("Display prop used to identify the button's state.")]
        [SerializeField] private Display display = null;

        private ButtonSet parentSet = null;
        private int buttonSetID = -1;
        private bool buttonState = false;

        // UnityEngine methods

        void Start()
        {
            parentSet = transform.parent.GetComponent<ButtonSet>();
            if (parentSet)
                buttonSetID = parentSet.GetButtonId(this.GetInstanceID());
        }

        private void OnCollisionEnter(Collision other) { SetState(); }

        // Interface methods

        public int GetButtonInstanceId() { return this.GetInstanceID(); }

        public bool GetButtonState() { return buttonState; }

        public void SetButtonState(bool state)
        {
            buttonState = state;
            StateChange();
        }

        // Private methods

        ///<summary>
        /// Set the button's state after the ball hits it.
        ///</summary>
        private void SetState()
        {
            if (parentSet)
                buttonState = parentSet.ButtonPressed(buttonSetID);
            else
                buttonState = !buttonState;
            StateChange();
        }

        ///<summary>
        /// Update display and perform corresponding action after a state change.
        ///</summary>
        private void StateChange()
        {
            if (display != null)
                display.SetDisplayState(buttonState);
        }
    }
}
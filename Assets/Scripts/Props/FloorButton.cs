using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinball.Progression;

namespace Pinball.Props
{
    public class FloorButton : MonoBehaviour, IButton
    {
        [Tooltip("Display prop used to identify the button's state.")]
        [SerializeField] private Display display = null;
        [Tooltip("Action executed when button is activated.")]
        [SerializeField] private Action activateAction = null;
        [Tooltip("Action executed when button is deactivated.")]
        [SerializeField] private Action deactivateAction = null;

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

        private void OnTriggerEnter(Collider other) { SetState(); }

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
            if (buttonState && activateAction != null)
                activateAction.DoAction();
            else if (!buttonState && deactivateAction != null)
                deactivateAction.DoAction();
        }
    }
}
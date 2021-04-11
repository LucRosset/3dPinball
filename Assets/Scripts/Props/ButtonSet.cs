using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinball.Progression;

namespace Pinball.Props
{
    public class ButtonSet : MonoBehaviour
    {
        [Tooltip("Can the buttons be deactivated if the ball collides with activated ones?")]
        [SerializeField] private bool CanDeactivateButtons = false;
        [Tooltip("Should the button states cycle with flipper input?")]
        [SerializeField] private bool cycleButtonStates = false;
        [Tooltip("Action to be performed when all buttons are pressed.")]
        [SerializeField] private Action[] actions = null;

        private IButton[] buttons = null;
        private int numButtons;
        private bool[] buttonStates;
        private bool allActive = false;

        // UnityEngine methods

        void Start()
        {
            buttons = GetComponentsInChildren<IButton>();
            numButtons = buttons.Length;
            buttonStates = new bool[numButtons];
            for (int i = 0; i < numButtons; i++)
                buttonStates[i] = false;
        }

        void Update()
        {
            if (cycleButtonStates)
                StateCycling();
            if (allActive && actions != null)
            {
                foreach (Action action in actions)
                    action.DoAction();
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].SetButtonState(false);
                    allActive = false;
                    buttonStates[i] = false;
                }
            }

        }

        // Private methods

        ///<summary>
        /// Checks and applies cycling to button states.
        ///</summary>
        private void StateCycling()
        {
            if (Input.GetButtonDown("Left Flippers")) // Cycle left
            {
                bool firstButtonState = buttonStates[0];
                for (int i = 0; i < numButtons-1; i++)
                {
                    buttonStates[i] = buttonStates[i+1];
                    buttons[i].SetButtonState(buttonStates[i]);
                }
                buttonStates[numButtons-1] = firstButtonState;
                buttons[numButtons-1].SetButtonState(firstButtonState);
            }
            if (Input.GetButtonDown("Right Flippers")) // Cycle right
            {
                bool lastButtonState = buttonStates[numButtons-1];
                for (int i = numButtons-1; i > 0; i--)
                {
                    buttonStates[i] = buttonStates[i-1];
                    buttons[i].SetButtonState(buttonStates[i]);
                }
                buttonStates[0] = lastButtonState;
                buttons[0].SetButtonState(lastButtonState);
            }
        }

        // Public methods

        ///<summary>
        /// Returns the list index of the child button calling the method.
        ///</summary>
        ///<param name="instanceID">
        /// InstanceID of the IButton object.
        ///</param>
        ///<return>
        /// The list index corresponding to the provided InstanceID.
        ///</return>
        public int GetButtonId(int instanceID)
        {
            int id;
            for (id = 0; id < numButtons; id++)
                if (buttons[id].GetButtonInstanceId() == instanceID)
                    break;
            return id;
        }

        ///<summary>
        /// Notifies the ButtonSet object that the caller Button was hit.
        ///</summary>
        ///<param name="id">
        /// List index of the caller, provided by ButtonSet.GetButtonId.
        ///</param>
        ///<return>
        /// The button's new state.
        ///</return>
        public bool ButtonPressed(int id)
        {
            buttonStates[id] = ! (buttonStates[id] & CanDeactivateButtons);
            // Check if all buttons are active
            allActive = true;
            foreach (bool state in buttonStates)
                allActive &= state; 
            return buttonStates[id];
        }

        ///<summary>
        /// Deactivates all buttons in the set.
        ///</summary>
        public void ResetButtons()
        {
            for (int i = 0; i < numButtons-1; i++)
            {
                buttonStates[i] = false;
                buttons[i].SetButtonState(false);
            }
        }
    }
}
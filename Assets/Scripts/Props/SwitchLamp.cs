using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Props
{
    public class SwitchLamp : Display
    {
        [Tooltip("Material for unlit Lamp.")]
        [SerializeField] private Material unlit = null;
        [Tooltip("Material for lit Lamp.")]
        [SerializeField] private Material lit = null;

        private bool currentState = false;
        private bool blinkAnimation = false;
        private MeshRenderer myRenderer;

        // UnityEngine Methods

        // Start is called before the first frame update
        void Start()
        {
            myRenderer = GetComponent<MeshRenderer>();
        }

        // IDisplay methods

        override public void SetDisplayState(bool state)
        {
            currentState = state;
            if (!blinkAnimation)
                myRenderer.material = state ? lit : unlit;
        }

        // Public methods

        ///<summary>
        /// Schedule a blinking pattern.
        ///</summary>
        ///<param name="cycles">
        /// Number of blinks.
        ///</param>
        ///<param name="period">
        /// Time the lamp stays on/off, in seconds.
        ///</param>
        public void Blink(int cycles, float period)
        {
            if (!blinkAnimation)
                StartCoroutine(BlinkEffect(cycles, period));
        }

        // Private methods

        ///<summary>
        /// Execute a blinking pattern.
        ///</summary>
        ///<param name="cycles">
        /// Number of blinks.
        ///</param>
        ///<param name="period">
        /// Time the lamp stays on/off, in seconds.
        ///</param>
        private IEnumerator BlinkEffect(int cycles, float period)
        {
            blinkAnimation = true;
            for (int i = 0; i < cycles; i++)
            {
                yield return new WaitForSeconds(period);
                myRenderer.material = lit;
                yield return new WaitForSeconds(period);
                myRenderer.material = unlit;
            }
            myRenderer.material = currentState ? lit : unlit;
            blinkAnimation = false;
        }
    }
}
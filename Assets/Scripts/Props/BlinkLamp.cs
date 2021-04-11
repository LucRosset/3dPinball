using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Props
{
    public class BlinkLamp : Display
    {
        [Tooltip("Time, in seconds, the light stays on.")]
        [SerializeField] private float blinkTime = .3f;
        [Tooltip("Materials for unlit Lamp.")]
        [SerializeField] private Material[] unlitColors = null;
        [Tooltip("Materials for lit Lamp.")]
        [SerializeField] private Material[] litColors = null;

        private MeshRenderer myRenderer;
        private Coroutine waitToTurnOff;
        private int currentColorIndex = 0;

        // UnityEngine Methods

        // Start is called before the first frame update
        void Start()
        {
            if (unlitColors == null || litColors == null || unlitColors.Length == 0 || litColors.Length != unlitColors.Length)
                Debug.LogError("Error: BlinkLamp must have nonzero-length arrays of same size for lit and unlit colors!");
            myRenderer = GetComponent<MeshRenderer>();
        }

        // IDisplay methods

        override public void SetDisplayState(bool state)
        {
            if (state)
            {
                myRenderer.material = litColors[currentColorIndex];
                if (waitToTurnOff != null)
                    StopCoroutine(waitToTurnOff);
                waitToTurnOff = StartCoroutine(WaitToTurnOff());
            }
            else
            {
                myRenderer.material = unlitColors[currentColorIndex];
                StopCoroutine(waitToTurnOff);
            }
        }

        // BlinkLamp methods

        ///<summary>
        /// Set the color to the next tier.
        ///</summary>
        public void NextPoints()
        {
            if (currentColorIndex < unlitColors.Length - 1)
            {
                currentColorIndex++;
                myRenderer.material = unlitColors[currentColorIndex];
            }
        }

        ///<summary>
        /// Set the lamp color index.
        ///</summary>
        ///<param name="index">
        /// Index of the color.
        ///</param>
        public void SetColor(int index)
        {
            if (index < unlitColors.Length)
            {
                currentColorIndex = index;
                myRenderer.material = unlitColors[currentColorIndex];
            }
        }

        ///<summary>
        /// Wait the component's set delay and light the lamp off.
        ///</summary>
        private IEnumerator WaitToTurnOff()
        {
            yield return new WaitForSeconds(blinkTime);
            myRenderer.material = unlitColors[currentColorIndex];
        }
    }
}
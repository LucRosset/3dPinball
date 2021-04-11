using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinball.Props;
using System;

namespace Pinball.Progression
{
    public class GameProgressController : MonoBehaviour
    {
        [Header("Progression components")]
        [Tooltip("Reference to points counter.")]
        [SerializeField] private PointsCounter pointsCounter = null;
        [Tooltip("Reference to points counter.")]
        [SerializeField] private CreditsCounter creditsCounter = null;

        [Header("Controllable components")]
        [Tooltip("Reference to the left flipper script.")]
        [SerializeField] private Flipper leftFlipper = null;
        [Tooltip("Reference to the right flipper script.")]
        [SerializeField] private Flipper rightFlipper = null;
        [Tooltip("Reference to the plunger script.")]
        [SerializeField] private PlungerPull plunger = null;

        [Header("Overlay components")]
        [Tooltip("Overlay canvas Game Object.")]
        [SerializeField] private GameObject overlay = null;
        [Tooltip("Previous score display text.")]
        [SerializeField] private EndScore endScore = null;

        private ButtonSet[] buttonSets;
        private CyclePointsTier[] pointsTiers;

        // Start is called before the first frame update
        void Start()
        {
            // Run dependency checks
            if (!pointsCounter || !creditsCounter)
                Debug.LogError("Error: GameProgressController must have pointsCounter and creditsCounter set!");
            if (!leftFlipper || !rightFlipper || !plunger)
                Debug.LogError("Error: GameProgressController must have flippers and plunger set!");
            if (!overlay || !endScore)
                Debug.LogError("Error: GameProgressController must have overlay and endScore text set!");
            // Actually start
            buttonSets = FindObjectsOfType<ButtonSet>();
            pointsTiers = FindObjectsOfType<CyclePointsTier>();
            GameOver();
        }

        // Update is called once per frame
        void Update()
        {
            // Start game
            if (overlay.activeInHierarchy && Input.GetButtonDown("Plunger"))
            {
                EnableOverlay(false);
                EnableControls(true);
            }
            // End game
            else if (creditsCounter.ballsLeft < 0)
            {
                GameOver();
            }
        }
        
        ///<summary>
        /// Runs the game-over sequence.
        ///</summary>
        private void GameOver()
        {
            creditsCounter.ResetBallsLeft();
            ResetButtons();
            EnableOverlay(true);
            EnableControls(false);
            pointsCounter.SetPoints(0);
        }

        ///<summary>
        /// Sets all buttons in all button sets to deactivated.
        ///</summary>
        private void ResetButtons()
        {
            foreach(ButtonSet buttonSet in buttonSets)
                buttonSet.ResetButtons();
            foreach(CyclePointsTier pointsTier in pointsTiers)
                pointsTier.ResetPoints();
        }

        ///<summary>
        /// Enables/disables overlay and updates the end score.
        ///</summary>
        ///<param name="state">
        /// Enables overlay if true, else disables it.
        ///</param>
        private void EnableOverlay(bool state)
        {
            overlay.SetActive(state);
            endScore.SetScore(pointsCounter.pointsTotal);
        }

        ///<summary>
        /// Enables/disables flippers and plunger controls.
        ///</summary>
        ///<param name="state">
        /// Enables controls if true, else disables them.
        ///</param>
        private void EnableControls(bool state)
        {
            leftFlipper.enabled = state;
            rightFlipper.enabled = state;
            plunger.enabled = state;
        }
    }
}
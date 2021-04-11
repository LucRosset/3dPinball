using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinball.Props;

namespace Pinball.Progression
{
    ///<summary>
    /// Creates two new balls when activated.
    ///</summary>
    public class Multiball : Action
    {
        [Tooltip("Prefab for the ball to be spawned by the multiball action.")]
        [SerializeField] GameObject ballPrefab = null;
        [Tooltip("First launcher to spawn a ball.")]
        [SerializeField] DelayedLauncher launcher1 = null;
        [Tooltip("Second launcher to spawn a ball.")]
        [SerializeField] DelayedLauncher launcher2 = null;
        [Tooltip("Time, in seconds, for the launchers to release the extra balls.")]
        [SerializeField] float delay = .5f;

        private Transform ballContainer;

        public void Start()
        {
            ballContainer = GameObject.Find("Balls").transform;
            if (ballPrefab == null || launcher1 == null || launcher2 == null)
                Debug.LogError("ERROR: Multiball must have the serialized parameters set: Ball Prefab, Launcher 1, Launcher 2.");
        }

        override public void DoAction() { StartMultiball(); }

        override public void DoAction(int value) { StartMultiball(); }

        private void StartMultiball()
        {
            GameObject newBall1 = Instantiate(ballPrefab);
            GameObject newBall2 = Instantiate(ballPrefab);
            newBall1.transform.SetParent(ballContainer);
            newBall2.transform.SetParent(ballContainer);
            launcher1.LaunchBall(newBall1.GetComponent<Rigidbody>(), delay);
            launcher2.LaunchBall(newBall2.GetComponent<Rigidbody>(), delay);
        }
    }
}
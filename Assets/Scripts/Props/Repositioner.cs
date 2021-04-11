using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Props
{
    public class Repositioner : MonoBehaviour
    {
        [Tooltip("Delayed Launcher to send the ball to.")]
        [SerializeField] private DelayedLauncher launcher = null;
        [Tooltip("Delay applied before launching the ball.")]
        [SerializeField] private float delay = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if (launcher)
            {
                launcher.LaunchBall(
                    other.GetComponent<Rigidbody>(),
                    delay
                );
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Props
{
public class DelayedLauncher : MonoBehaviour
{
    [Tooltip("Launching force used to propel the ball.")]
    [SerializeField] private float launchForce = 1000f;
    [Tooltip("Launching force's angle, in degrees.")]
    [SerializeField] private float launchAngle = 0f;
    [Tooltip("Should the launcher reset the ball's angular velocity?")]
    [SerializeField] private bool stopRotation = true;

    ///<summary>
    /// Wait for delay then launch ball with launchForce.
    ///</summary>
    ///<param name="ball">
    /// The ball's Rigidbody component.
    ///</param>
    ///<param name="delay">
    /// Delay before launching ball, in seconds.
    ///</param>
    public void LaunchBall(Rigidbody ball, float delay)
    {
        StartCoroutine(WaitAndLaunch(ball, delay));
    }

    ///<summary>
    /// Coroutine that implements the delay and launch.
    ///</summary>
    ///<param name="ball">
    /// The ball's Rigidbody component.
    ///</param>
    ///<param name="delay">
    /// Delay before launching ball, in seconds.
    ///</param>
    IEnumerator WaitAndLaunch(Rigidbody ball, float delay)
    {
        ball.useGravity = false;
        ball.velocity = Vector3.zero;
        if (stopRotation) { ball.angularVelocity = Vector3.zero; }
        ball.transform.position = transform.position;
        yield return new WaitForSeconds(delay);
        ball.useGravity = true;
        ball.AddForce(launchForce * new Vector3(
            Mathf.Cos(launchAngle * Mathf.Deg2Rad),
            0f,
            Mathf.Sin(launchAngle * Mathf.Deg2Rad)
        ));
    }
}
}
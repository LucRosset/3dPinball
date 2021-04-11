using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Pinball.Progression
{
    public class CreditsCounter : MonoBehaviour
    {
        [Tooltip("Initial number of balls.")]
        [SerializeField] private int initialBalls = 3;
        [Tooltip("Ball spawner that delivers the ball to the plunger.")]
        [SerializeField] BallSpawner mainSpawner = null;
        [Tooltip("Text displaying the number of balls left.")]
        [SerializeField] TextMeshProUGUI displayText = null;
        
        ///<summary>
        /// Indicates how many credits (balls) are left in this game session.
        /// A negative value flags the game is over.
        ///</summary>
        public int ballsLeft {get; private set;}

        void Awake()
        {
            // Keep only a single instance of CreditsCounter in the scene
            if (FindObjectsOfType<CreditsCounter>().Length > 1)
                Destroy(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            if (!mainSpawner)
                Debug.LogError("Error: CreditsCounter must have a BallSpawner set!");
            ballsLeft = initialBalls;
            displayText.text = ballsLeft.ToString();
        }

        ///<summary>
        /// Notifies the CreditsCounting component that all balls in the table were lost.
        ///</summary>
        ///<return>
        /// True if a new ball will be spawned, else false.
        ///</return>
        public void BallLost()
        {
            if (--ballsLeft < 0)
            {
                return;
            }
            mainSpawner.SpawnBall();
            displayText.text = ballsLeft.ToString();
        }

        ///<summary>
        /// Add to the number of balls left.
        ///</summary>
        ///<param name="balls">
        /// Number of balls (credits) to be added.
        ///</param>
        public void AddBalls(int balls)
        {
            ballsLeft += balls;
            displayText.text = ballsLeft.ToString();
        }

        ///<summary>
        /// Resets the number of balls to spawn at the plunger to the initial amount.
        ///</summary>
        public void ResetBallsLeft()
        {
            ballsLeft = initialBalls-1;
            displayText.text = ballsLeft.ToString();
            mainSpawner.SpawnBall();
        }
    }
}
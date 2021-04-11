using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Progression
{
    public class BallSpawner : MonoBehaviour
    {
        [Tooltip("Game Object to spawn.")]
        [SerializeField] private GameObject ball = null;
        [Tooltip("Time to wait for the next ball to spawn, in seconds.")]
        [SerializeField] float timeToSpawn = 2f;

        private Transform ballContainer;

        public void Start()
        {
            ballContainer = GameObject.Find("Balls").transform;
        }

        ///<summary>
        /// Spawn the provided GameObject or a default ball if no parameter is provided.
        ///</summary>
        ///<param name="spawned">
        /// GameObject to be spawned. Leave as null or empty to spawn a default ball.
        ///</param>
        public void SpawnBall(GameObject spawned=null)
        {
            if (spawned)
                StartCoroutine(WaitAndSpawn(spawned));
            else
                StartCoroutine(WaitAndSpawn(ball));
        }

        ///<summary>
        /// Instantiates a GameObject after this component's delay time.
        ///</summary>
        ///<param name="ball">
        /// GameObject or prefab to instantiate.
        ///</param>
        private IEnumerator WaitAndSpawn(GameObject ball)
        {
            yield return new WaitForSeconds(timeToSpawn);
            GameObject newBall = Instantiate(ball);
            newBall.transform.SetParent(ballContainer);
        }
    }
}
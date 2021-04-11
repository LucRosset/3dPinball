using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Progression
{
    ///<sumary>
    /// This Game Object keeps track of how many balls are in the table.
    /// When all balls are lost, it notifies the CreditsCounter.
    ///</summary>
    public class BallCounter : MonoBehaviour
    {
        private CreditsCounter creditsCounter;
        public bool waiting {get; set;} = false;

        // Start is called before the first frame update
        void Start()
        {
            creditsCounter = FindObjectOfType<CreditsCounter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!waiting && transform.childCount < 1)
            {
                creditsCounter.BallLost();
                waiting = true;
            }
            else if (transform.childCount > 0)
                waiting = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Progression
{
    ///<summary>
    /// Adds balls to the ball count.
    ///</summary>
    public class AddBalls : Action
    {
        private CreditsCounter creditsCounter;

        private void Start() { creditsCounter = FindObjectOfType<CreditsCounter>(); }

        override public void DoAction() { creditsCounter.AddBalls(1); }

        override public void DoAction(int value) { creditsCounter.AddBalls(value); }
    }
}
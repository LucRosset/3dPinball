using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinball.Props;

namespace Pinball.Progression
{
    public class CyclePointsTier : Action
    {
        [Tooltip("Number of blinks when all buttons are activated.")]
        [SerializeField] private int cycles = 3;
        [Tooltip("Time between switching lights on/off, in seconds.")]
        [SerializeField] private float period = .15f;

        private PointsOnContact[] pointProps;
        private SwitchLamp[] lamps;

        // Start is called before the first frame update
        void Start()
        {
            pointProps = GetComponentsInChildren<PointsOnContact>();
            lamps = GetComponentsInChildren<SwitchLamp>();
        }

        override public void DoAction()
        {
            foreach (PointsOnContact prop in pointProps)
                prop.NextPoints();
            foreach (SwitchLamp lamp in lamps)
                lamp.Blink(cycles, period);
        }

        override public void DoAction(int value)
        {
            foreach (PointsOnContact prop in pointProps)
                prop.NextPoints();
            foreach (SwitchLamp lamp in lamps)
                lamp.Blink(cycles, period);
            
        }

        public void ResetPoints()
        {
            foreach (PointsOnContact prop in pointProps)
                prop.SetPoints(0);
            foreach (SwitchLamp lamp in lamps)
                lamp.SetDisplayState(false);
        }
    }
}
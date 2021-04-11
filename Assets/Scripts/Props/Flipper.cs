using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Props
{
    public class Flipper : MonoBehaviour
    {
        [Tooltip("If true, responds to the left flippers button. Else, responds to the right flipper button")]
        [SerializeField] private bool isLeft = true;

        private HingeJoint myHingeJoint;
        private string button;
        
        // UnityEngine Methods

        private void Start()
        {
            myHingeJoint = GetComponent<HingeJoint>();
            myHingeJoint.useMotor = false;

            button = (isLeft ? "Left" : "Right") + " Flippers";
        }

        private void Update() { myHingeJoint.useMotor = Input.GetButton(button); }

        // Private Methods

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Props
{
    public class PlungerPull : MonoBehaviour
    {
        [Tooltip("Displacement on the connected body to pre-tension the spring.")]
        [SerializeField] private Vector3 axis = Vector3.forward;
        [Tooltip("How far the plunger can be pulled away from the spring's connected anchor.")]
        [SerializeField] private float maxOffset = 4f;
        [Tooltip("Pulling cycle's frequency, in Hz.")]
        [SerializeField] private float frequency = .3f;

        private Vector3 startingPosition;
        private bool pullPlunger = false;
        private bool ready = false;
        private Rigidbody myRigidbody;
        private float cicleTime = 0f;

        // Start is called before the first frame update
        void Start()
        {
            // float startingDistance;
            myRigidbody = GetComponent<Rigidbody>();
            SpringJoint springJoint = GetComponent<SpringJoint>();
            startingPosition = transform.position;
            springJoint.axis = axis;
            frequency *= 2*Mathf.PI;
        }

        // Update is called once per frame
        void Update()
        {
            ready = Mathf.Abs(transform.position.z - startingPosition.z) < .1f;

            if (Input.GetButtonDown("Plunger") && ready)
            {
                pullPlunger = true;
                myRigidbody.isKinematic = true;
                cicleTime = 0f;
            }
            else if (Input.GetButtonUp("Plunger"))
            {
                pullPlunger = false;
                myRigidbody.isKinematic = false;
            }
        }

        private void FixedUpdate()
        {
            if (pullPlunger)
            {
                cicleTime += frequency * Time.fixedDeltaTime;
                transform.position = startingPosition + new Vector3(
                    0f,
                    0f,
                    - maxOffset * Mathf.Abs(Mathf.Sin(cicleTime))
                );
            }
        }
    }
}
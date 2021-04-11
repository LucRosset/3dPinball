using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Props
{
    public class Spinner : MonoBehaviour
    {
        [Tooltip("Scale the angular velocity of the spinner when the ball hits it.")]
        [SerializeField] private float spinScaler = 1f;

        private Rigidbody myRigidbody;
        private Vector3 myDirection;
        private Vector3 spinAxis;

        // Start is called before the first frame update
        void Start()
        {
            float angle = transform.eulerAngles.y * Mathf.Deg2Rad;
            myRigidbody = transform.GetComponentInChildren<Rigidbody>();
            myDirection = -(new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)));
            spinAxis = new Vector3(Mathf.Cos(angle), 0f, -Mathf.Sin(angle));
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            Spin(other.GetComponent<Rigidbody>().velocity);
        }

        // Private Methods

        private void Spin(Vector3 ballVelocity)
        {
            float spinVelocity = Vector3.Dot(ballVelocity, myDirection);
            myRigidbody.angularVelocity = spinVelocity * spinScaler * spinAxis;
        }
    }
}
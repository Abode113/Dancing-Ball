using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace UnityStandardAssets.Vehicles.Ball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float m_MovePower = 5; // The force added to the ball to move it.
        [SerializeField] private bool m_UseTorque = true; // Whether or not to use torque to move the ball.
        [SerializeField] private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
        [SerializeField] private float m_JumpPower = 2; // The force added to the ball when it jumps.

        private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
        private Rigidbody m_Rigidbody;

		//------------------
		private Ball ball;

		private Vector3 move;

		private Transform cam;
		private Vector3 camForword;
		private bool jump, jumpp;

		float right = -0.06f, up = 1.5f;
		//------------------
		private void Awake(){
			ball = GetComponent<Ball> ();

			if (Camera.main != null) {
				cam = Camera.main.transform;
			} else {
				Debug.LogWarning (
					"Warning: no main camera found. Ball needs a camera tagged \"MainCamera\", for camera-relative controls.");
			}
		}
		//------------------
        private void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            // Set the maximum angular velocity.
            GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
        }


        public void Move(Vector3 moveDirection, bool jump)
        {
            // If using torque to rotate the ball...
            if (m_UseTorque)
            {
                // ... add torque around the axis defined by the move direction.
                m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x)*m_MovePower);
            }
            else
            {
                // Otherwise add force in the move direction.
                m_Rigidbody.AddForce(moveDirection*m_MovePower);
            }

            // If on the ground and jump is pressed...
            if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength) && jump)
            {
                // ... add force in upwards.
                m_Rigidbody.AddForce(Vector3.up*m_JumpPower, ForceMode.Impulse);
            }
        }
		private void Update(){
			//float h = CrossPlatformInputManager.GetAxis ("Horizontal");
			//float v = CrossPlatformInputManager.GetAxis ("Vertical");

			//jump = CrossPlatformInputManager.GetButton ("Jump");


			if (cam != null) {
				camForword = Vector3.Scale (cam.forward, new Vector3 (1, 0, 1)).normalized;
				move = (up * camForword + right * cam.right).normalized;
			} else {
				move = (up * Vector3.forward + right * Vector3.right).normalized;
			}
			if (Input.GetButtonDown("Jump")) {
				m_Rigidbody.velocity = Vector3.zero;
				//right = -right;
				if (right == -0.06f && up == 1.5f) {
					up = 0.0f;right = -1.5f;
				}else if (right == -1.5f && up == 0.0f) {
					right = -0.06f;up = 1.5f;
				}

				move = (2 * up * Vector3.forward + 2 * right * Vector3.right).normalized;
				m_Rigidbody.AddTorque (new Vector3 (move.z, 0, -move.x) * m_MovePower);
			} else {
				m_Rigidbody.AddTorque (new Vector3 (move.z, 0, -move.x) * m_MovePower);
			}
//			if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength) && jump)
//			{
//				// ... add force in upwards.
//				m_Rigidbody.AddForce(Vector3.up*m_JumpPower, ForceMode.Impulse);
//			}
		}
    }
}

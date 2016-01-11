using UnityEngine;
using System.Collections;

public class PlayerMotor : CharacterMotor
{
	#region Properties
	public float walkSpeed = 1f;
	public float runSpeed = 2f;
	public float jumpForce = 10f;
	public float inertialTime = 0.1f;
	public float MouseXSensitivity = 1f;
	public float MouseYSensitivity = 1f;
	public Rigidbody charactRigidbody = null;
	public Camera cameraObject = null;
	public Transform cameraSpot = null;

	protected float currentSpeed = 0f;
	protected Vector3 currentVelocity = Vector3.zero;
	protected float nextTimeJump = 0f;
	#endregion

	#region Class Methods
	internal override void Enter()
	{
		currentVelocity = Vector3.zero;
		nextTimeJump = 0f;
	}

	internal override void Manage()
	{
		Move ();
		MoveCamera ();
	}
	#endregion

	#region Movement Methods
	internal void Move()
	{
		//Init
		Vector3 newDir = Vector3.zero;

		//Check Speed
		if (JEngine.Instance.inputManager.GetInput ("Run"))
			currentSpeed = runSpeed;
		else
			currentSpeed = walkSpeed;

		//Compute direction vector
		if(JEngine.Instance.inputManager.GetInput("Up"))
			newDir += transform.forward;
		if(JEngine.Instance.inputManager.GetInput("Down"))
			newDir -= transform.forward;
		if(JEngine.Instance.inputManager.GetInput("Left"))
			newDir -= transform.right;
		if(JEngine.Instance.inputManager.GetInput("Right"))
			newDir += transform.right;

		newDir.Normalize ();
		newDir *= currentSpeed;
		newDir = (newDir - currentVelocity) * (Time.deltaTime / inertialTime);

		//Check Jump
		currentVelocity.y = 0f;
		if(JEngine.Instance.inputManager.GetInputEnter ("Jump") && nextTimeJump <= Time.time)
		{
			//Ground detection
			Collider[] collisions = Physics.OverlapSphere(transform.position, 0.25f, LayerMask.GetMask("Ground", "Obstacle"));
			if(collisions.Length > 0)
			{
				//Jump
				currentVelocity.y = jumpForce;
				nextTimeJump = Time.time + 0.25f;
			}
		}

		//Apply Changes
		currentVelocity += newDir;
		charactRigidbody.velocity = new Vector3 (currentVelocity.x, charactRigidbody.velocity.y + currentVelocity.y, currentVelocity.z);
	}

	internal void MoveCamera()
	{
		//Compute Camera movement
		//Horizontal
		transform.rotation = Quaternion.Euler (new Vector3(transform.rotation.eulerAngles.x,
		                                                   transform.rotation.eulerAngles.y + (Input.GetAxis ("Mouse X") * MouseYSensitivity),
		                                                   transform.rotation.eulerAngles.z));

		//Vertical
		float angle = cameraSpot.rotation.eulerAngles.x + (-Input.GetAxis ("Mouse Y") * MouseXSensitivity);
		if(angle <= 80f || angle >= 270f)
		{
			cameraSpot.rotation = Quaternion.Euler (new Vector3(angle,
			                                                    cameraSpot.rotation.eulerAngles.y,
			                                                    cameraSpot.rotation.eulerAngles.z));
		}

		//Apply Changes
		cameraObject.transform.position = cameraSpot.position;
		cameraObject.transform.rotation = cameraSpot.rotation;
	}
	#endregion
}

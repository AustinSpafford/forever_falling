using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float DefaultGravityMagnitude = 1.0f;

	public Vector3 CurrentGravityAcceleration { get; private set; }
	
	public Quaternion DesiredBodyOrientation { get; private set; }
	public Vector3 DesiredGravityAcceleration { get; private set; }

	public bool DebugEnabled = false;
	
	public void Awake ()
	{
		playerRigidbody = GetComponent<Rigidbody>();

		CurrentGravityAcceleration = Vector3.down;
	}

	public void Start ()
	{
	}
	
	public void Update ()
	{
		ApplyLocalGravity();

		if (DebugEnabled)
		{
			RenderGravityShiftGhost();
		}
	}

	public void StartGravityShift(
		Vector3 desiredGravityAcceleration)
	{
		DesiredGravityAcceleration = desiredGravityAcceleration;

		DesiredBodyOrientation = BuildGravityShiftBodyOrientation(desiredGravityAcceleration);

		// Full-stop so we stay aligned with the transition-effect.
		playerRigidbody.velocity = Vector3.zero;

		// Long-term, the goal is to perform this once the player has fallen through a 
		// portal (or something similar); for now though, we'll just immediately pop them.
		SnapRotationToGravityReferenceFrame();
	}
	
	public void CancelGravity()
	{
		CurrentGravityAcceleration = Vector3.zero;

		DesiredGravityAcceleration = Vector3.zero;
		DesiredBodyOrientation = transform.rotation;
		
		playerRigidbody.velocity = Vector3.zero;
	}

	public void OnCollisionEnter (
		Collision collision)
	{
		ContactPoint contactPoint = collision.contacts[0];

		Quaternion oldBodyOrientation = transform.rotation;

		// TODO Refactor this so we're not having to abuse the public-API.
		StartGravityShift(-1 * contactPoint.normal);

		if (DebugEnabled)
		{
			Debug.LogFormat(
				"Collision with normal {0} rotated us by {1}.",
				contactPoint.normal,
				(Quaternion.Inverse(oldBodyOrientation) * transform.rotation).eulerAngles);
		}
	}

	private Rigidbody playerRigidbody = null;

	private void ApplyLocalGravity ()
	{
		playerRigidbody.AddForce(CurrentGravityAcceleration, ForceMode.Acceleration);
	}

	private Quaternion BuildGravityShiftBodyOrientation (
		Vector3 desiredGravityAcceleration)
	{
		Vector3 currentDownVector = (transform.rotation * Vector3.down);
		
		// Our new body orientation _must_ be matched to the new gravity vector, but other than that we want to
		// as closely as possible match our current camera orientation. 
		Quaternion desiredBodyOrientation = (
			Quaternion.FromToRotation(currentDownVector, desiredGravityAcceleration) *
			transform.rotation);

		return desiredBodyOrientation;
	}

	private void RenderGravityShiftGhost ()
	{
		Camera childCamera = GetComponentInChildren<Camera>();

		Quaternion ghostBodyOrientation = 
			BuildGravityShiftBodyOrientation(childCamera.transform.forward);

		Debug.DrawLine(
			transform.position, 
			(transform.position + (ghostBodyOrientation * Vector3.right)),
			Color.red);
		
		Debug.DrawLine(
			transform.position, 
			(transform.position + (ghostBodyOrientation * Vector3.up)),
			Color.green);
		
		Debug.DrawLine(
			transform.position, 
			(transform.position + (ghostBodyOrientation * Vector3.forward)),
			Color.blue);
	}

	private void SnapRotationToGravityReferenceFrame ()
	{
		CurrentGravityAcceleration = DesiredGravityAcceleration;
		
		transform.rotation = DesiredBodyOrientation;
	}
}

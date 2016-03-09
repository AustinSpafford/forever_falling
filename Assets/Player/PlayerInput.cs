using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	public void Awake ()
	{
		childCamera = GetComponentInChildren<Camera>();
		playerMovement = GetComponent<PlayerMovement>();
	}

	public void Start ()
	{
	}
	
	public void Update ()
	{
		if (Input.GetButtonDown("Recenter HMD"))
		{
			UnityEngine.VR.InputTracking.Recenter();
		}
		
		if (Input.GetButtonDown("Fall"))
		{
			playerMovement.StartGravityShift(childCamera.transform.forward * playerMovement.DefaultGravityMagnitude);
		}
		
		if (Input.GetButtonDown("Cancel Gravity"))
		{
			playerMovement.CancelGravity();
		}
	}
	
	private Camera childCamera = null;
	private PlayerMovement playerMovement = null;
}

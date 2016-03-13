using UnityEngine;
using System.Collections;

public class PortalCameraMimic : MonoBehaviour
{
	public bool DebugEnabled = false;

	public void Start()
	{
	}
	
	public void Update()
	{
		Camera mainCamera = Camera.main;

		transform.localPosition = mainCamera.transform.localPosition;
		transform.localRotation = mainCamera.transform.localRotation;
	}
}

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PortalImageEffectRenderer : MonoBehaviour
{
	public bool DebugEnabled = false;
	
	public Material portalMaterial = null;

	public void Awake()
	{
		leftEyeCamera = GameObject.FindGameObjectWithTag("Portal - Left Eye").GetComponent<Camera>();
		rightEyeCamera = GameObject.FindGameObjectWithTag("Portal - Right Eye").GetComponent<Camera>();
	}

	public void Start()
	{
	}
	
	public void Update()
	{
	}
	
	public void OnPreRender()
	{
		portalMaterial.SetTexture("_LeftEyeTexture", leftEyeCamera.targetTexture);
		portalMaterial.SetTexture("_RightEyeTexture", rightEyeCamera.targetTexture);
	}
	
	public void OnRenderImage(
		RenderTexture sourceTexture, 
		RenderTexture destinationTexture)
	{
		// First make sure the destination is rendered.
		// BUG! Surely there's some way we can avoid this! Are we using the wrong event-handler?
		Graphics.Blit(sourceTexture, destinationTexture);

		// Apply the portal's view.
		Graphics.Blit(sourceTexture, destinationTexture, portalMaterial);
	}

	private Camera leftEyeCamera = null;
	private Camera rightEyeCamera = null;
}

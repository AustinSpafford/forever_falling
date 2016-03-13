using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PortalImageEffectRenderer : MonoBehaviour
{
	public bool DebugEnabled = false;
	
	public Material portalMaterial;

	public void Awake()
	{
	}

	public void Start()
	{
	}
	
	public void Update()
	{
	}
	
	void OnRenderImage(
		RenderTexture sourceTexture, 
		RenderTexture destinationTexture)
	{
		Graphics.Blit(sourceTexture, destinationTexture, portalMaterial);
	}
}

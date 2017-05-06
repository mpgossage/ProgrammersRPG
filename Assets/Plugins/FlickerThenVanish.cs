using UnityEngine;
using System.Collections;

public class FlickerThenVanish : MonoBehaviour {
	
	public float flickerTime=5;
	public float flickerRate=0.2f;
	public float vanishTime=10;
	public float variation=1.0f;
	
	SkinnedMeshRenderer meshRenderer;
	// Use this for initialization
	void Start () {
		
		meshRenderer=GetComponentInChildren<SkinnedMeshRenderer>();
		float v=Random.Range(-variation/2,variation/2);
		InvokeRepeating("Flicker",flickerTime+v,flickerRate);
		Destroy(gameObject,vanishTime+v);		
	}
	
	void Flicker()
	{
		if (renderer!=null)
			renderer.enabled=!renderer.enabled;
		if (meshRenderer!=null)
			meshRenderer.enabled=!meshRenderer.enabled;
			
	}
}

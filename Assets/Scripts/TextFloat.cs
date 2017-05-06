using UnityEngine;
using System.Collections;

public class TextFloat : MonoBehaviour {
	
	public float lifeTime=4;
	public Vector3 velocity=Vector3.up;
	public bool alphaFade=true;
	public Color baseColor;
	float age=0;
	// Use this for initialization
	void Start () {
		baseColor=GetComponent<Renderer>().material.color;
	
	}
	
	// Update is called once per frame
	void Update () {
		age+=Time.deltaTime;
		if (age>=lifeTime)
		{
			Destroy(gameObject);
			return;
		}
		transform.position=transform.position+velocity*Time.deltaTime;
		
		if (alphaFade)
		{
			Color col=baseColor;
			col.a*=1-(age/lifeTime);
			GetComponent<Renderer>().material.color=col;
		}
	}
}

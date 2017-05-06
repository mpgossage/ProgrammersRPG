using UnityEngine;
using System.Collections;

// like the face camera script, but this looks for a object tagged 'Player' and looks at it
public class FacePlayer : MonoBehaviour {
	
	public string TagToFace="Player";
	public bool IgnoreYAxis=false;
	Transform thingToFace;
	// Use this for initialization
	void Start () 
	{
		thingToFace=GameObject.FindGameObjectWithTag(TagToFace).transform;	
	}
	
	void LateUpdate () 
	{
		Vector3 target=thingToFace.position;
		if (IgnoreYAxis)
			target.y=transform.position.y;
        transform.LookAt(target,Vector3.up);
	}
}

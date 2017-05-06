using UnityEngine;
using System.Collections;

public class BasicCamera : MonoBehaviour {

    public Transform lootAt;
    public Transform desiredPos;
    public float smooth = 6;

	
	// Update is called once per frame
	void LateUpdate () 
    {
        transform.position = Vector3.Lerp(transform.position, desiredPos.position, smooth * Time.deltaTime);
        transform.LookAt(lootAt);
	
	}
}

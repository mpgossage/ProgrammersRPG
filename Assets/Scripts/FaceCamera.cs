using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

	public bool ignoreYAxis=false;
	// Update is called once per frame
	void LateUpdate () 
	{
        // keep aligned to camera
        // from http://unifycommunity.com/wiki/index.php?title=CameraFacingBillboard
        // modified to use Vector3.forward rather than Vector3.back
        //transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
        //        Camera.main.transform.rotation * Vector3.up);
	
		Vector3 forward=Camera.main.transform.rotation * Vector3.forward;
		if (ignoreYAxis)
			forward.y=0;
        transform.LookAt(transform.position + forward,
                Camera.main.transform.rotation * Vector3.up);
	}
}

using UnityEngine;
using System.Collections;

public class LookWheel : MonoBehaviour {

    public float minDist = -1;
    public float maxDist = -10;

    float val = 0.5f;
	
	// Update is called once per frame
	void Update () 
    {
        float v=Input.GetAxis("Mouse ScrollWheel");
        val = Mathf.Clamp01(val + v);

        Vector3 pos = transform.localPosition;
        pos.z = Mathf.Lerp(minDist, maxDist, val);
        transform.localPosition = pos;
	}
}

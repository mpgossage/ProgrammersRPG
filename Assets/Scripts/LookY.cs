using UnityEngine;
using System.Collections;

public class LookY : MonoBehaviour {

    public float sensitivity = 5.0f;
    public float minY = -20, maxY = 80;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 rot = transform.localEulerAngles;
        rot.x -= Input.GetAxis("Mouse Y") * sensitivity;    // rotate
        if (rot.x > 180) rot.x -= 360;  // deal with overrotation
        rot.x = Mathf.Clamp(rot.x, minY, maxY); // limit
        transform.localEulerAngles = rot;
    }
}

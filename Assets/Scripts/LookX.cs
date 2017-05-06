using UnityEngine;
using System.Collections;

public class LookX : MonoBehaviour {

    public float sensitivity = 5.0f;
    float mouseX = 0.0f;

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");

        Vector3 rot = transform.localEulerAngles;
        rot.y += mouseX * sensitivity;
        transform.localEulerAngles = rot;
    }
}

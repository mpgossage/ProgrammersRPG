using UnityEngine;
using System.Collections;

/// <summary>
/// Lock cursor: stops cursor from appearing
/// </summary>
public class LockCursor : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
        //Screen.lockCursor = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //    Screen.lockCursor = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            //Screen.lockCursor = true;        
        }
    }
	void OnDestroy()
	{
		//Screen.lockCursor = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}


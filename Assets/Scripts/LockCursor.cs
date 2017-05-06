using UnityEngine;
using System.Collections;

/// <summary>
/// Lock cursor: stops cursor from appearing
/// </summary>
public class LockCursor : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Screen.lockCursor = false;
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
			Screen.lockCursor = true;
	}
	void OnDestroy()
	{
		Screen.lockCursor = false;
	}
}

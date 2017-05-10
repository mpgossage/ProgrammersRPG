using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// very simple toggle fullscreen for webgl
public class ToggleFullscreen : MonoBehaviour
{
    [SerializeField]
    private KeyCode toggleKey;
	
	// Update is called once per frame
	void Update ()
    {
#if UNITY_WEBGL
        if (Input.GetKeyDown(toggleKey))
            Screen.fullScreen = !Screen.fullScreen;
#endif       
    }
}

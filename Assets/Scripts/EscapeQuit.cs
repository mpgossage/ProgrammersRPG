using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EscapeQuit : MonoBehaviour 
{
	public string exitScene=null;
	public float clickRate=1.0f;
	
	float lastClick=-1;

	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			float timeSinceLastClick=Time.time-lastClick;
			if (timeSinceLastClick<=clickRate)	// its a fast click
			{
                SceneManager.LoadScene(exitScene);
			}
			else
			{
				MessageDisplay.Show("Press <Escape> twice to exit");
				lastClick=Time.time;	// remember the last click
			}
		}	
	}
}

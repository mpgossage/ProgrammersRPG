using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
	public Texture2D background;
	public GUISkin skin;
	public AudioClip selectClip,cancelClip;
    [SerializeField]
    private Texture2D btnFullScreen;

    private AudioSource audioSource;
	string nextScene;

	
	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void OnGUI() 
	{
		GUISkin oldSkin=GUI.skin;
		GUI.skin=skin;
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),background);
	
		if (GUI.Button(new Rect(50,Screen.height*0.5f,Screen.width/4,Screen.height*0.1f),"Play"))
		{
            audioSource.clip=selectClip;
            audioSource.Play();
			Transition.FadeIn(Transition.Black,1);
			nextScene="scene1";
			Invoke("GoNextScene",1.5f);	// call next scene soon
		}
		if (GUI.Button(new Rect(50,Screen.height*0.65f,Screen.width/4,Screen.height*0.1f),"Credits"))
		{
            audioSource.clip=selectClip;
            audioSource.Play();
			Transition.FadeIn(Transition.Black,1);
			nextScene="credits";
			Invoke("GoNextScene",1.5f);	// call next scene soon
		}
#if UNITY_STANDALONE // if its an exe
		if (GUI.Button(new Rect(50,Screen.height*0.8f,Screen.width/4,Screen.height*0.1f),"Exit"))
		{
			audioSource.clip=cancelClip;
			audioSource.Play();
			Transition.FadeIn(Transition.Black,1);
			nextScene=null;	// quit
			Invoke("GoNextScene",1.5f);	// call next scene soon
		}
#else
        if (GUI.Button(new Rect(50,Screen.height*0.8f,Screen.width/4,Screen.height*0.1f),"Website"))
		{
            audioSource.clip=cancelClip;
            audioSource.Play();
			Transition.FadeIn(Transition.Black,1);
			Invoke("GotoWebsite",1.5f);	// call website
		}
#endif
#if UNITY_WEBGL // if its an webgl
		if (GUI.Button(new Rect(Screen.width-20- btnFullScreen.width*2,Screen.height-40-btnFullScreen.height*2, 
                        btnFullScreen.width*2,btnFullScreen.height*2),btnFullScreen))
		{
            Screen.fullScreen = !Screen.fullScreen;
		}
#endif


        GUI.skin=oldSkin;
	}
	
	void GotoWebsite()
	{
		Application.OpenURL("http://codethegame.blogspot.com/2013/09/the-programmers-rpg.html");
	}
	
	void GoNextScene()
	{
        if (nextScene == null)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
	}
}

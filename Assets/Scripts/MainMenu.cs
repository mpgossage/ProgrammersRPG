﻿using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public Texture2D background;
	public GUISkin skin;
	public AudioClip selectClip,cancelClip;
	
	string nextScene;

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI() 
	{
		GUISkin oldSkin=GUI.skin;
		GUI.skin=skin;
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),background);
	
		if (GUI.Button(new Rect(50,Screen.height*0.5f,Screen.width/4,Screen.height*0.1f),"Play"))
		{
			audio.clip=selectClip;
			audio.Play();
			Transition.FadeIn(Transition.Black,1);
			nextScene="scene1";
			Invoke("GoNextScene",1.5f);	// call next scene soon
		}
		if (GUI.Button(new Rect(50,Screen.height*0.65f,Screen.width/4,Screen.height*0.1f),"Credits"))
		{
			audio.clip=selectClip;
			audio.Play();
			Transition.FadeIn(Transition.Black,1);
			nextScene="credits";
			Invoke("GoNextScene",1.5f);	// call next scene soon
		}
#if UNITY_STANDALONE // if its an exe
		if (GUI.Button(new Rect(50,Screen.height*0.8f,Screen.width/4,Screen.height*0.1f),"Exit"))
		{
			audio.clip=cancelClip;
			audio.Play();
			Transition.FadeIn(Transition.Black,1);
			nextScene=null;	// quit
			Invoke("GoNextScene",1.5f);	// call next scene soon
		}
#else
		if (GUI.Button(new Rect(50,Screen.height*0.8f,Screen.width/4,Screen.height*0.1f),"Website"))
		{
			audio.clip=cancelClip;
			audio.Play();
			Transition.FadeIn(Transition.Black,1);
			Invoke("GotoWebsite",1.5f);	// call website
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
		if (nextScene==null)
			Application.Quit();
		else
			Application.LoadLevel(nextScene);
	}
}
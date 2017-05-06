using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour {
	
	public AudioClip music;
	// Use this for initialization
	void Start () {
		MusicManager.Instance.StartMusic(music);
		Destroy(this);	
	}
}

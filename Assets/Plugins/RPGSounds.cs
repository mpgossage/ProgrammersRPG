using UnityEngine;
using System.Collections;

public class RPGSounds : MonoBehaviour {
	public AudioClip mainMusic,fightMusic;
	public AudioClip[] combatSounds;
	public AudioClip playerDieClip,teleportClip;
	
	void Start()
	{
		EndCombat();
	}

	
	public void PlayHitSound()
	{
		PlayOneShot(combatSounds[Random.Range(0,combatSounds.Length)]);
	}
	public void BeginCombat()
	{
		MusicManager.Instance.FadeMusic(fightMusic,1);
	}
	public void EndCombat()
	{
		MusicManager.Instance.FadeMusic(mainMusic,1);
	}
	
	public void PlayOneShot(AudioClip clip)
	{
		// ignore pos
		audio.PlayOneShot(clip);
	}
	
	
	#region Singleton
	static RPGSounds instance;
	public static RPGSounds Instance{get{return instance;}}
	void Awake()
	{
		instance=this;
	}	
	#endregion
}

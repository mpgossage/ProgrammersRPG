using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class RPGSounds : MonoBehaviour {
	public AudioClip mainMusic,fightMusic;
	public AudioClip[] combatSounds;
	public AudioClip playerDieClip,teleportClip;
    private AudioSource audioSource;

	void Start()
	{
        audioSource = GetComponent<AudioSource>();

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
        audioSource.PlayOneShot(clip);
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

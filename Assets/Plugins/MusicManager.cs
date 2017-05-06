using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour 
{
	float transitionTime=0,transitionDuration=0;
	AudioSource source,source2;
	
	// Update is called once per frame
	void Update () 
	{
		if (transitionTime>=transitionDuration)
			return;	// no work
		if (source2.clip==null)
			return;	// no work
		transitionTime+=Time.deltaTime;
		float propotion=Mathf.Clamp01(transitionTime/transitionDuration);
		source2.volume=propotion;
		source.volume=1-propotion;
		if (propotion>=1)	// trnasition complete
		{
			// stop old source:
			source.Stop();
			source.clip=null;			
			// swap sources:
			AudioSource temp=source;
			source=source2;
			source2=temp;
		}	
	}
	public void StartMusic(AudioClip clip)
	{
		FadeMusic(clip,5.0f);
	}
	
	public void FadeMusic(AudioClip clip, float duration)
	{
		if (source.clip==clip) return;
		if (source.clip==null)
		{
			source.clip=clip;
			source.volume=1;
			source.Play();
			source.loop=true;
		}
		else
		{
			transitionDuration=duration;
			transitionTime=0;	//reset timer
			source2.clip=clip;
			source2.volume=0;
			source2.Play();
			source2.loop=true;
		}
	}


	
	#region Singleton Code
	private static MusicManager instance;

	public static MusicManager Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject obj=new GameObject("MusicManager");
				instance = obj.AddComponent<MusicManager>();
				instance.source=obj.AddComponent<AudioSource>();
				instance.source2=obj.AddComponent<AudioSource>();
				GameObject.DontDestroyOnLoad(instance);	// don't get destroyed in a level loading
			}
			return instance;
		}
	}

	public void OnApplicationQuit ()
	{
		instance = null;
	}
	#endregion	
}

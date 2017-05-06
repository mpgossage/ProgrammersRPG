using UnityEngine;
using System.Collections;

public class GolemChase : MonoBehaviour {

	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag=="Player")
		{
			Golem.chaseTarget=other.transform;
			if (GameObject.FindGameObjectsWithTag("Monster")!=null)
				RPGSounds.Instance.BeginCombat();
		}
	}
				
	void OnTriggerExit(Collider other)
	{
		if (other.tag=="Player")
		{
			Golem.chaseTarget=null;
			if (GameObject.FindGameObjectsWithTag("Monster")!=null)
				RPGSounds.Instance.EndCombat();
		}
	}
}

using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {


	
	void OnTriggerEnter(Collider collid)
	{
		//Debug.Log("sword hit:"+collid.name);
		if (collid.gameObject.tag=="Monster")
		{
			Debug.Log("sword hit");
			Golem golem=collid.gameObject.GetComponent<Golem>();
			golem.Damage(20);
		}
	}
}

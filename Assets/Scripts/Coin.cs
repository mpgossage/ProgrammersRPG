using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	
	float flySpeed=10;
	bool pickable=false;
	Transform target=null;
	public AudioClip sound;
	// Use this for initialization
	void Start () {        
		Invoke("Pickable",2.0f);
	}
	
	void Update()
	{	
		if (target==null)	return;
		
		Vector3 PLAYER_OFFSET=Vector3.up;	// offset to deal with players centre being in the floor
		Vector3 delta=(target.position-transform.position+PLAYER_OFFSET);
		
		// if there
		if (delta.magnitude<flySpeed*Time.deltaTime)
		{
			// we have to use a PlayOneShot, as this object will be destroyed & we cannot hear the sound
			RPGSounds.Instance.PlayOneShot(sound);
			Destroy(gameObject);
		}
		// else move		
		delta.Normalize();
		transform.position=transform.position+delta*flySpeed*Time.deltaTime;
	}
	
	void Pickable()
	{
		pickable=true;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag=="Player" && pickable)
		{
			target=collider.transform;	// home in on the player
			gameObject.collider.enabled=false;	// turn off collisions for coin			
			Destroy(rigidbody);
		}
	}
}

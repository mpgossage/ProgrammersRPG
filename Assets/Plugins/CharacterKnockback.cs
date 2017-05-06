using UnityEngine;
using System.Collections;

public class CharacterKnockback : MonoBehaviour {

    public float gravity = 10;
    CharacterController character;
    Vector3 velocity = Vector3.zero;
    bool knocked = false;
    public bool IsKnocked{get{return knocked;}}

    // Use this for initialization
	void Start () 
    {
        character = GetComponent<CharacterController>();	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!knocked) return;
        character.Move(velocity * Time.deltaTime);
        if (character.isGrounded)
        {
            if (Mathf.Abs(velocity.y) > 1)
            {
                velocity *= 0.5f;   // 50% all movement
                velocity.y = -velocity.y;   // invert the y
            }
            else
            {
                // cancel knockback
                knocked = false;
                velocity = Vector3.zero;    // small movement become no movement
            }
        }
        else
            velocity.y -= gravity * Time.deltaTime;

    }

    public void Knockback(Vector3 velocity)
    {
        this.velocity = velocity;
		transform.position+=velocity*Time.deltaTime*5;	// move it a little bit back
        knocked = true;
    }
	public void Knockback(Vector3 attackPos,Vector3 myPos,float scale)
	{
		Vector3 direction=myPos-attackPos;
		direction.y=0;
		direction.Normalize();
		direction.y=+0.6f;	// up a little
		Knockback(direction*scale);
		//Debug.Log ("Knockback"+direction);
	}
}

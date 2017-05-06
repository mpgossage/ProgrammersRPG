using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class BasicMove : MonoBehaviour {

    public float moveSpeed = 5;
    public float jumpSpeed = 10;
    public float gravity = 5;
	public float runMultiplier=3;
	
	CharacterKnockback knockback;
    CharacterController character;
    float yVel = 0;
	bool running=false,/*jumping=false,*/skillAttack=false;
	int attack=-1;
	bool attackFlag=false;
	// animation stuff:
	//string currentState="Idle";
	public AnimationClip idleClip,walkClip,backClip,runClip,jumpClip,skillClip;
	public AnimationClip deathClip;
	public AnimationClip[] attackClips;
	public AudioClip[] footstepAudios;
	
	AudioSource audio2;
	
	// Use this for initialization
	void Start () 
    {
        character = GetComponent<CharacterController>();	
		knockback=GetComponent<CharacterKnockback>();
		//RpgDialog.Show("Greetings mighty warrior",null,null);
		audio2=gameObject.AddComponent<AudioSource>();	// second clip!!!
	}

    // Update is called once per frame
    void Update()
    {
		if (knockback.IsKnocked)	return;
		if (RpgDialog.Visible)
		{
			GetComponent<Animation>().CrossFade(idleClip.name);
			GetComponent<Animation>().wrapMode=WrapMode.Once;
			character.Move(Vector3.down * Time.deltaTime);	// move down rather than allow motion
			return;			
		}
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direction * moveSpeed;
		if (Input.GetKey(KeyCode.LeftShift))	// sprint	
			running=true;
		if (direction==Vector3.zero)
			running=false;
		
		if (running)
			velocity*=runMultiplier;

		// always add gravity: even if on the ground, it keeps the guy there
        yVel -= gravity;    // move player down
        if (character.isGrounded)
        {
			//jumping=false;
            if (Input.GetButtonDown("Jump"))
            {
                yVel = jumpSpeed;   // going up
				//jumping=true;
            }
        }

        velocity.y = yVel;
        velocity = transform.TransformDirection(velocity);

        character.Move(velocity * Time.deltaTime);
		
		
		// update animation:
		AnimationClip goal=idleClip;
		
		if (direction.z>0.1f || Mathf.Abs(direction.x)>0.1f)
		{
			if (running)	// sprint	
				goal=runClip;
			else
				goal=walkClip;
		}
		if (direction.z<-0.1f)
			goal=backClip;
		
		// attacks:
		if (attack==-1 && skillAttack==false)
		{
			if (Input.GetMouseButton(0))
				attack=Random.Range(0,attackClips.Length);
			else if (Input.GetMouseButton(1))
				skillAttack=true;
		}
		if (attack>=0)
		{
			// check if animation finished (or within 0.1)
			AnimationState anim=GetComponent<Animation>()[attackClips[attack].name];
			if (anim.time/anim.length>0.5f && anim.time/anim.length<0.6f)
			{
				if (attackFlag==false)
					DoAttack(10);
				attackFlag=true;
			}
			else
				attackFlag=false;
			
			if (anim.time > anim.length-0.1f)
			{
				attack=-1;
			}
			if (attack>=0)
				goal=attackClips[attack];
		}
		if (skillAttack)
		{
			// check if animation finished (or within 0.1)
			AnimationState anim=GetComponent<Animation>()[skillClip.name];
			if (anim.time/anim.length>0.5f && anim.time/anim.length<0.6f)
			{
				if (attackFlag==false)
					DoAttack(20);
				attackFlag=true;
			}
			else
				attackFlag=false;

			if (anim.time > anim.length-0.1f)
			{
				skillAttack=false;
			}
			if (skillAttack)				
				goal=skillClip;
		}
		
		GetComponent<Animation>().CrossFade(goal.name);
		GetComponent<Animation>().wrapMode=WrapMode.Once;
		
		if (goal==walkClip || goal==runClip || goal==backClip)
		{
			// only start if a clip is stopped & player on the ground
			if (GetComponent<AudioSource>().isPlaying==false && character.isGrounded)
			{
				PlayFootstep(running);
			}
		}
    }
	
	void PlayFootstep()
	{
		PlayFootstep(false);
	}
	void PlayFootstep(bool running)
	{
		GetComponent<AudioSource>().clip=footstepAudios[Random.Range(0,footstepAudios.Length)];
		GetComponent<AudioSource>().Play();
		if (running)	// running use audio2
		{
			float delay=GetComponent<AudioSource>().clip.length/2;
			audio2.clip=footstepAudios[Random.Range(0,footstepAudios.Length)];
			audio2.PlayDelayed(delay);
		}
	}
	
	void DoAttack(int dmg)
	{
		const float ATTACK_RANGE=5;
		// yuck: i need to check all bad guys!
		GameObject[] enemy=GameObject.FindGameObjectsWithTag("Monster");
		for(int i=0;i<enemy.Length;i++)
		{
			if (Vector3.Distance(transform.position,enemy[i].transform.position)<ATTACK_RANGE)
			{
				RPGSounds.Instance.PlayHitSound();
				Golem g=enemy[i].GetComponent<Golem>();
				if (g!=null)
					g.Damage(dmg);
				CharacterKnockback kb=enemy[i].GetComponent<CharacterKnockback>();
				if (kb!=null)
				{
					kb.Knockback(transform.position,enemy[i].transform.position,5);
				}
			}
		}
	}
	
	public void Injure(int dmg,GameObject who)
	{
		RPG.InjurePlayer(dmg);
		RPG.FloatingTextAbove(transform,dmg.ToString(),Color.red,2);
		RPGSounds.Instance.PlayHitSound();
		if (who!=null)
		{
			knockback.Knockback(who.transform.position,gameObject.transform.position,10);
			/*Vector3 direction=(transform.position-who.transform.position);
			direction.y=0;
			direction.Normalize();
			direction.y=+1;
			knockback.Knockback(direction*5);
			Debug.Log("player kb:"+direction);*/
		}
	}
	
	
}

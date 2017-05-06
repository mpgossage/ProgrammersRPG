using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Golem : MonoBehaviour {
	
	CharacterController controller;
	CharacterKnockback knock;
	DisplayBar3D health;
	
	public static Transform chaseTarget=null;
	public float walkSpeed=2,runSpeed=4;
	public AnimationClip idleClip,walkClip,runClip,attackClip;
	float attackRange=3;
	
	bool attackFlag=false;
	public GameObject toGuard;
	Vector3 patrolPoint;
	float patrolDistance=100;
		
	enum Task{Patrol,Chase,Wait};
	Task current=Task.Wait;
	float taskCounter=1;
	
	public int maxHP=50;
	int hp;
	
	// Use this for initialization
	void Start () {
		health=gameObject.GetComponent<DisplayBar3D>();
		controller=GetComponent<CharacterController>();
		knock=GetComponent<CharacterKnockback>();
		hp=maxHP;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (hp<=0)	return;
		if (knock.IsKnocked) return;
		
		taskCounter-=Time.deltaTime;
		if (taskCounter<=0)
		{
			taskCounter=Random.Range(5.0f,10.0f);
			if (Random.value<0.5f)
				current=Task.Wait;
			else
			{
				current=Task.Patrol;
				patrolPoint=toGuard.transform.position+new Vector3(Random.Range(-patrolDistance,patrolDistance),0,Random.Range(-patrolDistance,patrolDistance));
			}
		}
		
		if (chaseTarget!=null)
		{
			patrolPoint=chaseTarget.position;
			if (Vector3.Distance(transform.position,chaseTarget.position)<attackRange)
			{
				MoveTowards(patrolPoint,0);	// turn to it
				animation.CrossFade(attackClip.name); //attack		
				// player damaged: (need attackFlag to stop multitrigger) 
				AnimationState state=animation[attackClip.name];
				if (state.time>state.length-0.1f)
				{
					if (attackFlag==false)
					{
						BasicMove bm=chaseTarget.GetComponent<BasicMove>();
						bm.Injure(10,gameObject);
					}
					attackFlag=true;
				}
				else
				{
					attackFlag=false;	
				}
			}
			else
			{
				MoveTowards(patrolPoint,runSpeed);
				animation.CrossFade(runClip.name);			
			}
		}
		else
		{
			if (current==Task.Wait)
			{
				animation.CrossFade(idleClip.name);			
			}
			if (current==Task.Patrol)
			{
				MoveTowards(patrolPoint,walkSpeed);
				animation.CrossFade(walkClip.name);			
			}
		}
	}
	
	void MoveTowards(Vector3 pos,float speed)
	{
		Quaternion rot=transform.rotation;
		Vector3 direction=pos-transform.position;
		direction.y=0;
		Quaternion desired=Quaternion.LookRotation(direction);
		transform.rotation=Quaternion.Slerp(rot,desired,Time.deltaTime);
		controller.SimpleMove(transform.forward*speed);
	}
	
	public void Damage(int amount)
	{
		if (hp<=0)	return;
		RPG.FloatingTextAbove(transform,amount.ToString(),Color.yellow,4);
		hp-=amount;
		//health.enabled=true;	// turn on the healthbar
		health.Value=(float)hp/maxHP;
		if (hp<=0)
		{
			animation.CrossFade("death");
			//Destroy(health,3);
			Destroy(knock,4);
			gameObject.AddComponent<FlickerThenVanish>();	// will get rid of it
		}
	}
}

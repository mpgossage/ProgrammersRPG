using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPG : MonoBehaviour
{
    public static bool CutScene = false;
    public static Dictionary<string, string> Flags = new Dictionary<string, string>();
    public static int Gold = 0;

    public List<GameObject> spawnables = new List<GameObject>();
	public Texture2D loseTex;
	public bool shortDialog=false;

    //GameObject player;
    //BasicMove playerMove;
	
	public static int playerHp=100,playerMaxHp=100;
	static RPG instance;
	public static RPG Instance{get{return instance;}}

    //public string startQuest = "";
    //public GameObject textFloatPrefab;
	
	DisplayBar hpBar;
	
	void Awake()
	{
		instance=this;
	}
	
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //playerMove = player.GetComponent<BasicMove>();
		hpBar=GetComponent<DisplayBar>();
		hpBar.BarRectangle.width=Screen.width/5;
		hpBar.BarRectangle.height=Screen.height/10;
		Rect r=hpBar.BarRectangle;
		hpBar.BarRectangle.x=Screen.width-5-r.width;
		hpBar.BarRectangle.y=5;
    }
    void Update()
    {
		hpBar.Value=(float)playerHp/playerMaxHp;
        /*if (RpgDialog.Visible)
            playerMove.enabled = false;
        else
            playerMove.enabled = true;*/
    }

    public GameObject Spawn(string name, string location)
    {
        // do a lookup
        GameObject loc = GameObject.Find(location);
        if (loc == null)
        {
            Debug.LogError("RPG.Spawn: No such location:" + location);
            return null;
        }
        return Spawn(name,loc.transform.position);
    }

	public GameObject Spawn(string name, Vector3 location)
    {
        // do a lookup
        GameObject toSpawn = null;
        foreach (GameObject o in spawnables)
        {
            if (o.name == name)
                toSpawn = o;
        }
        if (toSpawn == null)
        {
            Debug.LogError("RPG.Spawn: No such spawnable:" + name);
            return null;
        }
        return (GameObject)Instantiate(toSpawn, location, Quaternion.identity);
    }
	
	public static void ResetAll()
	{
		playerHp=playerMaxHp;
		Flags.Clear();		
	}
    public static bool GetBool(string name)
    {
        if (Flags.ContainsKey(name) && Flags[name] == "True")
            return true;
        return false;
    }
    public static void SetBool(string name, bool val)
    {
        SetFlag(name, val.ToString());
    }
    public static void SetFlag(string name, string val)
    {
		if (Flags.ContainsKey(name))
			Flags[name]=val;
		else
        	Flags.Add(name, val);
    }
	
	public static TextMesh FloatingText(Vector3 pos,string txt,Color colour)
	{
		GameObject obj=instance.Spawn("Text3D",pos);
		TextMesh txtMsh=obj.GetComponent<TextMesh>();
		txtMsh.text=txt;
		txtMsh.color=colour;
		return txtMsh;
	}
	public static TextMesh FloatingTextAbove(Transform tgt,string txt,Color colour,float height)
	{
		TextMesh txtMsh=FloatingText(tgt.position+Vector3.up*height,txt,colour);
		txtMsh.transform.parent=tgt;	// reparent it
		return txtMsh;
	}
	
	
	public static void HealPlayer()
	{
		playerHp=playerMaxHp;
	}

	public static void InjurePlayer(int amount)
	{
		if (playerHp<=0)	return;
		Debug.Log("player hurt:"+amount);
		playerHp-=amount;
		if (playerHp<=0)
		{
			KillPlayer("You ran out of HP's");
		}
	}
	
	public static void KillPlayer(string reason)
	{
		MessageDisplay.Show(reason);
	
		instance.StartCoroutine(PlayerDie());
	}	
	static IEnumerator PlayerDie()
	{
		// play sound
		RPGSounds.Instance.PlayOneShot(RPGSounds.Instance.playerDieClip);
		// get player:
		GameObject player=GameObject.FindWithTag("Player");
		BasicMove move=player.GetComponent<BasicMove>();
		move.enabled=false;	// turn off user control
		CharacterKnockback knock=player.GetComponent<CharacterKnockback>();
		knock.enabled=false;	// turn off knockback too
		player.animation.CrossFade("Death");
        yield return new WaitForSeconds(player.animation["Death"].length);  // wait for death
		// run a transition
        Transition.FadeIn(instance.loseTex, 1);
        yield return new WaitForSeconds(1.5f);  // 1 second on, then 1/2 way though the wait
		// reset animation:
		player.animation.Play("Idle");
        // mid transition: respawn
        RespawnPoint.RespawnPlayer();
		playerHp=playerMaxHp;
		move.enabled=true;	// turn on user control
		knock.enabled=true;	// turn on knockback too
    /*
 see http://unitygems.com/coroutines/
     * http://www.blog.silentkraken.com/2010/01/23/coroutines-in-unity3d-c-version/
     * http://www.altdevblogaday.com/2011/07/07/unity3d-coroutines-in-detail/
IEnumerator Die()
{
       //Wait for the die animation to be 50% complete
       yield return StartCoroutine(WaitForAnimation("die",0.5f, true));
       //Drop the enemies on dying pickup
       DropPickupItem();
       //Wait for the animation to complete
       yield return StartCoroutine(WaitForAnimation("die",1f, false));
       Destroy(gameObject);
 
}*/
	}

}	


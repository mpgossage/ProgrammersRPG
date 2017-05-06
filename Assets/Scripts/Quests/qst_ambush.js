#pragma strict

function Start () 
{
	// Clear messages
	MessageDisplay.Clear();
	// remove arrow 
    TargetArrow.SetTarget(null);

	var portrait:Texture2D=Resources.Load("portrait") as Texture2D;
	var portmon:Texture2D=Resources.Load("port_mon") as Texture2D;
	
	yield RpgDialog.ShowCo("Well this seems quite straight forward\nJust a long walk, follow the arrow and...",portrait,null);
	var monsters = SpawnMonsters();	// instant, so don't need the yield
	if (!RPG.Instance.shortDialog)
	{
		yield RpgDialog.ShowCo("<Roars!!!>",null,portmon);
		yield RpgDialog.ShowCo("Oh...\nI seem to be surrounded...",portrait,null);
		yield RpgDialog.ShowCo("RUN AWAY!!!!!",portrait,null);
	}

	yield FightMonsters(monsters);
	yield RpgDialog.ShowCo("Ok, well that seems to be the lot of them",portrait,null);
	
	var girl:GameObject=RPG.Instance.Spawn("BladeGirl",GameObject.Find("GirlSpawn").transform.position);	
	yield RpgDialog.ShowCo("Hey, who's that over there?",portrait,null);
	yield RpgDialog.ShowCo("Lets go and talk to them",portrait,null);
	TargetArrow.SetTarget(girl);
	Destroy(this);	// remove it
}

function SpawnMonsters()
{
    var player:GameObject=GameObject.FindGameObjectWithTag("Player");
    var rpg:RPG = GameObject.FindGameObjectWithTag("GameController").GetComponent(RPG);

	// golems will chase player when activated:
   	Golem.chaseTarget=player.transform;

     
    var monsters = new Array();
    var numMonster=8;
    for (var i = 0; i < numMonster; i++)
    {
        var rot:Quaternion = Quaternion.Euler(0, 360.0f * i / numMonster, 0);
        var delta:Vector3 = Vector3(0, 0, 8);
        var pos:Vector3 = player.transform.position + rot * delta;
        var obj:GameObject = rpg.Spawn("Monster",pos);
        obj.transform.LookAt(player.transform);
        var g:Golem=obj.GetComponent("Golem") as Golem;
        g.toGuard=gameObject;	// guard this thing
        g.enabled=false; // dont act yet
        obj.GetComponent.<Animation>().CrossFade(g.idleClip.name);	// idle animation
        monsters.Push(obj);
    }
    return monsters;
}

function FightMonsters(monsters:Array)
{
	// activate all monsters:
    for (var i = 0; i < monsters.Count; i++)
    {
    	var obj:GameObject=monsters[i] as GameObject;
    	obj.GetComponent(Golem).enabled=true;
    }
    
    RPGSounds.Instance.BeginCombat();
	MessageDisplay.Show("You had better fight your way out of this one");
	MessageDisplay.Show("Somehow...");
	yield WaitForSeconds(5.0);
	MessageDisplay.Show("Then show there trolls who is boss by slaying them all");
	
	
	while(monsters.Count>0)
	{
        for(i=0;i<monsters.Count;i++)
        {
            if (monsters[i]==null)  // it appears you can compare with null to detect
            {
                //Debug.Log("Monster " + i + " is not active");
                monsters.RemoveAt(i);
                break;
            }
        }
		yield 0;
	}
    RPGSounds.Instance.EndCombat();
}
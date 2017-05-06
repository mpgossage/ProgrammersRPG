#pragma strict

function Start () 
{
    // in JS: you don't need the StartCoroutine, only the yield
    // remove arrow 
    TargetArrow.SetTarget(null);
	var portrait:Texture2D=Resources.Load("portrait") as Texture2D;
	var oldport:Texture2D=Resources.Load("port_oldman") as Texture2D;

    // long text
    if (!RPG.Instance.shortDialog)
    {
	    yield RpgDialog.ShowCo("Greetings mighty warrior",null,oldport);
	    yield RpgDialog.ShowCo("Oh, hello",portrait,null);
	    yield RpgDialog.ShowCo("You have come, just as fortold",null,oldport);
	    yield RpgDialog.ShowCo("<Surprised> Really?",portrait,null);
	    yield RpgDialog.ShowCo("You wish to know the meaning of life\nthe universe\nand everything..",null,oldport);
	    yield RpgDialog.ShowCo("Not really...",portrait,null);
	    yield RpgDialog.ShowCo("You seek the location of the golden fleece..",null,oldport);
	    yield RpgDialog.ShowCo("<Shaking head> No ...",portrait,null);
	    yield RpgDialog.ShowCo("<Hopefully> You are looking for your lost kitten, miffy?",null,oldport);
	    yield RpgDialog.ShowCo("Excuse me?",portrait,null);
	    yield RpgDialog.ShowCo("...",null,oldport);
	    yield RpgDialog.ShowCo("I'm trying to find out where I am, I'm a bit lost",portrait,null);
	    yield RpgDialog.ShowCo("That is good, because I Zardwark the all-knowing can teach you all things",null,oldport);
	    yield RpgDialog.ShowCo("<Spluttering> All knowing! You don't even know why I was talking to you!",portrait,null);
	    yield RpgDialog.ShowCo("Quiet child, I will empower your sword with magical energies",null,oldport);
	    yield RpgDialog.ShowCo("<Sceptical> Really?",portrait,null);
	    yield RpgDialog.ShowCo("Yes, go and test in on that tree over there",null,oldport);
	    yield RpgDialog.ShowCo("<Looking about> What tree?",portrait,null);
	    yield RpgDialog.ShowCo("<Annoyed> Just go and test it",null,oldport);
	    yield RpgDialog.ShowCo("<Sly Grin>\nOK, I will go and 'test' it",portrait,null);
    }
    var oldman:GameObject=GameObject.Find("OldMan") as GameObject;
    //add arrow back
    TargetArrow.SetTarget(oldman);
    MessageDisplay.Show("Use the left mouse button to 'test' your sword on the old man");
    MessageDisplay.Show("The right mouse button is your big attack");
    yield HitOldMan(oldman);
    
    // remove arrow 
    TargetArrow.SetTarget(null);
    if (!RPG.Instance.shortDialog)
    {
	    yield RpgDialog.ShowCo("<Furious> What the?",null,oldport);
	    yield RpgDialog.ShowCo("<Grinning> Yes, my weapon is greatly empowered!\nYou truly are powerful..",portrait,null);
	    yield RpgDialog.ShowCo("Err...",null,oldport);
	    yield RpgDialog.ShowCo("Yes, excellent, just as I planned",null,oldport);
	    yield RpgDialog.ShowCo("Now, your final test is to cross the bridge and defeat the ice troll on the far side.",null,oldport);
	    yield RpgDialog.ShowCo("Ice troll?",portrait,null);
	    yield RpgDialog.ShowCo("Just follow the big red arrow",null,oldport);
	    TargetArrow.SetTarget(GameObject.Find("Chest"));
	    yield RpgDialog.ShowCo("Oh!\nYou can see that too?",portrait,null);
	    yield RpgDialog.ShowCo("Just get on with it!\nAnd bring back the key in the chest",null,oldport);
    }
    TargetArrow.SetTarget(GameObject.Find("Chest"));
    //GameObject.Find("bridge").SetActive(true);
    MessageDisplay.Show("You heard him");
    MessageDisplay.Show("Follow the arrow");
    RPG.SetBool("qst_oldman",true); // mark quest as done, so it triggers the next quest
    Destroy(this); // remove the quest
}

function HitOldMan(oldman:GameObject)
{
	oldman.tag="Monster";	// a monster tag makes it knockable
    // add behavior to allow knockback
    var knock: CharacterKnockback = oldman.AddComponent.<CharacterKnockback>() as CharacterKnockback;

	// wait for the oldman to be hit
    while (!knock.IsKnocked)    // if the person hasn't hit the man
        yield 0;
    // he has been hit:
    yield WaitForSeconds(2.0f);  // wait a while to ensure that the bouncing has stopped
    Destroy(knock);  // remove the knockback routine
	oldman.tag="Untagged";	// a normal tag makes it not knockable
}
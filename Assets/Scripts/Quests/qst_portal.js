#pragma strict


function Start () 
{
    // remove arrow 
    TargetArrow.SetTarget(null);
	var portrait:Texture2D=Resources.Load("portrait") as Texture2D;
	var oldport:Texture2D=Resources.Load("port_oldman") as Texture2D;

    // long text
	if (!RPG.Instance.shortDialog)
	{
	    yield RpgDialog.ShowCo("Hello there, who are you?",null,oldport);
	    yield RpgDialog.ShowCo("<Furious> WHAT?\nYou don't know who I am?",portrait,null);
	    yield RpgDialog.ShowCo("<Furious> You sent me to be nearly murdered by that Ice troll, and you DONT remember who I am?",portrait,null);
	    yield RpgDialog.ShowCo("<Nervously> Of course not, did you defeat the ice troll & get its treasure?",null,oldport);
	    yield RpgDialog.ShowCo("You asked me to get the key from its chest, but there wasn't one!",portrait,null);
	    yield RpgDialog.ShowCo("<Feigning surprise> There wasn't?",null,oldport);
	    yield RpgDialog.ShowCo("No, just these coins\n<Shows coins>",portrait,null);
	    yield RpgDialog.ShowCo("<Greedily> Give me the GOLD!",null,oldport);
	    yield RpgDialog.ShowCo("No! Why should I?",portrait,null);
	    yield RpgDialog.ShowCo("I, err...\nNeed them to complete my incarnation to send you to another dimension",null,oldport);
	    yield RpgDialog.ShowCo("So you can complete your quest",null,oldport);
	    yield RpgDialog.ShowCo("<Sceptical> Really?",portrait,null);
	    yield RpgDialog.ShowCo("<Indignant> You doubt the word of Zardwark the all-knowing?",null,oldport);
	    yield RpgDialog.ShowCo("Actually I do..",portrait,null);
	    yield RpgDialog.ShowCo("<Impatient> Just give them here",null,oldport);
	    yield RpgDialog.ShowCo("Ok\n<Gives coins>",portrait,null);
		yield RpgDialog.ShowCo("Let us depart",null,oldport);
	}
	RPGSounds.Instance.PlayOneShot(RPGSounds.Instance.teleportClip);
	var effect:Texture2D=Resources.Load("special_effect") as Texture2D;
	Transition.SlideIn(effect,2);
	yield WaitForSeconds(2.5);	// wait for mid transtion
	UnityEngine.SceneManagement.SceneManager.LoadScene("scene2");
}


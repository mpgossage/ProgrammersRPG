#pragma strict

function Start () {
	RPG.HealPlayer();
    // remove arrow 
    TargetArrow.SetTarget(null);
	var portrait:Texture2D=Resources.Load("portrait") as Texture2D;
	var oldport:Texture2D=Resources.Load("port_oldman") as Texture2D;

	while(!Transition.Finished)
		yield 0; // make sure transisiton is finished
	
	if (!RPG.Instance.shortDialog)
	{
	    yield RpgDialog.ShowCo("<Majestically> And we have arrived at our destination",null,oldport);
		yield RpgDialog.ShowCo("<Looks around> WHAT!!!",portrait,null);
	    yield RpgDialog.ShowCo("We are here\nAt our destination",null,oldport);
		yield RpgDialog.ShowCo("No we aren't!!!",portrait,null);
	    yield RpgDialog.ShowCo("I used my mighty powers to move us across time and space...",null,oldport);
		yield RpgDialog.ShowCo("But this is the SAME PLACE!!!",portrait,null);
	    yield RpgDialog.ShowCo("No it isn't...",null,oldport);
		yield RpgDialog.ShowCo("Yes IT IS!\nThey just changed the floor colour!!!",portrait,null);
	    yield RpgDialog.ShowCo("...",null,oldport);
		yield RpgDialog.ShowCo("<Ranting> And you, you are just a paper CUTOUT, just like those trees!!\nIt looks like it was drawn by a 10 year old!!!",portrait,null);
	    yield RpgDialog.ShowCo("<Indignant> I will have you know that Daniel is 11 years old...",null,oldport);
		yield RpgDialog.ShowCo("<Ranting> What is going on here?",portrait,null);
	    yield RpgDialog.ShowCo("<Patiently> Ok, I will try to explain this in simple terms, just calm down",null,oldport);
	    yield RpgDialog.ShowCo("Rather than work on both programming and art, the guy who built this game, decided to focus maximum effort on just one side. PROGRAMMING",null,oldport);
	    yield RpgDialog.ShowCo("Yes, the art is hopeless, its deliberate.",null,oldport);
	    yield RpgDialog.ShowCo("But in less than two days, this entire quest system was built.",null,oldport);
		yield RpgDialog.ShowCo("<Tentatively> Ok...",portrait,null);
	    yield RpgDialog.ShowCo("This long dialog we are going through is a single JavaScript quest routine which was written in less than 10 minutes.",null,oldport);
		yield RpgDialog.ShowCo("<Surprised> Really?\nYou can build a quest so quickly?",portrait,null);
	    yield RpgDialog.ShowCo("Thats the point of this game: to figure out how to build an RPG, by actually building one.",null,oldport);
	    yield RpgDialog.ShowCo("And adding in all the standard RPG features:\nFights, Quests, Conversations, an Arrow to direct the player, Coins spewing out of chests & flying to the player...",null,oldport);
	    yield RpgDialog.ShowCo("Now can we get back to the quest please?",null,oldport);
		yield RpgDialog.ShowCo("<Degrudgingly> Ok...\nBut you still look awful.",portrait,null);
	    yield RpgDialog.ShowCo("<Clears throat>",null,oldport);
	    yield RpgDialog.ShowCo("<Majestically> So go now to your final battle and fulfill your destiny!",null,oldport);
	}

    TargetArrow.SetTarget(GameObject.Find("MonsterSpawn"));
    
    MessageDisplay.Show("Ok, lets go for it...");
    yield WaitForSeconds(5.0f);
    MessageDisplay.Show("This seems a rather long walk");
    yield WaitForSeconds(3.0f);
    MessageDisplay.Show("Really could have done with a quest or two along the way");
    yield WaitForSeconds(3.0f);
    MessageDisplay.Show("This is a sure sign of poor time management");
    yield WaitForSeconds(3.0f);
    MessageDisplay.Show("Or the day job getting in the way of building a game");
}

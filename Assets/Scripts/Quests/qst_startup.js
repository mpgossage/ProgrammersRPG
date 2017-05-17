#pragma strict

function Start () {
	RPG.ResetAll();	// reset all flags for a new game

	TargetArrow.SetTarget(null);
	var portrait:Texture2D=Resources.Load("portrait") as Texture2D;
	if (!RPG.Instance.shortDialog)
	{
		yield RpgDialog.ShowCo("<Yawn>\nThat was a good nap",portrait,null);
		yield RpgDialog.ShowCo("<Looks Around>\nErr",portrait,null);
		yield RpgDialog.ShowCo("Where am I?",portrait,null);
		yield RpgDialog.ShowCo("I don't remember drinking *that* much beer",portrait,null);
		yield RpgDialog.ShowCo("I guess I have better look about",portrait,null);
	}
	yield DoWalkAbout();

	yield RpgDialog.ShowCo("Hey! I see someone over there, lets ask them",portrait,null);
    var oldman=RPG.Instance.Spawn("OldMan","OldManSpawn");
    oldman.name="OldMan";	// remove the 'clone' name
    TargetArrow.SetTarget(oldman);
    
    MessageDisplay.Show("Follow the arrow to the old man");
    MessageDisplay.Show("You can press the <shift> key to run");
}

function DoWalkAbout()
{
	MessageDisplay.Show("Press the WSAD keys to move about");
    var counter:float = 0;
    while (counter < 2)
    {
        if (Input.GetAxisRaw("Horizontal") != 0 ||
            Input.GetAxisRaw("Vertical") != 0)
        {
            counter+= Time.deltaTime;
        }
        yield 0; // check next cycle
    }
    MessageDisplay.Show("Use the mouse to look around");
    counter = 0;
    while (counter < 1)
    {
        if (Input.GetAxisRaw("Mouse X") != 0 ||
            Input.GetAxisRaw("Mouse Y") != 0)
        {
            counter += Time.deltaTime;
        }
        yield 0; // check next cycle
    }
    MessageDisplay.Show("Use the mouse wheel to move in and out");
    counter = 0;
    while (counter < 0.25f)
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            counter += Time.deltaTime;
        }
        yield 0; // check next cycle
    }        
}
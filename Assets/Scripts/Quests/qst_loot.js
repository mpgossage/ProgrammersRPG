#pragma strict

// this quest is executed when the user touches the chest
// it is executed at the same time as a different script which spawns all the coins
// the script tells how the hero discovers there is no key in the chest as the old man fortold
// and decides that he must return back to confront him
function Start () 
{
    // remove arrow 
    TargetArrow.SetTarget(null);
    
    // load the portrain of the player
    var portrait:Texture2D=Resources.Load("portrait") as Texture2D;

    yield WaitForSeconds(10.0);    // wait X seconds (for the coins to be collected/vanish)
    
    // turn off the combat music which would have started when the golem attacked
    RPGSounds.Instance.EndCombat();

    // this is a debugging flag which allows skip long dialogs is we wish
    if (!RPG.Instance.shortDialog)
    {
        // each of the calls displays a single text dialog and then waits for it to be completed
        yield RpgDialog.ShowCo("Thats strange, there was no key",portrait,null);
        yield RpgDialog.ShowCo("Lots of treasure, but no key",portrait,null);
        yield RpgDialog.ShowCo("Lets go and check with the old man again",portrait,null);
    }
    
    // set the arrow to point back to the old man
    TargetArrow.SetTarget(GameObject.Find("OldMan"));
    
    // update the quest flag to that when we touch the old man a different quest will execute
    RPG.SetBool("qst_loot",true);
    
    // put up the message display
    MessageDisplay.Show("Back to the old man I guess...");
}
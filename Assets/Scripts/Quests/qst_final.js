#pragma strict

function Start ()
 {
	// Clear messages
	MessageDisplay.Clear();
	// remove arrow 
    TargetArrow.SetTarget(null);

	var portrait:Texture2D=Resources.Load("portrait") as Texture2D;
	var portrait2:Texture2D=Resources.Load("portrait2") as Texture2D;
	
	yield RpgDialog.ShowCo("Err, hello...",portrait,null);
	yield RpgDialog.ShowCo("",null,portrait2);
	yield RpgDialog.ShowCo("Do I know you from somewhere?",portrait,null);
	yield RpgDialog.ShowCo("",null,portrait2);
	yield RpgDialog.ShowCo("Are you the person I was meant to rescue?",portrait,null);
	yield RpgDialog.ShowCo("",null,portrait2);
	yield RpgDialog.ShowCo("Then live happily ever after with?",portrait,null);
	yield RpgDialog.ShowCo("",null,portrait2);
	yield RpgDialog.ShowCo("",null,portrait2);
	yield RpgDialog.ShowCo("",null,portrait2);
	yield RpgDialog.ShowCo("Err...",portrait,null);
	yield RpgDialog.ShowCo("TOMMIE!!!!",null,portrait2);
	yield RpgDialog.ShowCo("Are you still playing that computer game!!!!",null,portrait2);
	yield RpgDialog.ShowCo("Go and do your homework!!!!",null,portrait2);
	yield RpgDialog.ShowCo("Yes Mum\n:-(",portrait,null);

	RPGSounds.Instance.PlayOneShot(RPGSounds.Instance.teleportClip);
	var effect:Texture2D=Resources.Load("YouWin") as Texture2D;
	Transition.SlideIn(effect,2);
	yield WaitForSeconds(2.0);	// wait for mid transtion
	Application.LoadLevel("credits");
}
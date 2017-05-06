using UnityEngine;
using System.Collections;

public class SlideShow : MonoBehaviour 
{
    public Texture2D[] slides;
    public string nextScene = "";
    Texture2D background;
    public float slideDuration = 2;
	public bool escapeToEnd=false;
    int currentSlide = -1;

	// Use this for initialization
	void Start () 
    {
        background = GuiUtils.Texture;  // generic black texture
        // InvokeRepeating(..,0,...) doesn't seem to work ok, so setting it to 0.01f
        InvokeRepeating("ShowSlide", 0.01f, slideDuration);
	}
	
	void Update()
	{
		if (escapeToEnd && Input.anyKey)	// skip to end
		{
			if (currentSlide<slides.Length)
				currentSlide = slides.Length-1;	// next will be the last
		}
	}
	
	// Update is called once per frame
	void ShowSlide() 
    {
        if (currentSlide >= 0 && currentSlide < slides.Length)
            background = slides[currentSlide];
        currentSlide++;
        Debug.Log("ShowSlide " + currentSlide + " " + Time.time);
        if (currentSlide < slides.Length)
        {
            // slide in & hold forever
            Transition.SlideIn(slides[currentSlide], float.MaxValue);
        }
		if (escapeToEnd && Input.anyKey)	// skip to end
		{
			currentSlide = slides.Length;
		}
        if (currentSlide >= slides.Length && nextScene != "")
        {
            // fade out
            Transition.DoTransitionOut(Transition.TransType.Fade, slides[slides.Length - 1]);
            Application.LoadLevel(nextScene);
        }
	}
    void OnGUI()
    {
        GUI.color = Color.white;
        if (background != null)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background, ScaleMode.StretchToFill);
        }
    }
}

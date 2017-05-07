using UnityEngine;
using System.Collections;

public class RpgDialog : MonoBehaviour 
{
    Rect mainRect;
    static float fade;

    public GUISkin skin;
    public float textRate = 2;
    public float top = -105, height = 100;
    public float border = 5;
	public AudioClip tickSound;

    static float textCounter = 0;
    public static Texture2D leftImage, rightImage;
    public static string theText = null;

    public static bool Finished { get { return theText==null; } }
    public static float Fade { get { return fade; } }
    public static bool Visible { get { return fade>0; } }
    public static void Reset() { textCounter = 0; }
    public static void Show(string txt, Texture2D left, Texture2D right)
    {
        theText = txt;
        leftImage = left;
        rightImage = right;
        textCounter = 0;
    }
    // coroutine friendly version
    public static IEnumerator ShowCo(string txt, Texture2D left, Texture2D right)
    {
		//Debug.Log("begin dialog");
        RpgDialog.Show(txt, left, right);
        while (!RpgDialog.Finished)
            yield return null;
		//Debug.Log("end dialog");
    }
	
    public IEnumerator DoDialog(string txt, Texture2D left, Texture2D right)
    {
		//Debug.Log("begin dialog");
        RpgDialog.Show(txt, left, right);
        while (!RpgDialog.Finished)
            yield return null;
		//Debug.Log("end dialog");
    }

    public static void Hide()
    {
        theText = null;
    }

    void Start()
    {
		if (tickSound!=null)
		{
			AudioSource aud=gameObject.AddComponent<AudioSource>();
			aud.clip=tickSound;
		}
		
        if (top>=0)
            mainRect = new Rect(border, top, Screen.width - border*2, height);
        else
            mainRect = new Rect(border, Screen.height + top, Screen.width - border * 2, height);
    }

	// Update is called once per frame
	void Update () 
    {
        if (Finished)   // final fade out
        {
            fade -= Time.deltaTime;
			if (fade<0) fade=0;
            return;
        }
        fade = 1;
		int oldCounter=Mathf.FloorToInt(textCounter); // for use later in audio
        if (Input.anyKey)   // any key makes it faster
            textCounter += Time.deltaTime * textRate*10;
		else
	        textCounter += Time.deltaTime * textRate;
		// tick sound when displaying
		if (tickSound!=null && textCounter < theText.Length)
		{
			if (oldCounter!=Mathf.FloorToInt(textCounter))	// if new text
			{
				GetComponent<AudioSource>().clip=tickSound;	// make sure it stays as a tick
				GetComponent<AudioSource>().Play();
			}
		}
        if (textCounter >= theText.Length)
        {
            Cursor.lockState = CursorLockMode.None;
            //Screen.lockCursor=false;
            if (Input.GetKeyDown(KeyCode.Space))
                Hide();
            // check mouse pos (inverted because unity has mouse at bottom left)
            /*if (Input.GetMouseButtonDown(0) && 
                    mainRect.Contains(new Vector2(Input.mousePosition.x,Screen.height-Input.mousePosition.y)))
                Hide();*/
            if (Input.GetMouseButtonDown(0))
                Hide();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Screen.lockCursor = true;
        }
    }

    void OnGUI()
    {
        if (Finished && fade<0) return;
        GUISkin oldskin = GUI.skin;
        Color oldCol = GUI.color;
        GUI.skin = skin;        
        GUI.color = new Color(1, 1, 1, fade);   // fade out
        GUI.Box(mainRect, "");
        Rect innerRect = GuiUtils.InflateRect(mainRect, -border, -border);
        if (leftImage != null)
        {
            GUI.DrawTexture(new Rect(innerRect.x + border, innerRect.y + (innerRect.height - leftImage.height) / 2, leftImage.width, leftImage.height), leftImage);
            innerRect.x += leftImage.width + border * 2;
            innerRect.width -= leftImage.width + border * 2;
        }
        if (rightImage != null)
        {
            GUI.DrawTexture(new Rect(innerRect.xMax - rightImage.height - border, innerRect.y + (innerRect.height - rightImage.height) / 2, rightImage.width, rightImage.height), rightImage);
            innerRect.width -= rightImage.width + border * 2;
        }
        if (theText != null)
        {
            string s = theText;
            if ((int)textCounter < theText.Length)
                s = theText.Substring(0, (int)textCounter);
            GUI.Label(innerRect, s);
        }
        // restore old stuff
        GUI.color = oldCol;
        GUI.skin = oldskin;
    }
}

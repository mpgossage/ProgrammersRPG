using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageDisplay : MonoBehaviour {

    public GUISkin skin = null;
    public float top = -105, height = 100;
    public float border = 5;
    public float advanceDelay = 5;

    Rect mainRect;
    List<string> messages = new List<string>();
    float nextAdvance = float.MaxValue;

    static MessageDisplay instance=null;

    public static void Show(string msg)
    {
        if (instance != null)
            instance.ShowMessage(msg);
    }
	
	public static void Clear()
	{
		for(int i=0;i<instance.messages.Count;i++)
			instance.messages[i]="";
	}
    public void ShowMessage(string msg)
    {
        // remove one, add one
        messages.RemoveAt(0);
        messages.Add(msg);
        nextAdvance=Time.time+advanceDelay;   // in X seconds remove a message
    }
    

    void Awake()
    {
        if (instance != null)
            Debug.LogError("MessageDisplay seems to have two instances!");
        instance = this;
    }

	// Use this for initialization
	void Start () 
    {
        if (top >= 0)
            mainRect = new Rect(border, top, Screen.width - border * 2, height);
        else
            mainRect = new Rect(border, Screen.height + top, Screen.width - border * 2, height);
        float lineHeight = skin.label.CalcSize(new GUIContent("W")).y;
        int maxLines = Mathf.FloorToInt(mainRect.height / lineHeight);
        //Debug.Log("lineHeight " + lineHeight + " maxLines " + maxLines);
        // setup it with empties
        for (int i = 0; i < maxLines; i++)
            messages.Add("");        
    }
	
	// Update is called once per frame
	void OnGUI() 
    {
        // clear the screen after a while
        if (Time.time >= nextAdvance)
        {
            ShowMessage("");    // adds a blank
            // which will advance the timer too
        }
        string s = "";
        for (int i = 0; i < messages.Count; i++)
        {
            s += messages[i];
            s += "\n";
        }
		Color oldCol=GUI.color;
		Color col=GUI.color;
		col.a=1-RpgDialog.Fade;	// inverse of the rpgs's fade
		GUI.color=col;
        GUI.Label(mainRect, s, skin.label);	
		GUI.color=oldCol;		
	}
}

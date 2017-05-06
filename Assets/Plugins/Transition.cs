using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour 
{
	#region Public accessors
    public static void DoTransition(TransType type, Texture2D tex, float showTime)
    {
        Transition t = Instance;
        t.texture = tex;
        t.type = type;
        t.fadeState = TransState.In;
        t.timer = 0;
        t.showDuration = showTime;
		t.PickTrans();
    }
    public static void DoTransitionOut(TransType type, Texture2D tex)
    {
        Transition t = Instance;
        t.texture = tex;
        t.type = type;
        t.fadeState = TransState.Out;
        t.timer = 0;
		t.PickTrans();
    }
    public static void FadeIn(Texture2D tex, float showTime)
	{
        DoTransition(TransType.Fade, tex, showTime);
	}
    public static void SlideIn(Texture2D tex, float showTime)
    {
        DoTransition(TransType.Slide, tex, showTime);
    }
	public static bool Finished{ get{return Instance.fadeState==TransState.Complete;}}
	
	static Texture2D black;
	public static Texture2D Black
    {
        get
        {
            if (black == null)
            {
                black = new Texture2D(1, 1);
                black.SetPixel(0, 0, Color.black);
                black.Apply();
            }
            return black;
        }
    }
    #endregion

	enum TransState{In,Show,Out,Complete};
	TransState fadeState=TransState.Complete;
    public enum TransType { Slide, Fade };
    TransType type = TransType.Slide;
	float timer;
	float showDuration=1;
	Vector2 slideTarget;
	
	Texture2D texture;

	// Update is called once per frame
	void Update() 
	{
		if (fadeState!=TransState.Complete)
		{
			timer+=Time.deltaTime;
			if (fadeState==TransState.In && timer>=1)
			{
				timer=0;
				fadeState=TransState.Show;
			}
			if (fadeState==TransState.Show && timer>=showDuration)
			{
				timer=0;
				fadeState=TransState.Out;
				PickTrans();
			}
			if (fadeState==TransState.Out && timer>=1)
			{
				timer=0;
				fadeState=TransState.Complete;
			}
		}	
	}
	void PickTrans()
	{
		// when sliding: we need a random direction:
		Vector2 dir=Vector2.zero;
		do
		{
			dir.x=Random.Range(-1,+1);
			dir.y=Random.Range(-1,+1);
		}
		while(dir.x==0 && dir.y==0);
		slideTarget.x=dir.x*Screen.width;
		slideTarget.y=dir.y*Screen.height;
	}
	
	void OnGUI()
	{
		if (fadeState==TransState.Complete) return;
		GUI.depth=-1000;	// VERY VERY TOP

        if (type == TransType.Slide)
            DoSlide();
        else
            DoFade();
    }

    void DoFade()
    {
        Color oldCol = GUI.color;
        Color col = Color.white;
        if (fadeState == TransState.In)
            col.a = timer;
        if (fadeState == TransState.Out)
            col.a = 1-timer;
        GUI.color = col;
        GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), texture, ScaleMode.StretchToFill);
        GUI.color = oldCol;
    }

    void DoSlide()
    {
        Vector2 pos=Vector2.zero;
        if (fadeState == TransState.In)
        {
            Vector2 from=slideTarget;//new Vector2(Screen.width, 0);
            Vector2 to=Vector2.zero;
            float t = ElasticEaseOut(timer);
            pos = Vector2.Lerp(from, to, t);
        }
        else if (fadeState == TransState.Out)
        {
            Vector2 from = Vector2.zero;
            Vector2 to = slideTarget;//new Vector2(-Screen.width,0);
            pos = Vector2.Lerp(from, to, timer);
        }

		GUI.DrawTexture(new Rect(pos.x,pos.y,Screen.width,Screen.height),texture,ScaleMode.StretchToFill);
	}

    //http://robertpenner.com/easing/
    //http://wpf-animation.googlecode.com/svn/trunk/src/WPF/Animation/PennerDoubleAnimation.cs
    //http://sol.gfxile.net/interpolation/
    //http://www.robertpenner.com/easing/easing_demo.html

    public static float ElasticEaseOut(float t)
    {
        const float b = 0, c = 1, d = 1;
        if (t == 1)
            return b + c;

        float p = d * .3f;
        float s = p / 4;

        return (c * Mathf.Pow(2, -10 * t) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) + c + b);
    }
   /* private float easeOutElastic(float value)
    {
        if (value <= 0) return 0;

        if (value >= 1) return   1;

        const float d = 1f;
        const float p = d * .3f;
        const float s = p / 4;
        const float a = 1;

        return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + 1);
    }	*/
    private float easeOutElastic(float start, float end, float value)
    {
        /* GFX47 MOD END */
        //Thank you to rafael.marteleto for fixing this as a port over from Pedro's UnityTween
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s = 0;
        float a = 0;

        if (value == 0) return start;

        if ((value /= d) == 1) return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + end + start);
    }		


	
	#region Singleton Code
	private static Transition instance;

	public static Transition Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("Transition").AddComponent<Transition>();
				GameObject.DontDestroyOnLoad(instance);	// don't get destroyed in a level loading
			}
			return instance;
		}
	}

	public void OnApplicationQuit ()
	{
		instance = null;
	}
	#endregion	
	
}

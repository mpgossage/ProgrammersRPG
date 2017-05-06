#region Licence
// Copyright Mark Gossage (mark.gossage@sp.edu.sg) 2010-2012.
// Distributed under the Boost Software License, Version 1.0.
//    (See http://www.boost.org/LICENSE_1_0.txt)
#endregion
using UnityEngine;
using System.Collections;

public class DisplayBar : MonoBehaviour {

    public float Value = 0.5f;
    public Rect BarRectangle = new Rect(20, 40, 60, 20);
    public int BorderWidth = 2;
    public bool Horizontal = true;
    public Color ForegroundColour = Color.green,
        BackgroundColour = Color.red,
        BorderColour = Color.black;

    // Use this for initialization
    void Start()
    {
    }

    void OnGUI()
    {
        Color oldCol = GUI.color;
        Texture2D tex = GuiUtils.Texture;
        Rect inner = BarRectangle;
        if (BorderWidth > 0)
        {
            GUI.color = BorderColour;
            GUI.DrawTexture(BarRectangle, tex, ScaleMode.StretchToFill);
            inner.xMin += BorderWidth;
            inner.xMax -= BorderWidth;
            inner.yMin += BorderWidth;
            inner.yMax -= BorderWidth;
        }
        GUI.BeginGroup(inner);
        GUI.color = BackgroundColour;
        GUI.DrawTexture(new Rect(0, 0, inner.width, inner.height), tex, ScaleMode.StretchToFill);
        GUI.color = ForegroundColour;
        if (Horizontal)
            GUI.DrawTexture(new Rect(0, 0, inner.width*Value, inner.height), tex, ScaleMode.StretchToFill);
        else
            GUI.DrawTexture(new Rect(0, inner.height * (1 - Value), inner.width, inner.height), tex, ScaleMode.StretchToFill);
        GUI.EndGroup();
        GUI.color = oldCol;
    }

}

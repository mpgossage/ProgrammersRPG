#region Licence
// Copyright Mark Gossage (mark.gossage@sp.edu.sg) 2010-2012.
// Distributed under the Boost Software License, Version 1.0.
//    (See http://www.boost.org/LICENSE_1_0.txt)
#endregion
using UnityEngine;
using System.Collections;

/** Healthbar script for Unity.
 * Creates a game object attached to the Unit which holds the mesh to display the 
 * health information
 */
public class DisplayBar3D : MonoBehaviour
{
    public float Width = 1.0f;
    public float Height = 0.2f;
    public float Border = 0.01f;
    public Vector3 Offset = new Vector3(0, 1, 0);
    public bool ColourBlend = true;
	public enum HideOption{HideIfFull,HideIfEmpty,HideIfEither,NeverHide};
	public HideOption options=HideOption.NeverHide;
    public float Value
    {
        get { return this.value; }
        set { SetValue(value); }
    }

    float value = 1;

    GameObject theBar;

    Mesh mesh;
	MeshRenderer meshRenderer;
    Vector3[] verts;
    Color[] cols;

    // Use this for initialization
    void Start()
    {
        // make a new game object
        theBar = new GameObject();
        theBar.transform.parent = transform;  // parent to me
        // add components
        meshRenderer = theBar.AddComponent<MeshRenderer>();
        MeshFilter mf = theBar.AddComponent<MeshFilter>();
        // arrange the mesh
        mesh = new Mesh();
        mf.mesh = mesh;

        // code based upon: http://docs.unity3d.com/Documentation/Manual/Example-CreatingaBillboardPlane.html
        // but using 2 rectangles & reversed winding

        // 2 rectangles
        // outer one is Width/2+Border, Height/2+Border
        // inner is without the Border
        float inW = Width / 2, inH = Height / 2, outW = inW + Border, outH = inH + Border;
        verts = new Vector3[8];
        // first 4 are outer
        verts[0] = new Vector3(-outW, -outH, 0);
        verts[1] = new Vector3(+outW, -outH, 0);
        verts[2] = new Vector3(-outW, +outH, 0);
        verts[3] = new Vector3(+outW, +outH, 0);
        // last 4 are inner
        verts[4] = new Vector3(-inW, -inH, -0.01f);
        verts[5] = new Vector3(+inW, -inH, -0.01f);
        verts[6] = new Vector3(-inW, +inH, -0.01f);
        verts[7] = new Vector3(+inW, +inH, -0.01f);
        mesh.vertices = verts;
        int[] tri = new int[12];
        tri[0] = 0; tri[1] = 2; tri[2] = 1;
        tri[3] = 2; tri[4] = 3; tri[5] = 1;
        tri[6] = 4; tri[7] = 6; tri[8] = 5;
        tri[9] = 6; tri[10] = 7; tri[11] = 5;
        mesh.triangles = tri;
        cols = new Color[8];
        cols[0] = cols[1] = cols[2] = cols[3] = Color.black;
        cols[4] = cols[5] = cols[6] = cols[7] = Color.green;
        mesh.colors = cols;
        // set material
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));

        SetValue(1);
    }

    public void SetValue(float val)
    {
        value = Mathf.Clamp01(val);
		// check hide options
		bool hide=false;
		if (value>=1 && options==HideOption.HideIfFull)	// hide if full value
			hide=true;	
		if (value<=0 && options==HideOption.HideIfEmpty)	// hide if empty value
			hide=true;	
		if (options==HideOption.HideIfEither && (value<=0 || value>=1))
			hide=true;	
		
		//Debug.Log("Render is "+!hide);
		meshRenderer.enabled=!hide;	// enable or not

        // update the vertexes
        float inW = Width / 2;
        float w = Width * value;
        verts[5].x = verts[7].x = w - inW;
        mesh.vertices = verts;  // update
        // update colours(fixed threshold)
        Color col = Color.green;
        if (ColourBlend == false)
        {
            if (value < 0.6f) col = Color.yellow;
            if (value < 0.3f) col = Color.red;
        }
        else
        {
            // update colours (blended)
            if (value > 0.5f)
                col = Color.Lerp(Color.yellow, Color.green, value * 2 - 1);
            else
                col = Color.Lerp(Color.red, Color.yellow, value * 2);
        }
        cols[4] = cols[5] = cols[6] = cols[7] = col;
        mesh.colors = cols;
    }
    // Update is called once per frame
    void Update()
    {
        // keep it above the unit
        theBar.transform.position = transform.position + Offset;
        // keep aligned to camera
        // from http://unifycommunity.com/wiki/index.php?title=CameraFacingBillboard
        // modified to use Vector3.forward rather than Vector3.back
        theBar.transform.LookAt(theBar.transform.position + Camera.main.transform.rotation * Vector3.forward,
                Camera.main.transform.rotation * Vector3.up);
    }
}

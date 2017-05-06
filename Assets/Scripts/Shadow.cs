using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {

    public Vector3 offset = new Vector3(0, 0.01f, 0);
    public float shadowSize = 5;
    public float maxRay = 5;
	// Update is called once per frame
	void Update () 
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.parent.position, Vector3.down, out hit, maxRay))
        {
            //Debug.Log("Ray hit:" + hit.collider.name);
			Vector3 pos=hit.point;
			pos.y=GetMaxHeight(transform.parent.position);
            transform.position = pos+offset;
			// get the min depth:
            float scale = shadowSize * (1 - hit.distance / maxRay);
            if (scale < 0) scale = 0;
            transform.localScale = new Vector3(scale, scale, scale);
        }
        else
            transform.localScale = Vector3.zero;
	
	}
	float GetMaxHeight(Vector3 pos)
	{
		float maxY=0;
		RaycastHit hit2;
		if (Physics.Raycast(pos+new Vector3(-1,0,-1), Vector3.down, out hit2, maxRay))
			if (maxY<hit2.point.y)
				maxY=hit2.point.y;
		if (Physics.Raycast(pos+new Vector3(+1,0,-1), Vector3.down, out hit2, maxRay))
			if (maxY<hit2.point.y)
				maxY=hit2.point.y;
		if (Physics.Raycast(pos+new Vector3(-1,0,+1), Vector3.down, out hit2, maxRay))
			if (maxY<hit2.point.y)
				maxY=hit2.point.y;
		if (Physics.Raycast(pos+new Vector3(+1,0,+1), Vector3.down, out hit2, maxRay))
			if (maxY<hit2.point.y)
				maxY=hit2.point.y;	
		return maxY;
	}
}

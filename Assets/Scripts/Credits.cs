using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	
	public GameObject pathBase;
	public float smooth=6;
	public float timePerItem=3;
	
	Transform target;
	int index=0;
	bool autoMove=true;
	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("NextItem",1,timePerItem);	
		// hide all the children
		for(int i=0;i<pathBase.transform.childCount;i++)
			pathBase.transform.GetChild(i).gameObject.SetActive(false);
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
		{
			autoMove=false;
			if (index>1)
			{
				index--;
				target=GetNumberedChild(pathBase,index);
			}
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
		{
			autoMove=false;
			if (index<pathBase.transform.childCount)
			{
				index++;
				target=GetNumberedChild(pathBase,index);
				target.gameObject.SetActive(true);
			}
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
		{
			autoMove=true;
			if (index==pathBase.transform.childCount)
			{
				index=0;
				// hide all the children (mainly the words)
				for(int i=0;i<pathBase.transform.childCount;i++)
					pathBase.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			audio.Play();
			Transition.FadeIn(Transition.Black,1);
			Invoke("GoMenu",1.5f);	// call next scene soon
		}
	}
	void GoMenu()
	{
		Application.LoadLevel("menu");
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		if (target==null) return;
		// align:
		transform.position=Vector3.Lerp(transform.position,target.position,Time.deltaTime*smooth);
		transform.rotation=Quaternion.Slerp(transform.rotation,target.rotation,Time.deltaTime*smooth);
	}
	
	Transform GetNumberedChild(GameObject parent,int index)
	{
		return parent.transform.FindChild(string.Format("{0:000}",index));
	}
	void NextItem()
	{
		if (!autoMove) return;
		if (index<pathBase.transform.childCount)
		{
			index++;
			target=GetNumberedChild(pathBase,index);
			target.gameObject.SetActive(true);
		}
	}
}

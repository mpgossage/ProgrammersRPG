using UnityEngine;
using System.Collections;

public class TargetArrow : MonoBehaviour 
{
    public GameObject target = null;
    public float distanceFromPlayer = 2;
    public float nearDistance = 5;
	public float heightAbovePlayer=0;
    GameObject player;

    static TargetArrow instance;
    public static void SetTarget(GameObject tgt)
    {
        instance.target=tgt;
    }

    void Awake()
    {
        instance = this;    // setup singleton
    }

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (target == null) // no target/ not visible
        {
            GetComponent<Renderer>().enabled = false;
            return;
        }
        GetComponent<Renderer>().enabled = true;
        if (Vector3.Distance(player.transform.position, target.transform.position) > nearDistance)
        {
            // player is not so near: it appears between player & target
            Vector3 direction = (target.transform.position - player.transform.position).normalized;
            transform.position = player.transform.position + direction * distanceFromPlayer+Vector3.up*heightAbovePlayer;
        }
        else
        {
            // its near: it appears above targets head (pointing up & down)
            transform.position = target.transform.position + Vector3.up * (heightAbovePlayer +Mathf.PingPong(Time.time,1));
        }
        transform.LookAt(target.transform);	
	}
}

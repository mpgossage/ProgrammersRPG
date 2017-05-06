using UnityEngine;
using System.Collections;

public class SpawnTreasure : MonoBehaviour {

    public GameObject toSpawn;
    public float velocity = 5;
    public int number = 50;
	public Vector3 offset=Vector3.up;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            for (int i = 0; i < number; i++)
            {
                Vector3 v = new Vector3(Random.Range(-velocity, velocity), Random.Range(velocity*0.5f,velocity*1.5f), Random.Range(-velocity, velocity));
                GameObject obj = (GameObject)Instantiate(toSpawn, transform.position+offset, Quaternion.identity);
                obj.GetComponent<Rigidbody>().velocity = v;
                obj.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere;
            }
            Destroy(this);
        }
    }
}

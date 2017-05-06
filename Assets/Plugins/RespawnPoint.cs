using UnityEngine;
using System.Collections;

public class RespawnPoint : MonoBehaviour {

    public static Vector3 location = Vector3.zero;
    public static GameObject player = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            location = transform.position;
        }
    }

    public static void RespawnPlayer()
    {
        player.transform.position = location;
    }
}

using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour 
{
    public string message = "KillBox";
    public Texture2D respawnTex;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
			RPG.KillPlayer(message); // kill them
        }
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);  // kill it
        }
    }

}

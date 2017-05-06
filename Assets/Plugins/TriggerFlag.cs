using UnityEngine;
using System.Collections;

public class TriggerFlag : MonoBehaviour {

    public string flagPrereq;
	public string flagName,flagValue;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (flagPrereq=="" || RPG.GetBool(flagPrereq))  // the prequisit is done
            {
				RPG.SetFlag(flagName,flagValue);
				Destroy(this);
            }
        }
    }
}

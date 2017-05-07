using UnityEngine;
using System.Collections;
using System;

public class QuestTrigger : MonoBehaviour {

    [SerializeField]
    private MonoBehaviour questComponent;
    public string flagPrereq;

    private void Awake()
    {
        questComponent.enabled = false; // turn it off
    }

    void OnTriggerEnter(Collider other)
    {
		Debug.Log("trigger:"+other.name+" "+other.tag);
        if (other.tag == "Player")
        {
            if (flagPrereq=="" || RPG.GetBool(flagPrereq))  // the prequisit is done
            {
				// add quest to the main RPG & this destroy myself to stop muliple triggers
				GameObject rpg=GameObject.FindGameObjectWithTag("GameController");
                // because unity will not allow add component by name
                // we now have an existing component attached & use its type to get the correct version
                // string=>Type never works for javascript behaviours
                rpg.AddComponent(questComponent.GetType()); // add this component to the quest system
                Destroy(this);
            }
        }
    }
}

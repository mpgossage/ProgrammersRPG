using UnityEngine;
using System.Collections;
using System;

public class QuestTrigger : MonoBehaviour {

    public string questName;
    public string flagPrereq;

    void OnTriggerEnter(Collider other)
    {
		Debug.Log("trigger:"+other.name+" "+other.tag);
        if (other.tag == "Player")
        {
            if (flagPrereq=="" || RPG.GetBool(flagPrereq))  // the prequisit is done
            {
				// add quest to the main RPG & this destroy myself to stop muliple triggers
				GameObject rpg=GameObject.FindGameObjectWithTag("GameController");
                Type questType = Type.GetType(questName);
                Debug.LogFormat("AddQuest {0} {1}", questName, questType);
                rpg.AddComponent(questType);

				//UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(rpg, "Assets/Plugins/QuestTrigger.cs (18,5)", questName);
				Destroy(this);
            }
        }
    }
    /*IEnumerator StartQuest()
    {
        RpgCutscene.Show(questName);
        // wait for it to finish
        while (RpgCutscene.IsRunning())
            yield return 0;
        Destroy(this);  // remove the behaviour to stop retriggering
    }*/
}

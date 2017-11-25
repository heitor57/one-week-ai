using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;

public class IAIntegration : Object {

	public static void TalkNPC(GameObject npc, bool conversando){
		npc.GetComponent<AIRig> ().AI.WorkingMemory.SetItem<bool>("talking",conversando);
	}
	public static void TalkNPC(GameObject player,GameObject npc, bool conversando){
		npc.GetComponent<AIRig> ().AI.WorkingMemory.SetItem<bool>("talking",conversando);
		npc.GetComponent<AIRig> ().AI.WorkingMemory.SetItem<GameObject> ("talkingperson",player);
	}
}

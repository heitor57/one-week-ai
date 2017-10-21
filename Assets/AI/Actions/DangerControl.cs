
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities.Aspects;
[RAINAction]
public class DangerControl : RAINAction
{
	
	public override void Start(RAIN.Core.AI ai)
	{
		base.Start(ai);
	}

	public override ActionResult Execute(RAIN.Core.AI ai)
	{
		if(ai.WorkingMemory.GetItem("AttackAs")!=null){
			for(int i=0;i<((List<RAINAspect>)ai.WorkingMemory.GetItem("AttackAs")).Count;i++){
				for (int j = 0; j < ai.WorkingMemory.GetItem<List<AboutPerson>> ("aboutperson").Count; j++) {
					if (((List<GameObject>)ai.WorkingMemory.GetItem ("AttackGo")) [i] ==
						ai.WorkingMemory.GetItem<List<AboutPerson>> ("aboutperson") [j].Person
						&& ai.WorkingMemory.GetItem<Dictionary<AboutPerson,bool>> ("alreadyattacked")[ai.WorkingMemory.GetItem<List<AboutPerson>> ("aboutperson") [j]] == false) {
						// Bloqueia a detecçao de ataque 
						ai.WorkingMemory.GetItem<Dictionary<AboutPerson,bool>> ("alreadyattacked") [ai.WorkingMemory.GetItem<List<AboutPerson>> ("aboutperson") [j]] = true;
						// Aumenta o quanto violento a pessoa é
						if (ai.WorkingMemory.GetItem<List<AboutPerson>> ("aboutperson") [j].Feelings [Constants.violency] == 0)
							ai.WorkingMemory.GetItem<List<AboutPerson>> ("aboutperson") [j].Feelings [Constants.violency] = 100;
						else
							ai.WorkingMemory.GetItem<List<AboutPerson>> ("aboutperson") [j].Feelings[Constants.violency] += 20 + 20*ai.WorkingMemory.GetItem<List<AboutPerson>> ("aboutperson") [j].Feelings [Constants.violency]/100;
						
						break; // ja achou a pessoa na memoria, então pode parar
					}
				}
			}

		}
		return ActionResult.RUNNING;
	}

	public override void Stop(RAIN.Core.AI ai)
	{
		base.Stop(ai);
	}
}

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
		if(ai.WorkingMemory.GetItem("AttackAs")!=null && ai.WorkingMemory.GetItem<GameObject>("EnemyGo") == null){
			for(int i=0;i<((List<GameObject>)ai.WorkingMemory.GetItem("AttackGo")).Count;i++){
				for (int j = 0; j < ai.WorkingMemory.GetItem<List<AboutAnimal>> ("aboutanimal").Count; j++) {
					if (((List<GameObject>)ai.WorkingMemory.GetItem ("AttackGo")) [i] ==
						ai.WorkingMemory.GetItem<List<AboutAnimal>> ("aboutanimal") [j].Target
						&& ai.WorkingMemory.GetItem<Dictionary<AboutAnimal,bool>> ("alreadyattacked")[ai.WorkingMemory.GetItem<List<AboutAnimal>> ("aboutanimal") [j]] == false) {
						// Bloqueia a detecçao de ataque 
						ai.WorkingMemory.GetItem<Dictionary<AboutAnimal,bool>> ("alreadyattacked") [ai.WorkingMemory.GetItem<List<AboutAnimal>> ("aboutanimal") [j]] = true;
						// Aumenta o quanto violento a pessoa é
						if (ai.WorkingMemory.GetItem<List<AboutAnimal>> ("aboutanimal") [j].Feelings [Constants.violency] == 0)
							ai.WorkingMemory.GetItem<List<AboutAnimal>> ("aboutanimal") [j].Feelings [Constants.violency] = 100;
						else
							ai.WorkingMemory.GetItem<List<AboutAnimal>> ("aboutanimal") [j].Feelings[Constants.violency] += 20 + 20*ai.WorkingMemory.GetItem<List<AboutAnimal>> ("aboutanimal") [j].Feelings [Constants.violency]/100;
						
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
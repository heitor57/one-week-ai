using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class DangerDistance : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		// Guarda objeto perigoso
		GameObject enemy = ai.WorkingMemory.GetItem<GameObject> ("EnemyGo");
		// Seta distancia entre objeto perigoso e a IA
		ai.WorkingMemory.SetItem<float> ("EnemyDistance",Vector3.Distance(enemy.transform.position,ai.Body.transform.position));
		return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}
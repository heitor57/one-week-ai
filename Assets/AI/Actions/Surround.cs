using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class Surround : RAINAction
{
	int surrounddistance = 3;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		List<Vector3> positions = new List<Vector3> ();
		positions.Add (new Vector3 (0, 0, surrounddistance));
		positions.Add (new Vector3 (surrounddistance, 0, 0));
		positions.Add (new Vector3 (0, 0, -surrounddistance));
		positions.Add (new Vector3 (-surrounddistance, 0,0));
		int index = 0, i = 0;
		float distancia = 100f;
		for (i = 0; i < 4; i++) {
			if(Vector3.Distance(ai.Body.transform.position, positions[i]+ai.WorkingMemory.GetItem<GameObject> ("EnemyGo").transform.position)<=distancia){
				distancia = Vector3.Distance(ai.Body.transform.position, positions[i]+ai.WorkingMemory.GetItem<GameObject> ("EnemyGo").transform.position);
				index = i;
			}
		}
		positions.RemoveAt(index);
		distancia = 0f;
		for (i = 0; i < 3; i++) {
			if(Vector3.Distance(ai.Body.transform.position, positions[i]+ai.WorkingMemory.GetItem<GameObject> ("EnemyGo").transform.position)>=distancia){
				distancia = Vector3.Distance(ai.Body.transform.position, positions[i]+ai.WorkingMemory.GetItem<GameObject> ("EnemyGo").transform.position);
				index = i;
			}
		}
		positions.RemoveAt(index);
		ai.WorkingMemory.SetItem<Vector3>("SurroundPosition",positions[Random.Range(0,positions.Count)]+ai.WorkingMemory.GetItem<GameObject> ("EnemyGo").transform.position);
		//Debug.Log (ai.WorkingMemory.GetItem<Vector3>("SurroundPosition"));
		//for (i = 0; i < positions.Count; i++)
		//	Debug.Log (positions [i]);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}
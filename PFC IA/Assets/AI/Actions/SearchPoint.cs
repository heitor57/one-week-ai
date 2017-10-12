using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Navigation;
using RAIN.Navigation.Graph;
using RAIN.Navigation.Waypoints;
[RAINAction]
public class SearchPoint : RAINAction
{
	
	public SearchPoint(){
		actionName = "SearchPoint";
	}
    public override void Start(RAIN.Core.AI ai)
    {
		base.Start(ai);
		if (ai.WorkingMemory.GetItem<GameObject> ("search_waypoint") != null) {
			GameObject.Destroy(ai.WorkingMemory.GetItem<GameObject> ("search_waypoint").gameObject);
			ai.WorkingMemory.RemoveItem("search_waypoint");
		}
		Vector3 UltPos = ai.WorkingMemory.GetItem<GameObject> ("EnemyGoPersistent").transform.position;
		GameObject obj = new GameObject();
		WaypointRig WaypointRigTemp = obj.AddComponent<WaypointRig>();
		//se Player fora do alcance
		WaypointRigTemp.WaypointSet.SetType = 0;
		WaypointRigTemp.WaypointSet.AddWaypoint(new Waypoint(UltPos, "UltimaPosicao"));
		WaypointRigTemp.WaypointSet.AddWaypoint(new Waypoint(new Vector3(UltPos.x + 10f, UltPos.y, UltPos.z + 10f), "quadrado 1"));
		WaypointRigTemp.WaypointSet.AddWaypoint(new Waypoint(new Vector3(UltPos.x + 10f, UltPos.y, UltPos.z - 10f), "quadrado 2"));
		WaypointRigTemp.WaypointSet.AddWaypoint(new Waypoint(new Vector3(UltPos.x - 10f, UltPos.y, UltPos.z - 10f), "quadrado 3"));
		WaypointRigTemp.WaypointSet.AddWaypoint(new Waypoint(new Vector3(UltPos.x - 10f, UltPos.y, UltPos.z + 10f), "quadrado 4"));
		WaypointRigTemp.WaypointSet.AddWaypoint(new Waypoint(new Vector3(UltPos.x + 10f, UltPos.y, UltPos.z + 10f), "quadrado 5"));

		ai.WorkingMemory.SetItem<GameObject> ("search_waypoint",obj);


    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}
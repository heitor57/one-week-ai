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

    public SearchPoint()
    {
        actionName = "SearchPoint";
    }
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
        if (ai.WorkingMemory.GetItem<GameObject>("search_waypoint") != null)
        {
            GameObject.Destroy(ai.WorkingMemory.GetItem<GameObject>("search_waypoint").gameObject);
            ai.WorkingMemory.RemoveItem("search_waypoint");

        }
        Vector3 UltPos = ai.WorkingMemory.GetItem<GameObject>("EnemyGoPersistent").transform.position;
        GameObject obj = new GameObject();
        WaypointRig WaypointRigTemp = obj.AddComponent<WaypointRig>();
        //se Player fora do alcance
        WaypointRigTemp.WaypointSet.SetType = 0;
        WaypointRigTemp.WaypointSet.AddWaypoint(new Waypoint(UltPos, "UltimaPosicao"));
        Vector3 Step = UltPos + (UltPos - ai.Kinematic.Position);
        float rand = Random.Range(1f, 360f);
        WaypointRigTemp.WaypointSet.AddWaypoint(new Waypoint(new Vector3(Step.x + Mathf.Cos(rand) * 10f, Step.y, Step.z + Mathf.Sin(rand) * 10f), "circulo 1"));
        rand = Random.Range(1f, 360f);
        WaypointRigTemp.WaypointSet.AddWaypoint(new Waypoint(new Vector3(Step.x + Mathf.Cos(rand) * 10f, Step.y, Step.z + Mathf.Sin(rand) * 10f), "quadrado 2"));
        rand = Random.Range(1f, 360f);
        WaypointRigTemp.WaypointSet.AddWaypoint(new Waypoint(new Vector3(Step.x + Mathf.Cos(rand) * 10f, Step.y, Step.z + Mathf.Sin(rand) * 10f), "quadrado 3"));
        rand = Random.Range(1f, 360f);
        WaypointRigTemp.WaypointSet.AddWaypoint(new Waypoint(new Vector3(Step.x + Mathf.Cos(rand) * 10f, Step.y, Step.z + Mathf.Sin(rand) * 10f), "quadrado 4"));
        

        ai.WorkingMemory.SetItem<GameObject>("search_waypoint", obj);


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
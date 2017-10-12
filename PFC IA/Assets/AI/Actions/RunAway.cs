using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Perception;
using UnityEngine.AI;
using RAIN.Perception.Sensors;
[RAINAction]
public class RunAway : RAINAction
{
	private  float viewRadius;
	private  float viewAngle;
	
	public LayerMask targetMask;
	public LayerMask obstacleMask;

	private List<Transform> visibleTargets = new List<Transform>();
	private List<Transform> visibleObstacleTargets = new List<Transform>();

	NavMeshAgent agent;
	private int count=0;
	private Vector3 last_position = new Vector3();
	public override void Start(RAIN.Core.AI ai)
	{
		viewRadius = ((RAIN.Perception.Sensors.VisualSensor)ai.Senses.GetSensor("Visual Sensor")).Range;
		viewAngle = ((RAIN.Perception.Sensors.VisualSensor)ai.Senses.GetSensor ("Visual Sensor")).HorizontalAngle;
		targetMask = ((RAIN.Perception.Sensors.VisualSensor)ai.Senses.GetSensor ("Visual Sensor")).LineOfSightMask;
		agent = ai.Body.GetComponent<NavMeshAgent>();
		base.Start(ai);
	}

	public override ActionResult Execute(RAIN.Core.AI ai)
	{
		FindVisibleTargets (ai);

		/*if (visibleTargets.Count > 0) {
			Vector3 runawayposition;
			GameObject Obj;
			Obj = visibleObstacleTargets [Random.Range (0, visibleObstacleTargets.Count)].gameObject;
			runawayposition = Obj.transform.position + Obj.transform.gameObject.GetComponent<Collider> ().bounds.size - new Vector3 (0,Obj.transform.gameObject.GetComponent<Collider> ().bounds.size.y,0);
			Debug.DrawLine (Vector3.zero,runawayposition);
			for (int v = 0; v < visibleObstacleTargets.Count; v++) {
				Debug.Log ("Obstaculo:"+visibleObstacleTargets[v]);
				Debug.Log (Obj.transform.localScale);
			}
			ai.WorkingMemory.SetItem<Vector3> ("RunAwayPosition", runawayposition);

		} else {*/
		Vector3 runawayposition = Vector3.zero;

		if(ai.WorkingMemory.GetItem<bool>("waitrunover") == false)
		runawayposition = ai.Kinematic.Position * 2 - ai.WorkingMemory.GetItem<GameObject> ("EnemyGo").transform.position;  


		if (agent.destination != agent.path.corners [agent.path.corners.Length - 1]) {
			Vector3 fwd = ai.Body.transform.TransformDirection(Vector3.forward);
			RaycastHit getObj;
			if (Physics.Raycast (ai.Body.transform.position, fwd, out getObj, ((VisualSensor)ai.Senses.GetSensor ("Visual Sensor")).Range)) {
				Vector3 scale = getObj.transform.localScale;
				runawayposition = getObj.transform.position + new Vector3(scale.x * Mathf.Sin (ai.Body.transform.eulerAngles.y*Mathf.Deg2Rad), -getObj.transform.position.y, scale.z * Mathf.Cos (ai.Body.transform.eulerAngles.y*Mathf.Deg2Rad));
				ai.WorkingMemory.SetItem<bool> ("waitrunover", true);

			}
		}
			
		ai.WorkingMemory.SetItem<Vector3> ("RunAwayPosition", runawayposition);


		if (ai.Body.transform.position == last_position) {
			count++;
			if (count > 6) {
				ai.WorkingMemory.SetItem<bool> ("Cornered", true);
			}
		} else {
			count = 0;
		}
		last_position = ai.Body.transform.position;
		//}
		return ActionResult.RUNNING;
	}
	void FindVisibleTargets(AI ai) {
		visibleTargets.Clear ();
		visibleObstacleTargets.Clear ();
		Collider[] targetsInViewRadius = Physics.OverlapSphere (ai.Body.transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - ai.Body.transform.position).normalized;
			if (Vector3.Angle (ai.Body.transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (ai.Body.transform.position, target.position);
				if (!Physics.Raycast (ai.Body.transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					visibleTargets.Add (target);
				}
			}
		}

		if (visibleTargets.Count > 0) {
			for (int j = 0; j < visibleTargets.Count; j++) {
				//UnityEditorInternal.InternalEditorUtility.tags.GetValue(7)
				if (visibleTargets [j].tag.Equals ("Obstacle")) {
					visibleObstacleTargets.Add (visibleTargets [j]);
				}
			}
		}
	}


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal, AI ai) {
		if (!angleIsGlobal) {
			angleInDegrees += ai.Body.transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
	public override void Stop(RAIN.Core.AI ai)
	{
		base.Stop(ai);
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using UnityEngine.AI;
using RAIN.Representation;
using RAIN.Navigation.Waypoints;
[RAINAction]
public class MoveUnity : RAINAction
{
	NavMeshAgent agent;
	public Expression Move_Target;
	public Expression Move_Speed;
	public Expression Acceleration;
	public Expression Turn_Speed;
	public Expression Close_Enough_Distance;
	public Expression Route;
	public Expression Infinite = new Expression();
	public Expression BacktoPoint = new Expression();
	public Expression ReturnByWay = new Expression();
	public Expression MoveOnWallIfWall = new Expression();
	private float speed;
	private float turnspeed;
	private float stoppingdistance;
	private bool infinite = true;
	public float acceleration_b;
	private int i=0;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		agent = ai.Body.GetComponent<NavMeshAgent> ();
		agent.stoppingDistance = 0.1f;
		speed = agent.speed;
		turnspeed = agent.angularSpeed;
		stoppingdistance = agent.stoppingDistance;
		acceleration_b = agent.acceleration;
		if (BacktoPoint.IsValid && bool.Parse(BacktoPoint.ExpressionAsEntered) == false)
			i = 0; 
		
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		
		if (Move_Target.IsVariable) {
			if (Move_Speed.IsValid) {
				// Define velocidade
				agent.speed = float.Parse(Move_Speed.ExpressionAsEntered);
			}
			if (Close_Enough_Distance.IsValid) {
				// Define o Close Distance
				agent.stoppingDistance=float.Parse(Close_Enough_Distance.ExpressionAsEntered);
			}
			if (Turn_Speed.IsValid) {
				agent.angularSpeed = float.Parse (Turn_Speed.ExpressionAsEntered);
			}
			if (Acceleration.IsValid) {
				agent.acceleration = float.Parse (Acceleration.ExpressionAsEntered);
			}
			if (Infinite.IsValid) {
				infinite = bool.Parse (Infinite.ExpressionAsEntered);
			}
			// Se preencheu rota interpreta posiçao sem ser rota
			if (Route.ExpressionAsEntered.Length > 0 && Route.IsValid == true && bool.Parse (Route.ExpressionAsEntered) == true) {
				// É rota
				// Ve se tem na memoria a posiçao
				WaypointSet temp = null;
				if (ai.WorkingMemory.GetItem<GameObject> (Move_Target.ExpressionAsEntered) != null)
					temp = ai.WorkingMemory.GetItem<GameObject> (Move_Target.ExpressionAsEntered).GetComponent<WaypointRig> ().WaypointSet;
				else if (ai.WorkingMemory.GetItem<GameObject> (Move_Target.ExpressionAsEntered) != null)
					temp = ai.WorkingMemory.GetItem<GameObject> (Move_Target.ExpressionAsEntered).GetComponent<WaypointRig> ().WaypointSet;
				// Ve se tem no jogo
				else if (GameObject.Find (Move_Target.ExpressionAsEntered).GetComponent<WaypointRig> ().WaypointSet != null)
					temp = GameObject.Find (Move_Target.ExpressionAsEntered).GetComponent<WaypointRig> ().WaypointSet;
				// Controle de waypoints inacessiveis

				// MoveOnWallIfWall pula o waypoint neste
				if (MoveOnWallIfWall.IsValid && MoveOnWallIfWall.ExpressionAsEntered.Length > 0 && bool.Parse (MoveOnWallIfWall.ExpressionAsEntered) &&
					agent.destination != agent.path.corners [agent.path.corners.Length - 1]) {
					
					if (temp.Waypoints.Count - 1 == i) {
						i = 0;
						if (infinite == false)
							return ActionResult.SUCCESS;
					}else 
						i++;
					
					if (ReturnByWay.IsValid && bool.Parse (ReturnByWay.ExpressionAsEntered) == true)
							return ActionResult.SUCCESS;
					else
						return ActionResult.RUNNING;
				}
					//agent.destination = agent.destination + new Vector3(10*Mathf.Cos(Mathf.Deg2Rad*agent.transform.eulerAngles.y),0,10*Mathf.Sin(Mathf.Deg2Rad*agent.transform.eulerAngles.y));
				if(temp != null){
					agent.SetDestination (temp.Waypoints[i].Position);
					if (distance(agent.destination) <= agent.stoppingDistance) {
						i++;
						if (temp.Waypoints.Count == i && infinite == true) {
							i = 0;
						}else if(temp.Waypoints.Count == i && infinite == false){
							i = 0;
							return ActionResult.SUCCESS;
						}
						if (ReturnByWay.IsValid && bool.Parse (ReturnByWay.ExpressionAsEntered) == true) // retorna sucess a cada waypoint chegado
							return ActionResult.SUCCESS;
					}
				}

				return ActionResult.RUNNING;
		
			}else{
				// Não é rota
				// Ve se tem na memoria a posição Vector3
				if (typeof(Vector3) == ai.WorkingMemory.GetItemType (Move_Target.ExpressionAsEntered)) {
					agent.SetDestination (ai.WorkingMemory.GetItem<Vector3> (Move_Target.ExpressionAsEntered));
				}
				// Ve se tem na memoria a posiçao GameObject
				else if (typeof(GameObject) == ai.WorkingMemory.GetItemType (Move_Target.ExpressionAsEntered)) {
					agent.SetDestination (ai.WorkingMemory.GetItem<GameObject> (Move_Target.ExpressionAsEntered).transform.position);
				}
				// Ve se tem no jogo
				else if (GameObject.Find (Move_Target.ExpressionAsEntered) != null) {
					agent.SetDestination (GameObject.Find (Move_Target.ExpressionAsEntered).transform.position);
				}
				if (MoveOnWallIfWall.IsValid && MoveOnWallIfWall.ExpressionAsEntered.Length > 0 && bool.Parse (MoveOnWallIfWall.ExpressionAsEntered) && 
					agent.destination != agent.path.corners [agent.path.corners.Length - 1]) {
					agent.destination = agent.path.corners [agent.path.corners.Length - 1];
				}
				//	agent.destination = agent.destination + new Vector3(5*Mathf.Cos(Mathf.Deg2Rad*agent.transform.eulerAngles.y),0,5*Mathf.Sin(Mathf.Deg2Rad*agent.transform.eulerAngles.y));
				// Aqui nao tem controle
				if (distance(agent.destination) <= agent.stoppingDistance) {
					return ActionResult.SUCCESS;
				}
			}
		return ActionResult.RUNNING;
		}



		return ActionResult.FAILURE;
    }
	float distance(Vector3 x){
		return Vector3.Distance (agent.transform.position,x);
	}
	 
	void recover(){
		agent.speed = speed;
		agent.angularSpeed = turnspeed;
		agent.stoppingDistance = stoppingdistance;
		agent.acceleration = acceleration_b;
		agent.ResetPath ();
	}

    public override void Stop(RAIN.Core.AI ai)
    {
		recover ();
        base.Stop(ai);
    }

}
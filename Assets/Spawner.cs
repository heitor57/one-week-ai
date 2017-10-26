using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Spawn{SpawnHumanSoldier,SpawnTiger};
[System.Serializable]
public class Spawner : MonoBehaviour {
	public Spawn Spawn;
	public float StartTime,RepeatingTime;

	// Use this for initialization
	void Start () {
		InvokeRepeating (this.Spawn.ToString(), StartTime, RepeatingTime);
	}
	private void SpawnHumanSoldier(){
		Debug.Log ("Humano");
	}
	private void SpawnTiger(){
		Debug.Log ("SpawnTiger");
	}
}

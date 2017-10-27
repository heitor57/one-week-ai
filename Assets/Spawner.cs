using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum Spawn{SpawnAnimal};
[System.Serializable]
public class Spawner : MonoBehaviour {
	public 	Spawn 	Spawn;
	public 	float 	StartTime,RepeatingTime;
	public 	int 	MaxObjects;
	public 	GameObject 	Object;
	public 	string	Faction;
	public Vector2 Area;
	public	int	charisma_i,charisma_f,
				courage_i,courage_f,
				threat_i,threat_f,
				goodness_i,goodness_f,
				leadership_i,leadership_f;
	//private List<GameObject> childs = new List<GameObject>();
	private Dictionary<GameObject,GameObject > childs = new Dictionary<GameObject,GameObject>();
	void Start () {

		foreach (Transform child in transform) {
			//childs.Add(child.gameObject);
			childs.Add (child.gameObject,null);
		}

		if (Object != null)
			InvokeRepeating (this.Spawn.ToString (), StartTime, RepeatingTime);
		else
			Debug.Log ("Missing spawn Object in: "+gameObject);
	}

	private void SpawnAnimal(){
		if (transform.childCount-childs.Count < MaxObjects) {
			//Instantiate
			GameObject obj = Instantiate (Object,transform.position+new Vector3(Random.Range(-Area.x/2,Area.x/2),0,Random.Range(-Area.y/2,Area.y/2)) ,Quaternion.Euler(new Vector3(0,Random.Range(0,360),0)),transform);

			// Modifications
			SerVivo temp = obj.GetComponent<SerVivo> ();
			temp.SetCarisma(Random.Range(charisma_i,charisma_f));
			temp.SetCoragem(Random.Range(courage_i,courage_f));
			temp.SetAmeaca(Random.Range(threat_i,threat_i));
			temp.SetBondade(Random.Range(goodness_i,goodness_f));
			temp.SetLideranca(Random.Range(leadership_i,leadership_f));
			temp.SetFacçao (Faction);
			if (childs.Count != 0) {
				GameObject child;
				if (childs.Count == MaxObjects) {
					for (int i =0;i<childs.Count;i++) {
						child = childs.Keys.ElementAt (i);
						if (childs [child] == null) {
							obj.GetComponent<AutoConfigNPC> ().route = child;
							childs [child] = obj;
						}
					}
				}else{
					child = childs.Keys.ElementAt (Random.Range(0,childs.Count));
					obj.GetComponent<AutoConfigNPC> ().route = child;
				}
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position, 0.5f);
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position,new Vector3(Area.x,0f,Area.y));
	}
}

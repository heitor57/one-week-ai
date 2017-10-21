using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorItem : MonoBehaviour {

	public Vector3 rotacao,posi;
	public GameObject prefab;
	public GameObject prefab2;
	public bool criar=false;
	public ArrayList teste = new ArrayList();
	public int j;
	public int k;


	// Use this for initialization
	public void Start () {
		teste.Add (prefab);
		teste.Add (prefab2);
	

	}
	
	// Update is called once per frame
	public void Update () {
		
		if (criar == true) {
			j = Random.Range (0, teste.Count);
			for(int i=0;i<k;i++){
				float x = Random.Range (81.0f, 100.0f);
				float z = Random.Range (200.0f, 255.0f);



				posi.Set(x,0f,z);
				Instantiate ((GameObject) teste[j], posi, Quaternion.Euler (rotacao));
			}
			criar = false;
		}
	}

	public void Criando(int k){
		criar = true;
		this.k = k;
	}
}

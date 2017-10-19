using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour {
	ArrayList a;

	void AdicionarItem(string b){
		a.Add (b);
	}

	void RemoverItem(string b){
		a.Remove (b);
	}

	void Itens(){

	}
}

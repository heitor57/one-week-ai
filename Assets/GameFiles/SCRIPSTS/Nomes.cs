using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nomes : MonoBehaviour {
	ArrayList Nome = new ArrayList();

	public string GetNome(int i)
	{
		Nome.Add ("Flor");
		Nome.Add ("Margarida");
		Nome.Add ("Cravo");
		return Nome[i].ToString();
	}

}

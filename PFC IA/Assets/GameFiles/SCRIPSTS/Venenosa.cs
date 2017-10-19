using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venenosa : Graminha {
	public Nomes n;
	void Start(){
		SetNome (n.GetNome(GetRnd ()));
		SetCor ("Verde");
		SetIdade (15);
		SetSexo ('M');

		SetVida (200.0f);
		SetFolha ("Mensurada");
		SetDano (100.0f);
	}
	void Update()
	{

	}
}

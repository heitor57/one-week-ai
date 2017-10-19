using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : Graminha {
	public Nomes n;
	void Start(){
		SetNome (n.GetNome (GetRnd ()));
		SetCor ("Verde");
		SetIdade (15);
		SetSexo ('M');
		SetVida (300.0f);
		SetFolha ("Mensurada");
		SetDano (0.0f);
	}
	void Update()
	{

	}
}

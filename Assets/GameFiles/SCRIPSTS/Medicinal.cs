using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicinal : Graminha {
	public Nomes n;
	void Start(){
		SetNome (n.GetNome(GetRnd ()));
		SetCor ("Verde");
		SetIdade (15);
		SetSexo ('M');
		SetVida (200.0f);
		SetFolha ("Mensurada");
		SetDano (-10.0f);
	}
	void Update()
	{

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gado : Animal {
	public Nomes n;

	void Start(){
		SetNome (n.GetNome(GetRnd ()));
		SetCor ("Marrom");
		SetIdade (15);
		SetSexo ('M');
		SetVida (100);
		SetLealdade (30);
	}

	public string Pastar()
	{
		return "Estou pastando";
	}
}

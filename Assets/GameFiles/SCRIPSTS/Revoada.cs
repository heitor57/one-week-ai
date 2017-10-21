using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revoada : Animal {
	public Nomes n;

	void Start(){
		SetNome (n.GetNome(GetRnd ()));
		SetCor ("Branco");
		SetIdade (15);
		SetSexo ('M');
		SetVida (100);
		SetLealdade (30);
	}

	public string Voar(){
		return "Estou Voando";
	}
}

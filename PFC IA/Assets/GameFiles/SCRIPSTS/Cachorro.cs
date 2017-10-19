using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cachorro : Animal {
	public Nomes n;

	void Start(){
		SetNome (n.GetNome(GetRnd ()));
		SetCor ("Marrom");
		SetIdade (15);
		SetSexo ('M');
		SetVida (100);
		SetLealdade (30);
	}

	public string Atacar()
	{
		return "Estou avançando";
	}
}

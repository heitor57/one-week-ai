using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arvore : Graminha {
	public Nomes n;
	void Start(){
		SetNome (n.GetNome (GetRnd ()));
		SetCor ("Verde");
		SetIdade (15);
		SetSexo ('M');
		SetVida (300.0f);
		SetFolha ("Mensurada");
		SetDano (0.0f);
		/*
		 * Quando estiver pronto a parte das animaçoes do ataque dele as arvores
		 * Criar uma condiçao para definir o quanto de dano ele vai levar e o quanto de dano a arvore vai levar.
		*/
	}

}

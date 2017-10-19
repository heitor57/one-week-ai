using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campones : Plebeu {
	



	public string Cultivar()
	{
		return "Estou cultivando";
	}
	public string Forjar()
	{
		return "Estou forjando";
	}
	public string Construir()
	{
		return "Estou construindo";
	}
	void Start(){
		SetNome ("Basilio");
		SetCor ("Branco");
		SetIdade (32);
		SetSexo ('M');
		SetProfissao ("Programador");
		SetVida (100);
		SetDano (0);
	}



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sudito : SerHumano {
	public string Servir(Realeza membro){
		return "Estou servindo a "+membro.GetNome();
	}
}

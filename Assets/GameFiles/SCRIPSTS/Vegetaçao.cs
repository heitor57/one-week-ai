using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vegetaçao : SerVivo {
	[SerializeField]
	protected string folha;

	public void SetFolha(string folha)
	{
		this.folha = folha;
	}
	
}

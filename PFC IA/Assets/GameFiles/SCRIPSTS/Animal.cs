using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : SerVivo {
	[SerializeField] protected int lealdade;

	public void SetLealdade(int lealdade){
		this.lealdade = lealdade;
	}
	public int GetLealdade()
	{
		return this.lealdade;
	}
}

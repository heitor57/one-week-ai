using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public abstract class  Destrutiveis : MonoBehaviour {
	
	[SerializeField]protected float vida;
	[SerializeField]protected float dano;
	[SerializeField]protected string nome;
	[SerializeField]protected int rnd;

	public int GetRnd()
	{
		
		return rnd = UnityEngine.Random.Range (1, 3);
	}
	public virtual void perdeVida (float dano, CharacterBase cb)
	{
		this.vida -= dano;
		if (vida <= 0)
		{
			Destroy (gameObject);
		}
	}
	public string GetNome()
	{
		return this.nome;
	}
	public float GetVida()
	{
		return this.vida;
	}
	public float GetDano()
	{
		return this.dano;
	}
	public void SetDano(float dano)
	{
		this.dano = dano;
	}
	public void SetNome(string nome)
	{
		this.nome = nome;
	}
	public void SetVida(float vida)
	{
		this.vida = vida;
	}
		
}

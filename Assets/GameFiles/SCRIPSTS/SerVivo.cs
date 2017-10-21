using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class SerVivo : CharacterBase {
	[SerializeField]
	protected char sexo;
	protected string cor;
	protected int idade;

	public string Parado()
	{
		return "Estou parado";
	}
	public string Andar()
	{
		return "Estou andando";
	}
	public string Correr()
	{
		return "Estou correndo";
	}
	public string SeguirRota()
	{
		return "Estou seguindo uma rota";
	}


	public char GetSexo()
	{
		return this.sexo;
	}
	public string GetCor()
	{
		return this.cor;
	}
	public int GetIdade()
	{
		return this.idade;
	}

	public void SetSexo(char sex)
	{
		this.sexo = sex;
	}
	public void SetCor(string cor)
	{
		this.cor = cor;
	}
	public void SetIdade(int idad)
	{
		this.idade = idad;
	}
}

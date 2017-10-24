using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class SerVivo : CharacterBase {
	[SerializeField]protected int bondade=0;
	[SerializeField]protected int carisma=0;
	[SerializeField]protected int coragem=0;
	[SerializeField]protected int influencia=0;
	[SerializeField]protected int lealdade=0;
	[SerializeField]protected int ameaca=0;
	[SerializeField]protected int lideranca=0;
	[SerializeField]protected int inteligencia=0;
	[SerializeField]protected string facçao="Neutro";
	[SerializeField]
	protected char sexo;
	protected string cor;
	protected int idade;



	public string GetFacçao(){
		return facçao;
	}
	public void SetFacçao(string facçao){
		this.facçao = facçao;
	}
	public void Interagir(SerHumano o)
	{
		Debug.Log("Estou interagindo com "+o.GetNome());
	}
	public string Montar()
	{
		return "Estou montado";
	}
	public string TerAnimais()
	{
		return "Estou pegando um animal para min";
	}
	public string TerFilhos()
	{
		return "Estou fazendo filhos";
	}
	public string MeusAnimais()
	{
		return "Meus animais sao";
	}
	public string Comandar()
	{
		return "Estou comandando";
	}

	public int GetBondade()
	{
		return this.bondade;
	}
	public int GetLealdade()
	{
		return this.lealdade;
	}
	public int GetCarisma()
	{
		return this.carisma;
	}
	public int GetCoragem()
	{
		return this.coragem;
	}
	public int GetInfluencia()
	{
		return this.influencia;
	}
	public int GetAmeaca()
	{
		return this.ameaca;
	}
	public int GetLideranca()
	{
		return this.lideranca;
	}
	public void SetBondade(int bondade)
	{
		this.bondade = bondade;
	}
	public void SetLealdade(int lealdade)
	{
		this.lealdade = lealdade;
	}
	public void SetCarisma(int carisma)
	{
		this.carisma = carisma;
	}
	public void SetCoragem(int coragem)
	{
		this.coragem = coragem;
	}
	public void SetInfluencia(int influencia)
	{
		this.influencia = influencia;
	}
	public void SetAmeaca(int ameaca)
	{
		this.ameaca = ameaca;
	}
	public void SetLideranca(int lideranca)
	{
		this.lideranca = lideranca;
	}

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

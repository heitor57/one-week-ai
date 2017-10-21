using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frases : MonoBehaviour {
	//Arraylist que vai guardar as frases
	ArrayList Frase = new ArrayList();
	//metodo que vai puxar as frases
	public string GetFrase()
	{
		//para a frase ser aleatoria
		int rnd=UnityEngine.Random.Range(0,Frase.Count-1);
		return Frase[rnd].ToString();
	}
	//inserir novas frases
	public void SetFrase(string frase){
		Frase.Add ("Bem");
		Frase.Add ("To levando");
		Frase.Add ("To mal");
		Frase.Add (frase);
	}
	//frases utilizadas para o teste
	void Start(){
		Frase.Add ("Bem");
		Frase.Add ("To levando");
		Frase.Add ("To mal");
	}
}

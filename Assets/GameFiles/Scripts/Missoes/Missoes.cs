using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missoes : MonoBehaviour {

	private string nome;//Nome que descreve a missão.Ex: Coleta de rochas
	private bool concluida=false;//Variável para verificação se a missão foi ou não concluida


	//Métodos de acesso e modificação dos dois atributos da classe Missoes
	public string getNome(){
		return nome;
	} 

	public bool getConcluida(){
		return concluida;
	} 

	public void setNome(string nome){
		this.nome = nome;
	} 

	public void setConcluida(bool concluida){
		this.concluida = concluida;
	} 


	//Método que realiza a verificação se a missão foi ou não concluida a partir da variável retorno
	protected bool missaoConcluida(){
		if(getConcluida() == true){
			concluida = true;
			return concluida;
		}
		concluida = false;
		return concluida;
	}
}

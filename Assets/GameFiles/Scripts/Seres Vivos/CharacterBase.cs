using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BasicStats  {
	
	public float startLife;
	public float startStm;
	public int strenght;
	public int agility;
	public float baseAttack;
	public float baseDefense;

	public void SetSTM(float a){
		this.startStm =a ;
	}
	public float GetStm(){
		return startStm;
	}
	public void SetLife(float a){
		this.startLife = a;
	}
	public float GetLife(){
		return startLife;
	}
	public void SetStr(int a){
		this.strenght = a;
	}
	public int GetStr(){
		return strenght;
	}
	public void SetAgi(int a){
		this.agility = a;
	}
	public int GetAgi(){
		return agility;
	}
	public void SetAtta(float a){
		this.baseAttack = a;
	}
	public float GetAtta(){
		return baseAttack;
	}
	public void SetDef(float a){
		this.baseDefense = a;
	}
	public float GetDef(){
		return baseDefense;
	}
}


public abstract class CharacterBase : PlayerStatsController {

	public Animator animator;
	//Basic attributes
	public int currentLevel;// = GetCurrentLevel();
	public float currentXP;// = GetCurrentXP();
	public BasicStats basicStats;


	// Use this for initialization
	protected void Awake () {
		
		SetVida (basicStats.startLife);
		SetDano (basicStats.baseAttack);
		this.currentXP = GetCurrentXP ();
		this.currentLevel = GetCurrentLevel ();
	}
	// Update is called once per frame


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BasicStats{
	public float startLife;
	public float startStm;
	public int strenght;
	public int agility;
	public float baseAttack;
	public float baseDefense;
}


public abstract class CharacterBase : PlayerStatsController {

	public Animator animator;
	//Basic attributes
	public int currentLevel;
	public BasicStats basicStats;


	// Use this for initialization
	protected void Awake() {
		
		SetVida (basicStats.startLife);
		SetDano (basicStats.baseAttack);
	}
	
	// Update is called once per frame
	protected void Update () {
		
	}

}

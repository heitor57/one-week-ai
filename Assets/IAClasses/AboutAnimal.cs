using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Constants{
	public const int amountfeelings = 6;
	public const int charisma = 0;
	public const int violency = 1;
	public const int courage = 2;
	public const int threat = 3;
	public const int goodness = 4;
	public const int leadership = 5;
}

public class AboutAnimal : Object {
	protected float[] feelings;
	protected string faction;

	protected float friendly_faction = 20f;
	protected float multiplier = 0;
	protected GameObject target;
	protected CharacterBase characbase;
	public GameObject Target {
		get {
			return this.target;
		}
		set {
			target = value;
		}
	}


	public CharacterBase Characbase {
		get {
			return this.characbase;
		}
		set {
			characbase = value;
		}
	}
	public float[] Feelings {
		get {
			return this.feelings;
		}
		set {
			feelings = value;
		}
	}

	public AboutAnimal(){

	}
	public AboutAnimal(GameObject target){
		Target = target;
		feelings = new float[Constants.amountfeelings];
		feelings [Constants.courage] = target.GetComponent<SerVivo> ().GetCoragem ();
		feelings [Constants.charisma] = target.GetComponent<SerVivo> ().GetCarisma ();
		feelings [Constants.threat] = target.GetComponent<SerVivo> ().GetAmeaca ();
		feelings [Constants.goodness] = target.GetComponent<SerVivo> ().GetBondade ();
		feelings [Constants.leadership] = target.GetComponent<SerVivo> ().GetLideranca ();
		feelings [Constants.violency] = 0;// implementado porem ele nao tem uma opiniao sobre a violencia
		characbase=  target.GetComponent<CharacterBase> ();
		faction =  target.GetComponent<SerVivo> ().GetFacçao ();

	}
	public AboutAnimal (GameObject target, float[] feelings, CharacterBase characbase, string faction){
		Target = target;
		this.feelings = feelings;
		this.characbase = characbase;
		this.faction = faction;
	}

	public int level(){
		return characbase.currentLevel;
	}

	public bool goBattle(SerVivo whowantstoknow){
		if( PotencyTogoBattle(whowantstoknow) >= 0 )
			return true;
		return false;
	}

	public float PotencyTogoBattle(SerVivo whowantstoknow){
		return whowantstoknow.GetCoragem()+feelings[Constants.violency]/5 + whowantstoknow.currentLevel*2 
			- feelings[Constants.threat]-level()*2;
	}

	public bool isEnemy(SerVivo whowantstoknow){
		multiplier = 0;
		if (whowantstoknow.GetFacçao() == faction)
			multiplier = 2;
		if ( feelings [Constants.violency] +feelings [Constants.threat] - feelings [Constants.charisma]  -friendly_faction*multiplier >= 40 || feelings[Constants.violency]>0 && (whowantstoknow.GetVida()/whowantstoknow.basicStats.startLife)<=0.5)
			return true;
		return false;
	}

	public bool isEnemyWeak(SerVivo whowantstoknow){
		multiplier = 0;
		if (whowantstoknow.GetFacçao() == faction)
			multiplier = 1;
		if (whowantstoknow.currentLevel - level() > -4 && goBattle(whowantstoknow) && 
			whowantstoknow.GetBondade()+multiplier*friendly_faction <= 30)
			return true;
		return false;
	}


	public static bool isGoingHelpUnknown(SerVivo whowantstoknow){
		if ((3*whowantstoknow.GetBondade() + 2*whowantstoknow.GetCoragem()) / 5 + whowantstoknow.currentLevel * 2 >= 55)
			return true;
		return false;
	}
	// Liderença
	public bool isLeader(SerVivo whowantstoknow){
		if (isFriendly(whowantstoknow) && 
			whowantstoknow.GetFacçao() == faction && 
			PotencyToLead() >= 80 ) {
			return true;
		}
		return false;
	}

	public float PotencyToLead(){
		return (feelings [Constants.leadership] + feelings [Constants.charisma]) / 2;
	}
	// Amigavel
	public bool isFriendly(SerVivo whowantstoknow){

		if (PotencyToFriendly(whowantstoknow) >= 60)
			return true;
		return false;
	}

	public float PotencyToFriendly(SerVivo whowantstoknow){
		if (!isEnemy (whowantstoknow) && !isEnemyWeak (whowantstoknow)) {
			multiplier = 0;
			if (whowantstoknow.GetFacçao() == faction)
				multiplier = 5;
			return friendly_faction * multiplier + feelings [Constants.charisma] 
				- feelings [Constants.threat] - feelings [Constants.violency];
		}
		return 0;
	}	
	public bool isGoingHelp(SerVivo whowantstoknow){
		if (isFriendly(whowantstoknow) && PotencyToGoingHelp(whowantstoknow) >= 55)
			return true;
		return false;
	}
	public float PotencyToGoingHelp(SerVivo whowantstoknow){
		multiplier = 0;
		if (whowantstoknow.GetFacçao() == faction)
			multiplier = 1.5f;
		return (3*whowantstoknow.GetBondade() + 2*feelings[Constants.charisma]) / 5 + friendly_faction*multiplier;
	}


}

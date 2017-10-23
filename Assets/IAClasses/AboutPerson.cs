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
public class AboutPerson : Object {
	private GameObject person;
	private float[] feelings;
	private CharacterBase characbase;
	private string faction;

	private float friendly_faction = 20f;
	private float multiplier = 0;

	public GameObject Person {
		get {
			return this.person;
		}
		set {
			person = value;
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

	public AboutPerson(){
		
	}
	public AboutPerson(GameObject person){
		this.person = person;
		feelings = new float[Constants.amountfeelings];
		feelings [Constants.courage] = person.GetComponent<SerHumano> ().GetCoragem ();
		feelings [Constants.charisma] = person.GetComponent<SerHumano> ().GetCarisma ();
		feelings [Constants.threat] = person.GetComponent<SerHumano> ().GetAmeaca ();
		feelings [Constants.goodness] = person.GetComponent<SerHumano> ().GetBondade ();
		feelings [Constants.leadership] = person.GetComponent<SerHumano> ().GetLideranca ();
		feelings [Constants.violency] = 0;// implementado porem ele nao tem uma opiniao sobre a violencia
		characbase=  person.GetComponent<CharacterBase> ();
		faction =  person.GetComponent<SerHumano> ().GetFacçao ();

	}
	public AboutPerson (GameObject person, float[] feelings, CharacterBase characbase, string faction){
		this.person = person;
		this.feelings = feelings;
		this.characbase = characbase;
		this.faction = faction;
	}

	public int level(){
		return characbase.currentLevel;
	}

	public bool goBattle(SerHumano whowantstoknow){
		if( PotencyTogoBattle(whowantstoknow) >= 0 )
			return true;
		return false;
	}

	public float PotencyTogoBattle(SerHumano whowantstoknow){
		return (3*whowantstoknow.GetCoragem()+2*feelings[Constants.violency])/5 + whowantstoknow.currentLevel*2 
			- feelings[Constants.threat]-level()*2;
	}

	public bool isEnemy(SerHumano whowantstoknow){
		multiplier = 0;
		if (whowantstoknow.GetFacçao() == faction)
			multiplier = 2;
		if ( feelings [Constants.violency] +feelings [Constants.threat] - feelings [Constants.charisma]  -friendly_faction*multiplier >= 40 || feelings[Constants.violency]>0 && (whowantstoknow.GetVida()/whowantstoknow.basicStats.startLife)<=0.5)
			return true;
		return false;
	}

	public bool isEnemyWeak(SerHumano whowantstoknow){
		multiplier = 0;
		if (whowantstoknow.GetFacçao() == faction)
			multiplier = 1;
		if (whowantstoknow.currentLevel - level() > -4 && goBattle(whowantstoknow) && 
			whowantstoknow.GetBondade()+multiplier*friendly_faction <= 30)
			return true;
		return false;
	}


	public static bool isGoingHelpUnknown(SerHumano whowantstoknow){
		if ((3*whowantstoknow.GetBondade() + 2*whowantstoknow.GetCoragem()) / 5 + whowantstoknow.currentLevel * 2 >= 55)
			return true;
		return false;
	}
	// Liderença
	public bool isLeader(SerHumano whowantstoknow){
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
	public bool isFriendly(SerHumano whowantstoknow){
		
		if (PotencyToFriendly(whowantstoknow) >= 60)
			return true;
		return false;
	}

	public float PotencyToFriendly(SerHumano whowantstoknow){
		if (!isEnemy (whowantstoknow) && !isEnemyWeak (whowantstoknow)) {
			multiplier = 0;
			if (whowantstoknow.GetFacçao() == faction)
				multiplier = 5;
			return friendly_faction * multiplier + feelings [Constants.charisma] 
					- feelings [Constants.threat] - feelings [Constants.violency];
		}
		return 0;
	}	
	public bool isGoingHelp(SerHumano whowantstoknow){
		if (isFriendly(whowantstoknow) && PotencyToGoingHelp(whowantstoknow) >= 55)
			return true;
		return false;
	}
	public float PotencyToGoingHelp(SerHumano whowantstoknow){
		multiplier = 0;
		if (whowantstoknow.GetFacçao() == faction)
			multiplier = 1.5f;
		return (3*whowantstoknow.GetBondade() + 2*feelings[Constants.charisma]) / 5 + friendly_faction*multiplier;
	}


}


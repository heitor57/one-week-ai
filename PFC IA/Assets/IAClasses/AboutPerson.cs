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
	private short multiplier = 0;

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
	public AboutPerson (GameObject person, float[] feelings, CharacterBase characbase, string faction)
	{
		this.person = person;
		this.feelings = feelings;
		this.characbase = characbase;
		this.faction = faction;
	}
	public int level(){
		return characbase.currentLevel;
	}
	public bool goBattle(SerHumano whowantstoknow){
		if( (3*whowantstoknow.GetCoragem()+2*feelings[Constants.violency])/5 + whowantstoknow.currentLevel*2 >= feelings[Constants.threat]+level()*2 )
			return true;
		return false;
	}

	public bool isEnemy(SerHumano whowantstoknow){
		multiplier = 0;
		if (whowantstoknow.GetFacçao() == faction)
			multiplier = 2;
		if ( feelings [Constants.violency] +feelings [Constants.threat] - feelings [Constants.charisma]  -friendly_faction*multiplier >= 40)
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

	public bool isFriendly(SerHumano whowantstoknow){
		if (!isEnemy (whowantstoknow) && !isEnemyWeak (whowantstoknow)) {
			multiplier = 0;
			if (whowantstoknow.GetFacçao() == faction)
				multiplier = 1;
			if (friendly_faction * multiplier + feelings [Constants.charisma] 
				- feelings [Constants.threat] - feelings [Constants.violency] >= 60)
				return true;
		}
		return false;
	}
	public static bool isGoingHelp(SerHumano whowantstoknow){
		if ((3*whowantstoknow.GetBondade() + 2*whowantstoknow.GetCoragem()) / 5 + whowantstoknow.currentLevel * 2 >= 55)
			return true;
		return false;
	}

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

}


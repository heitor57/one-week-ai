using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AboutPerson : AboutAnimal {
	public AboutPerson(){

	}
	public AboutPerson(GameObject person){
		Target = person;
		feelings = new float[Constants.amountfeelings];
		feelings [Constants.courage] = person.GetComponent<SerVivo> ().GetCoragem ();
		feelings [Constants.charisma] = person.GetComponent<SerVivo> ().GetCarisma ();
		feelings [Constants.threat] = person.GetComponent<SerVivo> ().GetAmeaca ();
		feelings [Constants.goodness] = person.GetComponent<SerVivo> ().GetBondade ();
		feelings [Constants.leadership] = person.GetComponent<SerVivo> ().GetLideranca ();
		feelings [Constants.violency] = 0;// implementado porem ele nao tem uma opiniao sobre a violencia
		characbase=  person.GetComponent<CharacterBase> ();
		faction =  person.GetComponent<SerVivo> ().GetFacçao ();

	}
	public AboutPerson (GameObject person, float[] feelings, CharacterBase characbase, string faction){
		Target = person;
		this.feelings = feelings;
		this.characbase = characbase;
		this.faction = faction;
	}

}


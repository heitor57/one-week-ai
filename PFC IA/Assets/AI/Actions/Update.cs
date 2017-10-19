using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities.Aspects;
using RAIN.Perception.Sensors;
using System.Linq;
[RAINAction]
public class Update : RAINAction
{
	int time;
	int i,j;

	List<AboutPerson> aboutperson;
	SerHumano aboutme;
	Dictionary<AboutPerson,bool> alreadyattacked;

	public override void Start(RAIN.Core.AI ai)
    {
		/*
		tempfloat = new float[Constants.amountfeelings];
		tempfloat[Constants.courage] = persontemp.GetComponent<SerHumano>().GetCoragem();
		tempfloat[Constants.charisma] = persontemp.GetComponent<SerHumano>().GetCarisma();
		tempfloat[Constants.intimidation] = persontemp.GetComponent<SerHumano>().GetAmeaca();
		tempfloat[Constants.goodness] = persontemp.GetComponent<SerHumano>().GetBondade();
		tempfloat [Constants.leadership] = persontemp.GetComponent<SerHumano> ().GetLideranca ();
		tempfloat[Constants.violency] = 0;// implementado porem ele nao tem uma opiniao sobre a violencia
		aboutperson.Add (new AboutPerson(persontemp,
			tempfloat, persontemp.GetComponent<CharacterBase>(),persontemp.GetComponent<SerHumano> ().GetFacçao ()));
		alreadyattacked.Add (aboutperson[aboutperson.Count -1],false);
		*/
		aboutme = ai.Body.GetComponent<SerHumano> ();
		aboutperson = (List<AboutPerson>)ai.WorkingMemory.GetItem ("aboutperson");
		alreadyattacked = ai.WorkingMemory.GetItem<Dictionary<AboutPerson,bool>> ("alreadyattacked");
		base.Start(ai);

    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		
		time = ai.WorkingMemory.GetItem<int>("time");
		if (time != 1800)
			ai.WorkingMemory.SetItem<int> ("time", time + 1);
		else 
			ai.WorkingMemory.SetItem<int> ("time", 0);

		// libera a detecçao de ataque pra certa pessoa
		for (i = 0; i < alreadyattacked.Count; i++){
			alreadyattacked [alreadyattacked.Keys.ElementAt (i)] = false;
		}
		// Algoritmo para visualização das caracteristicas das pessoas oo redor
		// **Inicio
		GameObject persontemp;
		float[] tempfloat;

		for (i = 0; i < aboutperson.Count; i++) 
			if (aboutperson [i].Person == null)
				aboutperson.RemoveAt (i);
		
		for ( i = 0; i < ((List<GameObject>)ai.WorkingMemory.GetItem ("person")).Count; i++) {
			persontemp = ((List<GameObject>)ai.WorkingMemory.GetItem ("person")) [i];
			
			if (aboutperson.Count == 0) {
				if (alreadyattacked.Count > 0)
					alreadyattacked.Clear ();
				tempfloat = new float[Constants.amountfeelings];

				tempfloat [Constants.courage] = persontemp.GetComponent<SerHumano> ().GetCoragem ();
				tempfloat [Constants.charisma] = persontemp.GetComponent<SerHumano> ().GetCarisma ();
				tempfloat [Constants.threat] = persontemp.GetComponent<SerHumano> ().GetAmeaca ();
				tempfloat [Constants.goodness] = persontemp.GetComponent<SerHumano> ().GetBondade ();
				tempfloat [Constants.leadership] = persontemp.GetComponent<SerHumano> ().GetLideranca ();
				tempfloat [Constants.violency] = 0;// implementado porem ele nao tem uma opiniao sobre a violencia
				aboutperson.Add (new AboutPerson (persontemp,
					tempfloat, persontemp.GetComponent<CharacterBase> (), persontemp.GetComponent<SerHumano> ().GetFacçao ()));
				alreadyattacked.Add (aboutperson [aboutperson.Count - 1], false);
				
			} else {
				for (j = 0; j < aboutperson.Count; j++) {
					if (aboutperson [j].Person == persontemp) {
						break;
					}
					if (j + 1 == aboutperson.Count) {
						tempfloat = new float[Constants.amountfeelings];
						tempfloat [Constants.courage] = persontemp.GetComponent<SerHumano> ().GetCoragem ();
						tempfloat [Constants.charisma] = persontemp.GetComponent<SerHumano> ().GetCarisma ();
						tempfloat [Constants.threat] = persontemp.GetComponent<SerHumano> ().GetAmeaca ();
						tempfloat [Constants.goodness] = persontemp.GetComponent<SerHumano> ().GetBondade ();
						tempfloat [Constants.leadership] = persontemp.GetComponent<SerHumano> ().GetLideranca ();
						tempfloat [Constants.violency] = 0;// implementado porem ele nao tem uma opiniao sobre a violencia
						aboutperson.Add (new AboutPerson (persontemp,
							tempfloat, persontemp.GetComponent<CharacterBase> (), persontemp.GetComponent<SerHumano> ().GetFacçao ()));
						if (!alreadyattacked.ContainsKey (aboutperson [aboutperson.Count - 1]))
							alreadyattacked.Add (aboutperson [aboutperson.Count - 1], false);
						break;
					}
				}
			}
		
		}

		// Pessoas que a IA consegue ver
		List<AboutPerson> observableperson = new List<AboutPerson>(); 
		for (i = 0; i < ((List<GameObject>)ai.WorkingMemory.GetItem ("person")).Count; i++) {
			for (j = 0; j < aboutperson.Count; j++) {
				if (aboutperson [j].Person == ai.WorkingMemory.GetItem<List<GameObject>> ("person") [i]) {
					observableperson.Add (aboutperson [j]);
					break;
				}
			}
		}
		// Atribuir "macrosentimentos"
		List<AboutPerson> enemyperson = new List<AboutPerson>(); 
		List<AboutPerson> enemyweakperson = new List<AboutPerson>(); 
		List<AboutPerson> leaderperson = new List<AboutPerson>(); 
		for(i = 0 ; i<observableperson.Count;i++ ){
			if(observableperson[i].isEnemy(aboutme) == true){ // esse o cara sempre deve atacar se tiver
				enemyperson.Add (observableperson [i]);
			}
			if(observableperson[i].isEnemyWeak(aboutme) == true){
				enemyweakperson.Add (observableperson [i]);
			}
			if(observableperson[i].isLeader(aboutme) == true){ // LEIA --- nao implementado
				leaderperson.Add (observableperson [i]);
			}
		}

		float radius = ((VisualSensor)ai.Senses.GetSensor("Visual Sensor")).Range;
		AboutPerson enemygo = new AboutPerson(),  enemyweakgo = new AboutPerson(), BestLeader = new AboutPerson();

		// inimigos normais mais proximos
		enemygo = MostClose(enemyperson , ai, radius);
		enemyweakgo = MostClose(enemyweakperson , ai, radius);

		for (i = 0; i < leaderperson.Count; i++) {
			if (i != 0) {
				if (leaderperson [i].PotencyToLead () >= BestLeader.PotencyToLead ())
					BestLeader = leaderperson [i];	
			} else {
				BestLeader = leaderperson[i];
			}
		}


		if (enemygo.Person != null) {
			ai.WorkingMemory.SetItem<GameObject> ("EnemyGo", enemygo.Person);
			ai.WorkingMemory.SetItem<bool> ("GoBattle", enemygo.goBattle (aboutme));
		} else if (enemyweakgo.Person != null && enemyweakperson.Count <= 3) {
			ai.WorkingMemory.SetItem<GameObject> ("EnemyGo", enemyweakgo.Person);
			ai.WorkingMemory.SetItem<bool> ("GoBattle", enemyweakgo.goBattle (aboutme));// poderia utilizar true
		} else {
			ai.WorkingMemory.SetItem<GameObject> ("EnemyGo", null);
		}
		if (BestLeader.Person != null) {
			ai.WorkingMemory.SetItem<GameObject> ("Leader", BestLeader.Person);
		} else {
			ai.WorkingMemory.SetItem<GameObject> ("Leader", null);
		}
		// **Fim
		// Algoritmo para visualização das caracteristicas das pessoas oo redor
        return ActionResult.SUCCESS;
    }
	public AboutPerson MostClose(List<AboutPerson> persons,AI ai ,float radius){
		AboutPerson person = new AboutPerson ();
		for(i = 0; i < persons.Count ;i++){
			if (Vector3.Distance (persons [i].Person.transform.position, ai.Body.transform.position) <= radius) {
				radius = Vector3.Distance (persons[i].Person.transform.position, ai.Body.transform.position);
				person = persons [i]; 
			}
		}
		return person;
	}
    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}
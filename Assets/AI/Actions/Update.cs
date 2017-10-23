using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities.Aspects;
using RAIN.Perception.Sensors;
using System.Linq;
using RAIN.Entities;
using RAIN.Entities.Aspects;
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


		for (i = 0; i < aboutperson.Count; i++) 
			if (aboutperson [i].Person == null)
				aboutperson.RemoveAt (i);
		
		for ( i = 0; i < ((List<GameObject>)ai.WorkingMemory.GetItem ("person")).Count; i++) {
			persontemp = ((List<GameObject>)ai.WorkingMemory.GetItem ("person")) [i];
			
			if (aboutperson.Count == 0) {
				if (alreadyattacked.Count > 0)
					alreadyattacked.Clear ();
				
				aboutperson.Add (new AboutPerson(persontemp));
				alreadyattacked.Add (aboutperson [aboutperson.Count - 1], false);
				
			} else {
				for (j = 0; j < aboutperson.Count; j++) {
					if (aboutperson [j].Person == persontemp) {
						break;
					}
					if (j + 1 == aboutperson.Count) {
						aboutperson.Add (new AboutPerson(persontemp));
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
		List<AboutPerson> friendlyperson = new List<AboutPerson>(); 
		List<AboutPerson> friendlypersonwithenemy = new List<AboutPerson>(); 
		// Categorizando
		for(i = 0 ; i<observableperson.Count;i++ ){
			if(observableperson[i].isEnemy(aboutme) == true){ // esse o cara sempre deve atacar se tiver
				enemyperson.Add (observableperson [i]);
			}
			if(observableperson[i].isEnemyWeak(aboutme) == true){
				enemyweakperson.Add (observableperson [i]);
			}
			if(observableperson[i].isLeader(aboutme) == true){ 
				leaderperson.Add (observableperson [i]);
			}
			if(observableperson[i].isFriendly(aboutme) == true){ 
				friendlyperson.Add (observableperson [i]);
				if(observableperson[i].Person.GetComponent<AIRig>().AI.WorkingMemory.GetItem<GameObject>("EnemyGo") != null){
					friendlypersonwithenemy.Add (observableperson [i]);
				}
			}
		}

		float radius = ((VisualSensor)ai.Senses.GetSensor("Visual Sensor")).Range;
		AboutPerson enemygo = new AboutPerson(),  enemyweakgo = new AboutPerson(), bestleader = new AboutPerson(),
		bestfriendly = new AboutPerson(), bestfriendlywithenemy = new AboutPerson();

		// inimigos mais proximos
		enemygo = MostClose(enemyperson , ai, radius);
		enemyweakgo = MostClose(enemyweakperson , ai, radius);
		// Busca do melhor lider
		for (i = 0; i < leaderperson.Count; i++) {
			if (i != 0) {
				if (leaderperson [i].PotencyToLead () >= bestleader.PotencyToLead ())
					bestleader = leaderperson [i];	
			} else {
				bestleader = leaderperson[i];
			}
		}

		// Busca da pessoa mais amigavel
		for (i = 0; i < friendlyperson.Count; i++) {
			if (i != 0) {
				if (friendlyperson [i].PotencyToFriendly(aboutme) >= bestfriendly.PotencyToFriendly(aboutme))
					bestfriendly = friendlyperson [i];	
			} else {
				bestfriendly = friendlyperson[i];
			}
		}
		// Busca da pessoa mais amigavel com inimigo
		for (i = 0; i < friendlypersonwithenemy.Count; i++) {
			if (i != 0) {
				if (friendlypersonwithenemy [i].PotencyToFriendly(aboutme) >= bestfriendlywithenemy.PotencyToFriendly(aboutme))
					bestfriendlywithenemy = friendlypersonwithenemy [i];	
			} else {
				bestfriendlywithenemy = friendlypersonwithenemy[i];
			}
		}


		//Setagem dos atributos para utilização da IA
		if (enemygo.Person != null) {
			ai.WorkingMemory.SetItem<GameObject> ("EnemyGo", enemygo.Person);
			ai.WorkingMemory.SetItem<bool> ("GoBattle", enemygo.goBattle (aboutme));
		} else if (enemyweakgo.Person != null && enemyweakperson.Count <= 3) {
			ai.WorkingMemory.SetItem<GameObject> ("EnemyGo", enemyweakgo.Person);
			ai.WorkingMemory.SetItem<bool> ("GoBattle", enemyweakgo.goBattle (aboutme));// poderia utilizar true
		} else if (bestfriendlywithenemy.Person != null) {
			GameObject bestfriendlyenemy = bestfriendlywithenemy.Person.GetComponent<AIRig> ().AI.WorkingMemory.GetItem<GameObject> ("EnemyGo");
			AboutPerson bestfriendlyenemyabout= new AboutPerson(bestfriendlyenemy);

			Debug.Log (bestfriendlyenemyabout.PotencyTogoBattle (aboutme)+" |||| "+ bestfriendlywithenemy.PotencyToGoingHelp (aboutme));
			if (bestfriendlywithenemy.isGoingHelp (aboutme) &&
			    bestfriendlyenemyabout.PotencyTogoBattle (aboutme) + bestfriendlywithenemy.PotencyToGoingHelp (aboutme) >= 45) {
				ai.WorkingMemory.SetItem<GameObject> ("EnemyGo", bestfriendlyenemy);
				ai.WorkingMemory.SetItem<bool> ("GoBattle", true);
			} else {
				ai.WorkingMemory.SetItem<GameObject> ("EnemyGo", null);
			}

		} else {
			ai.WorkingMemory.SetItem<GameObject> ("EnemyGo", null);
		}
		if (bestleader.Person != null) {
			ai.WorkingMemory.SetItem<GameObject> ("Leader", bestleader.Person);
			((SlotAspect)ai.Body.GetComponent<EntityRig> ().Entity.GetAspect ("slot")).Head = bestleader.Person;
		} else {
			ai.WorkingMemory.SetItem<GameObject> ("Leader", null);
			((SlotAspect)ai.Body.GetComponent<EntityRig> ().Entity.GetAspect ("slot")).Head = null;
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
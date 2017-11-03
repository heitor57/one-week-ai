using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities.Aspects;
using RAIN.Perception.Sensors;
using System.Linq;
using RAIN.Entities;
[RAINAction]
public class Update : RAINAction
{
	int time;
	int i,j;
	List<AboutAnimal> aboutanimal;
	List<AboutPerson> aboutperson;
	SerVivo aboutme;
	Dictionary<AboutAnimal,bool> alreadyattacked;

	public override void Start(RAIN.Core.AI ai)
    {
		
		if(ai.Body.GetComponent<SerHumano> () != null){
			aboutme = ai.Body.GetComponent<SerHumano> ();
		}else if(ai.Body.GetComponent<SerVivo> () != null){
			aboutme = ai.Body.GetComponent<SerVivo> ();
		}
		aboutanimal = (List<AboutAnimal>)ai.WorkingMemory.GetItem ("aboutanimal");
		alreadyattacked = ai.WorkingMemory.GetItem<Dictionary<AboutAnimal,bool>> ("alreadyattacked");
		base.Start(ai);

    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		aboutperson = new List<AboutPerson> ();
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


		for (i = 0; i < aboutanimal.Count; i++) {
			if (aboutanimal [i].Target == null)
				aboutanimal.RemoveAt (i);
			
		}

		for ( i = 0; i < ((List<GameObject>)ai.WorkingMemory.GetItem ("animal")).Count; i++) {
			persontemp = ((List<GameObject>)ai.WorkingMemory.GetItem ("animal")) [i];

			if (aboutanimal.Count == 0) {
				if (alreadyattacked.Count > 0)
					alreadyattacked.Clear ();
				
				aboutanimal.Add (new AboutPerson(persontemp));
				alreadyattacked.Add (aboutanimal [aboutanimal.Count - 1], false);
				
			} else {
				for (j = 0; j < aboutanimal.Count; j++) {
					if (aboutanimal [j].Target == persontemp) {
						break;
					}
					if (j + 1 == aboutanimal.Count) {
						aboutanimal.Add (new AboutPerson(persontemp));
						if (!alreadyattacked.ContainsKey (aboutanimal [aboutanimal.Count - 1]))
							alreadyattacked.Add (aboutanimal [aboutanimal.Count - 1], false);
						break;
					}
				}
			}
		
		}
		for (i = 0; i < aboutanimal.Count; i++) {
			if (aboutanimal [i].GetType () == typeof(AboutPerson))
				aboutperson.Add ((AboutPerson)aboutanimal[i]);
		}
		// Pessoas que a IA consegue ver
		List<AboutPerson> observableperson = new List<AboutPerson>(); 
		for (i = 0; i < ((List<GameObject>)ai.WorkingMemory.GetItem ("animal")).Count; i++) {
			for (j = 0; j < aboutperson.Count; j++) {
				if (aboutperson [j].Target == ai.WorkingMemory.GetItem<List<GameObject>> ("animal") [i]) {
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
				if(observableperson[i].Target.GetComponent<AIRig>() !=null  && observableperson[i].Target.GetComponent<AIRig>().AI.WorkingMemory.GetItem<GameObject>("EnemyGo") != null){
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
		if (enemygo.Target != null) {
			ai.WorkingMemory.SetItem<GameObject> ("EnemyGo", enemygo.Target);
			ai.WorkingMemory.SetItem<bool> ("GoBattle", enemygo.goBattle (aboutme));
		} else if (enemyweakgo.Target != null && enemyweakperson.Count <= 3) {
			ai.WorkingMemory.SetItem<GameObject> ("EnemyGo", enemyweakgo.Target);
			ai.WorkingMemory.SetItem<bool> ("GoBattle", enemyweakgo.goBattle (aboutme));// poderia utilizar true
		} else if (bestfriendlywithenemy.Target != null) {
			GameObject bestfriendlyenemy = bestfriendlywithenemy.Target.GetComponent<AIRig> ().AI.WorkingMemory.GetItem<GameObject> ("EnemyGo");
			AboutPerson bestfriendlyenemyabout= new AboutPerson(bestfriendlyenemy);

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
		if(((SlotAspect)ai.Body.GetComponent<EntityRig> ().Entity.GetAspect ("slot")) != null){
			if (bestleader.Target != null) {
				ai.WorkingMemory.SetItem<GameObject> ("Leader", bestleader.Target);
				((SlotAspect)ai.Body.GetComponent<EntityRig> ().Entity.GetAspect ("slot")).Head = bestleader.Target;
			} else {
				ai.WorkingMemory.SetItem<GameObject> ("Leader", null);
				((SlotAspect)ai.Body.GetComponent<EntityRig> ().Entity.GetAspect ("slot")).Head = null;
			}
		}
		// **Fim
		// Algoritmo para visualização das caracteristicas das pessoas oo redor
        return ActionResult.SUCCESS;
    }

	public AboutAnimal MostClose(List<AboutAnimal> persons,AI ai ,float radius){
		AboutAnimal person = new AboutAnimal ();
		for(i = 0; i < persons.Count ;i++){
			if (Vector3.Distance (persons [i].Target.transform.position,ai.Body.transform.position) <= radius) {
				radius = Vector3.Distance (persons[i].Target.transform.position, ai.Body.transform.position);
				person = persons [i]; 
			}
		}
		return person;
	}
	public AboutPerson MostClose(List<AboutPerson> persons,AI ai ,float radius){
		AboutPerson person = new AboutPerson ();
		for(i = 0; i < persons.Count ;i++){
			if (Vector3.Distance (persons [i].Target.transform.position,ai.Body.transform.position) <= radius) {
				radius = Vector3.Distance (persons[i].Target.transform.position, ai.Body.transform.position);
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
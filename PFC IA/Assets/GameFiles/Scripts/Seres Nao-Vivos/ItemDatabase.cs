using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
	public List<Colectibles> items = new List<Colectibles>();

	void Start(){
		items.Add (new Colectibles ("Wine Bottle",0,"Garrafa de Vinho",10,20,Colectibles.ItemType.QuestItem));
	}
}

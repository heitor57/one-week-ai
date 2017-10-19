using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Colectibles : NotAlive{
	
	public string itemName;
	public double itemLife;
	public double itemDmg;
	public int itemID;
	public string itemDes;
	public Texture2D itemIcon;
	public ItemType itemType;

	public enum ItemType{
		Weapon,
		Consumable,
		QuestItem
	}
	public Colectibles(string name,int id, string desc,double life, double dmg,ItemType type){
		itemName = name;
		itemLife = life;
		itemDmg = dmg;
		itemID = id;
		itemDes = desc;
		itemIcon = Resources.Load<Texture2D> ("Item Icons/" + name);	
		itemType = type;
	}
	public Colectibles(){

	}
}

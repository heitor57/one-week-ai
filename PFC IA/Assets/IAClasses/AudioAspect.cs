using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Entities.Aspects;
using RAIN.Serialization;
// Lista de falas:
// Help
// PleaseStop
// 
static class Speaks{
	public const string help="Help";
	public const string pleasestop="PleaseStop";
}

[RAINSerializableClass]
public class AudioAspect : VisualAspect  {
	public string data;
	public AudioAspect(){ }
	public AudioAspect(string aspectName, string data) : base(aspectName){
		this.data = data;
	}
}

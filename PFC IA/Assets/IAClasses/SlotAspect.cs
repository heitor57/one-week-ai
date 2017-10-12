using UnityEngine;
using RAIN.Entities.Aspects;
using RAIN.Serialization;
[RAINSerializableClass]
public class SlotAspect : VisualAspect {
	public Vector3 Slot;
	public SlotAspect(){ }
	public SlotAspect(string aspectName) : base(aspectName){ 	}
}

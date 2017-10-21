using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameLogic{

	public static float ExperienceForNextLevel(int currentLevel)
	{
		if (currentLevel == 0)
			return 0;
		return (20*(currentLevel * currentLevel + currentLevel + 3));
	}	
}

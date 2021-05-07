using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{

	[System.Flags]
	public enum CharacterState
	{
		none = 0,
		Clear = 1,
		Stunned = 2,
		Boosted = 4
	}
}

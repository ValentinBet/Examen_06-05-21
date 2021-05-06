using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{

	[System.Flags]
	public enum CharacterState
	{
		Clear = 1 << 0
	}
}

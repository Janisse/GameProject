using UnityEngine;
using System.Collections;

public class FPSGameMode : JGameMode
{
	#region Properties
	public PlayerCharacter player = null;

	internal Level level = null;
	#endregion

	#region Class Methods
	internal override void Enter ()
	{
		base.Enter ();
	}
	#endregion
}

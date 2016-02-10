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
		JEngine.Instance.eventManager.RegisterEvent ("GoToMainMenu", GoToMainMenu);
	}

	internal override void Manage ()
	{
		base.Manage ();

		if(!isPaused)
		{
			//Manage Level
			if(level != null)
			{
				level.ManageEnemies ();
			}
		}
	}

	internal override void Exit ()
	{
		JEngine.Instance.eventManager.UnregisterEvent ("GoToMainMenu", GoToMainMenu);
		base.Exit ();
	}
	#endregion

	#region Event Methods
	private void GoToMainMenu(JEventArgs args)
	{
		Time.timeScale = 1f;
		JEngine.Instance.gameManager.changeGameMode ("MenuGameMode");
	}
	#endregion
}

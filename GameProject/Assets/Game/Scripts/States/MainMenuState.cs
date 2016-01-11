using UnityEngine;
using System.Collections;

public class MainMenuState : JState
{
	#region Class Methods
	internal override void Enter()
	{
		base.Enter();
		JEngine.Instance.eventManager.RegisterEvent("Play", OnPlayClicked);
	}
	
	internal override void Manage()
	{
		base.Manage();
	}
	
	internal override void Exit()
	{
		JEngine.Instance.eventManager.UnregisterEvent("Play", OnPlayClicked);
		base.Exit();
	}
	#endregion
	
	#region Event Methods
	internal void OnPlayClicked(JEventArgs args)
	{
		JEngine.Instance.gameManager.changeGameMode ("FPSGameMode");
	}
	#endregion
}

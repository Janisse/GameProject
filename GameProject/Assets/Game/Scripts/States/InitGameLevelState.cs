using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InitGameLevelState : JState
{
	#region Class Methods
	internal override void Enter()
	{
		base.Enter();
		JEngine.Instance.eventManager.RegisterEvent("LevelLoaded", OnLevelLoad);
		SceneManager.LoadScene ((string)JEngine.Instance.gameManager.GetParamter("LevelName"), LoadSceneMode.Additive);
	}

	internal override void Manage()
	{
		base.Manage();
	}

	internal override void Exit()
	{
		JEngine.Instance.eventManager.UnregisterEvent("LevelLoaded", OnLevelLoad);
		base.Exit();
	}
	#endregion

	#region Level Methods
	internal void OnLevelLoad(JEventArgs args)
	{
		currentGameMode.RequestState ("GameFPSState");
	}
	#endregion
}

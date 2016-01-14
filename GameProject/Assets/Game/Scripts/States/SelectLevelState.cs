using UnityEngine;
using System.Collections;

public class SelectLevelState : JState
{
	#region Properties
	private string _levelSelected = "";
	private SelectLevelPanel _levelPanel = null;
	#endregion

	#region Class Methods
	internal override void Enter()
	{
		base.Enter();
		_levelPanel = (SelectLevelPanel)JEngine.Instance.uiManager.GetPanel ("SelectLevelPanel");
		_levelPanel.InitPanel ();
		JEngine.Instance.eventManager.RegisterEvent("Play", OnPlayClicked);
		JEngine.Instance.eventManager.RegisterEvent("SelectedLevel", OnLevelSelection);
	}

	internal override void Manage()
	{
		base.Manage();
	}

	internal override void Exit()
	{
		JEngine.Instance.eventManager.UnregisterEvent("Play", OnPlayClicked);
		JEngine.Instance.eventManager.UnregisterEvent("SelectedLevel", OnLevelSelection);
		base.Exit();
	}
	#endregion

	#region Event Methods
	internal void OnPlayClicked(JEventArgs args)
	{
		if (_levelSelected != "")
		{
			JEngine.Instance.gameManager.SetParameter ("LevelName", (System.Object)_levelSelected);
			JEngine.Instance.gameManager.changeGameMode ("FPSGameMode");
		}
		else
		{
			Debug.LogError ("TODO -> Display error: Select a Level");
		}
	}

	internal void OnLevelSelection(JEventArgs args)
	{
		_levelSelected = args.strArg;
		_levelPanel.SetSelectedLevel (_levelSelected);
	}
	#endregion
}

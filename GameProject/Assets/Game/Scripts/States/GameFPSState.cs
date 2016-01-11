using UnityEngine;
using System.Collections;

public class GameFPSState : JState
{
	#region Properties
	public PlayerCharacter player = null;

	internal int point = 0;

	private PlayHUDPanel _playHUDPanel = null;
	#endregion

	#region Class Methods
	internal override void Enter()
	{
		base.Enter();
		player.Init ();
		_playHUDPanel = (PlayHUDPanel)JEngine.Instance.uiManager.GetPanel ("PlayHUDPanel");
		_playHUDPanel.SetPointText (point.ToString());

		//Events
		JEngine.Instance.eventManager.RegisterEvent ("PointChange", PointChange);
	}
	
	internal override void Manage()
	{
		base.Manage();
		player.charactMotor.Manage ();
		CheckUserActions ();
	}
	
	internal override void Exit()
	{
		JEngine.Instance.eventManager.UnregisterEvent ("PointChange", PointChange);
		base.Exit();
	}
	#endregion

	#region State Methods
	internal void CheckUserActions()
	{
		//Shoot
		if(Input.GetMouseButton(0))
		{
			player.Shoot();
		}

		//Reload
		if(JEngine.Instance.inputManager.GetInputEnter("Reload"))
		{
			player.Reload();
		}

		//Aim Enter
		if(Input.GetMouseButtonDown(1))
		{
			player.EnableAim();
		}

		//Aim Exit
		if(Input.GetMouseButtonUp(1))
		{
			player.DisableAim();
		}
	}
	#endregion

	#region Event Methods
	internal void PointChange(JEventArgs a_args)
	{
		point += (int)a_args.floatArg;
		_playHUDPanel.SetPointText (point.ToString());
	}
	#endregion
}

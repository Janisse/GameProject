using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
	#region Properties
	public Transform playerSpawn = null;

	private FPSGameMode _currentGameMode = null;
	#endregion

	#region Class Methods
	void Start ()
	{
		//Link Level and GameMode
		_currentGameMode = ((FPSGameMode)JEngine.Instance.gameManager.GetCurrentGameMode);
		_currentGameMode.level = this;

		//Place the Player in the level
		_currentGameMode.player.transform.position = playerSpawn.position;
		_currentGameMode.player.transform.rotation = playerSpawn.rotation;

		//Level is Loaded
		JEngine.Instance.eventManager.FireEvent("LevelLoaded");
	}
	#endregion
}

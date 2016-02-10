using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
	#region Properties
	public Transform playerSpawn = null;

	internal List<EnemyCharacter> enemyList = null;

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

		//Init EnemyList
		enemyList = new List<EnemyCharacter>();

		//Level is Loaded
		JEngine.Instance.eventManager.FireEvent("LevelLoaded");
	}
	#endregion

	#region Enemy Methods
	internal void RegisterEnemy(EnemyCharacter a_enemy)
	{
		enemyList.Add (a_enemy);
	}

	internal void UnregisterEnemy(EnemyCharacter a_enemy)
	{
		enemyList.Remove (a_enemy);
	}

	internal void ManageEnemies()
	{
		foreach(EnemyCharacter enemy in enemyList)
		{
			enemy.Manage ();
		}
	}
	#endregion
}

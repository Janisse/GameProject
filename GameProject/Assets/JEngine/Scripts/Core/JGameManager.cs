using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

internal class JGameManager
{
    #region Properties
	internal int currentLevelID = 1;
	internal int deathNb = 0;

    private List<string> _gameModeList = null;
    private JGameMode _currentGameMode = null;
	private Dictionary<string, System.Object> _parameters = null;
    #endregion

    #region Class Methods
    internal JGameManager()
    {
        _gameModeList = new List<string>();
		_parameters = new Dictionary<string, System.Object> ();
    }

    internal void Manage()
    {
        if(_currentGameMode != null)
            _currentGameMode.Manage();
    }

    ~JGameManager()
    {
        _gameModeList.Clear();
        _gameModeList = null;
    }
    #endregion

    #region Methods
    internal JGameMode GetCurrentGameMode
    {
        get
        {
            return _currentGameMode;
        }
    }

    internal void registerGameMode(JGameMode a_gameMode)
    {
        _currentGameMode = a_gameMode;
        _currentGameMode.Enter();
    }

    internal void changeGameMode(string a_gameModeName)
    {
        if(_currentGameMode != null)
            _currentGameMode.Exit();
		SceneManager.LoadScene(a_gameModeName, LoadSceneMode.Single);
    }

	internal void SetParameter(string a_paramName, System.Object a_paramValue)
	{
		_parameters [a_paramName] = a_paramValue;
	}

	internal System.Object GetParamter(string a_paramName)
	{
		System.Object value = null;
		_parameters.TryGetValue (a_paramName, out value);
		return value;
	}
    #endregion
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JGameMode : MonoBehaviour
{
    #region Properties
    public JState firstState = null;
    public JState[] stateTab = new JState[0];
    public string[] panelToLoadTab = new string[0];


    private Dictionary<string, JState> _stateDict = null;
    private JState _currentState = null;
    #endregion

    #region Class Management
    internal void Awake()
    {
        JEngine.Instance.gameManager.registerGameMode(this);
    }
    #endregion

    #region Class Methods
    internal virtual void Enter()
    {
        //Load Panels
        JEngine.Instance.uiManager.LoadPanels(panelToLoadTab);

        //Init State Dictionary
        _stateDict = new Dictionary<string, JState>();
        foreach(JState state in stateTab)
        {
            string stateName = state.GetType().Name;
            _stateDict.Add(stateName, state);
        }

        StartCoroutine(LoadFirstState());
    }

    internal void Manage()
    {
        if(_currentState != null)
            _currentState.Manage();
    }

    internal virtual void Exit()
    {
        if (_currentState != null)
            _currentState.Exit();

        _stateDict.Clear();
        _stateDict = null;

        JEngine.Instance.uiManager.UnloadPanels(panelToLoadTab);
    }
    #endregion

    #region Methods
    internal void RequestState(string a_stateName)
    {
        if (_currentState != null)
            _currentState.Exit();
        _stateDict.TryGetValue(a_stateName, out _currentState);
        _currentState.Enter();
    }

    internal IEnumerator LoadFirstState()
    {
        yield return 0;

        //Load First State
        RequestState(firstState.GetType().Name);
    }
    #endregion
}

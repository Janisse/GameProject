using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class JState : MonoBehaviour
{
    #region Properties
    public string[] panelToDisplay = new string[0];

    protected JGameMode currentGameMode = null;
    #endregion

    #region Class Methods
    internal virtual void Enter()
    {
        //Get current GameMode
        currentGameMode = JEngine.Instance.gameManager.GetCurrentGameMode;

        //Hide all Panels
        JEngine.Instance.uiManager.HideAllPanels();

        //Display requested Panels
        foreach(string panelName in panelToDisplay)
        {
            JEngine.Instance.uiManager.ShowPanel(panelName);
        }

        //Register Events
        RegisterForEvents();
    }

    internal virtual void Manage() { }
    internal virtual void Exit()
    {
        //Unregister Events
        UnregisterForEvents();
    }
    #endregion

    #region Event Methods
    internal virtual void RegisterForEvents() { }
    internal virtual void UnregisterForEvents() { }
    #endregion
}

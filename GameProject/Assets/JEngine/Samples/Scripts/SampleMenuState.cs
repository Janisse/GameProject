using UnityEngine;
using System.Collections;

public class SampleMenuState : JState
{
    #region Class Methods
    internal override void Enter()
    {
        base.Enter();
        JEngine.Instance.eventManager.RegisterEvent("MainButtonClicked", OnButtonClicked);
    }

    internal override void Manage()
    {
        base.Manage();
    }

    internal override void Exit()
    {
        JEngine.Instance.eventManager.UnregisterEvent("MainButtonClicked", OnButtonClicked);
        base.Exit();
    }
    #endregion

    #region Event Methods
    internal void OnButtonClicked(JEventArgs args)
    {
        if (args.strArg.Equals("Play"))
            currentGameMode.RequestState("SampleLevelSelectionState");
        else if (args.strArg.Equals("Options"))
            Debug.Log("Options !");
    }
    #endregion
}

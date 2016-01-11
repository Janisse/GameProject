using UnityEngine;
using System.Collections;

public class SampleLevelSelectionState : JState
{
    #region Class Methods
    internal override void Enter()
    {
        base.Enter();
        JEngine.Instance.eventManager.RegisterEvent("LevelSelected", OnLevelSelected);
        JEngine.Instance.eventManager.RegisterEvent("Back", Back);
    }

    internal override void Manage()
    {
        base.Manage();
    }

    internal override void Exit()
    {
        JEngine.Instance.eventManager.UnregisterEvent("LevelSelected", OnLevelSelected);
        JEngine.Instance.eventManager.UnregisterEvent("Back", Back);
        base.Exit();
    }
    #endregion

    #region Event Methods
    internal void OnLevelSelected(JEventArgs args)
    {
        Debug.Log("Level selection: " + args.strArg);
    }

    internal void Back(JEventArgs args)
    {
        currentGameMode.RequestState("SampleMenuState");
    }
    #endregion
}

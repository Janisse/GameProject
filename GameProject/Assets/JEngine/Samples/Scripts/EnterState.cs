using UnityEngine;
using System.Collections;

public class EnterState : JState
{
    internal override void Enter()
    {
        base.Enter();
        Debug.Log("Je suis dans mon state !");
        StartCoroutine(wait2sec());
    }

    internal override void Manage()
    {
        base.Manage();
        Debug.Log("Je Manage !");
    }

    internal override void Exit()
    {
        base.Exit();
        Debug.Log("Je pars vers l'infini et l'au-delà !!!!");
    }

    IEnumerator wait2sec()
    {
        yield return new WaitForSeconds(2.0f);
        currentGameMode.RequestState("SecondState");
    }
}

using UnityEngine;
using System.Collections;

public class SecondState : JState
{
    internal override void Enter()
    {
        base.Enter();
        Debug.Log("Je suis dans mon second state !");
        StartCoroutine(wait2sec());

        //Load
        JEngine.Instance.saveManager.Load("save.JSave");
    }

    internal override void Manage()
    {
        base.Manage();
        Debug.Log("Et je Manage le second state!");
    }

    IEnumerator wait2sec()
    {
        yield return new WaitForSeconds(2.0f);
        JEngine.Instance.gameManager.changeGameMode("SampleMenuGameMode2");
    }
}

using UnityEngine;
using System.Collections;

public class ThirdState : JState
{

    internal override void Enter()
    {
        base.Enter();
        Debug.Log("nouveau gamemode, nouveau state !");
    }

    internal override void Manage()
    {
        base.Manage();
        Debug.Log("Et je Manage le state de mon second gamemode!");
    }
}

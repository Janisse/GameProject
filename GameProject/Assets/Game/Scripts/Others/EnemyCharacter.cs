using UnityEngine;
using System.Collections;

public class EnemyCharacter : Character
{
	#region properties
	internal enum EBehaviourState
	{
		None = 0,
		Idle,
		Alerted
	}

	public NavMeshAgent navMeshAgent = null;
	public Transform sightPosition = null;
	public Transform[] CheckPoint = null;
	public float angleOfView = 125f;			//In Degrees
	public float distanceOfView = 10f;

	internal Transform target = null;
	internal EBehaviourState state = EBehaviourState.None;

	private Level currentLevel = null;
	#endregion

	#region Class Methods
	internal void Start()
	{
		base.Init ();
		state = EBehaviourState.Idle;
		currentLevel = ((FPSGameMode)JEngine.Instance.gameManager.GetCurrentGameMode).level;
		currentLevel.RegisterEnemy (this);
	}

	internal override void Manage()
	{
		base.Manage ();
		switch(state)
		{
		case EBehaviourState.Idle:
			ManageIdle ();
			break;
		case EBehaviourState.Alerted:
			ManageAlerted ();
			break;
		}
	}
	#endregion

	#region State Management
	internal void ManageIdle()
	{
		Collider[] results = new Collider[0];
		results = Physics.OverlapSphere(sightPosition.position, distanceOfView, LayerMask.GetMask("Character"));
		foreach(Collider obj in results)
		{
			//Check if Player
			if(obj.tag == "Player")
			{
				//Check if in Sight
				float angle = Vector3.Angle(sightPosition.forward, obj.transform.position - transform.position);
				if(Mathf.Abs(angle) <= angleOfView/2f)
				{
					if(!Physics.Linecast(sightPosition.position, obj.ClosestPointOnBounds(sightPosition.position)))
					{
						//Alerted !
						state = EBehaviourState.Alerted;
						target = obj.transform;
					}
				}
			}
		}
	}

	internal void ManageAlerted()
	{
		navMeshAgent.SetDestination (target.position);
	}
	#endregion

	#region Character Methods
	internal override void Shoot()
	{
		base.Shoot ();
	}

	internal override void Die ()
	{
		base.Die ();
		currentLevel.UnregisterEnemy (this);
	}
	#endregion
}

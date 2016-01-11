using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JRoutineManager : MonoBehaviour
{
	#region Routine Methods
	internal Coroutine StartRoutine(IEnumerator a_routine)
	{
		Coroutine newRoutine = StartCoroutine (a_routine);
		return newRoutine;
	}

	internal void StopRoutine(Coroutine a_routine)
	{
		StopCoroutine (a_routine);
	}
	#endregion
}

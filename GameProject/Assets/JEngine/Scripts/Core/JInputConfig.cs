using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JInputConfig : MonoBehaviour
{
	#region Properties
	public List<JInputPair> inputConfigList = null;
	#endregion
}

[System.Serializable]
public class JInputPair
{
	public string key = "";
	public string value = "";
}
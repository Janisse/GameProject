using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JInputManager
{
	#region Properties
	protected List<string> pressedKeyEnter = null;
	protected List<string> pressedKey = null;
	protected List<string> pressedKeyExit = null;
	protected Dictionary<string, string> buttonConfig = null;
	#endregion

	#region Class Methods
	internal JInputManager(JInputConfig a_inputConfig)
	{
		pressedKeyEnter = new List<string>();
		pressedKey = new List<string>();
		pressedKeyExit = new List<string>();

		//Init Button Config
		buttonConfig = new Dictionary<string, string>();
		foreach(JInputPair pair in a_inputConfig.inputConfigList)
		{
			buttonConfig.Add(pair.key, pair.value);
		}
	}

	internal void LateManage()
	{
		ManageList ();
	}

	protected void ManageList()
	{
		pressedKeyEnter.Clear ();
		pressedKeyExit.Clear ();
	}
	#endregion

	#region Set Input Methods
	internal void SetInput(string a_key, string a_value)
	{
		if (!buttonConfig.ContainsKey (a_key))
			buttonConfig.Add (a_key, a_value);
		else
			buttonConfig [a_key] = a_value;
	}

	internal void NotifyInputEnter(string a_key)
	{
		if(!pressedKey.Contains(a_key))
		{
			pressedKeyEnter.Add (a_key);
			pressedKey.Add (a_key);
		}
	}

	internal void NotifyInputExit(string a_key)
	{
		if(!pressedKey.Contains(a_key))
		{
			pressedKey.Remove (a_key);
			pressedKeyExit.Add (a_key);
		}
	}
	#endregion

	#region Get Input Methods
	internal bool GetInputEnter(string a_key)
	{
		if(buttonConfig.ContainsKey(a_key))
		{
			if(Input.GetKeyDown(buttonConfig[a_key]) || pressedKeyEnter.Contains (buttonConfig[a_key]))
				return true;
			else
				return false;
		}

		if (Input.GetKeyDown (a_key) || pressedKeyEnter.Contains (a_key))
			return true;

		return false;
	}

	internal bool GetInput(string a_key)
	{
		if(buttonConfig.ContainsKey(a_key))
		{
			if(Input.GetKey(buttonConfig[a_key]) || pressedKey.Contains (buttonConfig[a_key]))
				return true;
			else
				return false;
		}

		if (Input.GetKey (a_key) || pressedKey.Contains (a_key))
			return true;

		return false;
	}

	internal bool GetInputExit(string a_key)
	{
		if(buttonConfig.ContainsKey(a_key))
		{
			if(Input.GetKeyUp(buttonConfig[a_key]) || pressedKeyExit.Contains (buttonConfig[a_key]))
				return true;
			else
				return false;
		}

		if (Input.GetKeyUp (a_key) || pressedKeyExit.Contains (a_key))
			return true;

		return false;
	}
	#endregion
}

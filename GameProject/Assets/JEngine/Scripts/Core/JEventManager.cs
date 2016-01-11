using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class JEventManager {

	#region Properties
	private Dictionary<string, Delegate> _eventTable = new Dictionary<string, Delegate> ();
	#endregion

	internal delegate void Delegate (JEventArgs args);

	#region Events Methods
	internal void RegisterEvent(string eventName, Delegate eventMethod)
	{
		if(_eventTable.ContainsKey(eventName))
		{
			_eventTable[eventName] += eventMethod;
		}
		else
		{
			_eventTable.Add (eventName, eventMethod);
		}
	}

	internal void UnregisterEvent(string eventName, Delegate eventMethod)
	{
		if(_eventTable.ContainsKey(eventName))
		{
			if(_eventTable[eventName].GetInvocationList().Length <= 0)
				_eventTable.Remove (eventName);
			else
				_eventTable[eventName] -= eventMethod;
		}
	}

	internal bool FireEvent(string key, JEventArgs args)
	{
		if(_eventTable.ContainsKey(key))
		{
			_eventTable[key] (args);
			return true;
		}
		return false;
	}

	internal bool FireEvent(string key)
	{
		if(_eventTable.ContainsKey(key))
		{
			_eventTable[key] (new JEventArgs());
			return true;
		}
		return false;
	}
	#endregion
}

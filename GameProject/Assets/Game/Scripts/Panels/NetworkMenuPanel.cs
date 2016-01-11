using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NetworkMenuPanel : JPanel
{
	#region Properties
	internal enum ENetDisplayPanel
	{
		Main,
		Host,
		Join
	}

	public GameObject mainDisplay = null;
	public GameObject hostDisplay = null;
	public GameObject joinDisplay = null;
	public Text ipDisplay = null;
	public InputField ipField = null;
	#endregion
	
	#region Display Methods
	internal void SetDisplay(ENetDisplayPanel a_display)
	{
		switch(a_display)
		{
		case ENetDisplayPanel.Main:
			mainDisplay.SetActive(true);
			hostDisplay.SetActive(false);
			joinDisplay.SetActive(false);
			break;
		case ENetDisplayPanel.Host:
			mainDisplay.SetActive(false);
			hostDisplay.SetActive(true);
			joinDisplay.SetActive(false);
			break;
		case ENetDisplayPanel.Join:
			mainDisplay.SetActive(false);
			hostDisplay.SetActive(false);
			joinDisplay.SetActive(true);
			break;
		}
	}
	#endregion
}

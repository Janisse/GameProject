using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectLevelPanel : JPanel
{
	#region Properties
	public Image[] buttonImageTab = null;
	public Image playButton = null;

	private string _currentSelectLevel = "";
	#endregion

	#region Display Methods
	internal void InitPanel()
	{
		//Init Button's Color
		foreach(Image item in buttonImageTab)
		{
			item.color = new Color(0.5f, 0.5f, 0.5f);
			playButton.color = new Color(0.5f, 0.5f, 0.5f);
		}
	}

	internal void SetSelectedLevel(string a_name)
	{
		//Set Color of Level button
		foreach(Image item in buttonImageTab)
		{
			if(item.name == a_name)
			{
				item.color = Color.white;
			}
			else if(item.name == _currentSelectLevel)
			{
				item.color = new Color(0.5f, 0.5f, 0.5f);
			}
		}
		_currentSelectLevel = a_name;

		//Set Color of Play button
		playButton.color = Color.white;

	}
	#endregion
}

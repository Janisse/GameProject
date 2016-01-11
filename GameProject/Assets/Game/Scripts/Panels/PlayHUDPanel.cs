using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayHUDPanel : JPanel
{
	#region Properties
	public Text weaponText = null;
	public Text ammoText = null;
	public Text pointText = null;
	#endregion

	#region Display Methods
	internal void SetWeaponName(string a_name)
	{
		weaponText.text = a_name;
	}

	internal void SetAmmoText(string a_text)
	{
		ammoText.text = a_text;
	}

	internal void SetPointText(string a_text)
	{
		pointText.text = a_text + " Pts";
	}
	#endregion
}

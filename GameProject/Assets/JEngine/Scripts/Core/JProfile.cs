using UnityEngine;
using System.Collections;

public class JProfile
{
	#region Properties
	internal string profileName = "";
	internal int profileAge = -1;
	internal bool isSurpriseMotherFucker = false;
	#endregion

	#region Class Methods
	internal JProfile(string a_profileName = "", int a_profileAge = -1, bool a_isSurpriseMotherFucker = false)
	{
		profileName = a_profileName;
		profileAge = a_profileAge;
		isSurpriseMotherFucker = a_isSurpriseMotherFucker;
	}
	#endregion

	#region Save & Load Methods
	internal void Save(JXmlManager a_xmlManager)
	{
		a_xmlManager.Write ("Name", profileName);
		a_xmlManager.Write ("Age", profileAge);
		a_xmlManager.Write ("Surprise", isSurpriseMotherFucker);
	}

	internal void Load(JXmlManager a_xmlManager)
	{
		a_xmlManager.ReadString ("Name", out profileName);
		a_xmlManager.ReadInt ("Age", out profileAge);
		a_xmlManager.ReadBool ("Surprise", out isSurpriseMotherFucker);
	}
	#endregion
}

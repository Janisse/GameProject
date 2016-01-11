using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JSaveManager
{
	#region Properties
	

	private string _savePath = "";
	private JXmlManager _xmlManager = null;
	#endregion

	#region Class Methods
	internal JSaveManager()
	{
		_savePath = Application.dataPath;
		_xmlManager = new JXmlManager ();
    }
	#endregion

	#region Save Methods
	internal void Save(string a_fileName)
	{
		_xmlManager.OpenWriteFile (_savePath + "/" + a_fileName);

		foreach(JProfile profile in JEngine.Instance.profileManager.GetProfileList())
		{
			_xmlManager.WriteOpenTag("Profile");
			SaveProfil(profile);
			_xmlManager.WriteCloseTag("Profile");
		}

		_xmlManager.CloseWriteFile ();
	}

	private void SaveProfil(JProfile a_profile)
	{
		a_profile.Save (_xmlManager);
	}
	#endregion

	#region Load Methods
	internal void Load(string a_fileName)
	{
		_xmlManager.OpenReadFile (_savePath + "/" + a_fileName);

		while(_xmlManager.ReadOpenTag ("Profile"))
		{
            JEngine.Instance.profileManager.AddProfile(LoadProfil());
			_xmlManager.ReadCloseTag ("Profile");
		}

		_xmlManager.CloseReadFile ();
	}

	private JProfile LoadProfil()
	{
		JProfile profile = new JProfile ();
		profile.Load (_xmlManager);
		return profile;
	}
	#endregion
}

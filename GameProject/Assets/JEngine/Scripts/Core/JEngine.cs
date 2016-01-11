using UnityEngine;
using System.Collections;

internal class JEngine {
	#region Properties
	internal JEventManager      eventManager        = null;
    internal JGameManager       gameManager         = null;
    internal JUIManager         uiManager           = null;
    internal JSaveManager       saveManager         = null;
    internal JProfileManager    profileManager      = null;
    internal JAudioManager      audioManager        = null;
	internal JInputManager		inputManager		= null;
	internal JRoutineManager	routineManager		= null;
	internal JNetworkManager	networkManager		= null;

    internal JRoot              root                = null;

    internal const double engineVersion = 0.21;
    #endregion

    #region Singleton Methods
    private static JEngine _instance = null;

    internal static JEngine Instance
	{
		get
		{
			if(_instance == null)
				_instance = new JEngine();
			return _instance;
		}
	}
	#endregion

	#region Class Methods
	internal JEngine()
	{
        Debug.LogWarning("JEngine v" + engineVersion.ToString());

		//Initialize Managers
		eventManager 	= new JEventManager ();
        gameManager 	= new JGameManager();
        uiManager 		= new JUIManager();
        saveManager 	= new JSaveManager();
		profileManager 	= new JProfileManager ();
    }

    internal void Manage()
    {
        gameManager.Manage();
    }

	internal void LateManage()
	{
		inputManager.LateManage ();
	}
	#endregion
}

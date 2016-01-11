using UnityEngine;
using System.Collections;

public class JEventArgs {
	#region Properties
	internal Object objArg;
	internal string strArg;
	internal float floatArg;
    #endregion

    #region Class Methods
    internal JEventArgs(Object a_objArg, string a_strArg = "", float a_floatArg = 0f)
    {
        objArg = a_objArg;
        strArg = a_strArg;
        floatArg = a_floatArg;
    }

    internal JEventArgs(Object a_objArg, float a_floatArg = 0f)
    {
        objArg = a_objArg;
        strArg = "";
        floatArg = a_floatArg;
    }

    internal JEventArgs(string a_strArg, float a_floatArg = 0f)
    {
        objArg = null;
        strArg = a_strArg;
        floatArg = a_floatArg;
    }

    internal JEventArgs(float a_floatArg)
    {
        objArg = null;
        strArg = "";
        floatArg = a_floatArg;
    }

    internal JEventArgs()
    {
        objArg = null;
        strArg = "";
        floatArg = 0f;
    }
    #endregion
}

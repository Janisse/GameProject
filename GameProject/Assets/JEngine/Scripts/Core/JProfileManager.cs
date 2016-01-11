using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class JProfileManager
{
    #region Properties
    private List<JProfile> _profileList = new List<JProfile>();
    #endregion

    #region Class Methods
    internal JProfileManager()
    {
        _profileList = new List<JProfile>();

        //Debug
        //_profileList.Add(new JProfile("Janisse", 21, true));
        //_profileList.Add(new JProfile("Perceval", 32, false));
        //End Debug
    }
    #endregion

    #region Profile Methods
    internal void AddProfile(JProfile a_profile)
    {
        _profileList.Add(a_profile);
    }

    internal List<JProfile> GetProfileList()
    {
        return _profileList;
    }
    #endregion
}

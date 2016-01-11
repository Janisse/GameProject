using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

internal class JUIManager
{
    #region Properties
    internal Dictionary<string, JPanel> panelDict = null;
    internal List<JPanel> currentShowedPanelList = null;
    internal Canvas uiCanvas = null;
    internal int nbPanelInTransition = 0;
    #endregion

    #region Class Methods
    internal JUIManager()
    {
        panelDict = new Dictionary<string, JPanel>();
        currentShowedPanelList = new List<JPanel>();
    }
    #endregion

    #region UI Management
    internal void LoadPanels(string[] a_panelsNameTab)
    {
        if (a_panelsNameTab == null) return;
        foreach(string panelName in a_panelsNameTab)
        {
			SceneManager.LoadScene(panelName, LoadSceneMode.Additive);
        }
    }

    internal void UnloadPanels(string[] a_panelsNameTab)
    {
        if (a_panelsNameTab == null) return;
        JPanel panel = null;
        foreach (string panelName in a_panelsNameTab)
        {
            panelDict.TryGetValue(panelName, out panel);
            if(panel != null)
            {
                if (currentShowedPanelList.Contains(panel))
                    currentShowedPanelList.Remove(panel);
                panelDict.Remove(panelName);
                Object.Destroy(panel.gameObject);
            }
        }
    }

    internal void LoadPanelAdditive(string a_panelsName)
    {
		SceneManager.LoadScene(a_panelsName, LoadSceneMode.Additive);
    }

    internal void RegisterPanel(JPanel a_panel)
    {
        string panelName = a_panel.name;
        if (!panelDict.ContainsKey(panelName))
        {
            panelDict.Add(panelName, a_panel);
            //a_panel.gameObject.SetActive(false);
            a_panel.canvasGroup.blocksRaycasts = false;
        }
        else
        {
            //TODO Debug Msg
        }
    }

    internal void UnregisterPanel(string a_panelName)
    {
        if (panelDict.ContainsKey(a_panelName))
        {
            panelDict.Remove(a_panelName);
        }
        else
        {
            //TODO Debug Msg
        }
    }

    internal void ShowPanel(string a_panelName)
    {
        JPanel panel = null;
        panelDict.TryGetValue(a_panelName, out panel);

        if(panel != null)
        {
            //panel.gameObject.SetActive(true);
            panel.canvasGroup.blocksRaycasts = true;
            panel.StartPanelTransition(true);
            currentShowedPanelList.Add(panel);
        }
        else
        {
            //TODO Debug Msg
        }
    }

    internal void HidePanel(string a_panelName)
    {
        JPanel panel = null;
        panelDict.TryGetValue(a_panelName, out panel);

        if (panel != null)
        {
            panel.StartPanelTransition(false);
        }
        else
        {
            //TODO Debug Msg
        }
    }

    internal void HideAllPanels()
    {
        foreach (JPanel panel in currentShowedPanelList)
        {
            HidePanel(panel.name);            
        }
    }

    internal JPanel GetPanel(string a_panelName)
    {
        JPanel panel = null;
        panelDict.TryGetValue(a_panelName, out panel);
        return panel;
    }
    #endregion
}

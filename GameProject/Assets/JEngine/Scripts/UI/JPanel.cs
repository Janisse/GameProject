using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JPanel : MonoBehaviour
{
    #region Properties
    public CanvasGroup canvasGroup = null;
    public float transitionDuration = 0.5f;
    public AnimationCurve transitionCurve = null;

    private Coroutine transitionCoroutine = null;
    #endregion

    #region Class Management
    void Start()
    {
        JEngine.Instance.uiManager.RegisterPanel(this);

        canvasGroup.alpha = 0f;

        //Find the root parent
        Transform _parent = transform.parent;
        while(true)
        {
            if (_parent.parent == null)
                break;
            _parent = transform.parent;
        }

        //Place the Panel in the Root hierarchy
        transform.SetParent(JEngine.Instance.uiManager.uiCanvas.transform);
        Destroy(_parent.gameObject);
    }

    void OnDestroy()
    {
        if(transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
            JEngine.Instance.uiManager.nbPanelInTransition--;
        }
    }
    #endregion

    #region Transitions Methods
    internal void StartPanelTransition(bool isShowing)
    {
        if (transitionCoroutine != null)
        {
            JEngine.Instance.uiManager.nbPanelInTransition--;
            StopCoroutine(transitionCoroutine);
        }
        transitionCoroutine = StartCoroutine(PanelTransition(isShowing));
    }

    protected IEnumerator PanelTransition(bool isShowing)
    {
        //Init
        float timeLeft = 0f;
        JEngine.Instance.uiManager.nbPanelInTransition++;

        //Process
        yield return new WaitForEndOfFrame();
        if (isShowing)
        {
            while (canvasGroup.alpha < 1f)
            {
                timeLeft += Time.unscaledDeltaTime;
                canvasGroup.alpha = transitionCurve.Evaluate(timeLeft / transitionDuration);
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (canvasGroup.alpha > 0f)
            {
                timeLeft += Time.unscaledDeltaTime;
                canvasGroup.alpha = transitionCurve.Evaluate(1f - (timeLeft / transitionDuration));
                yield return new WaitForFixedUpdate();
            }
            canvasGroup.blocksRaycasts = false;
        }

        transitionCoroutine = null;
        JEngine.Instance.uiManager.nbPanelInTransition--;
    }
    #endregion

}

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Button))]
public class JButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Properties
    public string key = "";
    public string args = "";

	protected bool isButtonCliked = false;
	protected bool isButtonStay = false;
    protected Button _button;
	protected Coroutine _buttonStayCoroutine = null;
    #endregion

    #region Class Methods
    internal virtual void Start()
    {
		isButtonCliked = false;
		isButtonStay = false;
    }
    #endregion

	#region Button Methods
	public virtual void OnButtonEnter()
	{
//		Debug.Log ("OnButtonEnter !");
	}

	public virtual void OnButtonStay()
	{
//		Debug.Log ("OnButtonStay !");
	}

	public virtual void OnButtonClick()
	{
		if (JEngine.Instance.uiManager.nbPanelInTransition == 0)
		{
			JEventArgs eventArgs = new JEventArgs();
			eventArgs.strArg = args;
			JEngine.Instance.eventManager.FireEvent(key, eventArgs);
		}
//		Debug.Log ("OnButtonClick !");
	}

	public virtual void OnButtonExit()
	{
//		Debug.Log ("OnButtonExit !");
	}
	#endregion

    #region Interface Methods
	public virtual void OnPointerDown(PointerEventData eventData)
	{
		isButtonCliked = true;
		_buttonStayCoroutine = StartCoroutine (OnStayCoroutine());
		OnButtonEnter ();
	}

	public virtual void OnPointerUp(PointerEventData eventData)
	{
		isButtonCliked = false;
		StopCoroutine (_buttonStayCoroutine);
		_buttonStayCoroutine = null;
		if(isButtonStay == false)
		{
			OnButtonClick();
		}
		isButtonStay = false;
		OnButtonExit ();
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{

	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		if(isButtonCliked == true)
		{
			OnPointerUp(eventData);
		}
	}

	public virtual void OnDisable()
	{
		if(_buttonStayCoroutine != null)
		{
			StopCoroutine (_buttonStayCoroutine);
			_buttonStayCoroutine = null;
		}
	}

	public IEnumerator OnStayCoroutine()
	{
		yield return new WaitForSeconds (0.1f);
		isButtonStay = true;
		while(true)
		{
			OnButtonStay ();
			yield return -1;
		}
	}
    #endregion

    #if UNITY_EDITOR
    #region Inspector Methods
    [MenuItem("JEngine/UI/JButton")]
    protected static void AddJButtonOnInspector()
    {
        GameObject newButton = (GameObject)Instantiate(Resources.Load("Button"), Vector3.zero, Quaternion.identity);
        newButton.transform.SetParent(Selection.activeGameObject.transform);
        newButton.name = "Button";
    }
    #endregion
    #endif
}

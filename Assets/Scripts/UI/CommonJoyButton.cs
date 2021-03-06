using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AttackTypeDefine;


public class CommonJoyButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    #region System

    private void Awake()
    {
        PressDown = new GameBtnEvent();
        OnDragEvent = new GameBtnEvent();
        PressUp = new GameBtnEvent();
    }

    #endregion

    #region Joy Stick Event Callback

    public Image ImageBackGround;
    public Image ImageHandle;
    public Vector3 HandleOriginPoint;


    public GameBtnEvent PressDown;
    public GameBtnEvent OnDragEvent;
    public GameBtnEvent PressUp;

    public float MaxRadius;

    protected Vector3 _Dir;
    public Vector3 Dir => _Dir;

    Vector3 PointDownPos;
    int FingerID = int.MinValue;


    public void OnPointerDown(PointerEventData eventData)
    {
        if ((FingerID = eventData.pointerId) < -1) 
        {
            return;
        }

        ImageBackGround.transform.position = PointDownPos = eventData.position;

        PressDown?.Invoke(eventData);

    }
    public void OnDrag(PointerEventData eventData)
    {
        if ((FingerID = eventData.pointerId) < -1)
        {
            return;
        }
        //get distance
        var distance = (eventData.position - (Vector2)PointDownPos);

        //Max and Min of the radus Mathf.Clamp(Value,Min, Max) 
        var radius = Mathf.Clamp(Vector3.Magnitude(distance), 0, MaxRadius);

        var tmp = radius * distance.normalized;


        var localPos = new Vector2()
        {
            x = tmp.x,
            y = tmp.y
        };

        ImageHandle.transform.localPosition = localPos;

        _Dir = ImageHandle.transform.localPosition.normalized;


        OnDragEvent?.Invoke(eventData);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if ((FingerID = eventData.pointerId) < -1)
        {
            return;
        }

        //Vector3 origin;
        //origin = new Vector3(250, -320, 0);
        ImageBackGround.transform.localPosition = HandleOriginPoint;
        ImageHandle.transform.localPosition = Vector3.zero;
        _Dir = Vector3.zero;

        PressUp?.Invoke(eventData);
    }
    #endregion
}

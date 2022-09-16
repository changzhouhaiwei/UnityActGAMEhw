using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    //获得创建cell组件
    CreateCell m_ccComp;
    Vector3 startPos;
    EventTrigger triggerComp;

    int dis = 4;
   
    // Start is called before the first frame update
    void Start()
    {
        m_ccComp = gameObject.GetComponent<CreateCell>();

        triggerComp = GetComponent<EventTrigger>();
        triggerComp.triggers = new List<EventTrigger.Entry>();

        EventTrigger.Entry entryBeginDrag = new EventTrigger.Entry();
        entryBeginDrag.eventID = EventTriggerType.BeginDrag;
        entryBeginDrag.callback = new EventTrigger.TriggerEvent();
        UnityAction<BaseEventData> beginDragCb = new UnityAction<BaseEventData>(OnBeginDrag);
        entryBeginDrag.callback.AddListener(beginDragCb);
        triggerComp.triggers.Add(entryBeginDrag);


        Invoke("GameStart", 1.0f);
    }
    public void GameStart()
    {
        //清除所有，初始化单个方块
        m_ccComp.InitCell();
    }

    public Vector2[,] GetAllRealPosArr()
    {
        return m_ccComp.posArr;
    }


    private bool  CheckCanMove()
    {
        return m_ccComp.GetCanMove();
    }


    public void OnBeginDrag(BaseEventData eventData)
    {
        if (!CheckCanMove()) return;
        var data = (PointerEventData)eventData;
        Debug.Log("haha" + data.delta.x + "++" +data.delta.y);

        if(data.delta.x < -dis)
        {
            m_ccComp.CreateToLeft();
            Debug.Log("向左");
        }else if(data.delta.x > dis)
        {
            Debug.Log("向右");
            m_ccComp.CreateToRight();
        }
        else if(data.delta.y > dis)
        {
            m_ccComp.CreateToUp();
            Debug.Log("向上");
        }
        else if (data.delta.y < -dis)
        {
            Debug.Log("向下");
            m_ccComp.CreateToDown();
        }
    }

}

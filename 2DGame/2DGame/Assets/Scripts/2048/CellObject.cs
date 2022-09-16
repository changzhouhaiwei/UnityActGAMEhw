using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CellObject : MonoBehaviour
{
    public CreateCell m_ccComp;

    //移动相关参数
    public float speed = 100;

    private Vector2 speedV ; //矢量速度
    private Vector2 dstPos; //目的坐标
    private Vector2 dstVPos; //目的数组标

    private float m_moveTm; 
    public bool bMoving;
    private float m_totalMoveTm;

    public Vector2 m_curVPos; //当前数组表

    //数字相关参数
    private Text m_textLb;
    public int m_value; //cell的值
    public int[] valueArr;

    private CellObject m_target;


    RectTransform m_rectTran;

    // Start is called before the first frame update
    void Start()
    {
        m_rectTran = GetComponent<RectTransform>();
        m_textLb = transform.Find("Text").gameObject.GetComponent<Text>();
        valueArr = new int[] { 2,2,2,2,2};

        RandomValue();
    }

    public void MoveToPos(Vector2 pos)
    {
        dstPos = pos;
        var curPos = this.GetComponent<RectTransform>().anchoredPosition;
        var distance = Vector2.Distance(curPos, pos);

        float ti = distance / speed;
        m_totalMoveTm = ti;

        Vector2 deltaPos = pos - this.GetComponent<RectTransform>().anchoredPosition;
        speedV = deltaPos / ti;
        m_moveTm = 0;
        bMoving = true;

    }

    public void MoveToVPos(Vector2 vPos)
    {
        dstVPos = vPos;
        Vector2 pos =  m_ccComp.posArr[(int)vPos.y, (int)vPos.x];
        MoveToPos(pos);
    }
    
    public void MoveToAndCombine(Vector2 vPos,CellObject target)
    {
        m_target = target;
        MoveToVPos(vPos);
        Invoke("Combine", m_totalMoveTm);
    }

    public void Combine()
    {
        m_value += m_target.m_value;
        m_textLb.text = m_value.ToString();
        Destroy(m_target.gameObject);
    }

    public void TestMove()
    {
        MoveToVPos(new Vector2(0,3));
    }

    public void FixedUpdate()
    {
        if(bMoving)
        {
            float deltaTm = Time.deltaTime;
            m_moveTm += deltaTm;
            Vector2 deltaMove = deltaTm * speedV;
            m_rectTran.anchoredPosition += deltaMove;

            if (m_moveTm > m_totalMoveTm)
            {
                m_rectTran.anchoredPosition = dstPos;
                bMoving = false;
                m_curVPos = dstVPos;
            }
        }
    }

    //设置值
    public void RandomValue()
    {
        var len = valueArr.Length;
        m_value = valueArr[Random.Range(0, len)];
        m_textLb.text =  m_value.ToString();
        m_textLb.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CreateCell : MonoBehaviour
{
    public GameObject m_curCellObj;
    public Transform initTran;

    private int m_createIndex;
    //private CellObject m_toRmObk
    public Vector2[,] posArr = { 
        
    };


    List<CellObject> m_allCellList;

     void Start()
    {
        m_allCellList = new List<CellObject>();

        //保存所有坐标数据到二维数组中
        posArr = new Vector2[4, 4];
        var initPos = new Vector2(50, -50);
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                var pos = new Vector2();
                pos.x = initPos.x + 110 * x;
                pos.y = initPos.y - 110 * y;
                posArr[y,x] = pos;
            }
        }

        m_createIndex = 0;

    }

    public void AddCell()
    {
        foreach (var item in m_allCellList)
        {
            Destroy(item.gameObject);
        }
        m_allCellList.Clear();

        InitCell();
       
    }

    public void InitCell()
    {
        AddCellAtVPos(new Vector2(0, 0));
    }


    public void AddCellAtVPos(Vector2 vPos, bool force = false,int value = 0)
    {
        Transform tt = Instantiate(initTran, transform, false);
        tt.gameObject.GetComponent<RectTransform>().anchoredPosition = posArr[(int)vPos.y, (int)vPos.x];
        tt.gameObject.SetActive(true);
        m_curCellObj = tt.gameObject;

        var cellComp = m_curCellObj.GetComponent<CellObject>();
        cellComp.m_curVPos = vPos;

        m_allCellList.Add(cellComp);

        m_createIndex++;
        cellComp.gameObject.name = "grid" + m_createIndex;

    }

    public void CreateToLeft()
    {
        CreateHorizion(true);
    }

    public void CreateToRight()
    {
        CreateHorizion(false);
    }

    public void CreateHorizion(bool bL)
    {
        //找出空位
        Vector2 emptyV = new Vector2(-1, -1);
        for (int i = 3; i >= 0; i--) //从右向左
        {
            var x = i;
            if (!bL)
            {
                x = 3 - i;
            }
            for (int y = 0; y < 4; y++)
            {
                CellObject com = m_allCellList.Find(s => s.m_curVPos.x == x && s.m_curVPos.y == y);
                if (com == null)
                {
                    emptyV = new Vector2(x, y);
                    break;
                }
            }

            if (emptyV != null)
            {
                break;
            }
        }

        if (emptyV.x >= 0)//找到的空位
        {
            //塞入
            AddCellAtVPos(emptyV);
        }

        //已有的移动到左侧并且合成
        for (int y = 0; y < 4; y++)
        {
            int index = 0;
            List<CellObject> tList = new List<CellObject>();
            for (int i = 0; i < 4; i++)
            {
                var x = i;
                if(!bL) { x = 3 - i; };

                CellObject com = m_allCellList.Find(s => s.m_curVPos.x == x && s.m_curVPos.y == y);
                if (com)
                {
                    tList.Add(com);
                }
            }

            CellObject lastObj = null;
            List<CellObject> rmList = new List<CellObject>();
            bool bCombined = false;
            foreach (var item in tList)
            {
                if (lastObj && lastObj.m_value == item.m_value && bCombined == false)
                {
                    var dstX = index - 1;
                    if (!bL) { dstX = 3 - dstX; };
                    item.MoveToAndCombine(new Vector2(dstX, y), lastObj);
                    //tList.Remove(lastObj);
                    rmList.Add(lastObj);
                    lastObj = null;
                    index--;
                    bCombined = true;
                }
                else
                {
                    var dstX = index;
                    if (!bL) { dstX = 3 - dstX; };

                    item.MoveToVPos(new Vector2(dstX, y));
                }

                lastObj = item;
                index++;
            }

            foreach (var item in rmList)
            {
                tList.Remove(item);
                m_allCellList.Remove(item);
            }
        }
        //合成
    }


    public void CreateVertical(bool bU)
    {
        Vector2 emptyV = new Vector2(-1, -1);
        for (int i = 3; i >= 0; i--) //从右向左
        {
            var y = i;
            if (!bU)
            {
                y = 3 - i;
            }

            for (int x = 0; x < 4; x++)
            {
                CellObject com = m_allCellList.Find(s => s.m_curVPos.y == y && s.m_curVPos.x == x);
                if (com == null)
                {
                    emptyV = new Vector2(x, y);
                    break;
                }
            }

            if (emptyV != null)
            {
                break;
            }
        }

        if (emptyV.y >= 0)//找到的空位
        {
            //塞入
            AddCellAtVPos(emptyV);
        }

        //已有的移动到左侧并且合成
        for (int x = 0; x < 4; x++)
        {
            int index = 0;
            List<CellObject> tList = new List<CellObject>();
            for (int i = 0; i < 4; i++)
            {
                var y = i;
                if (!bU) { y = 3 - i; };

                CellObject com = m_allCellList.Find(s => s.m_curVPos.x == x && s.m_curVPos.y == y);
                if (com)
                {
                    tList.Add(com);
                }
            }

            CellObject lastObj = null;
            List<CellObject> rmList = new List<CellObject>();
            foreach (var item in tList)
            {
                if (lastObj && lastObj.m_value == item.m_value)
                {
                    var dstY = index - 1;
                    if (!bU) { dstY = 3 - dstY; };
                    item.MoveToAndCombine(new Vector2(x, dstY), lastObj);
                    rmList.Add(lastObj);
                    lastObj = null;
                    index--;
                }
                else
                {
                    var dstY = index;
                    if (!bU) { dstY = 3 - dstY; };
                    item.MoveToVPos(new Vector2(x, dstY));
                    lastObj = item;
                }

   
                index++;
            }

            foreach (var item in rmList)
            {
                tList.Remove(item);
                m_allCellList.Remove(item);
            }
        }
    }

    public void CreateToUp()
    {
        CreateVertical(true);
    }

    public void CreateToDown()
    {
        CreateVertical(false);
    }

    public bool GetCanMove()
    {
        foreach (var item in m_allCellList)
        {
            if (item.bMoving)
            {
                return false;
            }
        }
        return true;
    }


}

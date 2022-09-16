using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestUnityAction : MonoBehaviour
{
    UnityAction action1;

    UnityEvent event1;

    // Start is called before the first frame update
    void Start()
    {
        action1 = new UnityAction(MyFunction1);

        action1 += MyFunction2;

        event1 = new UnityEvent();

        event1.AddListener(action1);

        MyFunction3(1,() =>{
            Debug.Log("from lambda");
        });
    }

    public void Test()
    {
        event1.Invoke();
    }

    void MyFunction1()
    {
        Debug.Log("helo1");
    }

    void MyFunction2()
    {
        Debug.Log("helo2");
    }


    void MyFunction3(int i,UnityAction act)
    {
        //act.Invoke();

        act.Invoke();
    }
}

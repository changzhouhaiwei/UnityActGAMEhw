using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInterface : MonoBehaviour
{
    public enum BoderSide :int { Left = 2,Right = 3};

    public TestInterfaceMain m_tim;

    private void Start()
    {
        m_tim = new TestInterfaceMain();

        BoderSide side = BoderSide.Left;

        Debug.Log("side===" + side.ToString());
        //m_tim.I2.Foo();

        Debug.Log("side===" + (int)side);

        ((I2)m_tim).Foo();
    }
}



public interface I1 {
   public void Foo();
}


public interface I2 {
    public void Foo();
}


public class TestInterfaceMain : I1, I2
{
    public void Foo()
    {
        // throw new System.NotImplementedException();
        Debug.Log("I1.s Foo");
    }

    void I2.Foo()
    {
        Debug.Log("I2.s Foo");
    }
}


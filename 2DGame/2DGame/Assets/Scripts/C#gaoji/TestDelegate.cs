using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TestDelegate : MonoBehaviour
{
    delegate int Transformer(int x); //����ָ��

    static int func1(int x) { return x * x; }
    static int func2 (int x) =>  x*x;
    // Start is called before the first frame update

    Transformer tt = func1;

    // Update is called once per frame
    void Update()
    {
        tt(2);
        tt.Invoke(2);
    }
}


//ί��
public class TestDelegate2 {
    public delegate int Transformer(int x);

    class Util
    {
        public static void Transform(int[] values, Transformer tt)
        {
            foreach (var item in values)
            {
                tt(item);
            }
        }

    }


    class Test
    { 
        static void Main()
        {
            int[] values = { 1, 2, 3 };
            Util.Transform(values, Square);

            foreach (var item in values)
            {
                Debug.Log("item==" + item);
            }

            static int Square(int x) => x*x;
        }

    }
}


//�ಥί��

class TestDUOBO
{
    public delegate void ProgressReporter(int percentComplete);

    public class Util
    {
        public static void HardWork(ProgressReporter p)
        {
            for (int i = 0; i < 10; i++)
            {
                p(i * 10);
            }
        }
    }


    class Test
    {
        static void WriteProgressToConsole(int percentComplete) => Debug.Log("toconsole" + percentComplete);

        static void WriteProgressToFile(int percentComplete) => Debug.Log("tofile" + percentComplete);

        static void Main()
        {
            ProgressReporter p = WriteProgressToConsole;
            p += WriteProgressToFile;
        }
    }

}


//����ί��

class FanxinWeituo {

    public delegate int Transformer<T>(T arg);

    delegate TResult Func<out TResult> ();

    delegate TResult Func<in T, out TResult>   (T arg);

    delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);

    delegate void Action();

    delegate void Action<in T>(T arg);

    delegate void Action<in T1, in T2>(T1 arg1, T2 arg2);



}

//����ί�н�������⣬�������ýӿڽ����

class TestInterfaceDelegate
{
    public interface ITransformer
    {
        int Transform(int x);
    }
}









using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIGController : MonoBehaviour
{
    public Rigidbody2D m_rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerW()
    {
        m_rigidBody.AddForce(new Vector2(0, 5000));
        //m_rigidBody.velocity = new Vector2(0, 1);
    }

    public void TriggerS()
    {
        m_rigidBody.AddForce(new Vector2(0, -5000));
        //m_rigidBody.velocity = new Vector2(0, 1);
    }

    public void TriggerA()
    {
        m_rigidBody.AddForce(new Vector2(-5000, 0));
        //m_rigidBody.velocity = new Vector2(0, 1);
    }

    public void TriggerD()
    {
        m_rigidBody.AddForce(new Vector2(5000, 0));
        //m_rigidBody.velocity = new Vector2(0, 1);
    }

}

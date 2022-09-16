using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string XAxis = "Horizontal";
    public string YAxis = "Vertical";
    public string JumpKey = "Jump";


    public float speed = 400f;
    public float jumpSpeed = 400f;

    private Rigidbody2D m_rb;
    private SpriteRenderer m_sr;

    private bool m_bJump;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = gameObject.GetComponent<Rigidbody2D>();
        m_sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //var y = Input.GetAxis(YAxis);
        //m_rb.
      
    }

    private void FixedUpdate()
    {
        float dt = Time.deltaTime;
        MoveByX(dt);
        DoJump(dt);
    }

    void MoveByX(float dt)
    {
        //if(!m_bJump)
        {
            float hmove = Input.GetAxis(XAxis);

            if (hmove != 0)
            {
                //hmove = x * speed;
                m_rb.velocity = new Vector2(hmove * speed * dt, m_rb.velocity.y);
                m_sr.flipX = hmove < 0;
            }
        }
    }

    void DoJump(float dt)
    {
        m_bJump = Input.GetButtonDown("Jump");
        if(m_bJump)
        {
            m_rb.velocity = new Vector2(m_rb.velocity.x, jumpSpeed * dt);
        }
    }

}

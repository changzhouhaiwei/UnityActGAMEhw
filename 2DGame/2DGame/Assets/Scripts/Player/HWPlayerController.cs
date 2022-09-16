using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HWPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController controller;
    Animator animator;

    public string XAxis = "Horizontal";
    public string YAxis = "Vertical";
    Vector2 input = default(Vector2);
    
    public float gravityScale = 6.6f;
    public string lastPlayName;

    bool isGrounded;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        isGrounded = controller.isGrounded;

        if (!isGrounded)
        {
            Vector3 gravityDeltaVelocity = Physics.gravity * gravityScale * dt;
            controller.Move(gravityDeltaVelocity * dt);
        }

        bool inputAttackStop = Input.GetButtonUp("Fire1");
        bool inputAttackStart = Input.GetButtonDown("Fire1");

        input.x = Input.GetAxis(XAxis);
        input.y = Input.GetAxis(YAxis);



        if (inputAttackStart)
        {
            PlayAnim("Skill1");
        }

        if(inputAttackStop)
        {

        }

    }

    void PlayAnim(string animName)
    {
        //if(lastPlayName.Length > 0)
        //{
        //    animator.SetBool(lastPlayName, false);
        //}

        //animator.SetBool(animName, true);
        //lastPlayName = animName;

        animator.Play("Skill1",0,0f);

    }

}

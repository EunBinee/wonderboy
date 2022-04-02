using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;

    float movDir;
    public float speed=250f;
    public float JumpPower=5f;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movDir = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            rigid.velocity = new Vector2(0, JumpPower);
        }
    }
    void FixedUpdate()
    {
        rigid.AddForce(new Vector2(movDir * Time.fixedDeltaTime * speed, rigid.velocity.y));   
    }
}

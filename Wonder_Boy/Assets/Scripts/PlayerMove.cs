using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;

    float refVelocity;

    float movDir;
    public float speed = 250f;
    public float JumpPower = 5f;
    public float maxSpeed = 5.5f;
    public LayerMask whatisGround;

    public float slideRate = 0.35f;
    public float AttackSlideRate = 0.25f;

    public bool isGround;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerInput();
        GroundCheck();
        PlayerAnim();
        GroundFricton(); 

    }
    void FixedUpdate()
    {
        if (!IsPlayingAnim("Attack"))
        {
            if (PlayerFlip() || Mathf.Abs(movDir * rigid.velocity.x) < maxSpeed)
            {
                rigid.AddForce(new Vector2(movDir * Time.fixedDeltaTime * speed, 0));
            }
        }


    }

    bool IsPlayingAnim(string AnimName)
    {
        //�ִϸ��̼��� ����ǰ��ִ��� Ȯ���ϴ� �޼ҵ�
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(AnimName))
            return true;
        //AnimName�� �ִϸ��̼��� ���� ������ �ǰ� �ִ°�?
        //������ true �ƴϸ� false
        return false;
    }
    void MyAnimSetTrigger(string AnimName)
    {
        //�ִϸ��̼��� �����Ű�� �޼ҵ�
        if (!IsPlayingAnim(AnimName))
        {
            //AnimName�� ������ �ǰ� �����ʴٸ� ����!!
            anim.SetTrigger(AnimName);
        }
    }

    void PlayerInput()
    {
        movDir = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGround && !IsPlayingAnim("Attack"))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, JumpPower);
            MyAnimSetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //x�� ������ ����
            MyAnimSetTrigger("Attack");
        }
    }

    void GroundFricton()
    {
        if (isGround)
        {
            if (IsPlayingAnim("Attack"))
            {
                rigid.velocity = new Vector2(Mathf.SmoothDamp(rigid.velocity.x, 0f, ref refVelocity, slideRate + AttackSlideRate), rigid.velocity.y);
            }
            else if (Mathf.Abs(movDir) <= 0.01f)
            {
                rigid.velocity = new Vector2(Mathf.SmoothDamp(rigid.velocity.x, 0f, ref refVelocity, slideRate), rigid.velocity.y);
            }
        }
    }

    void PlayerAnim()
    {
        if(isGround&&!IsPlayingAnim("Attack"))
        {
            if ((Mathf.Abs(movDir) <= 0.01f || Mathf.Abs(rigid.velocity.x) <= 0.01f) && Mathf.Abs(rigid.velocity.y) <= 0.01f)
                MyAnimSetTrigger("Idle");
            else if (Mathf.Abs(rigid.velocity.x) > 0.01f && Mathf.Abs(rigid.velocity.y) <= 0.01f)
                MyAnimSetTrigger("Walk");
        }
    }

    bool PlayerFlip()
    {
        bool flipSprite = (spriteRenderer.flipX ? movDir > 0f : movDir < 0f);

        if(flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        return flipSprite;
    }

    void GroundCheck()
    {
        if (Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, 0.01f, whatisGround))
        {
            isGround = true;
            anim.ResetTrigger("Idle");
        }
        else
            isGround = false;
    }

    


}

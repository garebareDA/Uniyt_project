using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed;
    public float jump;
    public float down;

    private Animator anim;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private bool grounded = false;
    public LayerMask groundlayer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        bool input_a = Input.GetKeyDown("joystick button 0");
        bool input_space = Input.GetKeyDown("space");
        bool up_a = Input.GetKeyUp("joystick button 0");
        bool up_space = Input.GetKeyUp("space");
        grounded = Physics2D.Linecast(transform.position,transform.position - transform.up * 2, groundlayer);

        //Joysticの入力
        if (horizontalKey > 0)
        {
            xSpeed = Input.GetAxis("Horizontal") * speed;
            sprite.flipX = false;
           
            if(horizontalKey > 0.8)
            {
                anim.SetBool("run", true);
            }
            else
            {
                anim.SetBool("walk", true);
            }
        }
        else if(horizontalKey < 0)
        {
            sprite.flipX = true;
            xSpeed = Input.GetAxis("Horizontal") * speed;
            if (horizontalKey < -0.8)
            {
                anim.SetBool("run", true);
            }
            else
            {
                anim.SetBool("walk", true);
            }
        }
        else
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            xSpeed = 0.0f;
        }
        rigid.velocity = new Vector2(xSpeed, rigid.velocity.y);

        //ジャンプの処理
        if (input_a && grounded || input_space && grounded)
        {
            rigid.AddForce(Vector2.up * jump);
        }
        
        if (up_a || up_space)
        {
            rigid.AddForce(Vector2.down * down);
        }
    }
}

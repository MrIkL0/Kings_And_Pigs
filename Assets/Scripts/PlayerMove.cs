using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //-v- Для автоматического присваивания в переменную, радиуса коллайдера объекта «GroundCheck»
        GroundCheckRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
    }
    //------- Функция/метод, выполняемая каждый кадр в игре ---------
    void Update()
    {
        Damage();
        Walk();
        Reflect();
        Jump();
        CheckingGround();
    }

    public Transform punch;
    public float punchRadius;

    void Damage()
    {
        if(Input.GetKey(KeyCode.R))
        {
            anim.SetTrigger("press");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Fight2D.Action(punch.position, punchRadius, 11, 50, false);
        }
        //Здесь мы сообщаем функции, -точку контакта, -радиус, -номер слоя юнита, 
        // -урон по цели, и в конце обозначаем что урон получит только одна цель
        //(ближайшая от точки). Соответственно, если поставить true, урон получат все юниты попавшие в радиус
        //атаки, это удобно для супер приема например.

    }
    //------- Функция/метод для перемещения персонажа по горизонтали ---------
    public Vector2 moveVector;
    public int speed = 3;
    void Walk()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
    }
    //------- Функция/метод для отражения персонажа по горизонтали ---------
    public bool faceRight = true;
    void Reflect()
    {
        if ((moveVector.x > 0 && faceRight) || (moveVector.x < 0 && !faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }
    //------- Функция/метод для обнаружения земли ---------
    public bool onGround;
    public LayerMask Ground;
    public Transform GroundCheck;
    private float GroundCheckRadius;
    void CheckingGround()
    {
        //onGround = Physics2D.OverlapBox();
        onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
        anim.SetBool("onGround", onGround);
    }
    //------- Функция/метод для прыжка ---------
    public int jumpForce = 0;
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Physics2D.IgnoreLayerCollision(9, 10, true);
            Invoke("IgnoreLayerOff", 0.2f);
        }
        if (onGround && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }


    void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(9, 10, false);

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Reflect();
        Jump();
        CheckingPlatform();

    }
    public Vector2 moveVector;
    public float speed = 2f;


    void Run()
    {
        //To Do beter control
        moveVector.x = Input.GetAxis("Horizontal");

        anim.SetFloat("RunX", Mathf.Abs(moveVector.x));
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
    }

    public bool faceRight = true;
    void Reflect()
    {
        if((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }


    public bool onPlatform;
    public Transform PlatformCheck;
    public float checkRadius = 0.25f;
    public LayerMask Platform;

    void CheckingPlatform()
    {
        onPlatform = Physics2D.OverlapCircle(PlatformCheck.position, checkRadius, Platform);
        anim.SetBool("onPlatform", onPlatform);
    }

    //Пофіксити прижок вліво при ходьбі, працює гріше ніж прижок в право

    public float jumpForce = 210;
    private int jumpCounte = 0;
    public int maxJumpValue = 2;
    //private bool jumpControl;
    //private int jumpIteration = 0;
    //public int jumpValueIteration = 60;

    //
    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.UpArrow)) && (onPlatform || (++jumpCounte < maxJumpValue)))
        {
            rb.AddForce(Vector2.up * jumpForce);
            //if (onPlatform) { jumpControl = true; }
        }
        if (onPlatform) { jumpCounte = 0; }
        //else { jumpControl = false; }

        //if (jumpControl)
        //{
        //    if (jumpIteration++ < jumpValueIteration)
        //    {
        //        rb.AddForce(Vector2.up * jumpForce / jumpIteration);
        //    }
        //}
        //else { jumpIteration = 0; }
    }
}

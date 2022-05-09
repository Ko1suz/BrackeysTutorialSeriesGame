using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMove : MonoBehaviour
{
    // Start is called before the first frame update

   
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    [HideInInspector]
    public Rigidbody2D rb;
    private SpriteRenderer SRend;
    private Animator anim;
    private bool isGrounded;
    private enum States{idle,run,jump};
    private States state = States.idle;
    public GameObject LeftArm;
    public GameObject RightArm;
    private AudioManager audioManager;
    public string FootStepsSoundsName = "LandingFootSteps";

    public void FootSteps(){
        audioManager.PlaySound(FootStepsSoundsName);
    }

    void Start()
    {
        audioManager = AudioManager.instance;
        rb = GetComponent<Rigidbody2D>();
        SRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
       
        animatonStates();
        anim.SetInteger("States",(int)state);
        
    }

    void Move(){
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection>0)
        {
            rb.velocity = new Vector2(PlayerStats.instance.speed,rb.velocity.y); 
            SRend.flipX = false;
            // LeftArm.SetActive(false);
            // RightArm.SetActive(true);    
        }
        else if (hDirection<0)
        {
            rb.velocity = new Vector2(-PlayerStats.instance.speed,rb.velocity.y); 
            SRend.flipX = true;
            // LeftArm.SetActive(true);
            // RightArm.SetActive(false);
        }
        else
        {
            Surtunme();
        }
        
    }
    
   
    void Jump(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,checkRadius,whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x,PlayerStats.instance.jumpForce);
            
        }
    }

    void Surtunme(){
        if (isGrounded==true)
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y);
        }
    }

    void animatonStates(){
        if (isGrounded==false)
        {
            state = States.jump;
        }
        else
        {
            if (Mathf.Abs(rb.velocity.x)>1)
            {
                state = States.run;
            }
            else
            {
                state = States.idle;
            }
        }
       
    }
}

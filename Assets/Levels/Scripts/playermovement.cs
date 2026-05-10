using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    
    public Transform groundCheck;
    public Transform wallCheck;
    private float baseSpeed;
    private Coroutine speedRoutine;
    float wallJumpDir;
    float wallJumpTime = 0.2f;
    float wallJumpCounter;
    float wallJumpDuration = 0.4f;
    float moveY;
    public Vector2 wallJumpPower =  new Vector2 (10f, 20f);
    public Transform attackPoint;
    public float WallSlideSpeed;
    public float attackRange = 0.5f;
    public float climbSpeed = 5f;
    public int attackDamage = 1;
    public LayerMask enemyLayer;

    [SerializeField] public float speed = 10f;
    [SerializeField] public float moveSpeed = 5f;


    bool isWallJumping;

    
    bool isWallSliding;
    private bool isFacingRight = true;
     private bool isClimbing;
    private float vertical;
    private float horizontal;
    [HideInInspector]public Vector3 origin;

    [SerializeField] private float wallCheckDistance = 0.2f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;

    private float gravityScale;

  



    


    public float jump = 5f;
    private Rigidbody2D rb;
    private bool isGrounded = true;


    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        rb = GetComponent<Rigidbody2D>();

        baseSpeed = moveSpeed;

        gravityScale = rb.gravityScale;
    }

    public void Reset()
    {
        transform.position = origin;
    }

    // Update is called once per frame
    void Update()
    {
    
    horizontal = Input.GetAxisRaw("Horizontal");
    WallSlide();
    WallJump();

    if(horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    vertical = Input.GetAxis("Vertical");

    if(isClimbing && Mathf.Abs(vertical) > 0)
        {
            rb.gravityScale = 0f;

            float moveX = Input.GetAxisRaw ("Horizontal");
            rb.linearVelocity = new Vector2(moveX * moveSpeed, vertical * climbSpeed);
        }

        else
        {
           rb.gravityScale = gravityScale;
        }

    isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isClimbing)
    {
        rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
    }

    if (!isWallJumping)
    {
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    HandleAttack();

        if(Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }

        anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
    }

    IEnumerator SpeedBoost(float multiplier, float duration)
    {
        moveSpeed  = baseSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        moveSpeed = baseSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }



    public IEnumerator BoostJump(float boostAmount, float boostDuration)
    {
        jump += boostAmount;
        yield return new WaitForSeconds(boostDuration);
       
       

    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        if(speedRoutine != null)
        StopCoroutine(speedRoutine);

        speedRoutine = StartCoroutine(SpeedBoost(multiplier, duration));
    }

    bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, wallCheckDistance, wallLayer);
    }

    void StopWallJump()
    {
        isWallJumping = false;
    }
        
    void WallSlide()
    {
        if (isWalled() && !isGrounded && horizontal != 0f) 
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -WallSlideSpeed, float.MaxValue));
        }

        else
        {
            isWallSliding = false;
        }
    }

    void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDir = -transform.localScale.x;
            wallJumpCounter = wallJumpTime;
            CancelInvoke(nameof(StopWallJump));

            
        }

        {
            wallJumpCounter -= Time.deltaTime;
        }
        
        if(Input.GetButtonDown("Jump") && wallJumpCounter > 0f)
        {
            isWallJumping= true;
            rb.linearVelocity = new Vector2(wallJumpDir * wallJumpPower.x, wallJumpPower.y);
            wallJumpCounter = 0f;

            if (transform.localScale.x != wallJumpDir) 
            {
                isFacingRight = !isFacingRight;
                Vector3 localSCale = transform.localScale;
                localSCale.x *= -1;
                transform.localScale = localSCale;
            }


        }

        Invoke(nameof(StopWallJump), wallJumpDuration);
    }

    void HandleAttack() // f key attack + key pressing input
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Attack();
            Debug.Log("ATTACK PRESSED");
        }

        // void Attack()
        // {
        //     Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, enemyLayer);

        //     if(hit != null)
        //     {
        //         Vector2 direction = (hit.transform.position - transform.position).normalized;
        //         hit.GetComponent<EnemyHealth>().TakeDamage(attackDamage, direction);
        //     }
        // }
    }
    void Attack()
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer);

            foreach (Collider2D enemy in enemies)
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if(enemyHealth != null)
                {
                // if(enemyRb != null)
                    enemyHealth.TakeDamage(attackDamage, (transform.localScale.x > 0 ? -1 : 1) * Vector2.right); // change transform.forward hinto the hit direction when u attack.
                }
            }
        }
}
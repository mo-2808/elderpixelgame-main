using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform wallCheck;

    float wallJumpDir;
    float wallJumpTime = 0.2f;
    float wallJumpCounter;
    float wallJumpDuration = 0.4f;
    public Vector2 wallJumpPower =  new Vector2 (10f, 20f);
    public float WallSlideSpeed;

    bool isWallJumping;
    bool isWallSliding;
    private bool isFacingRight = true;
    private float horizontal;
    [HideInInspector]public Vector3 origin;

    [SerializeField] private float wallCheckDistance = 0.2f;
    [SerializeField] private LayerMask wallLayer;

    

  

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0f, moveZ);
        transform.Translate(move * speed * Time.deltaTime);
    }


    public float jump = 5f;
    private Rigidbody2D rb;
    private bool isGrounded = true;


    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Reset()
    {
        transform.position = origin;
    }

    // Update is called once per frame
    void Update()
    {
        WallSlide();
        WallJump();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jump, (ForceMode2D)ForceMode.Impulse); 
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if(!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


    public IEnumerator BoostSpeed(float boostAmount, float boostDuration)
    {
        speed += boostAmount;
        yield return new WaitForSeconds(boostDuration);
    }

    public IEnumerator BoostJump(float boostAmount, float boostDuration)
    {
        jump += boostAmount;
        yield return new WaitForSeconds(boostDuration);
       
       

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

        else
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
}
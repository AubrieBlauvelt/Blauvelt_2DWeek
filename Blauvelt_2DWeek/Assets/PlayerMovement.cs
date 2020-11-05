using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class PlayerMovement : MonoBehaviour
{
    public TextMeshProUGUI countText;

    Rigidbody2D rB2D;

    private int count;

    public float runSpeed;
    public float jumpSpeed;

    public Animator animator;

    public SpriteRenderer spriteRenderer; 

    // Start is called before the first frame update
    void Start()
    {
        rB2D = GetComponent<Rigidbody2D>();

        count = 0;

        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            int levelMask = LayerMask.GetMask("Level");

            if (Physics2D.BoxCast(transform.position, new Vector2(1f, .1f), 0f, Vector2.down, .01f, levelMask))
            {
                Jump();
            } 
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }


    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        rB2D.velocity = new Vector2(horizontalInput * runSpeed * Time.deltaTime, rB2D.velocity.y);

        if (rB2D.velocity.x > 0)
            spriteRenderer.flipX = false;
        else
        if (rB2D.velocity.x < 0)
            spriteRenderer.flipX = true;

        if (Mathf.Abs(horizontalInput) > 0f)
            animator.SetBool("IsRunning", false);
        else
            animator.SetBool("IsRunning", true);
    }

    void Jump()
    {
        rB2D.velocity = new Vector2(rB2D.velocity.x, jumpSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Cherry"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarlipoController : MonoBehaviour
{
    public Collider2D collider;
    public Collision2D col;
    public Animator animator;
    bool isCollided = false;
    public Rigidbody2D rb;

    public GameObject dialogFrame;
    public Text dialogMagictext;
    private bool[] CollideDirection;

    public float moveSpeed;
    public float moveX, moveY;
    public Vector2 moveDirection;

    public GameObject shopPanel;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dialogFrame.SetActive(false);
        dialogMagictext.text = null;

        animator.SetFloat("Horizontal", 1);
        animator.SetFloat("Vertical", 0);
        moveY = 0; moveX = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        moveDirection = new Vector2(moveX, moveY).normalized;
        rb.transform.Translate(moveDirection * Time.deltaTime * moveSpeed);
        if (isCollided)
        {
            moveSpeed = 0;
            if (Input.GetKeyDown(KeyCode.C))
            {
                dialogFrame.SetActive(true);
                dialogMagictext.text = "要买点什么吗？我这里应有尽有。（按G打开商店）";
            }
            if (!shopPanel.active&&Input.GetKeyDown(KeyCode.G))
            {
                shopPanel.SetActive(true);
                dialogFrame.SetActive(true) ;
                dialogMagictext.text = "再次按G关闭商店";
            }
            if (shopPanel.active && Input.GetKeyDown(KeyCode.G))
            {
                shopPanel.SetActive(false);
                dialogFrame.SetActive(false) ;
                dialogMagictext.text = "";
            }
        }
        if (rb.transform.position.x <= -6.59f || rb.transform.position.y >= 2.23F || rb.transform.position.x >= 7.51f || rb.transform.position.y <= -4)
        {
            float moveTurn;
            moveTurn = moveX;
            moveX = -moveY;
            moveY = -moveTurn;
            animator.SetFloat("Horizontal", moveX);
            animator.SetFloat("Vertical", moveY);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollided = true;
            moveSpeed = 0;
            if (collision.contacts[0].normal.y == -1)
            {
                moveX = 0; moveY = 1;
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 1);
            }
            else if (collision.contacts[0].normal.y == 1)
            {
                moveX = 0; moveY = -1;
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", -1);
            }
            else if (collision.contacts[0].normal.x == -1)
            {
                moveY = 0; moveX = 1;
                animator.SetFloat("Horizontal", 1);
                animator.SetFloat("Vertical", 0);

            }
            else if (collision.contacts[0].normal.x == 1)
            {
                moveY = 0; moveX = -1;
                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Vertical", 0);

            }
            animator.SetFloat("Speed", 0);
        }
        //else
        //{
        //    float moveTurn;
        //    moveTurn = moveX;
        //    moveX = moveY;
        //    moveY = moveTurn;
        //    animator.SetFloat("Horizontal", moveX);
        //    animator.SetFloat("Vertical", moveY);
        //}

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isCollided = false;
        dialogFrame.SetActive(false);
        dialogMagictext.text = null;
        animator.SetFloat("Speed", 1);
        moveSpeed = 2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NPCSpecialController : MonoBehaviour
{
    //public Collider2D collider;
    public Animator animator;
    bool isCollided = false;
    public Rigidbody2D rb;

    //public GameObject dialogFrame;
    //public Text dialogMagictext;


    public float moveSpeed;
    public float moveX, moveY;
    public Vector2 moveDirection;

    //public GameObject shopPanel;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //dialogFrame.SetActive(false);
        //dialogMagictext.text = null;
        moveSpeed = 0; moveY = 0; moveX = 1;

        //animator.SetFloat("Speed", 1);
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", -1);

        //shopPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector2(moveX, moveY).normalized;
        rb.transform.Translate(moveDirection * Time.deltaTime * moveSpeed);
        //if (isCollided)
        //{
        //    moveSpeed = 0;
        //    if (Input.GetKeyDown(KeyCode.C))
        //    {
        //        dialogFrame.SetActive(true);
        //        dialogMagictext.text = "要买点什么吗？我这里应有尽有。（按G打开商店）";
        //    }
        //    if (Input.GetKeyDown(KeyCode.G))
        //    {
        //        shopPanel.SetActive(true);
        //        dialogFrame.SetActive(true);
        //        dialogMagictext.text = "按Tab键关闭商店";
        //    }
        //    if (Input.GetKeyDown(KeyCode.Tab))
        //    {
        //        shopPanel.SetActive(false);
        //        dialogFrame.SetActive(false);
        //        dialogMagictext.text = "";
        //    }
        //}


    }
    private void FixedUpdate()
    {

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
        else
        {
            moveSpeed = 0;
            animator.SetFloat("Speed", 0);
            //Invoke("turnBack", 2f);


        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isCollided = false;
        //dialogFrame.SetActive(false);
        //dialogMagictext.text = null;
        //animator.SetFloat("Speed", 1);
        moveSpeed = 0;
    }
}

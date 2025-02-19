using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    //public Collider2D collider;
    public Animator animator;
    bool isCollided=false;
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
        try
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            dialogFrame.SetActive(false);
            dialogMagictext.text = null;
            moveSpeed = 1; moveY = 0; moveX = 1;

            animator.SetFloat("Speed", 1);
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Vertical", 0);

            shopPanel.SetActive(false);
        }
        catch(NullReferenceException e)
        {
            Debug.LogWarning(e.Message); 
        }
        
    }

    // Update is called once per frame
    void Update()
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
            if (Input.GetKeyDown(KeyCode.G))
            {
                shopPanel.SetActive(true);
                dialogFrame.SetActive(true);
                dialogMagictext.text = "按Tab键关闭商店";
            }
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                shopPanel.SetActive(false);
                dialogFrame.SetActive(false);
                dialogMagictext.text = "";
            }
        }
        

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
            Invoke("turnBack", 2f);

            
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isCollided = false;
        dialogFrame.SetActive(false);
        dialogMagictext.text = null;
        animator.SetFloat("Speed", 1);
        moveSpeed = 1;
    }
    private void turnBack()
    {
        moveSpeed = 1;
        animator.SetFloat("Speed", 1);
        moveX = -moveX;
        moveY = -moveY;
        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        Invoke("turnAndGo", 1f);
    }
    private void turnAndGo()
    {
        try
        {
            moveSpeed = 1;
            animator.SetFloat("Speed", 1);
            float moveTurn;
            int[] randomNumber = { 1, -1, 0 };
            moveTurn = randomNumber[UnityEngine.Random.Range(0, randomNumber.Length)] * moveX;
            moveX = randomNumber[UnityEngine.Random.Range(0, randomNumber.Length)] * moveY;
            moveY = moveTurn;
            if (moveX * moveY != 0)
            {
                moveX = UnityEngine.Random.Range(-1, 1) < 0 ? moveX : 0;
                moveY = UnityEngine.Random.Range(-1, 1) >= 0 ? moveY : 0;
            }
            else if (moveX == 0 && moveY == 0)
            {
                moveX = UnityEngine.Random.Range(-1, 1) < 0 ? randomNumber[UnityEngine.Random.Range(0, randomNumber.Length - 1)] : 0;
                moveY = UnityEngine.Random.Range(-1, 1) >= 0 ? randomNumber[UnityEngine.Random.Range(0, randomNumber.Length - 1)] : 0;
            }
            animator.SetFloat("Horizontal", moveX);
            animator.SetFloat("Vertical", moveY);
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
        
    }
}

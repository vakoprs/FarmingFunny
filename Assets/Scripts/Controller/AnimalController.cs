using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public Animator animator;
    public int AnimalID;
    public string Type;
    bool isCollided = false;
    public Rigidbody2D rb;

    //private int Feed, Month, Health;
    private int restBegin, restEnd;
    public bool noInterrupted;
    public float moveSpeed;
    public float moveX, moveY;
    public Vector2 moveDirection;

    public int matureIn;
    public string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject self = GetComponent<GameObject>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        restBegin = UnityEngine.Random.Range(15, 30);
        restEnd = UnityEngine.Random.Range(30, 45);
        moveSpeed = (matureIn == 1) ? 0.5f : 0.8f;
        AnimalID = UnityEngine.Random.Range(0, 200);



        //Ëæ»ú·½Ïò
        int r1= UnityEngine.Random.Range(0, 100);
        int r2= UnityEngine.Random.Range(0,100);
        if (r1<=50)
        {
            moveX = 0;
            if (r2 <=50)
            {
                moveY = 1;
            }
            else moveY = -1;
        }
        else
        {
            moveY = 0;
            if(r2 <= 50)
            {
                moveX = 1;
            }else moveX = -1;
        }

        noInterrupted = false;
        animator.SetFloat("Speed", 1);
        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);

        GameObject animal = GetComponent<GameObject>();
        //AnimalProducingController animalProducingController = animal.GetComponent<AnimalProducingController>();
        //if(animalProducingController != null)
        //{
        //    Feed = animalProducingController.getFeed();
        //    Month = animalProducingController.getMonth();
        //    Health = animalProducingController.getHealth();
        //}
        //Animal.InsertAnimalToDatabase(GetComponent<GameObject>());
        Animal.InsertAnimalToDatabase(animal);

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            moveDirection = new Vector2(moveX, moveY).normalized;
            rb.transform.Translate(moveDirection * Time.deltaTime * moveSpeed);


            if (TimeSystemController.Instance.showSeconds == restBegin)
            {
                moveSpeed = 0;
                animator.SetFloat("Speed", 0);
                animator.SetBool("isRest", true);
                noInterrupted = true;

            }
            if (TimeSystemController.Instance.showSeconds == restEnd)
            {
                noInterrupted = false;
                turnAndGo();
            }
        }catch(Exception e)
        {
            Debug.LogException(e);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            GameObject animalSelf = GetComponent<GameObject>();
            Animal.InsertAnimalToDatabase(animalSelf);
            if (!noInterrupted)
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
        }catch (Exception e)
        {
            Debug.LogException(e);
        }
        
        

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!noInterrupted)
        {
            try
            {
                isCollided = false;
                animator.SetFloat("Speed", 1);
                moveSpeed = (matureIn == 1) ? 0.5f : 0.8f;
            }catch (Exception e)
            {
                Debug.LogException(e);
            }
            

        }
        
    }
    private void turnBack()
    {
        moveSpeed = (matureIn == 1) ? 0.5f : 0.8f;
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
            moveSpeed = (matureIn == 1) ? 0.5f : 0.8f;
            animator.SetFloat("Speed", 1);
            animator.SetBool("isRest", false);
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

        }catch(Exception e)
        {
            Debug.LogException(e);
        }
        
    }
}

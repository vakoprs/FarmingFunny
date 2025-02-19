using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HospitalController : MonoBehaviour
{
    private GameObject NPC;

    public Text Curing;
    public GameObject Cure;

    private bool show;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        NPC = GetComponent<GameObject>();
        Cure.SetActive(false);
        Curing.text = "";
        show = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (show && Input.GetKeyDown(KeyCode.R))
            {
                Cure.SetActive(true);
                Curing.text = "你的病已经治好了！";
                player.GetComponent<PlayerController>().setFatigue(0);
                player.GetComponent<PlayerController>().happy.SetActive(true);
            }
            else if (show == false)
            {
                Cure.SetActive(false);
                Curing.text = "";
                player = null;
            }
        }
        catch(System.Exception) {
            Debug.Log("W");
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            if (!collision.collider.CompareTag("Player"))
            {
                throw new farm.ColliderException(collision.collider.gameObject.name);
            }
            player = collision.collider.GetComponent<PlayerController>();
            if (collision.gameObject.CompareTag("Player") && collision.collider.GetComponent<PlayerController>().getFatigue() > 80)
            {
                show = true;
            }
        }
        catch(farm.ColliderException e)
        {
            Debug.Log(e.Message);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        show = false;
    }
}

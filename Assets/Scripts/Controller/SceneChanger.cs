using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject MainCharacter;

    public GameObject Old;

    public GameObject New;

    public bool canTrans;

    public Transform transformDest;
    // Start is called before the first frame update
    void Start()
    {
        canTrans = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            if (collision.gameObject.CompareTag("Player") && canTrans)
            {
                canTrans = false;
                MainCharacter.transform.position = transformDest.position;
                New.SetActive(true);
                Old.SetActive(false);

            }
            if (!collision.gameObject.CompareTag("Player"))
            {
                throw new farm.ColliderException(collision.gameObject.name);
            }
        }
        catch(farm.ColliderException e)
        {
            Debug.Log(e.Message);
        }
        catch(System.Exception e)
        {
            Debug.LogError(e);
        }
        
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canTrans = true;
    }
}

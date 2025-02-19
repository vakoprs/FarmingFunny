using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateController : MonoBehaviour
{
    public static UpdateController Instance { get; private set; }
    public GameObject PrefabOld;
    public GameObject PrefabNew;
    public bool update;
    // Start is called before the first frame update
    void Start()
    {
        // update = GoodsSpecial.Instance.canBuy;
        update = false;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (GoodsSpecial.Instance == null)
            {
                throw new farm.InstanceNullException("ÊµÌåÎª¿Õ");
            }
            update = GoodsSpecial.Instance.canBuy;
            if (update == false)
            {
                PrefabOld.SetActive(true);
                PrefabNew.SetActive(false);
            }
            else if (update == true)
            {
                PrefabNew.SetActive(true);
                PrefabOld.SetActive(false);
            }
        }
        catch (farm.InstanceNullException e)
        {
            Debug.Log(e.Message);
        }
        //if (update)
        //{
        //    PrefabOld.SetActive(false);
        //    PrefabNew.SetActive(true);
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInformation : MonoBehaviour
{
    public static NPCInformation Instance { get; private set; }
    public string name;
    public int age;
    public string work;
    private int love;
    private ItemData like;
    public string sex;

    public int getLove()
    {
        return love;
    }

    public ItemData getLike()
    {
        return like;
    }
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            GameObject NPC = GetComponent<GameObject>();
            NPCFavorability NPCfavorability = NPC.GetComponent<NPCFavorability>();
            Instance = this;
            this.love = NPCfavorability.getFavor();
            this.like = NPCfavorability.like;
            if (Instance == null)
            {
                throw new farm.InstanceNullException("NPCInfoÊµÀýÎª¿Õ");
            }
        }
        catch(farm.InstanceNullException e)
        {
            Debug.Log(e.Message);
        }
        catch(System.Exception e)
        {
            Debug.LogError(e.Message);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

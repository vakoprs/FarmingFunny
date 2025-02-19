using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScene : MonoBehaviour
{

    public GameObject someNPC;
    // Start is called before the first frame update
    void Start()
    {

        initNPC();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initNPC()
    {
        //GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        //foreach (GameObject npc in npcs)
        //{
        //    NPC.InsertNPCToDatabase(npc);
        //    Debug.Log("≤Â»Î" + npc.name + "≥…π¶ £°");
        //}

        NPC.InsertNPCToDatabase();
        GameObject thisGO=GetComponent<GameObject>();
        //GameObject new1=Instantiate(someNPC);
        //new1.transform.SetParent(thisGO.transform,false);
        //new1.transform.position= thisGO.transform.position;
    }
}

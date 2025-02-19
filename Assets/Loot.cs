using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [Range(0, 100), Header("此战利品掉落的概率")]
    public int dropChance;
    
    public Loot(string lootName, int dropChance)
    {
        this.dropChance = dropChance;
    }
}

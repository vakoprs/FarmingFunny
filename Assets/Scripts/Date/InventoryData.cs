using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//数据仓库
[CreateAssetMenu]
public class InventoryData:ScriptableObject
{
    public List<SlotData> slotList;
    
}

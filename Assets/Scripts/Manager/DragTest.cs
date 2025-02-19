using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//拖拽栏内物品，监听
public class DragTest : MonoBehaviour
{
    public void BeginDrag()
    {
        print("BeginDrag:"+gameObject);
    }

    public void OnDrag()
    {
        print("OnDrag:"+gameObject);
    }

    public void EndDrag()
    {
        print("EndDrag:"+gameObject);
    }

    public void OnDrop()
    {
        
    }
}

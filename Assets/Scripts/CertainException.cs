using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace farm
{
    //选择为空
    internal class SlotDataNullException : Exception
    {
        public SlotDataNullException(string message) : base(message) { }
    }
    
    //购买为空
    internal class GoodNullReferenceException : Exception
    {
        public GoodNullReferenceException(string message) : base(message) { }
    }

    //碰撞体不对
    internal class ColliderException : Exception
    {
        public ColliderException(string message) : base(message) { }
    }

    //实体为空
    internal class InstanceNullException : Exception
    {
        public InstanceNullException(string message) : base(message) { }
    }
}


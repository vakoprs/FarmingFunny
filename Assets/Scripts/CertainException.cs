using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace farm
{
    //ѡ��Ϊ��
    internal class SlotDataNullException : Exception
    {
        public SlotDataNullException(string message) : base(message) { }
    }
    
    //����Ϊ��
    internal class GoodNullReferenceException : Exception
    {
        public GoodNullReferenceException(string message) : base(message) { }
    }

    //��ײ�岻��
    internal class ColliderException : Exception
    {
        public ColliderException(string message) : base(message) { }
    }

    //ʵ��Ϊ��
    internal class InstanceNullException : Exception
    {
        public InstanceNullException(string message) : base(message) { }
    }
}


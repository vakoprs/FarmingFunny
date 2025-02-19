using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothing;

    //��������λ�ý�����������ƶ�
    //public Vector2 minPosition;
    //public Vector2 maxPosition;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {

    }
    void LateUpdate()
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector3 targetPos = target.position;
                //targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
                //targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
                transform.position = Vector2.Lerp(transform.position, targetPos, smoothing);
            }
        }
    }
}




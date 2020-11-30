using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindObjectOfType<PlatformerController>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y != target.position.y)
        {
            Vector3 movetoPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
            transform.position = movetoPos;
        }
    }
}

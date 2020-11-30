using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    #region variables

    //variable for speed
    public float speed;

    //bool to control moving right
    private bool movingRight = true;

    //where the raycast comes out from
    private Transform groundDetect;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //get the grounddetect gameobject
        groundDetect = transform.Find("groundDetect");
    }

    // Update is called once per frame
    void Update()
    {
        //move character right
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        //Raycast for detecting ground
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, 2f);

        //If we don't detect any ground
        if(groundInfo.collider == false)
        {
            //If no ground and moving right
            if(movingRight == true)
            {
                //move left
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                //move right
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}

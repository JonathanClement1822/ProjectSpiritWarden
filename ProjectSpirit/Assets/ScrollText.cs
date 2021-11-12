using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollText : MonoBehaviour
{

    public float scrollSpeed = 20;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 localVectorUp = transform.TransformDirection(0, 1, 0);
        pos+= localVectorUp * scrollSpeed*Time.deltaTime;
        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            scrollSpeed = scrollSpeed + 5;

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {

            scrollSpeed = scrollSpeed - 5;
        }
    }
}

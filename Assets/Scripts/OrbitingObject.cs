using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrbitingObject : MonoBehaviour
{

    public int positionOffset;

    void Start()
    {
        m_camera = Camera.main;  
    }

    void Update()
    {
        var lookAtPos = Input.mousePosition;
        lookAtPos.z = transform.position.z - m_camera.transform.position.z;
        lookAtPos = m_camera.ScreenToWorldPoint(lookAtPos);
        transform.up = lookAtPos - transform.position;
        

        
    }

    Camera m_camera;
}


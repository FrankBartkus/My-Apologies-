using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject caMERA;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cam = caMERA.transform.position;
        cam.x = transform.position.x;
        cam.y = transform.position.y;
        caMERA.transform.position = cam;
    }
}

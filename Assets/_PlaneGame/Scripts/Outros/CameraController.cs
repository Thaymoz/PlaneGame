using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("An array of transforms representing camera position")]
    [SerializeField] Transform[] povs;
    [Tooltip("The speed at wich the camera follows the plane")]
    [SerializeField] float speed;

    private int index = 1;
    private Vector3 target;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        // Numbers 1-4 represent different povs 
        if (Input.GetKeyDown(KeyCode.Alpha1)) index = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) index = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) index = 2;

        // set our target to the relevant POV
        target = povs[index].position;
    }

    private void FixedUpdate()
    {
        //Move camera to desired position/orientation. Must be in FixedUpdate to avoid camera jitters.
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        transform.forward = povs[index].forward;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCamera : MonoBehaviour
{
    public GameObject cameraLiga;
    public GameObject cameraDesliga;


    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has the "Player" tag
        if (other.CompareTag("Player"))
        {
            cameraLiga.SetActive(true);
            cameraDesliga.SetActive(false);
            Debug.Log("teste");
        }


    }
}

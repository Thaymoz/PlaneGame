// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Aro : MonoBehaviour
// {
    
//     [Header("Configuracao do Boost")]
//     [Tooltip("O quanto a velocidade do player sera aumentada ao passar pelo aro.")]
//     [SerializeField] private float boostAmount = 15f;

//     [Tooltip("Aumento de velocidade aplicado apenas uma vez.")]
//     [SerializeField] private float oneTimeSpeedIncrease = 10f;
//     private void OnTriggerEnter(Collider other)
//     {

//         if (other.gameObject.CompareTag("Player"))
//         {
            
//             voo2 playerVooScript = other.gameObject.GetComponent<voo2>();

 
//             if (playerVooScript != null)
//             {
              
//             }
//         }
//     }
// }
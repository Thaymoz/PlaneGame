using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // A câmera atualmente ativa na cena
    private GameObject activeCamera;

    // Função principal que os Triggers irão chamar
    public void ActivateNewCamera(GameObject newCamera)
    {
        // Se a nova câmera é a mesma que já está ativa, ignora
        if (activeCamera == newCamera)
        {
            return;
        }

        // 1. Desliga a câmera que estava ativa antes
        if (activeCamera != null)
        {
            activeCamera.SetActive(false);
        }

        // 2. Liga a nova câmera
        newCamera.SetActive(true);
        activeCamera = newCamera; // Atualiza a referência
        
        Debug.Log("Câmera ativada: " + newCamera.name);
    }
}
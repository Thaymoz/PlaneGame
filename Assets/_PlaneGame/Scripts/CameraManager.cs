using UnityEngine;
using UnityEngine.Animations;

public class CameraManager : MonoBehaviour
{
    // A câmera atualmente ativa na cena
    private GameObject activeCamera;

    private void SetAimConstraintEnabled(GameObject cameraObject, bool enabled)
    {
        if (cameraObject == null) return;
        var constraint = cameraObject.GetComponent<AimConstraint>();
        if (constraint != null)
        {
            (constraint as Behaviour).enabled = enabled;
        }
        else
        {
            Debug.LogWarning($"AimConstraint não encontrado na câmera {cameraObject.name}. Certifique-se de que ele está na raiz do objeto da câmera.");
        }
    }


    // Função principal que os Triggers irão chamar
    public void ActivateNewCamera(GameObject newCamera)
    {
        if (activeCamera == newCamera)
        {
            return;
        }

        // 1. Desliga o Aim Constraint da câmera que estava ativa ANTES de desligar o GameObject
        if (activeCamera != null)
        {
            SetAimConstraintEnabled(activeCamera, false);
            activeCamera.SetActive(false); // Desliga o GameObject
        }

        // 2. Liga a nova câmera
        newCamera.SetActive(true);

        // 3. ⭐ Solução: Liga o Aim Constraint da nova câmera APÓS ligar o GameObject
        // Isso força o Constraint a se recalibrar imediatamente, corrigindo a rotação.
        SetAimConstraintEnabled(newCamera, true);
        
        activeCamera = newCamera; // Atualiza a referência
        
        Debug.Log($"Câmera ativada: {newCamera.name}. Aim Constraint reativado.");
    }
}
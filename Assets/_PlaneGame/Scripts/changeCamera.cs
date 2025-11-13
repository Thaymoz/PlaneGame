using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [Header("Configuração Local")]
    [Tooltip("Arraste a Câmera que deve ser ligada quando o Player entrar.")]
    public GameObject targetCamera; 

    // Variável para guardar a referência do nosso CameraManager
    private CameraManager cameraManager;

    void Start()
    {
        // Encontra o ÚNICO CameraManager na cena e armazena a referência
        cameraManager = FindObjectOfType<CameraManager>();

        if (cameraManager == null)
        {
            Debug.LogError("O CameraManager não foi encontrado na cena! A troca de câmera não vai funcionar.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checa se o objeto que colidiu é o Player
        if (other.CompareTag("Player"))
        {
            // Checa se o Gerenciador foi encontrado e se a Câmera alvo está configurada
            if (cameraManager != null && targetCamera != null)
            {
                // Chama a função no CameraManager que encontramos no Start()
                cameraManager.ActivateNewCamera(targetCamera);
            }
        }
    }
}
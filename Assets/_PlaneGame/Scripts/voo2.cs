using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class voo2 : MonoBehaviour
{
    [Header("Movimento e Velocidade")]
    [SerializeField] private float flySpeed = 0f;
    [SerializeField] private float flySpeed2 = 0f;
    //[SerializeField] private float quedaDeVelocidade = 1f;

    [Header("Controle de Rotação")]
    [SerializeField] private float yawAmount = 120f;
    [SerializeField] private float pitchAmount = 45f;
    [SerializeField] private float rollAmount = 60f;

    [Header("Suavização")]
    [SerializeField] private float yawSmoothness = 5f;
    [SerializeField] private float rollSmoothness = 10f;
    [SerializeField] private float pitchSmoothness = 10f; // NOVO: Suavidade do Pitch!

    [Header("Física Manual")]
    [SerializeField] private float gravidade = 1f;

    [Header("Morte")]
    [SerializeField] private Gamemanager gameManager; 
    private bool isDead = false;

    // === Variáveis Privadas de Estado ===
    private Coroutine desaceleracaoCoroutine;
    private float yaw;
    private float pitch;
    private float roll;
    private float currentRoll;
    private float currentPitch; // NOVO: Usado para suavizar o pitch

    // private void Start()
    // {
    //     gameManager = FindObjectOfType<Gamemanager>();
    // }

    private void Update()
    {
        if (isDead) return;

        transform.position += flySpeed * Time.deltaTime * transform.forward;
        transform.position += Vector3.down * gravidade * Time.deltaTime;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float targetYawChange = yawAmount * horizontalInput;
        float smoothedYawChange = Mathf.Lerp(0, targetYawChange, yawSmoothness * Time.deltaTime);
        yaw += smoothedYawChange;

        float targetPitch = Mathf.Lerp(0, pitchAmount, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);

        currentPitch = Mathf.Lerp(currentPitch, targetPitch, pitchSmoothness * Time.deltaTime);

        pitch = currentPitch;

        float targetRoll = Mathf.Lerp(0, rollAmount, Mathf.Abs(horizontalInput)) * Mathf.Sign(horizontalInput);
        currentRoll = Mathf.Lerp(currentRoll, targetRoll, rollSmoothness * Time.deltaTime);
        roll = currentRoll;

        transform.localRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.back * roll);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            flySpeed += flySpeed2;

    //         if (desaceleracaoCoroutine != null)
    //         {
    //             StopCoroutine(desaceleracaoCoroutine);
    //         }
    //         desaceleracaoCoroutine = StartCoroutine(MorrendoCoroutine());
    //     }
    // }

    // private IEnumerator MorrendoCoroutine()
    // {
    //     while (flySpeed > 5)
    //     {
    //         yield return new WaitForSeconds(1f);

    //         flySpeed -= quedaDeVelocidade;

    //         if (flySpeed < 5)
    //         {
    //             flySpeed = 5;
    //         }
        }
    }

    // public void AplicarBoost(float boost, float oneTimeIncrease)
    // {
    //     flySpeed += boost;

    //     if (desaceleracaoCoroutine != null)
    //     {
    //         StopCoroutine(desaceleracaoCoroutine);
    //     }
    //     desaceleracaoCoroutine = StartCoroutine(MorrendoCoroutine());

    //     flySpeed += oneTimeIncrease;
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;
        if (gameManager == null)
        {
             Debug.LogError("GameManager nulo na colisão. Abortando verificação.");
             return;
        }

        string requiredTag = gameManager.GetSelectedTag();
        string objectTag = other.gameObject.tag;

        if (objectTag == requiredTag)
        {
            Debug.Log("Colidiu com a tag correta: " + requiredTag);
            Destroy(gameObject);
        }
        else if (objectTag == "verificadores")
        {
            Debug.Log("passou por um verificador");
            return;
        }
        else
        {
            Debug.Log("Colidiu com a tag INCORRETA ou Obstáculo: " + objectTag + ". Requerida: " + requiredTag);
            HandleDeath();
        }
    }
    
    private void HandleDeath()
    {
        isDead = true;
        flySpeed = 0f;
        
        // Coloque aqui o código para mostrar a tela de Game Over, carregar a cena, etc.
        Debug.Log("Fim de Jogo. Aperte R para reiniciar (Exemplo).");
        // Time.timeScale = 0f; // Congelar o tempo
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voo2 : MonoBehaviour
{
    // === Variáveis de Configuração ===
    [Header("Movimento e Velocidade")]
    [SerializeField] private float flySpeed = 0f;
    [SerializeField] private float flySpeed2 = 0f;
    [SerializeField] private float quedaDeVelocidade = 1f;

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

    // === Variáveis Privadas de Estado ===
    private Coroutine desaceleracaoCoroutine;
    private float yaw;
    private float pitch;
    private float roll;
    private float currentRoll;
    private float currentPitch; // NOVO: Usado para suavizar o pitch

    private void Update()
    {
        // 1. APLICAÇÃO DO MOVIMENTO E GRAVIDADE
        transform.position += flySpeed * Time.deltaTime * transform.forward;
        transform.position += Vector3.down * gravidade * Time.deltaTime;

        // 2. OBTENÇÃO DOS INPUTS
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 3. CÁLCULO DAS ROTAÇÕES E SUAVIZAÇÃO

        // YAW (Guinada - Eixo Y): Acumulativo e Suavizado (Mantido)
        float targetYawChange = yawAmount * horizontalInput;
        float smoothedYawChange = Mathf.Lerp(0, targetYawChange, yawSmoothness * Time.deltaTime);
        yaw += smoothedYawChange;

        // PITCH (Inclinação - Eixo X): CORRIGIDO E SUAVIZADO!

        // 1. Calcula o valor ALVO para o Pitch
        float targetPitch = Mathf.Lerp(0, pitchAmount, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);

        // 2. Interpola o valor ATUAL (currentPitch) para o valor ALVO (targetPitch)
        currentPitch = Mathf.Lerp(currentPitch, targetPitch, pitchSmoothness * Time.deltaTime);

        // 3. Aplica o Pitch Suavizado
        pitch = currentPitch;

        // ROLL (Rolamento - Eixo Z): Suavização da transição (Mantido)
        float targetRoll = Mathf.Lerp(0, rollAmount, Mathf.Abs(horizontalInput)) * Mathf.Sign(horizontalInput);
        currentRoll = Mathf.Lerp(currentRoll, targetRoll, rollSmoothness * Time.deltaTime);
        roll = currentRoll;

        // 4. APLICAÇÃO DA ROTAÇÃO FINAL
        transform.localRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.back * roll);

        // 5. ACELERAÇÃO E CONTROLE DA DESACELERAÇÃO
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flySpeed += flySpeed2;

            if (desaceleracaoCoroutine != null)
            {
                StopCoroutine(desaceleracaoCoroutine);
            }
            desaceleracaoCoroutine = StartCoroutine(MorrendoCoroutine());
        }
    }

    // CORROTINA: Reduz a velocidade linearmente (1 ponto a cada 1 segundo) (Mantida)
    private IEnumerator MorrendoCoroutine()
    {
        while (flySpeed > 5)
        {
            yield return new WaitForSeconds(1f);

            flySpeed -= quedaDeVelocidade;

            if (flySpeed < 5)
            {
                flySpeed = 5;
            }
        }
    }
    // --- ADICIONE ESTE MÉTODO AO FINAL DO SEU SCRIPT 'voo2' ---
    // Este método é chamado pelo Aro para dar um "empurrão"
    public void AplicarBoost(float boost, float oneTimeIncrease)
    {
        // Aumenta a velocidade base
        flySpeed += boost;

        // Simula a aceleração com a Barra de Espaço
        if (desaceleracaoCoroutine != null)
        {
            StopCoroutine(desaceleracaoCoroutine);
        }
        desaceleracaoCoroutine = StartCoroutine(MorrendoCoroutine());

        // Se quiser um aumento de velocidade extra e imediato:
        flySpeed += oneTimeIncrease;
    }
    // -----------------------------------------------------------
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aro : MonoBehaviour
{
    // A. VARIÁVEIS DE CONFIGURAÇÃO
    [Header("Configuração do Boost")]
    [Tooltip("O quanto a velocidade do player será aumentada ao passar pelo aro.")]
    [SerializeField] private float boostAmount = 15f;

    [Tooltip("Aumento de velocidade aplicado apenas uma vez.")]
    [SerializeField] private float oneTimeSpeedIncrease = 10f;

    // B. FUNÇÃO DE COLISÃO / TRIGGER
    // Requer que o objeto 'Aro' tenha um Collider com 'Is Trigger' marcado no Inspetor.
    private void OnTriggerEnter(Collider other)
    {
        // 1. VERIFICAÇÃO DA TAG
        // Checa se o objeto que entrou no trigger tem a tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // 2. TENTATIVA DE ACESSO AO SCRIPT VOADORES
            // Tenta obter o componente 'voo2' no objeto que colidiu (o Player)
            voo2 playerVooScript = other.gameObject.GetComponent<voo2>();

            // 3. APLICAÇÃO DO BOOST
            // Verifica se o script 'voo2' foi encontrado
            if (playerVooScript != null)
            {
                // Acessa a função de aceleração do script 'voo2' e aplica o boost
                // Eu criei um método público no voo2 para fazer isso (veja abaixo)
                playerVooScript.AplicarBoost(boostAmount, oneTimeSpeedIncrease);

                // **OPCIONAL:** Desativar/Destruir o Aro após o uso.
                // Destroy(gameObject); 
                // OU: gameObject.SetActive(false);
            }
        }
    }
}
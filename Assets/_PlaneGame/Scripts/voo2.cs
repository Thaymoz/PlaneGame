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

    private float yaw;
    private float pitch;
    private float roll;
    private float currentRoll;
    private float currentPitch; 

    [SerializeField] private Gamemanager gameManager; 
    private bool isDead = false;
    [SerializeField] private Transform startPoint;

//referencia de efeitos
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem particulasVitoria;
    [SerializeField] private AudioSource soundFX;
    [SerializeField] private AudioClip successFX;
    [SerializeField] private AudioClip faillFX;
    [SerializeField] private AudioSource flySound;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isDead) return;
        transform.position += flySpeed * Time.deltaTime * transform.forward;
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
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("doFlip");
        }
        if (flySound != null)
        {
            // Apenas começa a tocar SE o avião está em movimento E o som está parado.
            if (flySpeed > 0 && !flySound.isPlaying)
            {
                flySound.Play();
            }
            // Apenas para de tocar SE o avião está parado E o som está tocando.
            else if (flySpeed <= 0 && flySound.isPlaying)
            {
                flySound.Stop();
            }
        }
    }

    public void impulsoInicial()
    {
        flySpeed += flySpeed2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        string requiredTag = gameManager.GetSelectedTag();
        string objectTag = other.gameObject.tag;

        if (objectTag == requiredTag)
        {
            Debug.Log("Colidiu com a tag correta: " + requiredTag);
            gameManager.checkList();
            particulasVitoria.Play();
            soundFX.PlayOneShot(successFX,1.0f);
            flySpeed = 0f;
        }
        else if (objectTag == "verificadores")
        {
            Debug.Log("passou por um verificador");
            return;
        }
        else
        {
            flySpeed = 0f;
            soundFX.PlayOneShot(faillFX,1.0f);
            Debug.Log("Colidiu com a tag INCORRETA ou Obstáculo: " + objectTag + ". Requerida: " + requiredTag);
            StartCoroutine(ResetToStart(startPoint.position));
            
        }
    }
    
    public IEnumerator ResetToStart(Vector3 newPosition) 
    {
        rb.velocity = Vector3.zero;
        isDead = false;
        flySpeed = 0f;
        yield return new WaitForSeconds(1.0f);
        gameManager.tagDisplay.text = "";
        gameManager.menuHud.SetActive(true);
        transform.position = newPosition;
        transform.localRotation = Quaternion.identity; 
        yaw = 0f;
        pitch = 0f;
        roll = 0f;
        currentPitch = 0f;
        currentRoll = 0f;

        Debug.Log("Player resetado para a posição inicial após delay.");
    }
}
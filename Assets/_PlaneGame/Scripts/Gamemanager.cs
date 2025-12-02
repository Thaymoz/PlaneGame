using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.EditorTools;
public class Gamemanager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tagDisplay;
    [SerializeField] private voo2 scriptPlayer;
    [SerializeField] private Transform startPoint;

    public List<string> tagList = new List<string>()
    {
    };
    private string selectedTag;
    [SerializeField] private GameObject taskListParent;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetGame();
        }
    }
    public void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    void ChooseRandomTag()
    {
        int randomIndex = Random.Range(0, tagList.Count);
        selectedTag = tagList[randomIndex];
    }
    //Metodo para pegar a tag escolhida
    public string GetSelectedTag()
    {
        return selectedTag;
    }

    public void startGame()
    {
        scriptPlayer.impulsoInicial();
        ChooseRandomTag();
        tagDisplay.text = "Alvo: " + selectedTag;
    }

    public void ExitGame()
    {
    Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
    public void checkList()
    {
        scriptPlayer.StartCoroutine(scriptPlayer.ResetToStart(startPoint.position));
        tagDisplay.text = "Seu próximo alvo é";
        Transform childTransform = taskListParent.transform.Find(selectedTag);
        if (childTransform != null)
        {
        // Tenta obter o componente TextMeshProUGUI neste filho
            TextMeshProUGUI taskText = childTransform.GetComponent<TextMeshProUGUI>();

            if (taskText != null)
            {
            // 3. Aplica a lógica de Riscado (StrikeThrough)
                string originalText = taskText.text;
            
                if (!originalText.Contains("<s>"))
                {
                    taskText.text = $"<s>{originalText}</s>"; // Aplica o StrikeThrough
                    Debug.Log($"Tarefa '{selectedTag}' riscada na UI.");
                }
            }
        }
    }
    
}

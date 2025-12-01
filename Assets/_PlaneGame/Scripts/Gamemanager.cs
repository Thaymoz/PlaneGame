using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Gamemanager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tagDisplay;
    [SerializeField] private voo2 scriptPlayer;


    public List<string> tagList = new List<string>()
    {
        "TagA",
        "TagB",
        "TagC",
        "TagD",
        "TagE",
        "TagF"
    };
    private string selectedTag;
    
    void Start()
    {
        ChooseRandomTag();
        tagDisplay.text = "Alvo: " + selectedTag;
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

    }

    public void ExitGame()
    {
    Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}

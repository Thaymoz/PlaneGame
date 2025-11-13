using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tagDisplay;

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
}

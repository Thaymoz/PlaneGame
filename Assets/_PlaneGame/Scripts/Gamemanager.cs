using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
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
        
        //Colocar aqui para alterar o texto
        Debug.Log("A tag aleatória escolhida é: " + selectedTag);
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

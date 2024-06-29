using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectCharacter : MonoBehaviour
{

    public ScrollRect ScrollRect;
    void Start()
    {
        
    }

    public void ClickBtnSelect()
    {
        SceneManager.LoadScene("SchoolSceneDay");
    }
    public void ClickBtnReturn()
    {
        SceneManager.LoadScene("LogIn");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public InputField iField;
    public Text highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        if(DataManager.Instance.scores.Count != 0)
        {
            iField.text = DataManager.Instance.playerName;
            highscoreText.gameObject.SetActive(true);
            highscoreText.text = "Best Score : " + DataManager.Instance.scores[0].playerName + " : " + DataManager.Instance.scores[0].currentScore;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newNameEntered()
    {
        if(iField.text != DataManager.Instance.playerName)
        {
            DataManager.Instance.playerName = iField.text;
            DataManager.Instance.currentScore = 0;
        }
        
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void ScoresScreen()
    {
        SceneManager.LoadScene("Scores");
    }

    public void Exit()
    {
        //MainManager.Instance.SaveColor();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

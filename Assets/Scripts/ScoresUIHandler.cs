using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoresUIHandler : MonoBehaviour
{
    public List<Text> scores;

    void Awake()
    {
        DisplayScores();
    }

    void DisplayScores()
    {
        for(int i = 0; i < DataManager.Instance.scores.Count; i++)
        {
            if (DataManager.Instance.scores.Count != 0)
            {
                scores[i].text = DataManager.Instance.scores[i].playerName + ": " + DataManager.Instance.scores[i].currentScore;
            }           
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

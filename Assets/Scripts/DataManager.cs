using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public List<Score> scores = new List<Score>();

    public string playerName;
    public int currentScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScores();
    }

    [System.Serializable]
    class SaveData
    {
        public List<Score> scores = new List<Score>();
    }

    [System.Serializable]
    public class Score
    {
        public string playerName;
        public int currentScore;
    }

    public void SaveScore()
    {       
        SaveData data = new SaveData();
        if (DataManager.Instance.scores.Count != 0)
        {
            //Work out if/where the score should be saved
            Score score = new Score();
            score.currentScore = DataManager.Instance.currentScore;
            score.playerName = DataManager.Instance.playerName;


            //copy old list
            List<Score> highscoreCopy = scores;
            //new list
            List<Score> newHighscore = new List<Score>();
            //get index to insert
            int newIndex = 11;
            //go through list and add scores
            for(int i = 0; i < scores.Count; i++)
            {
                if(score.currentScore >= scores[i].currentScore)
                {
                    newIndex = i;
                    break;
                }
            }

            if(score.currentScore < scores[scores.Count - 1].currentScore)
            {
                newIndex = scores.Count;
            }

            // if index is off table then stop
            if(newIndex > 10)
            {
                return;
            }

            newHighscore = highscoreCopy;
            newHighscore.Insert(newIndex, score);
            if(newHighscore.Count > 10)
            {
                newHighscore.RemoveRange(10, newHighscore.Count - 10);
            }            

            data.scores = newHighscore;
            scores = newHighscore;
        }
        else if(DataManager.Instance.scores.Count == 0)
        {
            Score score = new Score();
            score.currentScore = currentScore;
            score.playerName = playerName;
            data.scores.Add(score);
            scores.Add(score);
        }

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            scores = data.scores;
        }
    }
}

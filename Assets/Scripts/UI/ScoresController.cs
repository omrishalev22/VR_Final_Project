using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ScoresController : MonoBehaviour
{
    private static readonly string SCORES_PLAYER_PREFS_KEY = "scores";
    public static ScoresController instance;
    private List<HighscoreEntry> highscoreEntryList;

    [System.Serializable]
    public class HighscoreEntry
    {
        public int score;
    }

    [System.Serializable]
    private class Highscores
    {
        public List<HighscoreEntry> scores;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (highscoreEntryList == null)
        {
            // Load the scores from PlayerPrefs or creates a new scores list
            if (PlayerPrefs.HasKey(SCORES_PLAYER_PREFS_KEY))
            {
                highscoreEntryList = JsonUtility.FromJson<Highscores>(PlayerPrefs.GetString(SCORES_PLAYER_PREFS_KEY)).scores;
            } else
            {
                highscoreEntryList = new List<HighscoreEntry>();
            }
            
        }
    }

    private void SortHighscoreEntryList(List<HighscoreEntry> highscoreEntryList)
    {
        for (int i = 0; i < highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscoreEntryList.Count; j++)
            {
                if (highscoreEntryList[j].score > highscoreEntryList[i].score)
                {
                    HighscoreEntry tempEntry = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = tempEntry;
                }
            }
        }
    }

    public void AddHighscoreEntry(int score)
    {
        HighscoreEntry newEntry = new HighscoreEntry { score = score };
        highscoreEntryList.Add(newEntry);
        SortHighscoreEntryList(highscoreEntryList);
        SaveScores();

    }

    public List<HighscoreEntry> GetHighscoreEntries(int firstEntries)
    {
        return highscoreEntryList.GetRange(0, Math.Min(firstEntries, highscoreEntryList.Count));
    }

    public void SaveScores()
    {
        PlayerPrefs.SetString(SCORES_PLAYER_PREFS_KEY, JsonUtility.ToJson(new Highscores { scores = highscoreEntryList }));
        PlayerPrefs.Save();
    }
}

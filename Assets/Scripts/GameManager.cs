using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour {

    public static GameManager singleton;
    public LevelProgressUI levelProgressUI;
    public Interstitial interstitialAds;
    public int best;
    public int score;
    public int currentStage = 0;

    private void Start()
    {
        interstitialAds = GameObject.FindGameObjectWithTag("AdsManager").GetComponent<Interstitial>();
    }
    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        // Load the saved highscore
        best = PlayerPrefs.GetInt("Highscore");
    }

    public void NextLevel()
    {
        currentStage++;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
        levelProgressUI.SetLevelTexts(currentStage);
    }

    public void RestartLevel()
    {
        interstitialAds.ShowAd();
        Debug.Log("Restarting Level");
        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if (score > best)
        {
            PlayerPrefs.SetInt("Highscore", score);
            best = score;
        }
    }


}

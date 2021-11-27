using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreText : MonoBehaviour
{
    public Text scoreText;
    private void Start()
    {
        ChangeScore();
    }
    
    public void ChangeScore()
    {
        scoreText.text = $"목표 점수 = 2022\n현재 점수 : {GameManager.Instance.KillCount}";
    }
}

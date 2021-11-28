using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextManager : MonoBehaviour
{
    public static TextManager Instance { get; private set; }

    public Text txtRound;
    public Text txtLeftEnemy;
    public Text txtMoney;
    public Text txtSpawn;

    [Header("사용되지 않음")] public Text scoreText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeScore();
    }
    
    public void ChangeScore()
    {
        //scoreText.text = $"목표 점수 = 2022\n현재 점수 : {GameManager.Instance.KillCount}";
        txtRound.text = $"{GameManager.Instance.RoundCount + 1}";
        txtLeftEnemy.text = $"{GameManager.Instance.GetRoundEnemy() - GameManager.Instance.KillCount}";
    }
}

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
        scoreText.text = $"��ǥ ���� = 2022\n���� ���� : {GameManager.Instance.KillCount}";
    }
}

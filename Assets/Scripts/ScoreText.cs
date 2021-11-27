using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreText : MonoBehaviour
{
    public Text scoreText;
    public float KillMob = 0;
    private void Start()
    {
        //if(잡을때마다)
        scoreText.text = "목표 점수 = 2022" + "\n현재 점수 : " + KillMob.ToString();
    }

}

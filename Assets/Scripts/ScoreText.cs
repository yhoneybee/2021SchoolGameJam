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
        //if(����������)
        scoreText.text = "��ǥ ���� = 2022" + "\n���� ���� : " + KillMob.ToString();
    }

}

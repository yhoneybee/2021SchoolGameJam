using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Tutorial : MonoBehaviour
{
    Image image;
    [SerializeField] Sprite[] sprite;
    int nowsprite = 0;
    void Start()
    {
        image = GetComponent<Image>();
    }
    public void BottonClick()
    {
        if (nowsprite >= 16)
        {
            SceneManager.LoadScene("Ingame");
        }
        if (nowsprite < 17)
        {
            image.sprite = sprite[nowsprite];
        }
        nowsprite++;
    }
}

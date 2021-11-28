using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LifeLinker : MonoBehaviour
{
    public List<Image> imgLifes;

    private void Update()
    {
        switch (Player.life)
        {
            case 2:
                imgLifes[2].DOColor(Color.black, 1);
                break;
            case 1:
                imgLifes[1].DOColor(Color.black, 1);
                break;
            case 0:
                imgLifes[0].DOColor(Color.black, 1).onComplete = () =>
                {
                    // GameOver
                };
                break;
        }
    }
}

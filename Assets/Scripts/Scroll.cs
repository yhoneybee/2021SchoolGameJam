using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Scroll : MonoBehaviour
{
    public List<Image> img;
    public Text txtRound;

    RectTransform rtrn0, rtrn1;
    Sequence nextRound;

    private void Start()
    {
        rtrn0 = img[0].GetComponent<RectTransform>();
        rtrn1 = img[1].GetComponent<RectTransform>();

        img[0].DOFade(1, 1);
        img[1].DOFade(1, 1);
        nextRound = DOTween.Sequence()
            .SetAutoKill(false)
            .Insert(0, txtRound.DOText("NEXT ROUND", 0.7f))
            .Insert(0, rtrn0.DOAnchorPosX(-933, 0.7f))
            .Insert(0, rtrn1.DOAnchorPosX(0, 0.7f))
            .Insert(0.7f, rtrn0.DOAnchorPosX(933, 0))
            .Insert(0.7f, rtrn1.DOAnchorPosX(0, 0))
            .SetLoops(3);
        nextRound.onComplete = () =>
        {
            img[0].DOFade(0, 1);
            img[1].DOFade(0, 1);
            txtRound.text = "";
            GameManager.Instance.RoundCount++;
            GameManager.Instance.KillCount = 0;
            gameObject.SetActive(false);
        };
    }

    private void OnEnable()
    {
        img[0].DOFade(1, 1);
        img[1].DOFade(1, 1);
        nextRound.Restart();
    }
}

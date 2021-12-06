using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[CreateAssetMenu(fileName = "GumchihoSkill", menuName = "Datas/GumchihoSkill", order = 0)]
public class Gumchiho : CombineSkillData
{
    public override void OnCombine()
    {
        base.OnCombine();
        //ObjPool.ActiveEnemies[0].Pos -= 1;
        foreach (var enemy in ObjPool.ActiveEnemies)
        {
            Debug.Log(enemy.name);
            var dir = GameManager.Instance.poss[enemy.Pos - 1].anchoredPosition - enemy.GetComponent<RectTransform>().anchoredPosition;
            enemy.GetComponent<RectTransform>().DOAnchorPos(dir * 2, 1).SetRelative();
        }
    }
}
// MVC
// 모델, 뷰, 컨트롤
// 내부적으로 돌아가는 -> 모델
// 보여주는 것 -> 뷰
// 인게임에서 상호작용 -> 컨트롤
// View => 보여주는것
// 껍데기 <= 주사위 정보 ( DiceData )

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Gumchiho", menuName = "Datas/Gumchiho", order = 0)]
public class Gumchiho : CombineSkillData
{
    public override void OnCombine(Dice dice)
    {
        base.OnCombine(dice);
        //ObjPool.ActiveEnemies[0].Pos -= 1;
        foreach (var enemy in ObjPool.ActiveEnemies)
        {
            var dir = GameManager.Instance.poss[enemy.Pos - 1].anchoredPosition - enemy.GetComponent<RectTransform>().anchoredPosition;
            enemy.GetComponent<Rigidbody2D>().AddForce(dir * 3);
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

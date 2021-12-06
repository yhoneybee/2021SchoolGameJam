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
// ��, ��, ��Ʈ��
// ���������� ���ư��� -> ��
// �����ִ� �� -> ��
// �ΰ��ӿ��� ��ȣ�ۿ� -> ��Ʈ��
// View => �����ִ°�
// ������ <= �ֻ��� ���� ( DiceData )

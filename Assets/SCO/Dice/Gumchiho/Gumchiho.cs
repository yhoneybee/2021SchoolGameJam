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
// ��, ��, ��Ʈ��
// ���������� ���ư��� -> ��
// �����ִ� �� -> ��
// �ΰ��ӿ��� ��ȣ�ۿ� -> ��Ʈ��
// View => �����ִ°�
// ������ <= �ֻ��� ���� ( DiceData )

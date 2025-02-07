using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTwo : Character
{
    private Magic_Boom _Boom;
    private readonly float SkillCool = 5;
    private readonly float skillTimer = 5;
    private readonly float value = 3;
    private Coroutine coroutine;
    //��ų����Ʈ
    [SerializeField]
    private Projective projective;

    new void Start()
    {
        base.Start();
        _Boom = new Magic_Boom(GameManager.Instance.GetPlayer);
        GameManager.Instance.GetPlayer.Armory.Addon(_Boom);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    protected override void Skill()
    {
        coroutine ??= StartCoroutine(SkillTimer());
    }

    //�׽�Ʈ�� ���� ���ݷ��� ��½�Ű�� ��ų�� ����� ����
    private IEnumerator SkillTimer()
    {
        projective.gameObject.SetActive(true);
        GameManager.Instance.GetPlayer.Stat.AttackDamage += value;
        yield return new WaitForSeconds(skillTimer);
        GameManager.Instance.GetPlayer.Stat.AttackDamage -= value;
        yield return new WaitForSeconds(SkillCool);
        coroutine = null;
        projective.gameObject.SetActive(false);
    }
}

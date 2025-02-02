using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOne : Character
{
    private Magic_Axe _Axe;
    private readonly float SkillCool = 5;
    private readonly float skillTimer = 5;
    private readonly float value = 3;
    private Coroutine coroutine;
    [SerializeField]
    private Projective projective;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        _Axe = new Magic_Axe(GameManager.Instance.GetPlayer);
        GameManager.Instance.GetPlayer.Armory.Addon(_Axe);
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

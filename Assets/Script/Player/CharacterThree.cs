using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterThree : Character
{
    private Magic_Bullet arrow;
    private readonly float SkillCool = 5;
    private readonly float skillTimer = 5;
    private readonly float value = 3;
    private Coroutine coroutine;
    //��ų����Ʈ
    [SerializeField]
    private Projective projective;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        arrow = new Magic_Bullet(GameManager.Instance.GetPlayer);
        GameManager.Instance.GetPlayer.Armory.Addon(arrow);
        GameManager.Instance.GetPlayer.CardSelect.Addons.Add(arrow);
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

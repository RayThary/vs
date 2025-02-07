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
    //스킬이펙트
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

    //테스트를 위해 공격력을 상승시키는 스킬을 만들어 보자
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSelect : MonoBehaviour
{
    //인스펙터에서 테스트를 가능하게 해주는 용도
    [SerializeField]
    private bool on;
    public bool On { get { return on; } set {  on = value; } }

    [SerializeField]
    private List<CardUI> card;

    private List<IAddon> addons;
    public List<IAddon> Addons { get => addons; }
    private IAddon[] candidate = new IAddon[3];

    //플레이어에게 무기가 이미 5종류 있는가
    //없다면 전체에서 랜덤 3개
    //있다면 이미 있는것에서 랜덤 3개
    //추가적으로 업그레이드 카드들은 포함

    // Start is called before the first frame update
    void Start()
    {
        addons = new()
        {
            new Magic_6(GameManager.Instance.GetPlayer),
            new Magic_8(GameManager.Instance.GetPlayer),
            new Magic_9(GameManager.Instance.GetPlayer),
            new Magic_10(GameManager.Instance.GetPlayer),
            new Magic_15(GameManager.Instance.GetPlayer),
            new Magic_18(GameManager.Instance.GetPlayer),

            new Armor(),
            new AttackCool(),
            new AttackCount(),
            new AttackDamage(),
            new AttackRange(),
            new AttackSpeed(),
            new HP(),
            new HPRecovery(),
            new LifeAbsorption(),
            new ShieldPoint(),
            new SkillAmp(),
            new SkillCool(),
            new SkillDamage(),
            new Speed(),

        };
    }

    // Update is called once per frame
    void Update()
    {
        if(On)
        {
            //시간을 멈춰야 함
            GameManager.Instance.TimeScale = 0;
            RandomCard();
            on = false;
        }
    }

    public void RandomCard()
    {
        //플레이어에게 무기가 5개 미만
        if(GameManager.Instance.GetPlayer.Armory.Addons.Count(x => x.Weapon) < 5)
        {
            var random = new System.Random();

            candidate = addons
                .Where(x => x.Level < x.MaxLevel)
                .OrderBy(_ => random.Next())
                .Take(3)
                .ToArray();

            for(int i = 0; i < candidate.Length; i++)
            {
                if (candidate[i] != null)
                    card[i].Init(candidate[i]);
            }
        }
        //플레이어에게 무기가 5개
        else
        {
            var random = new System.Random();

            candidate = GameManager.Instance.GetPlayer.Armory.Addons
                        .Where(x => x.Level < x.MaxLevel)
                        .Where(x => x.Weapon)
                        .Concat(addons.Where(x => !x.Weapon).Where(x => x.Level < x.MaxLevel))
                        .Distinct(new AddonComparer()) // 중복 제거
                        .Take(3)
                        .ToArray();

            for (int i = 0; i < candidate.Length; i++)
            {
                if (candidate[i] != null)
                    card[i].Init(candidate[i]);
            }
        }
    }

    public void Select(int id)
    {
        card.ForEach(x => x.gameObject.SetActive(false));
        GameManager.Instance.GetPlayer.Armory.Addon(candidate[id]);
        candidate = new IAddon[3];
        //시간 재생
        GameManager.Instance.TimeScale = 1;
    }
}

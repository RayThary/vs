using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSelect : MonoBehaviour
{
    //�ν����Ϳ��� �׽�Ʈ�� �����ϰ� ���ִ� �뵵
    [SerializeField]
    private bool on;
    public bool On { get { return on; } set {  on = value; } }

    [SerializeField]
    private List<CardUI> card;

    private List<IAddon> addons;
    public List<IAddon> Addons { get => addons; }
    private IAddon[] candidate = new IAddon[3];

    //�÷��̾�� ���Ⱑ �̹� 5���� �ִ°�
    //���ٸ� ��ü���� ���� 3��
    //�ִٸ� �̹� �ִ°Ϳ��� ���� 3��
    //�߰������� ���׷��̵� ī����� ����

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
            //�ð��� ����� ��
            GameManager.Instance.TimeScale = 0;
            RandomCard();
            on = false;
        }
    }

    public void RandomCard()
    {
        //�÷��̾�� ���Ⱑ 5�� �̸�
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
        //�÷��̾�� ���Ⱑ 5��
        else
        {
            var random = new System.Random();

            candidate = GameManager.Instance.GetPlayer.Armory.Addons
                        .Where(x => x.Level < x.MaxLevel)
                        .Where(x => x.Weapon)
                        .Concat(addons.Where(x => !x.Weapon).Where(x => x.Level < x.MaxLevel))
                        .Distinct(new AddonComparer()) // �ߺ� ����
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
        //�ð� ���
        GameManager.Instance.TimeScale = 1;
    }
}

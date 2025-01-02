using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour
{
    //�ν����Ϳ��� �׽�Ʈ�� �����ϰ� ���ִ� �뵵
    [SerializeField]
    private bool on;
    public bool On { get { return on; } set {  on = value; } }

    [SerializeField]
    private List<CardUI> card;

    private List<IAddon> addons = new ();
    private IAddon[] candidate = new IAddon[3];

    //private Magic_6 magic_6;
    //private Magic_7 magic_7;
    //private Magic_8 magic_8;
    //private Magic_9 magic_9;
    //private Magic_15 magic_15;

    //�÷��̾�� ���Ⱑ �̹� 5���� �ִ°�
    //���ٸ� ��ü���� ���� 3��
    //�ִٸ� �̹� �ִ°Ϳ��� ���� 3��
    //�߰������� ���׷��̵� ī����� ����

    // Start is called before the first frame update
    void Start()
    {
        addons = new()
        {
            new Magic_6(GameManager.Instance.GetPlayer, 1),
            //new Magic_7(GameManager.Instance.GetPlayer), �̰��� 6���� ���׷��̵�
            new Magic_8(GameManager.Instance.GetPlayer, 3, 1),
            //new Magic_9(GameManager.Instance.GetPlayer, 5, 10, 1, 1),
            //new Magic_15(GameManager.Instance.GetPlayer, 3, 1),

            //new Armor(),
            //new AttackCool(),
            //new AttackCount(),
            new AttackDamage(),
            //new AttackRange(),
            //new AttackSpeed(),
            //new HP(),
            //new HPRecovery(),
            //new LifeAbsorption(),
            //new ShieldPoint(),
            //new SkillAmp(),
            //new SkillCool(),
            //new SkillDamage(),
            //new Speed(),
            
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

            card[0].Init(candidate[0]);
            card[1].Init(candidate[1]);
            card[2].Init(candidate[2]);
        }
        else
        {
            //�÷��̾�� ���Ⱑ 5��
            var random = new System.Random();

            candidate = GameManager.Instance.GetPlayer.Armory.Addons
                        .Where(x => x.Level < x.MaxLevel)
                        .Where(x => x.Weapon)
                        .Concat(addons.Where(x => !x.Weapon).Where(x => x.Level < x.MaxLevel))
                        .Distinct(new AddonComparer()) // �ߺ� ����
                        .ToArray();

            card[0].Init(candidate[0]);
            card[1].Init(candidate[1]);
            card[2].Init(candidate[2]);
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

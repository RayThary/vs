using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour
{
    //인스펙터에서 테스트를 가능하게 해주는 용도
    [SerializeField]
    private bool on;
    public bool On { get { return on; } set {  on = value; } }

    [SerializeField]
    private List<Image> card;

    private List<IAddon> addons = new ();
    private IAddon[] candidate = new IAddon[3];

    private Magic_9 fire;
    private Magic_15 magic_15;
    private Magic_8 magic_8;
    private Magic_6 magic_6;
    private Magic_7 magic_7;

    //플레이어에게 무기가 이미 5종류 있는가
    //없다면 전체에서 랜덤 3개
    //있다면 이미 있는것에서 랜덤 3개
    //추가적으로 업그레이드 카드들은 포함

    // Start is called before the first frame update
    void Start()
    {
        addons = new()
        {
            new Magic_6(GameManager.Instance.GetPlayer, 1),
            //new Magic_7(GameManager.Instance.GetPlayer), 이것은 6번의 업그레이드
            new Magic_8(GameManager.Instance.GetPlayer, 3, 1),
            new Magic_9(GameManager.Instance.GetPlayer, 5, 10, 1, 1),
            new Magic_15(GameManager.Instance.GetPlayer, 3, 1),
            new AttackDamage()
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
        List<int> list = new ();

        if(GameManager.Instance.GetPlayer.Armory.Addons.Count(x => x.Weapon) < 5)
        {
            var random = new System.Random();

            candidate = addons
                .OrderBy(_ => random.Next())
                .Take(3)
                .ToArray();

            card[0].sprite = candidate[0].Sprite;
            card[1].sprite = candidate[1].Sprite;
            card[2].sprite = candidate[2].Sprite;
        }
        else
        {
            //플레이어에게 무기가 5개
            var random = new System.Random();

            candidate = GameManager.Instance.GetPlayer.Armory.Addons
                .OrderBy(_ => random.Next())     // 랜덤 정렬
                .Take(3)                         // 최대 3개 선택
                .ToArray();

            card[0].sprite = candidate[0].Sprite;
            card[1].sprite = candidate[1].Sprite;
            card[2].sprite = candidate[2].Sprite;
        }

        card[0].gameObject.SetActive(true);
        card[1].gameObject.SetActive(true);
        card[2].gameObject.SetActive(true);
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

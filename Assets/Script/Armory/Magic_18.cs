using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_18 : IAddon
{
    public string AddonName => "18";

    public Sprite Sprite => GameManager.Instance.Magic[17];

    private readonly string description;
    public string Description => description;

    public bool Weapon => true;

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    private readonly Player player;

    private readonly List<Projective> projectives = new();

    //����ü �ӵ�
    private readonly float speed;
    //�����
    private readonly float damage;
    //���� ������
    private readonly float delay;

    //���� ������ ��� Ÿ�̸�
    private float timer;

    private bool enhance = false;
    public bool Enhance { get { return enhance; }  set { enhance = value; } }

    public Magic_18(Player player)
    {
        description = "�ϴÿ��� �������鼭 ���ظ� ������";
        this.player = player;
        speed = 10;
        damage = 1;
        delay = 5;
        level = 0;
        cam = Camera.main;
        CalculateWorldSize();
    }

    public void Addon()
    {
        level = 1;
        timer = Time.time;
    }

    //ũ�Ⱑ Ŀ������
    public void LevelUp()
    {
        level++;

        projectives.ForEach(x => x.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f));
        if (level == MaxLevel)
        {
            //18 + �������� = +5
            var power = player.Armory.Addons.OfType<LifeAbsorption>().FirstOrDefault();
            if (power != null && power.Level == power.MaxLevel)
            {
                if(!enhance)
                    enhance = true;
            }
        }
    }

    public void Remove()
    {
        level = 0;
        //��� �߻�ü ����
        projectives.ForEach(x => PoolingManager.Instance.RemovePoolingObject(x.gameObject));
        projectives.ForEach(x => x.transform.localScale = new Vector3(1, 1, 1));
        projectives.Clear();
        enhance = false;
    }

    public void Update()
    {
        //���� �����̰� �Ǿ�����
        if (timer + (delay - player.Stat.AttackCool) <= Time.time)
        {
            GameManager.Instance.StartCoroutine(Fire());
            timer = Time.time;
        }
    }

    private Rect rect;
    private readonly Camera cam;
    //�밢������ ������ ��ġ�� �����ؼ� �߻�
    private IEnumerator Fire()
    {
        for (int i = 0; i < player.Stat.AttackCount + 1; i++)
        {
            //ȭ���� ���� ���� ������ ���� ��ġ
            //����ü ����
            Projective projective = PoolingManager.Instance.CreateObject(PoolingManager.ePoolingObject.Magic18, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
            projective.Init();
            //���⼭ ������ �޾ƿ�
            Vector2 dir = new(1, -1);

            //rect.yMax������ ���� player.selectcharacter���� ������ ���̸�ŭ 
            float distance = rect.yMax - player.SelectCharacter.transform.position.y;
            projective.transform.position = new Vector3(Random.Range(player.SelectCharacter.transform.position.x - distance - 5, player.SelectCharacter.transform.position.x - distance + 6), rect.yMax);
            projective.transform.eulerAngles = new Vector3(0, 0, 45);
            projective.transform.localScale = new Vector3(level, level, 0);
            projective.Attributes.Add(new P_Move(projective, dir, speed));
            projective.Attributes.Add(new P_Damage(this, damage));
            projective.Attributes.Add(new P_DeleteTimer(projective, 10));
            if (enhance)
            {
                projective.Attributes.Add(new P_EnterCreate(PoolingManager.ePoolingObject.Magic5, this));
            }
            projectives.Add(projective);
            yield return new WaitForSeconds(0.1f);
        }
    }
    void CalculateWorldSize()
    {
        float size = cam.orthographicSize; // ī�޶��� Orthographic Size
        float aspectRatio = (float)Screen.width / Screen.height; // ȭ�� ����

        // ���� ���̿� �ʺ� ���
        float worldHeight = size * 2;
        float worldWidth = worldHeight * aspectRatio;

        rect = new()
        {
            xMin = -worldWidth * 0.5f,
            xMax = worldWidth * 0.5f,
            yMin = -worldHeight * 0.5f,
            yMax = worldHeight * 0.5f
        };
    }
}

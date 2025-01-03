using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magic_18 : IAddon
{
    public string AddonName => "18";

    public Sprite Sprite => GameManager.Instance.Magic[17];

    private string description;
    public string Description => description;

    public bool Weapon => true;

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }

    private int level;
    public int Level { get => level; set => level = value; }

    public int MaxLevel => 5;

    private readonly Player player;
    //�߻��� �߻�ü ����
    private readonly Projective projective;

    private readonly List<Projective> projectives = new();

    //����ü �ӵ�
    private readonly float speed;
    //�����
    private readonly float damage;
    //���� ������
    private readonly float delay;

    //���� ������ ��� Ÿ�̸�
    private float timer;

    public Magic_18(Player player)
    {
        projective = Resources.Load<Projective>("Magic/Magic_18");
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

    public void LevelUp()
    {

    }

    public void Remove()
    {

    }

    public void Update()
    {
        //���� �����̰� �Ǿ�����
        if (timer + delay <= Time.time)
        {
            for (int i = 0; i < player.Stat.AttackCount + 1; i++)
            {
                Fire();
            }
            timer = Time.time;
        }
    }

    private Rect rect;
    private Camera cam;
    //�밢������ ������ ��ġ�� �����ؼ� �߻�
    private void Fire()
    {
        //ȭ���� ���� ���� ������ ���� ��ġ
        //����ü ����
        Debug.Log("������Ʈ Ǯ���� ������� �ʴ� ����");
        Projective projective = Object.Instantiate(this.projective);
        projective.Init();
        //���⼭ ������ �޾ƿ�
        Vector2 dir = new(1, -1);

        projective.transform.position = new Vector3(Random.Range(rect.xMin, rect.xMin + (rect.xMax - rect.yMax)), rect.yMax);
        projective.transform.eulerAngles = new Vector3(0, 0, 45);
        projective.Attributes.Add(new P_Move(projective, dir, speed));
        projective.Attributes.Add(new P_Damage(this, damage));
        projectives.Add(projective);
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
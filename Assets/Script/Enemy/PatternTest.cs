using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PatternTest : IAddon
{
    private Projective virtical;
    private Projective horizontal;
    private List<Projective> projectives;

    public PatternTest()
    {
        virtical = Resources.Load<Projective>("VerticalPattern");
        horizontal = Resources.Load<Projective>("HorizontalPattern");
        projectives = new();
    }

    public string AddonName => "보스 패턴 테스트";

    public Sprite Sprite => null;

    public string Description => "일단은 보스 테스트 용으로 만들어 봄";

    public bool Weapon => false;

    private float statistics;
    public float Statistics { get => statistics; set => statistics = value; }
    public int Level { get => 0; set { } }

    public int MaxLevel => 0;

    public void Addon() { }
    public void LevelUp() { }
    public void Remove() { }
    public void Update() { }

    public void Left()
    {
        //-19
        //투사체 설정
        Projective projective = Object.Instantiate(virtical);
        projective.Init();

        projective.transform.localPosition = new Vector3(-19, 0, 0);
        projective.Attributes.Add(new P_Move(projective, Vector2.right, 4));
        projective.Attributes.Add(new P_DistanceDelete(40, new Vector2(-19, 0), projective));
        projective.Attributes.Add(new P_PlayerDamage(this, 1));
        projectives.Add(projective);
    }

    public void Right()
    {
        //19
        //투사체 설정
        Projective projective = Object.Instantiate(virtical);
        projective.Init();

        projective.transform.localPosition = new Vector3(19, 0, 0);
        projective.Attributes.Add(new P_Move(projective, Vector2.left, 4));
        projective.Attributes.Add(new P_DistanceDelete(40, new Vector2(19, 0), projective));
        projective.Attributes.Add(new P_PlayerDamage(this, 1));
        projectives.Add(projective);
    }

    public void Top()
    {
        //-19
        //투사체 설정
        Projective projective = Object.Instantiate(horizontal);
        projective.Init();

        projective.transform.localPosition = new Vector3(0, 11, 0);
        projective.Attributes.Add(new P_Move(projective, Vector2.down, 4));
        projective.Attributes.Add(new P_DistanceDelete(25, new Vector2(0, 11), projective));
        projective.Attributes.Add(new P_PlayerDamage(this, 1));
        projectives.Add(projective);
    }

    public void Bottom()
    {
        //19
        //투사체 설정
        Projective projective = Object.Instantiate(horizontal);
        projective.Init();

        projective.transform.localPosition = new Vector3(0, -11, 0);
        projective.Attributes.Add(new P_Move(projective, Vector2.up, 4));
        projective.Attributes.Add(new P_DistanceDelete(25, new Vector2(0, -11), projective));
        projective.Attributes.Add(new P_PlayerDamage(this, 1));
        projectives.Add(projective);
    }
}

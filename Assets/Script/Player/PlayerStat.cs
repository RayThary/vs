using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    //발사체 대미지
    [SerializeField]
    private float attackDamage;
    public float AttackDamage { get { return attackDamage; } set { attackDamage = value; } }
    //발사체 공격 주기
    [SerializeField]
    private float attackCool;
    public float AttackCool { get { return attackCool; } set { attackCool = value; } }
    //발사체의 스피드
    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    //공격체의 발사 수
    [SerializeField]
    private int attackCount;
    public int AttackCount { get => attackCount; set { attackCount = value; } }
    //공격체 범위
    [SerializeField]
    private float attackRange;
    public float AttackRange { get => attackRange; set => attackRange = value; }
    [SerializeField]
    private float hp;
    public float HP { get => hp; set => hp = value; }
    [SerializeField]
    private float hpRecovery;
    public float HPRecovery { get => hpRecovery; set { hpRecovery = value; } }
    [SerializeField]
    private float lifeAbsorption;
    public float LifeAbsorption { get => lifeAbsorption; set { lifeAbsorption = value; } }
    [SerializeField]
    private float armor;
    public float Armor { get => armor; set => armor = value; }
    [SerializeField]
    private float speed;
    public float Speed { get => speed; set => speed = value; }
    [SerializeField]
    private float shieldPoint;
    public float ShieldPoint { get => shieldPoint; set => shieldPoint = value; }
    [SerializeField]
    private float skillDamage;
    public float SkillDamage { get => skillDamage; set => skillDamage = value; }
    [SerializeField]
    private float skillCool;
    public float SkillCool { get => skillCool; set => skillCool = value; }
    [SerializeField]
    private float skillAmp;
    public float SkillAmp { get => skillAmp; set => skillAmp = value; }
    //[SerializeField]
    //public bool MirrorWizard;
    //[SerializeField]
    //public bool PenetrationMagic;
}

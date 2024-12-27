using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    [SerializeField]
    private float attackDamage;
    public float AttackDamage { get { return attackDamage; } set { attackDamage = value; } }
    [SerializeField]
    private float attackCool;
    public float AttackCool { get { return attackCool; } set { attackCool = value; } }
    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    [SerializeField]
    private float attackCount;
    public float AttackCount { get => attackCount; set { attackCount = value; } }
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

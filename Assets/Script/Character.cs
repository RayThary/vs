using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float maxHp;
    public float MaxHP { get { return maxHp; } set {  maxHp = value; } }

    [SerializeField]
    private float hp;
    public float HP { get => hp; set => hp = value; }

    private float maxSheild;
    public float MaxSheild { get {  return maxSheild; } set { maxSheild = value; } }

    [SerializeField]
    private float sheild;
    public float Sheild { get => sheild; set => sheild = value; }

    private float recover;
    private float sheildRecover;
    // Start is called before the first frame update
    void Start()
    {
        recover = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(recover  + 1 < Time.time)
        {
            recover = Time.time;
            hp += GameManager.Instance.GetPlayer.Stat.HPRecovery;
        }

        if(sheild != maxSheild && sheildRecover == 0)
        {
            sheildRecover = Time.time;
        }
        else if(sheild != maxSheild)
        {
            sheildRecover = 0;
        }
        else
        {
            if (sheildRecover + 5 < Time.time)
            {
                sheildRecover = Time.time;
                sheild += maxSheild;
            }
        }
    }
}

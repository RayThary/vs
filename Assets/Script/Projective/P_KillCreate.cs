using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_KillCreate : IP_Attribute
{
    private readonly PoolingManager.ePoolingObject spawn;
    private readonly IAddon addon;
    private float timer;
    private List<(float time, Enemy enemy)> enemies;

    public P_KillCreate(PoolingManager.ePoolingObject spawn, IAddon addon, float timer)
    {
        this.spawn = spawn;
        this.addon = addon;
        this.timer = timer;
        enemies = new();
    }

    public void Enter(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Enemy enemy))
        {
            enemies.Add((Time.time, enemy));
            
        }
    }
    public void Exit(Collider2D collider2D) { }
    public void LateEnter(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void LateUpdate()
    {
        for(int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i].time > Time.time + timer)
            {
                enemies.RemoveAt(i);
            }
            else if (enemies[i].enemy.HP <= 0)
            {
                Create(enemies[i].enemy);
                enemies.RemoveAt(i);
            }
        }
    }
    public void Update() { }

    private void Create(Enemy enemy)
    {
        Projective projective = PoolingManager.Instance.CreateObject(spawn, GameManager.Instance.GetPoolingTemp).GetComponent<Projective>();
        projective.Init();

        projective.transform.position = enemy.transform.position;
        projective.Attributes.Add(new P_DamageTimer(1, 1, addon));
        projective.Attributes.Add(new P_DeleteTimer(projective, 6));
    }
}

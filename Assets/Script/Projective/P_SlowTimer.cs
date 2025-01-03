using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_SlowTimer : IP_Attribute
{
    //느려지는 속도
    private  readonly float speed;
    //지속될 시간
    private readonly float time;
    //이미 효과를 보고있는 적들
    private readonly Dictionary<Enemy, Coroutine> enemies;

    public P_SlowTimer(float speed, float timer) 
    {
        this.speed = speed; 
        time = timer;
        enemies = new();
    }

    public void Enter(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Enemy enemy))
        {
            enemy.Speed -= speed;
            if(enemies.TryGetValue(enemy, out Coroutine c))
            {
                //슬로우 삭제
                enemies.Remove(enemy);
                GameManager.Instance.StopCoroutine(c);
            }
            //슬로우
            Coroutine coroutine = GameManager.Instance.StartCoroutine(Timer(enemy));
            enemies.Add(enemy, coroutine);
        }
    }
    private IEnumerator Timer(Enemy enemy)
    {
        yield return new WaitForSeconds(time);
        enemy.Speed += speed; 
        //슬로우 삭제
        if (enemies.TryGetValue(enemy, out Coroutine c))
        {
            enemies.Remove(enemy);
            GameManager.Instance.StopCoroutine(c);
        }
    }

    public void Exit(Collider2D collider2D) { }
    public void LateEnter(Collider2D collider2D) { }
    public void LateExit(Collider2D collider2D) { }
    public void LateUpdate() { }
    public void Update() { }
}

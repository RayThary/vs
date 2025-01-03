using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_SlowTimer : IP_Attribute
{
    //�������� �ӵ�
    private  readonly float speed;
    //���ӵ� �ð�
    private readonly float time;
    //�̹� ȿ���� �����ִ� ����
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
                //���ο� ����
                enemies.Remove(enemy);
                GameManager.Instance.StopCoroutine(c);
            }
            //���ο�
            Coroutine coroutine = GameManager.Instance.StartCoroutine(Timer(enemy));
            enemies.Add(enemy, coroutine);
        }
    }
    private IEnumerator Timer(Enemy enemy)
    {
        yield return new WaitForSeconds(time);
        enemy.Speed += speed; 
        //���ο� ����
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

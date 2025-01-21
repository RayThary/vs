using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Bounce : IP_Attribute
{
    private Projective projective;
    private P_Move move;

    //private Vector2 dir;
    //public Vector2 Direction { get => dir;  set => dir = value; }

    private Rect rect;
    public Rect Rect { get => rect; set => rect = value; }

    private float radius;
    private float size;
    public float Size { get => size; set => size = value; }
    private float _Size { get => size * radius; }
    private Camera cam;
    // Start is called before the first frame update
    

    public P_Bounce(Projective projective, P_Move p_Move, float size)
    {
        cam = Camera.main;
        rect =  GameManager.Instance.CalculateWorldSize();
        this.projective = projective;
        radius = projective.GetComponent<CircleCollider2D>().radius;
        move = p_Move;
        this.size = size;
    }

    public void Enter(Collider2D collider2D)
    {

    }

    public void LateEnter(Collider2D collider2D)
    {

    }

    public void Exit(Collider2D collider2D)
    {

    }

    public void LateExit(Collider2D collider2D)
    {

    }

    public void Update()
    {
        //투사체가 범위 +- 카메라 위치를 벗어나지 않았는지
        if (projective.transform.position.x + _Size > rect.xMax + cam.transform.position.x)
        {
            if (move.Direction.x > 0)
            {
                move.Direction = Vector2.Reflect(move.Direction, new Vector2(-1, 0));
            }
        }
        else if (projective.transform.position.x - _Size < rect.xMin + cam.transform.position.x)
        {
            if (move.Direction.x < 0)
            {
                move.Direction = Vector2.Reflect(move.Direction, new Vector2(1, 0));
            }
        }

        if (projective.transform.position.y + _Size > rect.yMax + cam.transform.position.y)
        {
            if (move.Direction.y > 0)
            {
                move.Direction = Vector2.Reflect(move.Direction, new Vector2(0, -1));
            }
        }
        else if (projective.transform.position.y - _Size < rect.yMin + cam.transform.position.y)
        {
            if (move.Direction.y < 0)
            {
                move.Direction = Vector2.Reflect(move.Direction, new Vector2(0, 1));
            }
        }
    }

    public void LateUpdate()
    {

    }
}

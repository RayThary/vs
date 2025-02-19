using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpriteColorControl : MonoBehaviour
{
    [SerializeField]
    private bool isHit = false;
    public bool SetHit { set { isHit = value; } }

    [SerializeField] private float ReturnTime = 0.5f;//���� ���󺹱ͽð�
    public float SetReturnTime { set { ReturnTime = value; } }

    [SerializeField] private Color unitColor;

    private Transform[] childList;// ����ڽĵ����
    private List<SpriteRenderer> m_spr = new List<SpriteRenderer>();//�������̷������ִ°͵����

    private List<Color> m_alphaNormal = new List<Color>();//�⺻����
    private List<Color> m_alphaTranslucent = new List<Color>();//���������
    private Color m_isTranslucent;

    private int isSprite;// ��������Ʈ����

    void Start()
    {


        childList = transform.GetComponentsInChildren<Transform>();
        findSprite();

    }
    private void findSprite()
    {
        isSprite = 0;

        for (int i = 0; i < childList.Length; i++)
        {
            if (childList[i].name == "Shadow")
            {
                continue;
            }

            if (childList[i].GetComponent<SpriteRenderer>() != null)
            {
                m_spr.Add(childList[i].GetComponent<SpriteRenderer>());
                m_alphaNormal.Add(m_spr[isSprite].color);
                m_alphaTranslucent.Add(m_spr[isSprite].color);

                isSprite += 1;
            }
        }



        for (int x = 0; x < isSprite; x++)
        {
            m_isTranslucent = m_alphaTranslucent[x];
            m_isTranslucent = unitColor;
            m_alphaTranslucent[x] = m_isTranslucent;
        }

    }

    void Update()
    {

        if (isHit)
        {
            StartCoroutine(alphaControl1());
            isHit = false;
        }
    }

    IEnumerator alphaControl1()
    {
        for (int i = 0; i < isSprite; i++)
        {
            m_spr[i].color = m_alphaTranslucent[i];
        }

        yield return new WaitForSeconds(ReturnTime);

        for (int i = 0; i < isSprite; i++)
        {
            m_spr[i].color = m_alphaNormal[i];
        }
    }

    private void alphaControl()
    {
        for (int i = 0; i < isSprite; i++)
        {
            m_spr[i].color = m_alphaTranslucent[i];
            Invoke("alphaReturn", ReturnTime);
        }
    }

    private void alphaReturn()
    {
        for (int i = 0; i < isSprite; i++)
        {
            m_spr[i].color = m_alphaNormal[i];

        }
    }
}

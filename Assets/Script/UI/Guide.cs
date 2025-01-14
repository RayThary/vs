using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    //���� ������
    [SerializeField]
    private CardSelect cardSelect;
    //������ ������ ����
    [SerializeField]
    private Transform contant;
    //������ ������
    [SerializeField]
    private ItemSlot itemSlot;
    //������ �����ִ� ������Ʈ
    [SerializeField]
    private Transform description;
    [SerializeField]
    private Text descriptionText;
    private Coroutine coroutine;
    //������ ���������� ��� ������� ������� ������ ä���� ��
    //���콺�� �÷������� �׿� ���� ������ ������� ��

    void Start()
    {
        for (int i = 0; i < cardSelect.Addons.Count; i++)
        {
            Debug.Log("������ƮǮ���� ������� �ʴ� ����");
            ItemSlot slot = Instantiate(itemSlot, contant);
            slot.Init(this, cardSelect.Addons[i]);
        }
    }

    public void DescriptionON(ItemSlot slot)
    {
        description.gameObject.SetActive(true);
        descriptionText.text = slot.Addon.Description;
        coroutine = StartCoroutine(Mouse(description));
    }

    public void DescriptionOFF(ItemSlot slot) 
    {
        description.gameObject.SetActive(false);
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator Mouse(Transform tf)
    {
        while (true)
        {
            tf.position = Input.mousePosition + new Vector3(200, -150);
            yield return null;
        }
    }
}

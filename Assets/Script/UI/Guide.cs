using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    //무기 정보들
    [SerializeField]
    private CardSelect cardSelect;
    //슬롯을 정렬할 공간
    [SerializeField]
    private Transform contant;
    //슬롯의 프리팹
    [SerializeField]
    private ItemSlot itemSlot;
    //설명을 보여주는 오브젝트
    [SerializeField]
    private Transform description;
    [SerializeField]
    private Text descriptionText;
    private Coroutine coroutine;
    //게임이 시작했을때 모든 무기들의 정보들로 도감을 채워야 함
    //마우스를 올려놓으면 그에 대한 설명을 보여줘야 함

    void Start()
    {
        for (int i = 0; i < cardSelect.Addons.Count; i++)
        {
            Debug.Log("오브젝트풀링을 사용하지 않는 생성");
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

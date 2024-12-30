using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewArmory : MonoBehaviour
{

    private Armory armory;
    [SerializeField]
    private Image armoryBack;

    readonly Dictionary<IAddon, GameObject> pairs = new();

    private void Start()
    {
        armory = GameManager.Instance.GetPlayer.GetComponent<Armory>();

        armory.AddCall += AddSprite;
        armory.RemoveCall += RemoveSprite;
    }

    public void Close()
    {
        armoryBack.gameObject.SetActive(false);
    }

    public void ViewWeapon()
    {
        //���� �̹������� ��� ������
        if (armoryBack.gameObject.activeSelf)
        {
            armoryBack.gameObject.SetActive(false);
        }
        else
        {
            armoryBack.gameObject.SetActive(true);

            foreach (GameObject pair in pairs.Values)
            {
                pair.transform.SetParent(armoryBack.transform);
            }
        }
    }

    //���� ���� �ִ��� �����ִ� �Լ�
    public void AddSprite(IAddon addon)
    {
        if (addon.Weapon)
        {
            Debug.Log("������ƮǮ���� ������� �ʴ� ����");
            GameObject gameObject = new("���� �̹���");
            gameObject.transform.parent = armoryBack.transform;
            Image image = gameObject.AddComponent<Image>();
            image.sprite = addon.Sprite;
            pairs.Add(addon, gameObject);
        }
    }

    public void RemoveSprite(IAddon addon)
    {
        if (addon.Weapon)
        {
            Debug.Log("������ƮǮ���� ������� �ʴ� ����");
            pairs.TryGetValue(addon, out GameObject gameObject);
            pairs.Remove(addon);
            Destroy(gameObject);
        }   
    }
}

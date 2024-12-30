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
        //무기 이미지들이 어디에 있을지
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

    //무슨 무기 있는지 보여주는 함수
    public void AddSprite(IAddon addon)
    {
        if (addon.Weapon)
        {
            Debug.Log("오브젝트풀링을 사용하지 않는 생성");
            GameObject gameObject = new("무기 이미지");
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
            Debug.Log("오브젝트풀링을 사용하지 않는 삭제");
            pairs.TryGetValue(addon, out GameObject gameObject);
            pairs.Remove(addon);
            Destroy(gameObject);
        }   
    }
}

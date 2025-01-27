using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    private GameObject back;
    [SerializeField] 
    private GameObject button;
    private Armory armory;
    private DamageView m_View;
    private readonly List<DamageView> damageViews = new();

    private void Start()
    {

        armory = GameManager.Instance.GetPlayer.GetComponent<Armory>();
        m_View = Resources.Load<DamageView>("UI/Damage");
        armory.AddCall += AddWeapon;
        armory.RemoveCall += RemoveWeapon;
    }

    public void AddWeapon(IAddon addon)
    {
        if(addon.Weapon)
        {
            List<IAddon> list = damageViews.Select(x => x.Addon).ToList();
            for (int i = 0; i < armory.Addons.Count; i++)
            {
                if (!list.Contains(armory.Addons[i]))
                {
                    //새로운 무기 그래프 추가
                    Debug.Log("오브젝트풀링이 없는 생성");
                    DamageView view = Instantiate(m_View, back.transform);
                    view.Interlock(armory, armory.Addons[i]);
                    damageViews.Add(view);
                }
            }
        }
    }
    public void RemoveWeapon(IAddon addon)
    {
        if (addon.Weapon)
        {
            for (int i = 0; i < damageViews.Count; i++)
            {
                if (damageViews[i].Addon == addon)
                {
                    Debug.Log("오브젝트 풀링이 없는 삭제");
                    Destroy(damageViews[i].gameObject);
                    damageViews.RemoveAt(i);
                    return;
                }
            }
        } 
    }

    public void Open()
    {
        
        back.SetActive(true);
        button.SetActive(true);
    }

    public void Close()
    {
        back.SetActive(false);
        button.SetActive(false);
    }
}

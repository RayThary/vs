using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    [SerializeField]
    private Image back;
    [SerializeField]
    private Image bar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GetPlayer.SelectCharacter != null)
        {
            if(GameManager.Instance.GetPlayer.SelectCharacter.MaxHP != 0)
            {
                bar.fillAmount = GameManager.Instance.GetPlayer.SelectCharacter.HP / GameManager.Instance.GetPlayer.SelectCharacter.MaxHP;
                back.fillAmount = 1;
            }
            transform.position = Camera.main.WorldToScreenPoint(GameManager.Instance.GetPlayer.SelectCharacter.transform.position + new Vector3(0, -0.5f, 0));
        }
        else
        {
            bar.fillAmount = 0;
            back.fillAmount = 0;
        }
    }
}

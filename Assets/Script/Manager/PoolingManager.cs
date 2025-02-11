using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public enum ePoolingObject
    {
        Enemy,
        CurvePatten,
        CurveImage,
        SmallExp,
        MediumExp,
        LargeExp,
        BossAttack,
        Arrow,
        Axe,
        Magic1,
        Magic3,
        Magic4,
        Magic5,
        Magic6,
        Magic7,
        Magic8,
        Magic9,
        Magic10,
        Magic11,
        Magic13,
        Magic14,
        Magic15,
        Magic16,
        Magic17,
        Magic18,
        Magic19,
        Magic20,
        Meteor,
        BulletA,
        SEnemy,
        MEnemy,
        LEnemy,
        EnemyBoss,
        EnemyMiddleBoss,
        EnemyLastBoss,
        BossExp,
    }

    [System.Serializable]
    public class cPoolingClip
    {
        public GameObject clip;
        public int count;
        [TextArea] public string description;
    }

    [SerializeField] private List<cPoolingClip> m_listPoolingClip;

    public static PoolingManager Instance;

    private void OnValidate()
    {


    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(this);
        initPoolingParents();
        initPoolingChild();
    }

    private void Start()
    {

    }



    private void initPoolingParents()
    {
        List<string> listParentName = new List<string>();

        int pCount = transform.childCount;
        int cCount = transform.childCount;
        for (int iNum = 0; iNum < pCount; ++iNum)
        {
            string name = transform.GetChild(iNum).name;
            listParentName.Add(name);
        }

        pCount = m_listPoolingClip.Count;
        for (int iNum = 0; iNum < pCount; ++iNum)
        {
            if (m_listPoolingClip[iNum].clip == null)
            {
                continue;
            }

            cPoolingClip data = m_listPoolingClip[iNum];

            string name = data.clip.name;
            bool exist = listParentName.Exists(x => x == name);
            if (exist == true)
            {
                listParentName.Remove(name);
            }
            else
            {
                GameObject objParent = new GameObject();
                objParent.transform.SetParent(transform);
                objParent.name = name;
            }
        }




        pCount = listParentName.Count;
        for (int iNum = pCount - 1; iNum > -1; --iNum)
        {
            GameObject obj = transform.Find(listParentName[iNum]).gameObject;
            Destroy(obj);
        }
    }

    private void initPoolingChild()
    {
        int pCount = m_listPoolingClip.Count;
        for (int iNum = 0; iNum < pCount; ++iNum)
        {
            if (m_listPoolingClip[iNum].clip == null)
            {
                continue;
            }

            cPoolingClip objPooing = m_listPoolingClip[iNum];
            GameObject obj = m_listPoolingClip[iNum].clip;
            string name = obj.name;
            Transform parent = transform.Find(name);

            int objCount = parent.childCount;

            for (int idNum = objCount - 1; idNum > -1; --idNum)
            {
                Destroy(transform.GetChild(idNum).gameObject);
            }

            if (objCount < objPooing.count)
            {
                int diffcount = objPooing.count - objCount;
                for (int icNum = 0; icNum < diffcount; ++icNum)
                {
                    GameObject cObj = createObject(name);
                    cObj.transform.SetParent(parent);
                }
            }
        }
    }

    private GameObject createObject(string _name)
    {
        GameObject obj = m_listPoolingClip.Find(x => x.clip.name == _name).clip;
        GameObject iobj = Instantiate(obj);
        iobj.SetActive(false);
        iobj.name = _name;
        return iobj;
    }

    public GameObject CreateObject(ePoolingObject _value, Transform _parent)
    {
        string findObjectName = _value.ToString().Replace("_", " ");
        return getPoolingObject(findObjectName, _parent);
    }

    public GameObject CreateObject(string _name, Transform _parent)
    {
        return getPoolingObject(_name, _parent);
    }



    private GameObject getPoolingObject(string _name, Transform _parent)
    {
        Transform parent = transform.Find(_name);

        if (parent == null)
        {
            Debug.LogError("�����տ� ������Ʈ�� �� ���� ������ �����ϴ�.");
            return null;
        }

        GameObject returnValue = null;
        if (parent.childCount > 0)
        {
            returnValue = parent.GetChild(0).gameObject;
        }
        else
        {
            returnValue = createObject(_name);
        }
        returnValue.transform.SetParent(_parent);
        returnValue.SetActive(true);
        return returnValue;
    }

    public void RemovePoolingObject(GameObject _obj)
    {
        string name = _obj.name;
        Transform parent = transform.Find(name);

        cPoolingClip poolingObj = m_listPoolingClip.Find(x => x.clip.name == name);

        int poolingCount = poolingObj.count;

        if (parent.childCount < poolingCount)//����������
        {
            _obj.transform.SetParent(parent);
            _obj.SetActive(false);
            _obj.transform.position = Vector3.zero;
        }
        else
        {
            Destroy(_obj);
        }
    }
    public void RemovePoolingObject(GameObject _obj, Vector3 _pos)
    {
        string name = _obj.name;
        Transform parent = transform.Find(name);

        cPoolingClip poolingObj = m_listPoolingClip.Find(x => x.clip.name == name);

        int poolingCount = poolingObj.count;

        if (parent.childCount < poolingCount)//����������
        {
            _obj.transform.SetParent(parent);
            _obj.SetActive(false);
            _obj.transform.position = _pos;
        }
        else
        {
            Destroy(_obj);
        }
    }

    public void RemoveAllPoolingObject(GameObject _obj)
    {
        int parentCount = _obj.transform.childCount;
        for (int i = parentCount - 1; i >= 0; i--)
        {
            Transform trsObj = _obj.transform.GetChild(i);

            if (trsObj == null)
            {
                Debug.Log("�����ʿ�");
                break;
            }
            string name = trsObj.name;

            Transform parent = transform.Find(name);

            cPoolingClip poolingObj = m_listPoolingClip.Find(x => x.clip.name == name);

            int poolingCount = poolingObj.count;

            if (parent.childCount < poolingCount)
            {
                trsObj.SetParent(parent);
                trsObj.gameObject.SetActive(false);
                trsObj.position = Vector3.zero;

            }
            else
            {
                Destroy(trsObj.gameObject);
            }
        }
    }





}

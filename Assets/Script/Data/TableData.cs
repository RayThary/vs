using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class TableData : MonoBehaviour
{
    private static TableData instance;
    public static TableData Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("Table").AddComponent<TableData>();
            return instance;
        }
    }

    [SerializeField]
    private Description description;
    public Description Description { get => description; }

    [SerializeField]
    private UserLevelTable userLevelTable;
    public UserLevelTable UserLevelTable { get => userLevelTable; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        userLevelTable = new UserLevelTable(this);
        description = new Description(this);
    }


    public IEnumerator TableLoad(string address, Action<string[]> action)
    {
        UnityWebRequest www = UnityWebRequest.Get(address);
        yield return www.SendWebRequest();
        string data = www.downloadHandler.text;
        string[] strings = data.Split(new[] { '\t', '\n' });
        action(strings);
    }

}

[Serializable]
public class UserLevelTable
{
    [SerializeField]
    private List<UserLevelTable_Part> table = new();
    public List<UserLevelTable_Part> Table { get => table; }
    private string address = "https://docs.google.com/spreadsheets/d/13-VZhHfaqJzhD2yXfQFsNt6qIlr6f9Gh20h3B1_GF3U/export?format=tsv&&range=B6:Q";

    public UserLevelTable(TableData tableData)
    {
        if (tableData != null)
            tableData.StartCoroutine(tableData.TableLoad(address, Init));
    }

    public void Init(string[] strings)
    {
        for (int i = 0; i < strings.Length / 16; i++)
        {
            table.Add(new UserLevelTable_Part(int.Parse(strings[i * 16]), int.Parse(strings[i * 16 + 1]), int.Parse(strings[i * 16 + 2]), int.Parse(strings[i * 16 + 3]), int.Parse(strings[i * 16 + 4]), int.Parse(strings[i * 16 + 5]), int.Parse(strings[i * 16 + 6]), int.Parse(strings[i * 16 + 7]), int.Parse(strings[i * 16 + 8]), int.Parse(strings[i * 16 + 9]), int.Parse(strings[i * 16 + 10]), int.Parse(strings[i * 16 + 11]), int.Parse(strings[i * 16 + 12]), int.Parse(strings[i * 16 + 13]), int.Parse(strings[i * 16 + 14]), int.Parse(strings[i * 16 + 15])));
        }
    }
}

[Serializable]
public class UserLevelTable_Part
{
    public int level;
    public int exp;
    public int hp;
    public int hpre;
    public int hpab;
    public int speed;
    public int armor;
    public int shield;
    public int damage;
    public int acool;
    public int aspeed;
    public int ac;
    public int ar;
    public int sd;
    public int sc;
    public int sa;

    public UserLevelTable_Part(int level, int exp, int hp, int hpre, int hpab, int speed, int armor, int shield, int damage, int acool, int aspeed, int ac, int ar, int sd, int sc, int sa)
    {
        this.level = level;
        this.exp = exp;
        this.hp = hp;
        this.hpre = hpre;
        this.hpab = hpab;
        this.speed = speed;
        this.armor = armor;
        this.shield = shield;
        this.damage = damage;
        this.acool = acool;
        this.aspeed = aspeed;
        this.ac = ac;
        this.ar = ar;
        this.sd = sd;
        this.sc = sc;
        this.sa = sa;
    }
}

[Serializable]
public class Description
{
    [SerializeField]
    private List<Description_Part> descriptions = new();
    private string address = "https://docs.google.com/spreadsheets/d/13-VZhHfaqJzhD2yXfQFsNt6qIlr6f9Gh20h3B1_GF3U/export?format=tsv&gid=2122506665&range=B6:D";

    public Description(TableData tableData)
    {
        if(tableData != null)
            tableData.StartCoroutine(tableData.TableLoad(address, Init));
    }

    public void Init(string[] strings)
    {
        for (int i = 0; i < strings.Length / 3; i++)
        {
            descriptions.Add(new Description_Part(strings[i * 3], strings[i * 3 + 1], strings[i * 3 + 2]));
        }
    }

    public string description(string name)
    {
        return descriptions.Where(x => x.Index.Equals(name)).FirstOrDefault()?.Description;
    }
}

[Serializable]
public class Description_Part
{
    [SerializeField]
    private string index;
    public string Index => index;
    [SerializeField]
    private string name;
    public string Name => name;
    [SerializeField]
    private string description;
    public string Description => description;

    public Description_Part(string index, string name, string description)
    {
        this.index = index;
        this.name = name;
        this.description = description;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAddon
{
    public string AddonName { get; }
    public void Update();
    public void Addon();
    public void Remove();
    public void LevelUp();
    public Sprite Sprite { get; }
    public bool Weapon { get; }
    public float Statistics { get; set; }
    public int Level { get; set; }
    public int MaxLevel { get; }
}

public class AddonComparer : IEqualityComparer<IAddon>
{
    public bool Equals(IAddon x, IAddon y)
    {
        // Addon�� Ư�� �Ӽ��� �������� ��
        return x != null && y != null && x.AddonName == y.AddonName;
    }

    public int GetHashCode(IAddon obj)
    {
        // �񱳿� ����� �Ӽ����� ���� �ؽ��ڵ� ����
        return obj.AddonName.GetHashCode();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // ���� �Ѿ�� ������ ����ǵ��� 

    private int gold;
    private int diamond;

    private int star;       // �������� Ŭ���� ��?
    private int life;       // �ΰ��� ������(��)

    public int Gold
    {
        get{ return gold; }
        set{ gold = value; }
    }
    public int Diamond
    {
        get { return diamond; }
        set {diamond = value;}
    }
    public int Star
    {
        get { return star; }
        set {star = value;}
    }
    public int Life
    {
        get { return life; }
        set {life = value;}
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);  // ���� �Ѿ�� object ���� X
    }

}

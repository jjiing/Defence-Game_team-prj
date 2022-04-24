using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    //public GameManager gameManager;
    int level = 1;
    //HP
    //�� �������ؼ� �������� �ϰ� �ϱ�********
    protected int warriorHPnow = 50;       //����ü��
    protected int warriorHPmax = 100;       //�ִ�ü��
    protected int warriorHPup = 5;         //����

    //Atk
    protected int warriorAtk = 40;
    protected int warriorUltAtk = 1;       //���ӵ�?




    //��ų��Ÿ��
    protected float coolTime2 = 40f;        //����
    protected float coolTime3 = 15f;        //�����ȯ
    protected float coolTime4 = 45f;         //�ñر� - ������



    float coolTimeRevive=5f;
    bool isCoolRevive = true;



    void Start()
    {
        
    }

    
    void Update()
    {
        if (warriorHPnow <= 0)
            WarriorDie();   //���� DIE�� �Լ��� ���� �ʿ䰡 �ֳ�? .. �׳� DESTROY���ָ� �����ʳ� ��Ȱ������ �Լ���?..

        //������ > ���� �ʿ�
        if (Input.GetKeyUp(KeyCode.P))  //���� ���� �׳� ���� ���� > ����
        {
            //if (level < 3 && gameManager.exp >= 100) //���ӸŴ��� �����ȵ� >����
                WarriorLevelUp();
        }

    }
    public int HP
    {
        get { return warriorHPnow; }
        protected set
        {
            if (value > warriorHPmax)
                value = warriorHPmax;
            else if (value < 0)
                value = 0;

            warriorHPnow = value;
        }
    }
    public int Atk
    {
        get { return warriorAtk; }
        protected set { warriorAtk = value;}
    }

    void WarriorLevelUp()
    {
        //gameManager.exp -= 100;   //���ӸŴ��� �����ȵ� > ����
        level += 1;
        warriorHPmax = (int)((double)warriorHPmax*1.1);
        warriorAtk = (int)((double)warriorAtk * 1.1);

    }

    void WarriorDie()
    {
        //������ ������� ����Ʈ?
        //�׾ ��(������)������ ���� ����?
    }


}

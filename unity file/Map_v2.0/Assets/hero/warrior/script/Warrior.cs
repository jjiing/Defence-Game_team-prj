using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    //HP
    //�� �������ؼ� �������� �ϰ� �ϱ�********
    protected int warriorHPnow = 20;       //����ü��
    protected int warriorHPmax = 100;       //�ִ�ü��
    protected int warriorHPup = 5;         //����

    //Atk
    protected int warriorAtk = 40;



    //��ų��Ÿ��
    protected float coolTime2 = 10f;        //����
    protected float coolTime3 = 15f;        //�����ȯ
    protected float coolTime4 = 4f;         //�ñر� - ������

    float coolTimeRevive=5f;


    bool isCoolRevive = true;



    void Start()
    {
        
    }

    
    void Update()
    {
        if (warriorHPnow <= 0)
            warriorDie();   //���� DIE�� �Լ��� ���� �ʿ䰡 �ֳ�? .. �׳� DESTROY���ָ� �����ʳ� ��Ȱ������ �Լ���?..


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
    void warriorDie()
    {
        
            Destroy(gameObject);
            //������ ��Ȱ�ϴ°� ����
            //StartCoroutine(ReviveCoolTimeCo(coolTimeRevive));
            //Instantiate(gameObject);
            //transform.position = new Vector3(-9, 16, 0);
            ////����� �̵�(�ȴ� ���)
        
       
    }
    IEnumerator ReviveCoolTimeCo(float cooltime)
    {
        isCoolRevive = false;
        yield return new WaitForSeconds(cooltime);
        isCoolRevive = true;
    }

}

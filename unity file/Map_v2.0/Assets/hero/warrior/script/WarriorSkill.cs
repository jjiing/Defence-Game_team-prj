using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSkill : Warrior
{
    public bool isCool2 = true;
    public bool isCool3 = true;
    public bool isCool4 = true;

    public knight knight;
    knight knight1, knight2, knight3;
    private void Start()
    {
       
    }
    void Update()
    {
        WarriorSkillSet();
    }
    void WarriorSkillSet()     //��ų1~4
    {


        
        if (isCool2)
        {
            WarriorSkill2();
            StartCoroutine(CoolTimeCo2());
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isCool3)
            {
                WarriorSkill3();
                StartCoroutine(CoolTimeCo3());
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isCool4)
            {
                WarriorSkill4();
                StartCoroutine(CoolTimeCo4());
            }
        }

    }

    //skill1
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Knight"))
        {
            int HpPlus = (int)((double)knight.knightHPmax * 0.2);
            int AtkPlus = (int)((double)knight.knightAtk * 0.2);
            //HP
            collision.GetComponent<knight>().KnightHP += HpPlus;
            Debug.Log("HpPlus" + HpPlus);
            Debug.Log("ü�� �ø� : " + collision.GetComponent<knight>().KnightHP);
            //ATK
            collision.GetComponent<knight>().KnightAtk += AtkPlus;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Knight"))
        {
            int HpPlus = (int)((double)knight.knightHPmax * 0.2);
            int AtkPlus = (int)((double)knight.knightAtk * 0.2);
            //�������� �� +�Ѹ�ŭ ���ִµ�, �װ� 0���� ������ �׳� ���� �Ǹ�ŭ ����
            //HP
            if (collision.GetComponent<knight>().KnightHP - HpPlus > 0)
            {
                collision.GetComponent<knight>().KnightHP -= HpPlus;
                Debug.Log("ü�� �ٽ� ���� : " + collision.GetComponent<knight>().KnightHP);
            }
            //ATK
            collision.GetComponent<knight>().KnightAtk -= AtkPlus;
        }
    }




    void WarriorSkill2()
    {
        double warriorHealPer = warriorHPup * 0.01;
        int plusHp = (int)(warriorHPmax * warriorHealPer);
        HP += plusHp;
    }
    void WarriorSkill3()
    {
        knight1 = ObjectPool.GetObject();
        knight1.transform.position = transform.position + new Vector3(0.7f, -0.7f, 0);
        knight2 = ObjectPool.GetObject();
        knight2.transform.position = transform.position + new Vector3(0.3f, -0.5f, 0);
        knight3 = ObjectPool.GetObject();
        knight3.transform.position = transform.position + new Vector3(0.3f, -0.9f, 0);

        //Instantiate(knight, transform.position+ new Vector3(0.7f, -0.7f, 0), transform.rotation);
        //Instantiate(knight, transform.position + new Vector3(0.3f, -0.5f, 0), transform.rotation);
        //Instantiate(knight, transform.position + new Vector3(0.3f, -0.9f, 0), transform.rotation);
    }
    void WarriorSkill4()
    {
        //�ñر� - ������
    }


 



    //��ų����Ÿ��
    IEnumerator CoolTimeCo2()
    {
        isCool2 = false;
        yield return new WaitForSeconds(coolTime2);
        isCool2 = true;
    }
    IEnumerator CoolTimeCo3()
    {
        isCool3 = false;
        yield return new WaitForSeconds(coolTime3);
        isCool3 = true;
    }
    IEnumerator CoolTimeCo4()
    {
        isCool4 = false;
        yield return new WaitForSeconds(coolTime4);
        isCool4 = true;
    }
}

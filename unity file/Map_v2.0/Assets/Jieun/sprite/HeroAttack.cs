using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : Warrior
{
    public HeroMovement heroMoveScript;
    
    
    float currentDist = 0;      //i��° ���Ϳ� ����Ÿ�
    float TargetDist = -1f;     //Ÿ�ٸ��Ϳ��� �Ÿ�
    int TargetIndex = -1;       //Ÿ���� �� �ε���

 
    GameObject targetMonster;
    List<GameObject> monstersAround = new List<GameObject>();   //���� ����Ʈ
    
    float distance;

    private Animator animator;

    bool isCool = true;

    bool isAttack = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //�⺻����
        FindTargetMonster();
        GoToTargetandAttack();
        WarriorAttackAni();

        if (Input.GetMouseButtonDown(1))
        {
            isAttack = false;
            StopAllCoroutines();
            
            heroMoveScript.heroSpeed = 1.5f;    //�ӵ� �ʱ�ȭ
        }
         


        //��ų
        WarriorSkill1();
        if(isCool)
        {
            WarriorSkill2();
            StartCoroutine(CoolTimeCo(coolTime2));
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (isCool)
            {
                WarriorSkill3();
                StartCoroutine(CoolTimeCo(coolTime3));
            }

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isCool)
            {
                WarriorSkill4();
                StartCoroutine(CoolTimeCo(coolTime4));
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) //���� �����ȿ� ������ ����Ʈ�� �ֱ� //���� ���̾�� �浹 ���ü��� �س���
    {
        if (collision.CompareTag("Monster"))
        {
            Debug.Log("�浹");
            monstersAround.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  //���Ͱ� �������� ������ ����Ʈ���� ����
    {
        if (collision.CompareTag("Monster"))
        {
            monstersAround.Remove(collision.gameObject);
        }
    }


    void FindTargetMonster()
    {
        if (monstersAround.Count != 0 )
        {
            for (int i = 0; i < monstersAround.Count; i++)
            {
                currentDist = Vector2.Distance(transform.position, monstersAround[i].transform.position);
                if (TargetDist == -1)               //ù ���͸� Ÿ������ ���ϰ� Ÿ�� �Ÿ��� ����
                {
                    TargetDist = currentDist;
                    TargetIndex = i;
                }
                else
                {
                    if (TargetDist >= currentDist)  //Ÿ�ٰŸ� ���ؼ� �� �������� �ε����� ����
                    {
                        TargetIndex = i;
                        TargetDist = currentDist;
                    }
                }
                targetMonster = monstersAround[TargetIndex];
            }
            TargetDist = -1f;  //�ʱ�ȭ   
        }
    }
    void GoToTargetandAttack()
    {

        if (monstersAround.Count != 0 && !heroMoveScript.isHeroCursorMove)
        {
            heroMoveScript.isHeroMove = true;


            heroMoveScript.heroMoveDir = (targetMonster.transform.position - transform.position).normalized;
            transform.position += heroMoveScript.heroMoveDir * heroMoveScript.heroSpeed * Time.deltaTime;

            distance = Vector2.Distance(transform.position, targetMonster.transform.position); //Ÿ�ٸ��Ϳ��ǰŸ�(�����̴� ����)
            if (distance <= 0.5f * heroMoveScript.heroSpeed)
            {
                heroMoveScript.isHeroMove = false;
                Debug.Log("�����̿͵�");
                heroMoveScript.heroSpeed = 0;
                if (isAttack == false)
                    StartCoroutine(HeroAttackCo());
            }

        }
    }
    IEnumerator HeroAttackCo()
    {

        animator.SetTrigger("Attack");
        heroMoveScript.isHeroMove = false;
        Debug.Log("���ݽ���");

        while (targetMonster != null && distance <= 0.5f)
        {
            isAttack = true;
            Debug.Log("���� 1ȸ");
            //animator.SetTrigger("Attack");
            targetMonster.GetComponent<Monster>().monterHP -= warriorAtk;
            Debug.Log("���� ü��" + targetMonster.GetComponent<Monster>().monterHP);
            
            if (targetMonster.GetComponent<Monster>().monterHP <= 0)
            {
                monstersAround.Remove(targetMonster);
                Debug.Log(targetMonster.name + "����");
                isAttack = false;
                heroMoveScript.isHeroMove = false;
            }

            if (monstersAround.Count<=0)
                break;

         
            yield return new WaitForSeconds(1f);
            
        }
        isAttack = false;
        heroMoveScript.heroSpeed = 1.5f;    //�ӵ� �ʱ�ȭ
    }

    void WarriorAttackAni()
    {
        if (isAttack == true)
        {
            animator.SetBool("isAttack", true);
            

            if (targetMonster != null)
            {
                heroMoveScript.heroMoveDir = (targetMonster.transform.position - transform.position).normalized;

                //����-������ ���Ⱚ(����ȭ������)
                animator.SetFloat("inputX", heroMoveScript.heroMoveDir.x);
                animator.SetFloat("inputY", heroMoveScript.heroMoveDir.y);
            }
        }
        else
            animator.SetBool("isAttack", false);        

    }




   



    IEnumerator CoolTimeCo(float cooltime)
    {
        isCool = false;
        yield return new WaitForSeconds(cooltime);
        isCool = true;
    }
    void WarriorSkill1()
    {
        //���� ����� ���ݷ�, ü�� ���̱� (�� ����. �����ϸ� ���)
    }
    void WarriorSkill2()
    {
        //(�� �������� �ڵ����)
        double warriorHealPer = warriorHPup * 0.01;
        int plusHp = (int)(warriorHPmax*warriorHealPer);
        warriorHPnow += plusHp;
        if (warriorHPnow > warriorHPmax)
            warriorHPnow = warriorHPmax;
       
    }
    void WarriorSkill3()
    {
        //���� ����� ��ȯ
    }
    void WarriorSkill4()
    {
        //�ñر� - ������
    }


}


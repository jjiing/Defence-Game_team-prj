using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knight : MonoBehaviour
{
    private Animator animator;
    GameObject targetMonster;
    List<GameObject> monstersAround = new List<GameObject>();   //���� ����Ʈ

    float targetDist;   //Ÿ�� ���Ϳ��� �Ÿ�

    float knightSpeed = 1f;
    bool isAttack = false;
    //bool isKnightMove = false;
    Vector3 targetKnightDirection;

    //atk
    public int knightAtk = 10;
    //hp
    public int knightHPnow = 50;
    public int knightHPmax = 100;
    //������ ������
    public float buffPercent= 0.2f; 


    private void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("DestroyKnight", 10f);
    }

    void Update()
    {
        //�⺻����
        FindTargetMonster();
        GoToTargetandAttack();
        WarriorAttackAni();


        if (knightHPnow <= 0)
            DestroyKnight();

    }

    public int KnightHP
    {
        get { return knightHPnow; }

        set
        {
           if (value < 0)
                value = 0;

            knightHPnow = value;
        }
    }
    public int KnightAtk
    {
        get { return knightAtk; }

        set
        {
            if (value > knightHPmax)
                value = knightHPmax;
            else if (value < 0)
                value = 0;

            knightAtk = value;
        }
    }
    private void DestroyKnight()
    {
        ObjectPool.ReturnObject(this);
    }
    


    private void OnTriggerEnter2D(Collider2D collision) //���� �����ȿ� ������ ����Ʈ�� �ֱ� 
    {
        if (collision.CompareTag("Monster"))
        {
            monstersAround.Add(collision.gameObject);
            Debug.Log("�浹����");
            Debug.Log(monstersAround.Count);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)  //���Ͱ� �������� ������ ����Ʈ���� ����
    {
        if (collision.CompareTag("Monster"))
        {
            monstersAround.Remove(collision.gameObject);
            Debug.Log("�浹�����");
            Debug.Log(monstersAround.Count);

        }
    }

    void FindTargetMonster()
    {
        if (monstersAround.Count != 0)
        {
            float TargetDistCheck = -1f;            //Ÿ�ٸ��Ϳ��� �Ÿ� - �Ÿ� ������
            int TargetIndex = -1;                   //Ÿ���� �� �ε���

            for (int i = 0; i < monstersAround.Count; i++)
            {
                float currentDist = Vector2.Distance(transform.position, monstersAround[i].transform.position);     //i��° ���Ϳ��� �Ÿ�

                if (TargetDistCheck == -1)               //ù ���͸� Ÿ������ ���ϰ� Ÿ�� �Ÿ��� ����
                {
                    TargetDistCheck = currentDist;
                    TargetIndex = i;
                }
                else
                {
                    if (TargetDistCheck >= currentDist)  //Ÿ�ٰŸ� ���ؼ� �� �������� �ε����� ����
                    {
                        TargetIndex = i;
                        TargetDistCheck = currentDist;
                    }
                }
                targetMonster = monstersAround[TargetIndex];
            }
            TargetDistCheck = -1f;  //�ʱ�ȭ   
        }
    }
    void GoToTargetandAttack()
    {
        if (monstersAround.Count != 0)
        {
            //isKnightMove = true;

            targetKnightDirection = (targetMonster.transform.position - transform.position).normalized;
            transform.position += targetKnightDirection * knightSpeed * Time.deltaTime;

            targetDist = Vector2.Distance(transform.position, targetMonster.transform.position); //Ÿ�ٸ��Ϳ��ǰŸ�(�����̴� ����)
            if (targetDist <= 0.5f * knightSpeed)
            {
                //isKnightMove = false;
                knightSpeed = 0;
                if (isAttack == false)
                    StartCoroutine(KnightAttackCo());
            }

        }
    }

    IEnumerator KnightAttackCo()
    {
        //animator.SetTrigger("Attack");
        Debug.Log("���ݽ���");

        while (targetMonster != null && targetDist <= 0.5f)
        {
            isAttack = true;
            Debug.Log("���� 1ȸ");
            //animator.SetTrigger("Attack");
            targetMonster.GetComponent<Monster>().monterHP -= knightAtk;
            Debug.Log("���� ü��" + targetMonster.GetComponent<Monster>().monterHP);

            if (targetMonster.GetComponent<Monster>().monterHP <= 0)
            {
                monstersAround.Remove(targetMonster);
                Debug.Log(targetMonster.name + "����");
                isAttack = false;
                //isKnightMove = false;
            }
            yield return new WaitForSeconds(1f);
        }
        isAttack = false;
        knightSpeed = 1.5f;    //�ӵ� �ʱ�ȭ
    }

    IEnumerator DestoryCo()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }


    void WarriorAttackAni()
    {
        if (isAttack == true)
        {
            animator.SetBool("isAttack", true);
            if (targetMonster != null)
            {
                Vector3 Dir = (targetMonster.transform.position - transform.position).normalized;
                //����-������ ���Ⱚ(����ȭ������)
                animator.SetFloat("inputX", Dir.x);
            }
        }
        else
            animator.SetBool("isAttack", false);
    }
}




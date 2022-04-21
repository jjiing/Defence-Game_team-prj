using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    public HeroMovement heroMoveScript;
    public MouseTransfrom cursor;

    float currentDist = 0;      //i��° ���Ϳ� ����Ÿ�
    public float disToTarget = 0;      //Ÿ�ٸ��ͱ����� �Ÿ�(�����̴µ���)
    float TargetDist = -1f;     //Ÿ�ٸ��Ϳ��� �Ÿ�
    int TargetIndex = -1;       //Ÿ���� �� �ε���


    GameObject targetMonster;
    List<GameObject> monstersAround = new List<GameObject>();   //���� ����Ʈ
    bool isAttack = false;
    //bool isAttackOnce = false;  //�ִϸ��̼ǿ�

    private Animator animator;

    private void OnTriggerEnter2D(Collider2D collision) //���� �����ȿ� ������ ����Ʈ�� �ֱ� //���� ���̾�� �浹 ���ü��� �س���
    {
        if (collision.CompareTag("Monster"))
        {
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



    private void Start()
    {
        animator = GetComponent<Animator>();
    }



    void Update()
    {
        FindTargetMonster();
        if (monstersAround.Count != 0 && heroMoveScript.isHeroMove == false)
        {
            if(targetMonster != null)
                GoToTargetandAttack();
        }
        WarriorAttackAni();
    }




    
    void FindTargetMonster()
    {
        if (monstersAround.Count != 0)
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
        heroMoveScript.heroDestPos = targetMonster.transform.position;//�������� ���� �� ���� ����� ���ͷ� �ٲ�
        heroMoveScript.isHeroMove = true;   //�����̴� �Լ� �۵�
        disToTarget = Vector2.Distance(transform.position, targetMonster.transform.position); //Ÿ�ٸ��Ϳ��ǰŸ�(�����̴� ����)
        if (disToTarget <= 0.5f)    //Ÿ�ٸ��Ϳ� ����
        {
            //float heroSpeedSave = heroMoveScript.heroSpeed;
            //heroMoveScript.heroSpeed = 0;
            heroMoveScript.isHeroMove = false;  //����
            if(isAttack == false)
                StartCoroutine(AttackCo());
            heroMoveScript.heroDestPos = new Vector3(cursor.cursorPos.x, cursor.cursorPos.y, 0); //������ ������� �ٲ��ֱ�
            //heroMoveScript.heroSpeed = heroSpeedSave;
        }
    }
    IEnumerator AttackCo()
    {
        isAttack = true;

        while (targetMonster != null)
        {
            Debug.Log("���� 1ȸ");
            //isAttackOnce = true;
            //WarriorAttackAni();
            targetMonster.GetComponent<Monster>().monterHP -= 40;
            //isAttackOnce = false;


            if (targetMonster.GetComponent<Monster>().monterHP <= 0)
                Debug.Log(targetMonster.name + "����");

            yield return new WaitForSeconds(1);
        }
        isAttack = false;
    }

    void WarriorAttackAni()
    {       
        if(isAttack == true)
            animator.SetBool("isAttack", true);
        else
            animator.SetBool("isAttack", false);

        Vector3 dir = heroMoveScript.dirHeroMst;
        //����-������ ���Ⱚ(����ȭ������)
        animator.SetFloat("inputX", dir.x);
        animator.SetFloat("inputY", dir.y);
    }
}



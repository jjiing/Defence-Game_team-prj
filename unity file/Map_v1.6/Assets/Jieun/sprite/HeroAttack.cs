using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    public HeroMovement heroMoveScript;
    float currentDist = 0;      //i��° ���Ϳ� ����Ÿ�
    float TargetDist = -1f;     //Ÿ�ٸ��Ϳ��� �Ÿ�
    int TargetIndex = -1;       //Ÿ���� �� �ε���

    GameObject targetMonster;
    List<GameObject> monstersAround = new List<GameObject>();   //���� ����Ʈ

    bool isAttack = false;
    float distance;


    private Animator animator;

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



    private void Start()
    {
        animator = GetComponent<Animator>();
    }



    void Update()
    {
        FindTargetMonster();
        GoToTargetandAttack();
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
        if (monstersAround.Count != 0 && heroMoveScript.isHeroBasicMove==false)
        {
            Debug.Log("if�� ����");
            if (targetMonster != null)
            {
                distance = Vector2.Distance(transform.position, targetMonster.transform.position); //Ÿ�ٸ��Ϳ��ǰŸ�(�����̴� ����)

                Vector3 direction = (targetMonster.transform.position - transform.position).normalized;  
                transform.position += direction * heroMoveScript.heroSpeed * Time.deltaTime;
                heroMoveScript.isHeroMove = true;


                if (distance <= 0.5f * heroMoveScript.heroSpeed)
                {
                    Debug.Log("�����̿͵�");
                    heroMoveScript.heroSpeed = 0;       
                    
                    
                    if (isAttack == false)
                        StartCoroutine(HeroAttackCo());
                }
                
            }
        }
    }
        IEnumerator HeroAttackCo()
        {
        
            isAttack = true;
            heroMoveScript.isHeroMove = false; //�� ������ �ȱ� ����� �Ȳ���
            Debug.Log("���ݽ���");

            while (targetMonster != null && distance <= 0.5f )
            {
                
                Debug.Log("���� 1ȸ");            
                targetMonster.GetComponent<Monster>().monterHP -= 40;
                if (targetMonster.GetComponent<Monster>().monterHP <= 0)
                {
                    Debug.Log(targetMonster.name + "����");
                    isAttack = false;
                    heroMoveScript.isHeroMove = false;
                }
                yield return new WaitForSeconds(1);
            }
            isAttack = false;
        
        heroMoveScript.heroSpeed = 1.5f;    //�ӵ� �ʱ�ȭ
           }

        void WarriorAttackAni()
        {
            if (isAttack == true)
                animator.SetBool("isAttack", true);
            else
                animator.SetBool("isAttack", false);

            if (targetMonster != null)
            {
                Vector3 dir = (targetMonster.transform.position - transform.position).normalized;
                    //����-������ ���Ⱚ(����ȭ������)
                animator.SetFloat("inputX", dir.x);
                animator.SetFloat("inputY", dir.y);
            }
        }
    
}


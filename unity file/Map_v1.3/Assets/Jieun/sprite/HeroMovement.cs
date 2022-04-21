using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float heroSpeed = 1.5f;    //�����̵� �ӵ�
    public Vector3 heroDestPos;      //������ ��ġ, ������ ��ġ ���Ͱ�

    //HeroAttack heroAttack;
    private Animator animator;
    public bool isHeroMove;
    public Vector3 dirHeroMst; //����
    //float disToDes;


     void Awake()
    {
        animator=GetComponent<Animator>();
    }
     void Update()
    {
        HeroMoveToDest();
        WarriorWalkAni();
    }

    public void HeroMoveToDest()        //�̵�
    {
        if (isHeroMove && transform.position != heroDestPos)
        { 
            transform.position = Vector3.MoveTowards(transform.position, heroDestPos, Time.deltaTime * heroSpeed);
            
           
            //if (heroAttack.disToTarget <= 0.7f)
                //isHeroMove = false;
        }
        if (transform.position == heroDestPos)
        {
            isHeroMove = false;
            //Debug.Log("is hero move �� " + isHeroMove);
        }
    }
    public void WarriorWalkAni()
    {
        if (isHeroMove==true)
            animator.SetBool("isMove", true);
        else
            animator.SetBool("isMove", false);

        dirHeroMst = (heroDestPos - transform.position).normalized;
        //����-������ ���Ⱚ(����ȭ������)
        animator.SetFloat("inputX", dirHeroMst.x);
        animator.SetFloat("inputY", dirHeroMst.y);    
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    float heroSpeed = 1.5f;    //�����̵� �ӵ�
    Vector3 heroDest, heroDestPos;      //������ ��ġ, ������ ��ġ ���Ͱ�

    private Animator animator;
    private bool isHeroMove;

     void Awake()
    {
        animator=GetComponent<Animator>();
    }
    private void Start()
    {
        heroDestPos = transform.position;      //������ ���Ͱ� �ʱ�ȭ(�����ָ� ���۰� ���ÿ� ù ��ġ�� ������)
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
            SetHeroDestPos();

        HeroMoveToDest();
    }

    void SetHeroDestPos()   //������ ���� �Լ�
    {
        heroDest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //world��ǥ�� �Է�, ���콺Ŭ�� ��ġ ��������(������)
        heroDestPos = new Vector3(heroDest.x, heroDest.y, 0);
        //���Ͱ��� ����ֱ�
        isHeroMove = true;
        animator.SetBool("isMove", true);

    }
    void HeroMoveToDest()
    {
        if (isHeroMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, heroDestPos, Time.deltaTime * heroSpeed);
        }
        if(Vector3.Distance(transform.position, heroDestPos)<=0.1f)
        {
            isHeroMove=false;
            animator.SetBool("isMove", false);
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTransfrom : MonoBehaviour
{
    public HeroMovement hero; 
    float heroSpeed = 1.5f;    //�����̵� �ӵ�
    Vector3 cursorPos;

    private void Start()
    {
        hero.heroDestPos = transform.position;      //������ ���Ͱ� �ʱ�ȭ(�����ָ� ���۰� ���ÿ� ù ��ġ�� ������)
    }

    void Update()
    {
        SetHeroDestPos();
    }

    void SetHeroDestPos()   //������ ���� �Լ�
    {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //world��ǥ�� �Է�, ���콺 Ŀ�� ��ġ

        if (Input.GetMouseButtonDown(1))
            hero.heroDestPos = new Vector3(cursorPos.x, cursorPos.y, 0);
        //������ ��ġ�� Ŀ�� ��ġ �ֱ�

        transform.position = hero.heroDestPos;      
    }


    private void OnTriggerStay2D(Collider2D collision)      //�±�, Ʈ���� �������� ��Ȳ���϶��� �����̰�(isHeroMove=true;)
    {
        if(collision.tag == "Clickable")
        {
            if (Input.GetMouseButton(1))
                 hero.isHeroMove = true; 
        }
    }
}

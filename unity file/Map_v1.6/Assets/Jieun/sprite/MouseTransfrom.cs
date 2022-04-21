using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTransfrom : MonoBehaviour
{
    public HeroMovement heroMoveScript;
    public Vector3 cursorInput, cursorFix;

    private void Start()
    {
        //heroMoveScript.heroDestPos = transform.position;      //������ ���Ͱ� �ʱ�ȭ(�����ָ� ���۰� ���ÿ� ù ��ġ�� ������)
    }

    void Update()
    {
        SetHeroDestPos();
    }

    void SetHeroDestPos()   //������ ���� �Լ�
    {
        cursorInput = Camera.main.ScreenToWorldPoint(Input.mousePosition); //��� �ٲ�� ���콺��

        //world��ǥ�� �Է�, ���콺 Ŀ�� ��ġ
        
        //if (Input.GetMouseButtonDown(1))
        //heroMoveScript.heroDestPos = new Vector3(cursorInput.x, cursorInput.y, 0);
        //������ ��ġ�� Ŀ�� ��ġ �ֱ�
        if (Input.GetMouseButtonDown(1))
        {
            cursorFix = new Vector3(cursorInput.x, cursorInput.y, 0);
        }
        transform.position = cursorFix;


    }


    private void OnTriggerStay2D(Collider2D collision)      //�±�, Ʈ���� �������� ��Ȳ���϶��� �����̰�(isHeroMove=true;)
    {
        if (collision.tag == "Clickable")
        {
            if (Input.GetMouseButton(1))
            {
                heroMoveScript.isHeroMove = true;
                heroMoveScript.isHeroBasicMove=true;
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    private Camera camera;
    private Animator animator;

    private bool isMove;            //ĳ���Ͱ� �����̰� �ִ��� ������ ����
    private Vector3 destination;    //������ ����

    private void Awake()
    {
        camera = Camera.main;
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))     //���콺 ��Ŭ�� ��ġ�� �������� ����
        {
            RaycastHit hit;
            if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                SetDestination(hit.point);
            }
        }
        Move();
    }
    private void SetDestination(Vector3 dest)   //������ ���� �Լ�
    {
        destination = dest;
        isMove = true;
        animator.SetBool("isMove",true);        //�ִϸ����� �ȿ��ִ� (�ȴ�)�ִϸ��̼� ����
    }
    private void Move()     //isMove=T�϶� ������ �������� �̵���Ű��
    {
        if(isMove)
        {
            var dir = destination- transform.position;
            transform.position += dir.normalized*Time.deltaTime * 5f ;
            //dir.normalized : Ŭ���� ���콺 ��ġ�� ĳ���� ��ġ ������ �Ÿ��� �ּ��� �̵��ӵ��� ������ �� ������
            //���� ���� normarlized ������ 1�� �������
            //Time.deltaTime : ������ ������ �ӵ��� ������ ���� �ʵ��� ����
            // 5f : �̵��ӵ���
        }

        if(Vector3.Distance(transform.position,destination)<=0.1f)
        {
            isMove = false;
            animator.SetBool("isMove", false);
        }
    }
}

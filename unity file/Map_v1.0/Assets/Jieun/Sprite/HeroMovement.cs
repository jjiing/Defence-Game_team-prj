using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float moveSpeed=1f;
    Vector3 heroDestination;
    Vector2 dirHeroDestination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float inputMousePointerX = Input.GetAxisRaw("Horizontal");  //A,D �Ǵ� �¿� ����Ű �Է¹���
        //float inputMousePointerY = Input.GetAxisRaw("Vertical");    //W,S �Ǵ� ���Ʒ� ����Ű �Է¹���
        //transform.Translate(new Vector2(inputMousePointerX, inputMousePointerY)*Time.deltaTime*moveSpeed);
    
        if(Input.GetMouseButton(1))
        {
            heroDestination = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //world��ǥ�� �Է�, ���콺 ��ġ ��������

        }
        dirHeroDestination = heroDestination - transform.position;

        //if(dirHeroDestination!=Vector2.zero) : �ִϸ��̼� ������

        transform.position = Vector2.MoveTowards(transform.position, heroDestination, Time.deltaTime * moveSpeed);  

    }
}

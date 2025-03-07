using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ex1_script1 : MonoBehaviour
{
    //����Ƽ �����Ϳ��� ex1_rod, ex1_red, ex1_green�� ���� �ν��Ͻ��� �����;��Ѵ�!
    public GameObject ex1_rod;
    public GameObject ex1_red;
    public GameObject ex1_green;

    //�� ��ũ��Ʈ�� ����ɶ� �� 1ȸ ����Ǵ� �κ�!
    void Start()
    {
        //�ʱ⿡ ex1_red�� ex1_green�� ������ �̹� �������ִ� ���±� ������
        //ex1_red�� ȸ���̵ǰ� ex1_green�� ����� �Ǿ����� ���� �� ����!
        ex1_red.GetComponent<Renderer>().material.color = Color.gray;
        ex1_green.GetComponent<Renderer>().material.color = Color.green;
    }

    //����Ƽ �������� �� �����Ӹ��� �����!
    void Update()
    {
        //Ű���忡 ����Ű �������� ������ �Ǹ����� �����Ѵ�(�̹� ����������)
        //�׶� ��ġ�� 0�� ��Ȳ
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ex1_rod.GetComponent<ArticulationBody>().SetDriveTarget(ArticulationDriveAxis.X, 0);
            ex1_red.GetComponent<Renderer>().material.color = Color.gray;
            ex1_green.GetComponent<Renderer>().material.color = Color.green;
        }

        //Ű���� ����Ű ������ ������ �Ǹ����� �����Ѵ�
        //x�ప�� 2�� �Է��ϸ� ������ ���°���!
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ex1_rod.GetComponent<ArticulationBody>().SetDriveTarget(ArticulationDriveAxis.X, 2);
            ex1_red.GetComponent<Renderer>().material.color = Color.red;
            ex1_green.GetComponent<Renderer>().material.color = Color.gray;
        }
    }
}

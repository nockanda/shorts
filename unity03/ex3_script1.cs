using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ex3_script1 : MonoBehaviour
{
    public GameObject ex3_plane;
    public GameObject ex3_led1;
    public GameObject ex3_led2;
    public GameObject ex3_led3;
    public GameObject ex3_led4;
    public GameObject ex3_led5;

    ArticulationBody ex3_plane_body;
    Material ex3_led1_material;
    Material ex3_led2_material;
    Material ex3_led3_material;
    Material ex3_led4_material;
    Material ex3_led5_material;

    void Start()
    {
        ex3_plane_body = ex3_plane.GetComponent<ArticulationBody>();
        ex3_led1_material = ex3_led1.GetComponent<Renderer>().material;
        ex3_led2_material = ex3_led2.GetComponent<Renderer>().material;
        ex3_led3_material = ex3_led3.GetComponent<Renderer>().material;
        ex3_led4_material = ex3_led4.GetComponent<Renderer>().material;
        ex3_led5_material = ex3_led5.GetComponent<Renderer>().material;

        //�⺻ 1���� �����ϱ� 1���� ��������ϰ� �������� ȸ������ ó����!
        ex3_led1_material.color = Color.green;
        ex3_led2_material.color = Color.gray;
        ex3_led3_material.color = Color.gray;
        ex3_led4_material.color = Color.gray;
        ex3_led5_material.color = Color.gray;
    }

    // Update is called once per frame
    void Update()
    {
        //Ű������ ����Ű 1~5�� ������ ����Ʈ�� �ش�Ǵ� ������ �̵��Ѵ�!
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //1��
            ex3_plane_body.SetDriveTarget(ArticulationDriveAxis.X, 0);
            ex3_led1_material.color = Color.green;
            ex3_led2_material.color = Color.gray;
            ex3_led3_material.color = Color.gray;
            ex3_led4_material.color = Color.gray;
            ex3_led5_material.color = Color.gray;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //2��
            ex3_plane_body.SetDriveTarget(ArticulationDriveAxis.X, 2.7f);
            ex3_led1_material.color = Color.gray;
            ex3_led2_material.color = Color.green;
            ex3_led3_material.color = Color.gray;
            ex3_led4_material.color = Color.gray;
            ex3_led5_material.color = Color.gray;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //3��
            ex3_plane_body.SetDriveTarget(ArticulationDriveAxis.X, 5.5f);
            ex3_led1_material.color = Color.gray;
            ex3_led2_material.color = Color.gray;
            ex3_led3_material.color = Color.green;
            ex3_led4_material.color = Color.gray;
            ex3_led5_material.color = Color.gray;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //4��
            ex3_plane_body.SetDriveTarget(ArticulationDriveAxis.X, 8.2f);
            ex3_led1_material.color = Color.gray;
            ex3_led2_material.color = Color.gray;
            ex3_led3_material.color = Color.gray;
            ex3_led4_material.color = Color.green;
            ex3_led5_material.color = Color.gray;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //5��
            ex3_plane_body.SetDriveTarget(ArticulationDriveAxis.X, 11f);
            ex3_led1_material.color = Color.gray;
            ex3_led2_material.color = Color.gray;
            ex3_led3_material.color = Color.gray;
            ex3_led4_material.color = Color.gray;
            ex3_led5_material.color = Color.green;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ex1_script1 : MonoBehaviour
{
    //유니티 에디터에서 ex1_rod, ex1_red, ex1_green에 대한 인스턴스를 가져와야한다!
    public GameObject ex1_rod;
    public GameObject ex1_red;
    public GameObject ex1_green;

    //이 스크립트가 실행될때 딱 1회 실행되는 부분!
    void Start()
    {
        //초기에 ex1_red와 ex1_green의 색상은 이미 전진해있는 상태기 때문에
        //ex1_red는 회색이되고 ex1_green은 녹색이 되었으면 좋을 것 같다!
        ex1_red.GetComponent<Renderer>().material.color = Color.gray;
        ex1_green.GetComponent<Renderer>().material.color = Color.green;
    }

    //유니티 엔진에서 매 프레임마다 실행됨!
    void Update()
    {
        //키보드에 방향키 오른쪽을 누르면 실린더가 전진한다(이미 전진해있음)
        //그때 위치가 0인 상황
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ex1_rod.GetComponent<ArticulationBody>().SetDriveTarget(ArticulationDriveAxis.X, 0);
            ex1_red.GetComponent<Renderer>().material.color = Color.gray;
            ex1_green.GetComponent<Renderer>().material.color = Color.green;
        }

        //키보드 방향키 왼쪽을 누르면 실린더가 후진한다
        //x축값에 2를 입력하면 후진한 상태가됨!
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ex1_rod.GetComponent<ArticulationBody>().SetDriveTarget(ArticulationDriveAxis.X, 2);
            ex1_red.GetComponent<Renderer>().material.color = Color.red;
            ex1_green.GetComponent<Renderer>().material.color = Color.gray;
        }
    }
}

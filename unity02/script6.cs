using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script6 : MonoBehaviour
{
    //�� ��ũ��Ʈ�� this�� ex1_cylinder�̴�!

    ArticulationBody mybody;

    void Start()
    {
        mybody = this.GetComponent<ArticulationBody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //Q�� �������� �̳༮�� �����ٵ��� �������� -2�� �ٲ�!
            mybody.SetDriveTarget(ArticulationDriveAxis.X, -2);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //Q�� �������� �̳༮�� �����ٵ��� �������� 0�� �ٲ�!
            mybody.SetDriveTarget(ArticulationDriveAxis.X, 0);
        }
    }
}

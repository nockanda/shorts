using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script6 : MonoBehaviour
{
    //이 스크립트의 this는 ex1_cylinder이다!

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
            //Q가 눌러지면 이녀석의 관절바디의 설정값을 -2로 바꿈!
            mybody.SetDriveTarget(ArticulationDriveAxis.X, -2);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //Q가 눌러지면 이녀석의 관절바디의 설정값을 0로 바꿈!
            mybody.SetDriveTarget(ArticulationDriveAxis.X, 0);
        }
    }
}

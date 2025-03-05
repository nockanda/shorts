using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script7 : MonoBehaviour
{
    ArticulationBody myjoint;
    void Start()
    {
        myjoint = this.GetComponent<ArticulationBody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            //180��ȸ��
            myjoint.SetDriveTarget(ArticulationDriveAxis.X, 180);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //0��ȸ��
            myjoint.SetDriveTarget(ArticulationDriveAxis.X, 0);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            //-180��ȸ��
            myjoint.SetDriveTarget(ArticulationDriveAxis.X, -180);
        }
    }
}

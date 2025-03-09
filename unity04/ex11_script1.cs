using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ex11_script1 : MonoBehaviour
{
    public Slider slider;
    public TMP_InputField textbox;

    public ArticulationBody rod;

    void Start()
    {
        slider.onValueChanged.AddListener(myfunction);
     
    }

    void myfunction(float num)
    {
        textbox.text = num.ToString();
        rod.SetDriveTarget(ArticulationDriveAxis.X, num);
    }
}

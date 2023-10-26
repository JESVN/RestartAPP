using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAppRestartTool : MonoBehaviour
{
    public Button Restart_Button;
    public Button Restart_Button2;

    // Start is called before the first frame update
    void Start()
    {
        Restart_Button.onClick.AddListener(AppRestartTool.ResartApp);
        Restart_Button2.onClick.AddListener(AppRestartTool2.ResartApp);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
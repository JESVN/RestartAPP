using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAppRestartTool : MonoBehaviour
{
    public Button Restart_Button;

    // Start is called before the first frame update
    void Start()
    {
        Restart_Button.onClick.AddListener(AppRestartTool.ResartApp);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
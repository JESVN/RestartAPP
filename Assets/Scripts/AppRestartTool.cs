using UnityEditor;
using UnityEngine;

public class AppRestartTool
{
    #region 对外公开使用的接口


    /// <summary>
    /// 重启应用
    /// </summary>
    public static void ResartApp()
    {
        Debug.Log(" ResartApp");
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        MJavaObject.Call("restartApp");
        MJavaObject.Dispose();
#endif

    }


    #endregion 对外公开使用的接口

    #region 帮助方法



    #endregion

    #region 私有变量
    static AndroidJavaObject javaObject;

    public static AndroidJavaObject MJavaObject
    {
        get
        {

            if (javaObject == null)
            {
                javaObject = new AndroidJavaObject("com.example.resartapplibrary.ResartAPP");
            }

            return javaObject;
        }
    }
    #endregion
}
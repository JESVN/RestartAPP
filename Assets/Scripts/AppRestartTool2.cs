using System.Threading;
using UnityEngine;

public class AppRestartTool2
{
    /// <summary>
    /// 重启应用
    /// </summary>
    public static void ResartApp()
    {
        Debug.Log(" ResartApp");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        //记录主线程
        SynchronizationContext sc = SynchronizationContext.Current;

        Thread t = new Thread(() =>
        {
            Thread.Sleep(500);

            //传递到主线程调用isPlaying
            sc.Post((object o) =>
            {
                UnityEditor.EditorApplication.isPlaying = true;
            }, null);
        });
        t.Start();


#elif UNITY_ANDROID
        restartAndroid();
#elif UNITY_IPHONE
        //苹果没办法重启,进行强退(未测试)
        UnityEngine.Diagnostics.Utils.ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.Abort);
#else
        throw new System.NotImplementedException();
#endif
    }

    private static void restartAndroid()
    {
        if (Application.isEditor)
            return;

        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            const int kIntent_FLAG_ACTIVITY_CLEAR_TASK = 0x00008000;
            const int kIntent_FLAG_ACTIVITY_NEW_TASK = 0x10000000;

            var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            var pm = currentActivity.Call<AndroidJavaObject>("getPackageManager");
            var intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage", Application.identifier);

            intent.Call<AndroidJavaObject>("setFlags", kIntent_FLAG_ACTIVITY_NEW_TASK | kIntent_FLAG_ACTIVITY_CLEAR_TASK);
            currentActivity.Call("startActivity", intent);
            currentActivity.Call("finish");
            var process = new AndroidJavaClass("android.os.Process");
            int pid = process.CallStatic<int>("myPid");
            process.CallStatic("killProcess", pid);
        }
    }
}
using UnityEngine;

public class Vibration
{
    public static void Vibrate(int level)
    {
        if (!Config.IsVibrationOn) return;
        // if platform is not android or IphonePlayer return
        if (Application.platform != RuntimePlatform.Android &&
            Application.platform != RuntimePlatform.IPhonePlayer) return;
        
#if UNITY_IOS
        VibrateIos(level);
#elif UNITY_ANDROID
        VibrateAnd(level);
#endif
    }

    private static void VibrateAnd(int level)
    {
        Native.Vibrate(level);
    }

    private static void VibrateIos(int level)
    {
        if (iOSHapticFeedback.Instance.IsSupported())
        {
            iOSHapticFeedback.Instance.Trigger((iOSHapticFeedback.iOSFeedbackType) level);
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    public static bool IsHapticSupported()
    {
#if UNITY_IOS
        return iOSHapticFeedback.Instance.IsSupported();
#elif UNITY_ANDROID
        return Native.getSDKInt() >= 26;
#endif
        return false;
    }
}
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class Config : MonoBehaviour
{
    public static string ActiveBackgroundName
    {
        get => PlayerPrefs.GetString("ActiveBackgroundName", "bg0");
        set => PlayerPrefs.SetString("ActiveBackgroundName", value);
    }

    public static bool IsVolumeOn
    {
        get => PlayerPrefsX.GetBool("IsVolumeOn", true);
        set => PlayerPrefsX.SetBool("IsVolumeOn", value);
    }

    public static bool IsVibrationOn
    {
        get => PlayerPrefsX.GetBool("IsVibrationOn", true);
        set => PlayerPrefsX.SetBool("IsVibrationOn", value);
    }

    public static string ActiveLanguage
    {
        get => PlayerPrefs.GetString("ActiveLanguage", "en");
        set => PlayerPrefs.SetString("ActiveLanguage", value);
    }

    public static void LoadLocale(string languageIdentifier)
    {
       LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales.First(x=>x.Identifier.Code == languageIdentifier);
    }
}
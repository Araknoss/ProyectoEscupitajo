using UnityEngine;
using System.IO;

public static class SettingsRepository
{
    private static string Path =>
        System.IO.Path.Combine(Application.persistentDataPath, "settings.json");

    public static void Save(SettingsData data)
    {
        File.WriteAllText(Path, JsonUtility.ToJson(data, prettyPrint: true));
    }

    public static void Load(SettingsData data)
    {
        if (!File.Exists(Path)) return;
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Path), data);
    }
}

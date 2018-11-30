using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ResetPlayerProgress
{
    [MenuItem("Tools/Reset progress")]
    public static void ResetProgress()
    {
        string _SaveGameString = "SaveGame.sav";
        File.Delete(Path.Combine(Application.persistentDataPath, _SaveGameString));
    }
}

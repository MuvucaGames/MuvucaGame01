using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WarpZoneDoor))]
public class WarpZoneDoorEditor : Editor {

    public bool timeBased;
    public bool bothHeroesNeeded;
    public bool isUnlocked;
    public bool isToggle;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        WarpZoneDoor warpZoneDoor = (WarpZoneDoor)target;
        isUnlocked = warpZoneDoor.isUnlocked;
        timeBased = warpZoneDoor.isTimeBased;
        bothHeroesNeeded = warpZoneDoor.bothHeroesNeeded;
        isToggle = warpZoneDoor.isToggle;

        warpZoneDoor.heroStrong = (HeroStrong)EditorGUILayout.ObjectField("Hero Strong",
                                                                     warpZoneDoor.heroStrong,
                                                                     typeof(HeroStrong), true);
        warpZoneDoor.heroFast = (HeroFast)EditorGUILayout.ObjectField("Hero Fast",
                                                                   warpZoneDoor.heroFast,
                                                                   typeof(HeroFast), true);
        warpZoneDoor.doorTarget = (WarpZoneDoor)EditorGUILayout.ObjectField("Warp Door Target",
                                                                            warpZoneDoor.doorTarget,
                                                                            typeof(WarpZoneDoor), 
                                                                            true);
        EditorGUILayout.Space();

        warpZoneDoor.isUnlocked = EditorGUILayout.ToggleLeft("Is Unlocked?", isUnlocked);
        warpZoneDoor.bothHeroesNeeded = GUILayout.Toggle(bothHeroesNeeded, "Both Heroes Needed");
        warpZoneDoor.isToggle = GUILayout.Toggle(isToggle, "Is Toggle?");
        warpZoneDoor.timeToTeleport = EditorGUILayout.FloatField(new GUIContent("Delay to Teleport"),
                                                                 warpZoneDoor.timeToTeleport);
        EditorGUILayout.Space();

        warpZoneDoor.isTimeBased = GUILayout.Toggle(timeBased, "Is Time Based?");
        warpZoneDoor.isTimeBased = EditorGUILayout.Foldout(warpZoneDoor.isTimeBased, 
                                                           "Time Based", 
                                                           EditorStyles.foldout);
        // TODO: This is still poorly desgined
        if (timeBased)
        {
            warpZoneDoor.timeToChangeLockState = EditorGUILayout.FloatField(new GUIContent("Change State Time"),
                                                                           warpZoneDoor.timeToChangeLockState);
            EditorGUILayout.Space();
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}

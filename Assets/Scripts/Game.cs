using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameLevel
{
    Bootstrap, //This level resets the game!
    SplashScreen,
    MainMenu,
    Options,
    SelectSlot,
    Credits,
    //A0_Level_1,
    //A1_Level_1,
    //A2_Level_1,
    //A3_Level_1,
    //A4_Level_1,
    //A5_Level_1,
    //RsvargasTestScene,
    //StefanWTestScene
}


// This is the Game Controller, it is a static class. All its member must be static
// so they're all available at all times.
// 
// Data in this class is not cleared between level loads.

public static class Game {
    private static int levelLoadCounter_ = 1;
    private static GameLevel currentLevel_;
    private static Dictionary<GameLevel, int> Levels = new Dictionary<GameLevel, int>();

    static Game()
    {
        string[] names = GameLevels.Levels;
        foreach (string n in names)
        {
            Debug.Log("Scene: " + n);
        }

        GameLevel[] values = Enum.GetValues(typeof(GameLevel)) as GameLevel[];
        foreach(GameLevel l in values)
        {
            int idx = Array.IndexOf(names, l.ToString() );
            if( idx == -1 )
                throw new System.ArgumentException("Invalid scene name in Game Controller: " + l.ToString() );
            Levels.Add(l, idx);
        }
    }

    public static void Start()
    {
        //Add code that should prepare the game here!
        LoadLevel(GameLevel.SplashScreen);
    }

    public static void LoadLevel(GameLevel l)
    {
        Application.LoadLevel(Levels[l]);
        currentLevel_ = l;
        levelLoadCounter_++;
    }

    public static void ReloadLevel()
    {
        LoadLevel(currentLevel_);
    }

    public static void Quit()
    {
        Application.Quit();
    }


    #region properties
    public static int levelLoadCount
    {
        get { return levelLoadCounter_; }
    }

    public static GameLevel currentLevel
    {
        get { return currentLevel_; }
    }
    #endregion
}

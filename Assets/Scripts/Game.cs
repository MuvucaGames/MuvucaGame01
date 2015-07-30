using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameLevel
{
    IntroMovie,
    MainMenu,
    Level1,
    Level2,
    Level3,
    //...
    CreditsScreen = 100,
    TestRsvargas,
    TestStefan
}

public static class Game {
    private static int counter = 0;
    private static Dictionary<GameLevel, string> Levels = new Dictionary<GameLevel, string>();
    //protected GameController() {}
    //private static GameController myInstance = null;
    //int counter = 0;

    //public static GameController instance
    //{
    //    get
    //    {
    //        if(myInstance == null)
    //        {
    //            myInstance = new GameController();
    //        }
    //        return myInstance;
    //    }
    //}

    public static void LoadLevel(GameLevel l)
    {
        try
        {
            if( Levels.Count == 0)
            {
                Levels.Add(GameLevel.IntroMovie, "SplashScreen");
                Levels.Add(GameLevel.MainMenu, "MainMenu");
                Levels.Add(GameLevel.TestRsvargas, "rsvargasScene");
                Levels.Add(GameLevel.TestStefan, "StefanWTestScene");
                Levels.Add(GameLevel.CreditsScreen, "Credits");
            }
            string levelname = Levels[l];

            Application.LoadLevel(levelname);
        }
        catch(Exception e)
        {
            Debug.Log("Could not load level: " + l + ", error: " + e.Message);
            Application.LoadLevel("MainMenu");
        }
    }

    public static int Teste()
    {
        //Just to demonstrate that counter is not reset when loading levels...
        Debug.Log("Counter = " + counter);
        return ++counter;
    }

}

using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    public void NewGame()
    {
        Game.LoadLevel(GameLevel.RsvargasTestScene);
    }

    public void Credits()
    {
        Game.LoadLevel(GameLevel.Credits);
    }
}

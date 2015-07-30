using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    public void NewGame()
    {
        Game.LoadLevel(GameLevel.TestRsvargas);
    }

    public void Credits()
    {
        Game.LoadLevel(GameLevel.CreditsScreen);
    }
}

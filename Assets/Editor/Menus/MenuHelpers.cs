using UnityEngine;
using System.Collections;
using UnityEditor;

public class MenuHelpers{
	[MenuItem("MuvucaGames/GDD")]
	public static void OpenGDD(){
		Help.BrowseURL ("https://github.com/MuvucaGames/MuvucaGame01/wiki/GDD");
	}

	[MenuItem("MuvucaGames/Trello")]
	public static void OpenTrello(){
		Help.BrowseURL ("https://trello.com/muvucagames");
	}

	[MenuItem("MuvucaGames/Github")]
	public static void OpenGithub(){
		Help.BrowseURL ("https://github.com/MuvucaGames/MuvucaGame01/");
	}
}

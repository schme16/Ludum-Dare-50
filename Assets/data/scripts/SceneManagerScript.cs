using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {
	public LevelChanger lc;
	public TextMeshProUGUI inputText;
	
	// Start is called before the first frame update
	void Start() {
	}

	// Update is called once per frame
	void Update() {
	}

	public void NewGame() {
		SceneManager.LoadScene("gameplay", LoadSceneMode.Single);
	}

	public void Restart() {
		SceneManager.LoadScene("restart", LoadSceneMode.Single);
	}

	public void Highscores() {
		SceneManager.LoadScene("highscores", LoadSceneMode.Single);
	}

	public void Mainmenu() {
		SceneManager.LoadScene("main-menu", LoadSceneMode.Single);
	}

	public void NewGameFadeSet() {
		lc.LevelToLoad = "gameplay";
		lc.FadeToLevel();
	}

	public void RestartFadeSet() {
		lc.LevelToLoad = "restart";
		lc.FadeToLevel();
	}

	public void HighscoresFadeSet() {
		lc.LevelToLoad = "highscores";
		lc.FadeToLevel();
	}

	public void MainmenuFadeSet() {
		lc.LevelToLoad = "main-menu";
		lc.FadeToLevel();
	}
	public void HowToPlayFadeSet() {
		lc.LevelToLoad = "howToPlay";
		lc.FadeToLevel();
	}
	public void HowToPlayFadeRestartSet() {
		lc.LevelToLoad = "howToPlay-restart";
		lc.FadeToLevel();
	}

	
	public void SetPlayerName() {
		PlayerPrefs.SetString("player-name", inputText.text);
		PlayerPrefs.Save();
	}

	public void Quit() {
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
		Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
#if (UNITY_EDITOR)
		UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
		    Application.Quit();
#elif (UNITY_WEBGL)
		    Application.OpenURL("about:blank");
#endif
	}
}
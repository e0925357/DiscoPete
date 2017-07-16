using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class StartButton : MonoBehaviour
	{
		public Text highscoreText;

		void Awake()
		{
			GameObject levelMasterGo = GameObject.FindWithTag("LevelAndPointMaster");
			int score = 0;

			if (levelMasterGo)
			{
				LevelAndPointBehaviour levelMaster = levelMasterGo.GetComponent<LevelAndPointBehaviour>();
				score = levelMaster.OverallPoints;

				Destroy(levelMasterGo);
			}

			bool newHighscore = false;
			int highscore;

			if (PlayerPrefs.HasKey("Highscore"))
			{
				highscore = PlayerPrefs.GetInt("Highscore");

				if (highscore < score)
				{
					highscore = score;
					newHighscore = true;
				}
			}
			else
			{
				highscore = score;
				newHighscore = true;
			}

			if (newHighscore)
			{
				PlayerPrefs.SetInt("Highscore", highscore);
				PlayerPrefs.Save();
				highscoreText.text = string.Format("New Highscore: {0}", highscore);
			}
			else
			{
				highscoreText.text = string.Format("Highscore: {0}", highscore);
			}
		}

		public void StartGame()
		{
			SceneManager.LoadScene(1);
		}
	}
}
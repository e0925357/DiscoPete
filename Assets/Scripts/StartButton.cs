using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class StartButton : MonoBehaviour
	{
		void Awake()
		{
			GameObject levelMaster = GameObject.FindWithTag("LevelAndPointMaster");

			if (levelMaster)
			{
				Destroy(levelMaster);
			}
		}

		public void StartGame()
		{
			SceneManager.LoadScene(1);
		}
	}
}
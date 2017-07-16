using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class StartButton : MonoBehaviour
	{
		public void StartGame()
		{
			SceneManager.LoadScene(1);
		}
	}
}
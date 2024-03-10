using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ALS.Aberration
{
    /// <summary>
    /// EventBusTest
    /// </summary>
    public class EventBusTest : MonoBehaviour
    {
		private void OnEnable()
		{
			SceneManager.sceneLoaded += NewLevelLoaded;
		}

		private void OnDisable()
		{
			SceneManager.sceneLoaded -= NewLevelLoaded;
		}

		void NewLevelLoaded(Scene scene, LoadSceneMode loadSceneMode)
		{
			Debug.Log($"scene with build index of {scene.buildIndex}, with {loadSceneMode}");
			if (scene.buildIndex == 1) StartCoroutine(RepeatedCallsToChangeView(2f));
		}

		IEnumerator RepeatedCallsToChangeView(float waitTime)
		{
			Debug.Log("Test Coroutine ran");
			int times = 10;
			while (times > 0)
			{
				yield return new WaitForSeconds(waitTime);
				Debug.Log($"Sending Event, times left: {times}, view sent {times % 3}");
				EventBus<SwitchMenuFocus>.Raise(new SwitchMenuFocus
				{
					View = times % 3
				});
				times--;
			}
		}
	}
}

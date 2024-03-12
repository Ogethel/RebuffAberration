using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


/// <summary>
/// MainMenuFocusController
/// </summary>
public class MainMenuFocusController : MonoBehaviour
{
	[SerializeField] GameObject[] _cameraPositions;

    EventBinding<SwitchMenuFocus> _menuFocusChanged;

	CinemachineVirtualCamera _virtualCamera;

	private void OnEnable()
	{
		_menuFocusChanged = new EventBinding<SwitchMenuFocus>(SwitchFocus);
		EventBus<SwitchMenuFocus>.Register(_menuFocusChanged);
		//Start off by setting the camera
		Transform target = _cameraPositions[0].transform;
		if (!target) return;

		_virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
		if (!_virtualCamera) return;

		Debug.Log("We got to the virtual camera");

		_virtualCamera.transform.position = target.position;
		_virtualCamera.transform.rotation = target.rotation;
		//Camera.main.transform.position = target.position;
		//Camera.main.transform.rotation = target.rotation;
	}

	private void OnDisable()
	{
		EventBus<SwitchMenuFocus>.DeRegister(_menuFocusChanged);
	}

	void SwitchFocus(SwitchMenuFocus focusEvent)
	{
		Debug.Log("Tried to Switch focus");
		Transform target = _cameraPositions[focusEvent.View].transform;
		if (!target) return;

		//Camera.main.transform.position = target.position;
		//Camera.main.transform.rotation = target.rotation;
		_virtualCamera.transform.position = target.position;
		_virtualCamera.transform.rotation = target.rotation;
	}
}
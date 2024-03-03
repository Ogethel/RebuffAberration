using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpinScript : MonoBehaviour
{
    [SerializeField] float _targetScale = 1;
	[SerializeField] Quaternion _targetRotation;
	private Vector3 _scaleVel;

	private void OnEnable()
	{
		UIDocmentTarodevReference.ScaleChanged += OnScaleChanged;
		UIDocmentTarodevReference.SpinClicked += OnSpinClicked;
	}
	private void OnDisable()
	{
		UIDocmentTarodevReference.ScaleChanged -= OnScaleChanged;
		UIDocmentTarodevReference.SpinClicked -= OnSpinClicked;
	}

	void OnSpinClicked()
	{
		_targetRotation = transform.rotation * Quaternion.Euler(Random.insideUnitSphere * 360);
	}

	void OnScaleChanged(float newScale)
	{
		_targetScale = newScale;
	}

	private void Update()
	{
		transform.localScale = Vector3.SmoothDamp(transform.localScale, 
			_targetScale * Vector3.one, ref _scaleVel, 0.15f);

		transform.rotation = Quaternion.Slerp(transform.rotation, 
			_targetRotation, Time.deltaTime * 5.2f);
	}
}

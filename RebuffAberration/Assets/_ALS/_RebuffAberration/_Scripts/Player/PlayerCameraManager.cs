using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace ALS.Aberration
{
    /// <summary>
    /// PlayerCameraManager
    /// </summary>
    public class PlayerCameraManager : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera _virtual;
        [SerializeField] Transform _playerTransform;

        Vector3 _conPos = new Vector3(0, 16.25f, -8);
        Vector3 _conRot = new Vector3(65, 0, 0);

		private void OnEnable()
		{
            //Setup the Virtual Camera
            _virtual = FindAnyObjectByType<CinemachineVirtualCamera>();
            if (!_virtual) return;

            _virtual.transform.position = _conPos;
            _virtual.transform.eulerAngles = _conRot;
            _virtual.Follow = _playerTransform;

            CinemachineTransposer transposer = _virtual.GetCinemachineComponent<CinemachineTransposer>();

            if (transposer != null)
            {
                // Set the binding mode (example: WorldSpace)
                transposer.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;

                // Set the follow offset (example: 2 units above and 5 units behind the target)
                transposer.m_FollowOffset = _conPos;
            }
            else
            {
                Debug.LogError("Cinemachine Transposer component not found on Virtual Camera!");
            }
        }
	}
}

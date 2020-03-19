namespace Mapbox.Examples
{
	using UnityEngine;

	namespace Scripts.Utilities
	{
		public class DragRotate : MonoBehaviour
		{
			public Transform playerTransform;

			[SerializeField]
			float _multiplier;

			Vector3 _startTouchPosition;

			public void Start()
			{
				//playerTransform = GameManager.instance.playerTransform;
			}

			void Update()
			{
				if (Input.GetMouseButtonDown(0))
				{
					_startTouchPosition = Input.mousePosition;
				}

				if (Input.GetMouseButton(0))
				{
					var dragDelta = Input.mousePosition - _startTouchPosition;
					var axis = new Vector3(0f, -dragDelta.x * _multiplier, 0f);
					transform.RotateAround(playerTransform.position, axis, _multiplier);
				}

			}
		}

		
	}
}

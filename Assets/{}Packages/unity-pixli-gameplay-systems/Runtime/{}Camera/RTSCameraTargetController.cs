using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace PixLi
{
	public class RTSCameraTargetController : MonoBehaviour
	{
		[SerializeField] private CustomCursor.Data _cursorData;
		public CustomCursor.Data _CursorData => this._cursorData;

		[SerializeField] private RangeFloat _movementSpeed = new RangeFloat(20f, 200f);
		[SerializeField] private float _zoomSpeed = 5f;

		[SerializeField] private BoundsReference _movementBoundsReference;
		public BoundsReference _MovementBounds => this._movementBoundsReference;

		[SerializeField] private Vector3 _offset;
		public Vector3 _Offset => this._offset;

		public void SetMovementBounds(Bounds bounds, Vector3 newPosition)
		{
			this._movementBoundsReference.Bounds = bounds;
			this.transform.position = newPosition + this._offset;
		}

		private const float _POINTER_OFFSET = 15f;

		private void Awake()
		{
			// In case you want to initially place camera target outside of bounds.
			// Mouse input will disrupt this position anyway once you move it.
			// So I guess for now it's the best to just snap it to correct values on the awake.
			this.transform.position = new Vector3(
				x: Mathf.Clamp(
					value: this.transform.position.x,
					min: this._movementBoundsReference.Bounds.min.x,
					max: this._movementBoundsReference.Bounds.max.x
				),
				y: Mathf.Clamp(
					value: this.transform.position.y,
					min: this._movementBoundsReference.Bounds.min.y,
					max: this._movementBoundsReference.Bounds.max.y
				),
				z: Mathf.Clamp(
					value: this.transform.position.z,
					min: this._movementBoundsReference.Bounds.min.z,
					max: this._movementBoundsReference.Bounds.max.z
				)
			);
		}

		private Vector3Int _movementDirection;

		private Vector2 GetMousePosition()
		{
#if ENABLE_LEGACY_INPUT_MANAGER
		return Input.mousePosition;
#elif ENABLE_INPUT_SYSTEM
			return Mouse.current.position.ReadValue();
#endif
		}

		private Vector2 GetMouseScroll()
		{
#if ENABLE_LEGACY_INPUT_MANAGER
		return Input.mouseScrollDelta;
#elif ENABLE_INPUT_SYSTEM
			return Mouse.current.scroll.ReadValue();
#endif
		}

		private bool IsMouseReady()
		{
#if ENABLE_LEGACY_INPUT_MANAGER
		return Input.mousePresent;
#elif ENABLE_INPUT_SYSTEM
			return !Mouse.current.CheckStateIsAtDefault();
#endif
		}

		private void LateUpdate()
		{
			if (this.IsMouseReady())
			{
				if (this._movementDirection.sqrMagnitude > 0)
					CustomCursor._Instance.Remove(data: this._cursorData);

				_movementDirection = Vector3Int.zero;
				float yPositionRatio = Mathf.InverseLerp(this._movementBoundsReference.Bounds.min.y, this._movementBoundsReference.Bounds.max.y, this.transform.position.y);

				Vector2 mousePosition = this.GetMousePosition();

				if (mousePosition.x < _POINTER_OFFSET)
				{
					_movementDirection.x = -1;
				}
				else if (mousePosition.x > Screen.width - _POINTER_OFFSET)
				{
					_movementDirection.x = 1;
				}

				if (mousePosition.y < _POINTER_OFFSET)
				{
					_movementDirection.z = -1;
				}
				else if (mousePosition.y > Screen.height - _POINTER_OFFSET)
				{
					_movementDirection.z = 1;
				}

				Vector3 normalizedMovementDirection = ((Vector3)_movementDirection).normalized;

				if (_movementDirection.x != 0 || _movementDirection.z != 0)
				{
					this._cursorData.Angle = Vector3.SignedAngle(
						from: Vector3.forward,
						to: normalizedMovementDirection,
						axis: Vector3.up
					);

					CustomCursor._Instance.Add(data: this._cursorData);
				}

				normalizedMovementDirection.y = -this.GetMouseScroll().y;

				this.transform.position = new Vector3(
					x: Mathf.Clamp(
						value: this.transform.position.x + normalizedMovementDirection.x * this._movementSpeed.Lerp(yPositionRatio) * Time.unscaledDeltaTime,
						min: this._movementBoundsReference.Bounds.min.x,
						max: this._movementBoundsReference.Bounds.max.x
					),
					y: Mathf.Clamp(
						value: this.transform.position.y + normalizedMovementDirection.y * this._zoomSpeed * Time.unscaledDeltaTime,
						min: this._movementBoundsReference.Bounds.min.y,
						max: this._movementBoundsReference.Bounds.max.y
					),
					z: Mathf.Clamp(
						value: this.transform.position.z + normalizedMovementDirection.z * this._movementSpeed.Lerp(yPositionRatio) * Time.unscaledDeltaTime,
						min: this._movementBoundsReference.Bounds.min.z,
						max: this._movementBoundsReference.Bounds.max.z
					)
				) + this._offset;
			}
		}

#if UNITY_EDITOR
		private void OnDrawGizmosSelected()
		{
			if (this._movementBoundsReference != null)
				this._movementBoundsReference.DrawBoundsGizmos();
		}
#endif
	}
}
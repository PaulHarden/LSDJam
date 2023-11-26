﻿using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class FirstPersonController : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 4.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 6.0f;
		[Tooltip("Rotation speed of the character")]
		public float RotationSpeed = 1.0f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;
		private bool canSprint = true;
		public float health;
		public float stamina;
		private float staminaMax = 100f;
		public float staminaRate;
		public bool canPiss = true;
		public float piss;
		private float pissMax = 100f;
		public float pissRate;
		public GameObject PissFX;
		private ParticleSystem.EmissionModule _emissionModule;

		[Space(10)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;
		[Tooltip("The height the player's vision is when crouched")]
		private float _startHeight = 1.375f;
		public float CrouchHeight = 0.5f;
		[Tooltip("The amount of time it takes to full crouch/stand")]
		public float CrouchTime = 3f;
		private bool _isCrouched;

		[Space(10)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.1f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.5f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 90.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -90.0f;

		// cinemachine
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _targetSpeed;
		private float _rotationVelocity;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;
		public bool isPaused;

		// timeout deltatime
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

	
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		private PlayerInput _playerInput;
#endif
		private CharacterController _controller;
		public StarterAssetsInputs input;
		private GameObject _mainCamera;
		private const float _threshold = 0.01f;

		private bool IsCurrentDeviceMouse
		{
			get
			{
				#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
				return _playerInput.currentControlScheme == "KeyboardMouse";
				#else
				return false;
				#endif
			}
		}

		private void Awake()
		{
			if (_mainCamera == null)
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		}

		private void Start()
		{
			_controller = GetComponent<CharacterController>();
			input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
			_playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif
			
			// reset our timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;
			
			// stamina meter
			stamina = staminaMax;
			
			// piss stream
			piss = pissMax;
			_emissionModule = PissFX.GetComponent<ParticleSystem>().emission;
		}

		private void Update()
		{
			JumpAndGravity();
			GroundedCheck();
			Move();
			Crouch();
			Piss();
			
			if (input.pause)
			{
				input.pause = !input.pause;
				PauseResume();
			}
		}

		private void LateUpdate() => CameraRotation();

		private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
		}

		private void CameraRotation()
		{
			// if there is an input
			if (input.look.sqrMagnitude >= _threshold)
			{
				//Don't multiply mouse input by Time.deltaTime
				float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
			
				_cinemachineTargetPitch += input.look.y * RotationSpeed * deltaTimeMultiplier;
				_rotationVelocity = input.look.x * RotationSpeed * deltaTimeMultiplier;

				// clamp our pitch rotation
				_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

				// Update Cinemachine camera target pitch
				CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

				// rotate the player left and right
				transform.Rotate(Vector3.up * _rotationVelocity);
			
				// rotate piss stream up and down with camera
				//PissFX.transform.rotation = CinemachineCameraTarget.transform.rotation;
			}
		}

		private void Move()
		{
			// set target speed based on move speed, sprint speed and if sprint is pressed
			if (input.sprint)
			{
				if (stamina > 0 && canSprint)
				{
					stamina -= Time.deltaTime * staminaRate;
					_targetSpeed = SprintSpeed;
					canSprint = true;
				}
				else
				{
					_targetSpeed = MoveSpeed;
					canSprint = false;
				}
			}
			else
			{
				if (stamina <= staminaMax)
					stamina += Time.deltaTime * staminaRate;			
				_targetSpeed = MoveSpeed;
				canSprint = true;
			}

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (input.move == Vector2.zero) _targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = input.analogMovement ? input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < _targetSpeed - speedOffset || currentHorizontalSpeed > _targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, _targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
				_speed = _targetSpeed;

			// normalise input direction
			Vector3 inputDirection = new Vector3(input.move.x, 0.0f, input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (input.move != Vector2.zero)
			{
				// move
				inputDirection = transform.right * input.move.x + transform.forward * input.move.y;
			}

			// move the player
			_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
		}

		private void JumpAndGravity()
		{
			if (Grounded)
			{
				// reset the fall timeout timer
				_fallTimeoutDelta = FallTimeout;

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				// Jump
				if (input.jump && _jumpTimeoutDelta <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f)
					_jumpTimeoutDelta -= Time.deltaTime;
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDelta = JumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
					_fallTimeoutDelta -= Time.deltaTime;

				// if we are not grounded, do not jump
				input.jump = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
				_verticalVelocity += Gravity * Time.deltaTime;
		}
		
		private void Crouch()
		{
			float velY = 0;
			if (_isCrouched && input.crouch)
			{
				float newY = Mathf.SmoothDamp(CinemachineCameraTarget.transform.localPosition.y, _startHeight, ref velY, Time.deltaTime * CrouchTime);
				CinemachineCameraTarget.transform.localPosition = new Vector3(0, newY, 0);
				GetComponent<CharacterController>().height = 2f;
				_isCrouched = false;
				input.crouch = false;
			}

			if (!_isCrouched && input.crouch)
			{
				float newY = Mathf.SmoothDamp(CinemachineCameraTarget.transform.localPosition.y, CrouchHeight, ref velY, Time.deltaTime * CrouchTime);
				CinemachineCameraTarget.transform.localPosition = new Vector3(0, newY, 0);
				GetComponent<CharacterController>().height = CrouchHeight;
				_isCrouched = true;
				input.crouch = false;
			}
		}

		private void Piss()
		{
			if (input.piss)
			{
				if (piss > 0 && canPiss)
				{
					_emissionModule.enabled = true;
					piss -= Time.deltaTime * pissRate;
					canPiss = true;
				}
				else
				{
					_emissionModule.enabled = false;
					canPiss = false;
				}
			}
			else
			{
				_emissionModule.enabled = false;
				if (piss <= pissMax)
					piss += Time.deltaTime * pissRate;			
				canPiss = true;
			}
		}

		public void PauseResume()
		{
			if (!isPaused)
			{
				Time.timeScale = 0f;
				input.SetCursorState(false);
				input.cursorLocked = false;
				input.cursorInputForLook = false;
				isPaused = true;
			}
			else
			{
				Time.timeScale = 1f;
				input.SetCursorState(true);
				input.cursorLocked = true;
				input.cursorInputForLook = true;
				isPaused = false;
			}
		}

		public void Quit() => Application.Quit();

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}
		
		private void OnTriggerEnter(Collider other)
		{
			// collects an object when collided with.
			ICollectable collectable = other.GetComponent<ICollectable>();
			if (collectable != null)
				collectable.Collect();

			// enemy detection
			if (other.gameObject.CompareTag("Enemy"))
				health -= 10f;
		}
	}
}
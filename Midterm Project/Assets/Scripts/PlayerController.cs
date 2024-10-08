using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMover : MonoBehaviour
{
	//public CharacterController controller;
	Rigidbody rb;
	//Animator anim;

	public Transform cam;

	public float speed = 6f;
	public float turnSmoothTime = 0.1f;
	private float turnSmoothVelocity;

	void Start()
	{
		//anim = gameObject.GetComponentInChildren<Animator>();
		rb = gameObject.GetComponent<Rigidbody>();
	}

	void Update()
	{
		float horiz = Input.GetAxisRaw("Horizontal");
		float vert = Input.GetAxisRaw("Vertical");
		Vector3 direct = new Vector3(horiz, 0f, vert).normalized;

		if (direct.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			//controller.Move(moveDir.normalized * speed * Time.deltaTime);
			rb.MovePosition(transform.position + moveDir * speed * Time.deltaTime);
		}
	}
}
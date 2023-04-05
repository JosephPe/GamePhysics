using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2d : MonoBehaviour
{
	[SerializeField] float speed;
	[SerializeField] float turnRate;
	[SerializeField] float jumpHeight;
	[SerializeField, Range(1, 5)] float fallRateMultiplier;
	[SerializeField] float doubleJump;
	[SerializeField] float hitForce;
	[Header("Ground")]
	[SerializeField] Transform groundTransform;
	[SerializeField] LayerMask groundLayerMask;

	Rigidbody2D rb;
	Vector2 velocity = Vector2.zero;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		bool onGround = Physics2D.OverlapCircle(groundTransform.position, 0.2f, groundLayerMask) != null;
		// get direction input
		Vector2 direction = Vector2.zero;
		direction.x = Input.GetAxis("Horizontal");


			velocity.x = direction.x * speed;
		// set velocity
		if (onGround)
		{

			if (velocity.y < 0) velocity.y = 0;
			if (Input.GetButtonDown("Jump"))
			{
				velocity.y += Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
				StartCoroutine(DoubleJump(1));
			}
		}
		float gravityMultiplier = 1;
		if (!onGround && velocity.y < 0) gravityMultiplier = fallRateMultiplier;
		if (!onGround && velocity.y > 0 && !Input.GetButton("Jump")) gravityMultiplier = fallRateMultiplier;
		velocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;

		rb.velocity = velocity;
	}

	IEnumerator DoubleJump(float timer)
	{
		// wait a little after the jump to allow a double jump
		yield return new WaitForSeconds(0.01f);
		// allow a double jump while moving up
		while (velocity.y > 0)
		{
			// if "jump" pressed add jump velocity
			if (Input.GetButtonDown("Jump"))
			{
				velocity.y += Mathf.Sqrt(doubleJump * -2 * Physics.gravity.y);

				break;
			}
			yield return null;
		}
	}
}

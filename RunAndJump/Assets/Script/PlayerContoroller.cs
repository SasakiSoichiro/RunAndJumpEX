using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class PlayerContoroller : MonoBehaviour
{

	public float speed = 10.0f;
	public float airVelocity = 8.0f;
	public float gravity = 40.0f;
	public float maxVelocityChange = 10.0f;
	public float jumpHeight = 4.0f;
	public float maxFallSpeed = 20.0f;
	public float rotateSpeed = 25f; //Speed the player rotate
	private Vector3 moveDir;
	public GameObject cam;
	private Rigidbody rb;
	public float MaxSpeed = 5.0f;
	public float addSpeed = 0.33f;
	public float deadSpeed = 0.25f;
	private float countSpeed = 0.0f;
	public bool isGrounded = true;

	private float distToGround;

	private bool canMove = true; //If player is not hitted
	public bool isStuned = false;
	private bool wasStuned = false; //If player was stunned before get stunned another time
	private float pushForce;
	private Vector3 pushDir;

	public Vector3 checkPoint;
	private bool slide = false;

	void Start()
	{
		// get the distance to ground
		distToGround = GetComponent<Collider>().bounds.extents.y;
	}

	bool IsGrounded()//地面にいるかどうか＆当たり判定
	{
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		rb.useGravity = false;

		checkPoint = transform.position;
		Cursor.visible = false;
	}

	void FixedUpdate()//よくわからん更新処理
	{
		if (canMove)
		{
			if (moveDir.x != 0 || moveDir.z != 0)//移動量が入ってたら
			{
				Vector3 targetDir = moveDir; //Direction of the character

				targetDir.y = 0;
				if (targetDir == Vector3.zero)
					targetDir = transform.forward;
				Quaternion tr = Quaternion.LookRotation(targetDir); //Rotation of the character to where it moves
				Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * rotateSpeed); //Rotate the character little by little
				transform.rotation = targetRotation;
			}

			if (IsGrounded())
			{
				// Calculate how fast we should be moving
				Vector3 targetVelocity = moveDir;
				targetVelocity *= speed;

				// Apply a force that attempts to reach our target velocity
				Vector3 velocity = rb.velocity;
				if (targetVelocity.magnitude < velocity.magnitude) //If I'm slowing down the character
				{
					targetVelocity = velocity;
					rb.velocity /= 1.1f;
				}
				Vector3 velocityChange = (targetVelocity - velocity);
				velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
				velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
				velocityChange.y = 0;
				if (!slide)
				{
					if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
						rb.AddForce(velocityChange, ForceMode.VelocityChange);
				}
				else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
				{
					rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
					//Debug.Log(rb.velocity.magnitude);
				}

				// Jump
				if (IsGrounded() && Input.GetButton("Jump"))
				{
					rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				}
			}
			else
			{
				if (!slide)
				{
					Vector3 targetVelocity = new Vector3(moveDir.x * airVelocity, rb.velocity.y, moveDir.z * airVelocity);
					Vector3 velocity = rb.velocity;
					Vector3 velocityChange = (targetVelocity - velocity);
					velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
					velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
					rb.AddForce(velocityChange, ForceMode.VelocityChange);
					if (velocity.y < -maxFallSpeed)
						rb.velocity = new Vector3(velocity.x, -maxFallSpeed, velocity.z);
				}
				else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
				{
					rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
				}
			}
		}
		else
		{
			rb.velocity = pushDir * pushForce;
		}
		// We apply gravity manually for more tuning control
		rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));
	}

	private void Update()//更新処理
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			countSpeed = Mathf.Min(
				countSpeed + addSpeed, MaxSpeed);
		}
		else
		{
			countSpeed = Mathf.Max(
				countSpeed - deadSpeed * Time.deltaTime,
				0);
		}
		//float h = Input.GetAxis("Horizontal");
		//float v = 0.0f;//Input.GetAxis("Vertical");

		//Vector3 v2 = v * cam.transform.forward; //Vertical axis to which I want to move with respect to the camera
		//Vector3 h2 = h * cam.transform.right; //Horizontal axis to which I want to move with respect to the camera
		//moveDir = (v2 + h2).normalized; //Global position to which I want to move in magnitude 1
		float totalSpeed = speed + countSpeed;
		transform.Translate(Vector3.right * totalSpeed * Time.deltaTime);
		// スペースキーでジャンプ
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
			isGrounded = false;
		}
		//RaycastHit hit;
		//if (Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + 0.1f))
		//{
		//	if (hit.transform.tag == "Slide")
		//	{
		//		slide = true;
		//	}
		//	else
		//	{
		//		slide = false;
		//	}
		//}
	}

	float CalculateJumpVerticalSpeed()//ジャンプしてる時のスピード
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

	public void HitPlayer(Vector3 velocityF, float time)//プレイヤーの当たり判定
	{
		rb.velocity = velocityF;

		pushForce = velocityF.magnitude;
		pushDir = Vector3.Normalize(velocityF);
		StartCoroutine(Decrease(velocityF.magnitude, time));
	}

	private IEnumerator Decrease(float value, float duration)
	{
		if (isStuned)
			wasStuned = true;
		isStuned = true;
		canMove = false;

		float delta = 0;
		delta = value / duration;

		for (float t = 0; t < duration; t += Time.deltaTime)
		{
			yield return null;
			if (!slide) //Reduce the force if the ground isnt slide
			{
				pushForce = pushForce - Time.deltaTime * delta;
				pushForce = pushForce < 0 ? 0 : pushForce;
				//Debug.Log(pushForce);
			}
			rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0)); //Add gravity
		}

		if (wasStuned)
		{
			wasStuned = false;
		}
		else
		{
			isStuned = false;
			canMove = true;
		}
	}
}

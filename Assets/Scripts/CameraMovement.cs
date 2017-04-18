using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	public Sprite background;
	Camera camera;
	float moveZone = 30;
	float moveSpeed = 2;

	void Start()
	{
		camera = GetComponent<Camera>();

	}

	void Update()
	{
		float x = 0, y = 0;
		float speed = moveZone * Time.deltaTime;


		if(Input.mousePosition.x < moveZone)
		{
			x -=speed;
		}
		else if (Input.mousePosition.x > Screen.width - moveZone)
		{
			x += speed;
		}

		if(Input.mousePosition.y < moveZone)
		{
			y -=speed;
		}
		else if (Input.mousePosition.y > Screen.height - moveZone)
		{
			y += speed;
		}

		Vector3 targetPosition = new Vector3(x,y,0) + transform.position;
		
		targetPosition.x = Mathf.Clamp(targetPosition.x,
										background.bounds.min.x + camera.rect.width*2,
										background.bounds.max.x - camera.rect.width*2);
		
		targetPosition.y = Mathf.Clamp(targetPosition.y,
										background.bounds.min.y + camera.rect.height,
										background.bounds.max.y - camera.rect.height);

		transform.position = targetPosition;
	}
}

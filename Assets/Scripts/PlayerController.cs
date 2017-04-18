using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	Rect selectionRect;
	float thickness;
	Texture2D whitePixel;
	bool release;
	Camera camera;

	void Start()
	{
		camera = Camera.main;
		selectionRect = new Rect();
		thickness = 2f;
		whitePixel  = new Texture2D(1,1);
		whitePixel.SetPixel(0,0,Color.white);
		whitePixel.Apply();
	}

	void Update ()
	{
		SelectionBox();
	}

	void LateUpdate()
	{

	}

	void OnGUI()
	{
		//TODO: Make visual pleasing
		if(!release)
		{
			SelectionBoxVisual(WorldToScreenRectangle(selectionRect),
								new Color( 0.8f, 0.8f, 0.95f ));
		}
		
	}


	// Player tools
	private void SelectionBox ()
	{
		// Binds min positions to mouse on first frame LBM(0) is pressed
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 pixelPosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
																		  Screen.height - Input.mousePosition.y,
																		  0));

			selectionRect.xMin = pixelPosition.x;
			selectionRect.yMin = pixelPosition.y;
			release = false;
		}

		else if (Input.GetMouseButtonUp(0))
		{
			//TODO: lock selection
			release = true;
		}

		// Binds max position to mouse when LBM(0) is down
		if (Input.GetMouseButton(0))
		{
			Vector3 pixelPosition2 = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
																		  Screen.height - Input.mousePosition.y,
																		  0));
			selectionRect.xMax = pixelPosition2.x;
			selectionRect.yMax = pixelPosition2.y;
		}

		Debug.Log(selectionRect);
	}


	//GUI visuals
	private void SelectionBoxVisual(Rect rect, Color color)
	{

		GUI.color = color;

		// Top
    	GUI.DrawTexture( new Rect( rect.xMin, rect.yMin, rect.width, thickness ), whitePixel);
    	// Left
    	GUI.DrawTexture( new Rect( rect.xMin, rect.yMin, thickness, rect.height ), whitePixel);
    	// Right
    	GUI.DrawTexture( new Rect( rect.xMax - thickness, rect.yMin, thickness, rect.height ), whitePixel);
    	// Bottom
    	GUI.DrawTexture( new Rect( rect.xMin, rect.yMax - thickness, rect.width, thickness ), whitePixel);

    	// Filling
    	color.a = 0.25f;
    	GUI.color = color;
    	GUI.DrawTexture(rect, whitePixel);
	}

	private Rect WorldToScreenRectangle (Rect rect)
	{
		Rect tempRect = new Rect();

		// convert WorldCoordinates to ScreenCoordinates
		Vector3 screenPointMin = camera.WorldToScreenPoint(new Vector3(rect.xMin, rect.yMin, 0));
		Vector3 screenPointMax = camera.WorldToScreenPoint(new Vector3(rect.xMax, rect.yMax, 0));

		tempRect.xMin = screenPointMin.x;
		tempRect.xMax = screenPointMax.x;
		tempRect.yMin = screenPointMin.y;
		tempRect.yMax = screenPointMax.y;

		return tempRect;

	}

}

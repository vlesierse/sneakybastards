using UnityEngine;
using System.Collections;

public class MoveByMouseClick : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				var destination = new Vector3(hit.point.x, transform.position.y, hit.point.z);
				iTween.MoveTo(gameObject, destination, 2);
				// Calculate the direction of the target and rotation
				var lookDirection = (destination - transform.position).normalized;
				var lookRotation = Quaternion.LookRotation(lookDirection);
				iTween.RotateTo(gameObject, lookRotation.eulerAngles, 1);
			}
		}
	}

}

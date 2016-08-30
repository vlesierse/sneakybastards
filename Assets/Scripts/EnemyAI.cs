using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class EnemyAI : MonoBehaviour
{

	public Transform player;

	public float fieldOfViewAngle = 120;
	public float fieldOfViewDistance = 5;

	public bool followPlayer;

	private LineRenderer _lineRenderer;

	void Start()
	{
		_lineRenderer = GetComponent<LineRenderer>();
		_lineRenderer.SetVertexCount(5);

		iTween.RotateBy(gameObject, iTween.Hash(
			"delay", 1,
			"y", 1,
			"time", 5,
			"easetype", iTween.EaseType.linear,
			"looptype", iTween.LoopType.pingPong));
	}

	void Update()
	{
		if (!FollowPlayer())
		{
			var lookDirection = player.position - transform.position;
			if ((Vector3.Angle(lookDirection, transform.forward)) <= (fieldOfViewAngle / 2))
			{
				if (Physics.Raycast(transform.position, lookDirection, fieldOfViewDistance / 2))
				{
					followPlayer = true;
					iTween.Stop(gameObject);
				}
			}
		}
		UpdateFieldOfView();
	}

	bool FollowPlayer()
	{
		if (followPlayer)
		{
			transform.LookAt(player);
		}
		return followPlayer;
	}

	void UpdateFieldOfView()
	{
		var localLookHorizon = Vector3.forward * fieldOfViewDistance;
		// Render the gizmo
		_lineRenderer.SetPosition(0, Vector3.zero);
		_lineRenderer.SetPosition(1, Quaternion.AngleAxis(fieldOfViewAngle / 2, Vector3.up) * localLookHorizon);
		_lineRenderer.SetPosition(2, localLookHorizon);
		_lineRenderer.SetPosition(3, Quaternion.AngleAxis(-(fieldOfViewAngle / 2), Vector3.up) * localLookHorizon);
		_lineRenderer.SetPosition(4, Vector3.zero);
	}

}

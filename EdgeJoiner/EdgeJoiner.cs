using UnityEditor;
using UnityEngine;
using System;

public class EdgeJoiner : EditorWindow 
{
	[MenuItem("Window/Join Edges")]
	public static void ShowWindow() 
	{
		EditorWindow.GetWindow (typeof(EdgeJoiner));
	}

	GameObject[] GOs;
	
	EdgeCollider2D left, right;

	Vector2 leftPoint, rightPoint, w_leftPoint, w_rightPoint;

	Vector2[] vertsLeft = new Vector2[0];
	Vector2[] vertsRight = new Vector2[0];

	void OnGUI()
	{
		GUILayout.Label ("EdgeCollider2D point editor", EditorStyles.boldLabel);

		GOs = Selection.gameObjects;

		if (GOs.Length == 2)
		{
			AssignLeftRight();

			if (left == null || right == null)
			{
				showHelp ();
				return;
			}

			vertsLeft = left.points;
			vertsRight = right.points;

			GUILayout.Label ("Left Edge: " + left.name);
			GUILayout.Label ("Right Edge: " + right.name);

			if (GUILayout.Button ("Join"))
			{
				leftPoint = left.points [left.points.Length - 1];
				rightPoint = right.points [0];

				w_leftPoint = left.transform.TransformPoint (leftPoint);
				w_rightPoint = right.transform.TransformPoint (rightPoint);

				vertsLeft[left.points.Length - 1] = left.transform.InverseTransformPoint (w_rightPoint);
				vertsRight[0] = right.transform.InverseTransformPoint (w_rightPoint);

				left.points = vertsLeft;
				right.points = vertsRight;
			}
		}
		else
		{
			if (GOs.Length == 0)
			{
				showHelp ();
			}
		}
	}

	void AssignLeftRight ()
	{
		Vector2 endPointsGO0 = EndPoints(GOs[0].GetComponent<EdgeCollider2D>());
        Vector2 endPointsGO1 = EndPoints(GOs[1].GetComponent<EdgeCollider2D>());

		if (endPointsGO0.x < endPointsGO1.x)
		{			
			left = GOs[0].GetComponent<EdgeCollider2D>();
            right = GOs[1].GetComponent<EdgeCollider2D>();
		}
		else
		{
			left = GOs[1].GetComponent<EdgeCollider2D>();
            right = GOs[0].GetComponent<EdgeCollider2D>();
		}
	}

	Vector2 EndPoints (EdgeCollider2D edge)
	{
		float leftPoint = Mathf.Infinity;
		float rightPoint = Mathf.Infinity;

		for (int i = 0; i < edge.points.Length; i++)
		{
			Vector2 point = edge.transform.TransformPoint(edge.points[i]);

			if (point.x < leftPoint)
			{
				leftPoint = point.x;
			}

			if (point.x > rightPoint)
			{
				rightPoint = point.x;
			}
		}

		return new Vector3 (leftPoint, rightPoint);
	}

	void showHelp ()
	{
		GUILayout.Label ("<HELP: Select two objects that each contain a 2D Edge Collider>", EditorStyles.label);
	}
}

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

	GameObject[] gos;

	EdgeCollider2D left, right;

	Vector2 leftPoint, rightPoint, w_leftPoint, w_rightPoint;

	Vector2[] vertsLeft = new Vector2[0];
	Vector2[] vertsRight = new Vector2[0];

	void OnGUI()
	{
		GUILayout.Label ("EdgeCollider2D point editor", EditorStyles.boldLabel);

		gos = Selection.gameObjects;

		if (gos.Length == 2)
		{
			if (gos[0].transform.position.x < gos[1].transform.position.x)
			{
				left = gos[0].GetComponent<EdgeCollider2D>();
				right = gos[1].GetComponent<EdgeCollider2D>();
			}
			else
			{
				left = gos[1].GetComponent<EdgeCollider2D>();
				right = gos[0].GetComponent<EdgeCollider2D>();
			}

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
			if (gos.Length == 0)
			{
				showHelp ();
			}
		}
	}

	void showHelp ()
	{
		GUILayout.Label ("<HELP: Select two objects that each contain a 2D Edge Collider>", EditorStyles.label);
	}
}

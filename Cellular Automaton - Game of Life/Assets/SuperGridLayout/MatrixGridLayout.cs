using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MatrixGridLayout : GridLayoutGroup
{
	[SerializeField]
	public Vector2 CellRation = new Vector2(1, 1);

	[SerializeField]
	public int ColumnCount = 10;

	[SerializeField]
	public int RowCount = 10;

	public override void SetLayoutHorizontal()
	{
		UpdateCellSize();
		base.SetLayoutHorizontal();
	}

	public override void SetLayoutVertical()
	{
		UpdateCellSize();
		base.SetLayoutVertical();
	}

	private void UpdateCellSize()
	{
		if (CellRation.x < 0)
		{
			CellRation.x = 0;
		}
		if (CellRation.y < 0)
		{
			CellRation.y = 0;
		}

		float maxCellSizeX = (rectTransform.rect.size.x - spacing.x * (ColumnCount - 1)) / CellRation.x / ColumnCount;
		float maxCellSizeY = (rectTransform.rect.size.y - spacing.y * (RowCount - 1)) / CellRation.y / RowCount;
		this.constraint = Constraint.FixedColumnCount;
		this.constraintCount = ColumnCount;

		var bestSize = Mathf.Max(Mathf.Min(maxCellSizeX, maxCellSizeY), 0);

		var paddingHorizontal = rectTransform.rect.size.x - bestSize * ColumnCount * CellRation.x - spacing.x * (ColumnCount - 1f) ;
		var paddingVertical = rectTransform.rect.size.y - bestSize * RowCount * CellRation.y - spacing.y * (RowCount - 1f);

		padding.left = (int)(paddingHorizontal / 2f);
		padding.right = (int)(paddingHorizontal / 2f);

		padding.top = (int)(paddingVertical / 2f);
		padding.bottom = (int)(paddingVertical / 2f);

		this.cellSize = new Vector2(bestSize * CellRation.x, bestSize * CellRation.y);
	}
}

[CustomEditor(typeof(MatrixGridLayout), true)]
[CanEditMultipleObjects]
public class SquareLayoutGroupEditor : UnityEditor.Editor
{
	//SerializedProperty m_Padding;
	SerializedProperty m_Spacing;
	SerializedProperty m_StartCorner;
	SerializedProperty m_StartAxis;
	SerializedProperty m_ChildAlignment;
	SerializedProperty m_CellRation;
	SerializedProperty m_ColumnCount;
	SerializedProperty m_RowCount;

	protected virtual void OnEnable()
	{
		//m_Padding = serializedObject.FindProperty("m_Padding");
		m_Spacing = serializedObject.FindProperty("m_Spacing");
		m_StartCorner = serializedObject.FindProperty("m_StartCorner");
		m_StartAxis = serializedObject.FindProperty("m_StartAxis");
		m_ChildAlignment = serializedObject.FindProperty("m_ChildAlignment");
		m_CellRation = serializedObject.FindProperty("CellRation");
		m_ColumnCount = serializedObject.FindProperty("ColumnCount");
		m_RowCount = serializedObject.FindProperty("RowCount");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		//EditorGUILayout.PropertyField(m_Padding, true);
		EditorGUILayout.PropertyField(m_Spacing, true);
		EditorGUILayout.PropertyField(m_CellRation, true);
		EditorGUILayout.PropertyField(m_StartCorner, true);
		EditorGUILayout.PropertyField(m_StartAxis, true);
		EditorGUILayout.PropertyField(m_ChildAlignment, true);
		EditorGUILayout.PropertyField(m_ColumnCount, true);
		EditorGUILayout.PropertyField(m_RowCount, true);
		serializedObject.ApplyModifiedProperties();
	}
}
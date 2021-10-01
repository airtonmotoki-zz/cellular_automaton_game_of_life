using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : GridLayoutGroup
{
	[SerializeField]
	public int ColumnCount = 12;

	[SerializeField]
	public int RowCount = 20;

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
		float x = (rectTransform.rect.size.x - padding.horizontal - spacing.x * (ColumnCount - 1)) / ColumnCount;
		float y = (rectTransform.rect.size.y - padding.vertical - spacing.y * (RowCount - 1)) / RowCount;
		this.constraint = Constraint.FixedColumnCount;
		this.constraintCount = ColumnCount;
		this.cellSize = new Vector2(x, y);
	}
}


[CustomEditor(typeof(FlexibleGridLayout), true)]
[CanEditMultipleObjects]
public class FlexibleLayoutGroupEditor : UnityEditor.Editor
{

	SerializedProperty m_Padding;
	SerializedProperty m_Spacing;
	SerializedProperty m_StartCorner;
	SerializedProperty m_StartAxis;
	SerializedProperty m_ChildAlignment;
	SerializedProperty m_ColumnCount;
	SerializedProperty m_RowCount;

	protected virtual void OnEnable()
	{
		m_Padding = serializedObject.FindProperty("m_Padding");
		m_Spacing = serializedObject.FindProperty("m_Spacing");
		m_StartCorner = serializedObject.FindProperty("m_StartCorner");
		m_StartAxis = serializedObject.FindProperty("m_StartAxis");
		m_ChildAlignment = serializedObject.FindProperty("m_ChildAlignment");
		m_ColumnCount = serializedObject.FindProperty("ColumnCount");
		m_RowCount = serializedObject.FindProperty("RowCount");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		EditorGUILayout.PropertyField(m_Padding, true);
		EditorGUILayout.PropertyField(m_Spacing, true);
		EditorGUILayout.PropertyField(m_StartCorner, true);
		EditorGUILayout.PropertyField(m_StartAxis, true);
		EditorGUILayout.PropertyField(m_ChildAlignment, true);
		EditorGUILayout.PropertyField(m_ColumnCount, true);
		EditorGUILayout.PropertyField(m_RowCount, true);
		serializedObject.ApplyModifiedProperties();
	}
}

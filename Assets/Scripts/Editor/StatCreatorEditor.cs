using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StatCreator))]
public class StatCreatorEditor : Editor
{
	private SerializedProperty _maxHP;
	private SerializedProperty _maxMP;
	private SerializedProperty _strength;
	private SerializedProperty _defense;
	private SerializedProperty _magic;
	private SerializedProperty _resistance;
	private SerializedProperty _moveSpeed;

	private void OnEnable()
	{
		_maxHP = serializedObject.FindProperty("_maxHP");
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		//EditorGUILayout.PropertyField(_maxHP, new GUIContent("MaxHP"));

		if (GUILayout.Button("StatData 생성"))
			Debug.Log("StatData Created");
	}
}

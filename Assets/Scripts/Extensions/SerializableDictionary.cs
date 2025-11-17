using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class SerializableKeyValuePair<TKey, TValue> {
  public TKey key;
  public TValue value;
}

[Serializable]
public class SerializableDictionary<TKey, TValue> {
  public List<SerializableKeyValuePair<TKey, TValue>> entries = new();
}


[CustomPropertyDrawer(typeof(SerializableKeyValuePair<,>))]
public class SerializableKeyValuePairDrawer : PropertyDrawer {
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    EditorGUI.BeginProperty(position, label, property);

    float half = position.width / 2f;

    var keyProp = property.FindPropertyRelative("key");
    var valueProp = property.FindPropertyRelative("value");

    Rect keyRect = new Rect(position.x, position.y, half - 5, position.height);
    Rect valRect = new Rect(position.x + half, position.y, half - 5, position.height);

    EditorGUI.PropertyField(keyRect, keyProp, GUIContent.none);
    EditorGUI.PropertyField(valRect, valueProp, GUIContent.none);

    EditorGUI.EndProperty();
  }

  public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
    return EditorGUIUtility.singleLineHeight;
  }
}
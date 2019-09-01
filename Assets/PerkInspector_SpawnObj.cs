using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;


[CustomEditor(typeof(OnHit_Buff))]
public class Editor_ChangeVar : Editor
{

        OnHit_Buff comp;
        static bool showTileEditor = false;


        public void OnEnable()
        {
            comp = (OnHit_Buff)target;
        }
        
        public override void OnInspectorGUI()
        {


            EditorGUILayout.LabelField("General Informaion", EditorStyles.boldLabel);
            //MAP DEFAULT INFORMATION
            comp.perkName = EditorGUILayout.TextField("PerkName", comp.perkName);

            EditorGUILayout.LabelField("Description");
            comp.desc = EditorGUILayout.TextArea(comp.desc);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(GUILayout.Width(100f));
            EditorGUILayout.LabelField("PERK TYPE", GUILayout.Width(100f));
            comp.perkType = (PerkType)EditorGUILayout.EnumPopup(comp.perkType, GUILayout.Width(100f));
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical(GUILayout.Width(100f));
            EditorGUILayout.LabelField("TRIGGER TYPE", GUILayout.Width(100f));
            comp.triggerType = (TriggerType)EditorGUILayout.EnumPopup(comp.triggerType, GUILayout.Width(100f));
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            comp.aSprite = (Sprite)EditorGUILayout.ObjectField("Sprite",comp.aSprite, typeof(Sprite), false, GUILayout.Height(16f));
            comp.aSound = (AudioClip)EditorGUILayout.ObjectField("Audio Clip", comp.aSound, typeof(AudioClip), false);


            //VARIABLE CHANGERS
            EditorGUILayout.LabelField("Variable Changer", EditorStyles.boldLabel);

            comp.percentChance = EditorGUILayout.FloatField("Percent", comp.percentChance);

            comp.variableToChange = (VariableToChange)EditorGUILayout.EnumPopup("Variable To Change",comp.variableToChange);
            comp.variableName = EditorGUILayout.TextField("Variable Name", comp.variableName);
            comp.property = EditorGUILayout.Toggle("Property?",comp.property);

        // showTileEditor = EditorGUILayout.Foldout(showTileEditor, "Tile Editor");

        switch (comp.variableToChange)
            {
                case VariableToChange.String:
                comp.stringBoolAmount = EditorGUILayout.TextField("Amount", comp.stringBoolAmount);
                break;

                case VariableToChange.Bool:
                comp.stringBoolAmount = EditorGUILayout.TextField("Amount", comp.stringBoolAmount);
                break;

                case VariableToChange.Int:
                comp.amount = EditorGUILayout.IntField("Amount", (int)comp.amount);
                break;

                case VariableToChange.Float:
                comp.amount = EditorGUILayout.FloatField("Amount", comp.amount);
                break;


        }
            EditorUtility.SetDirty(comp);
        }

    }
#endif
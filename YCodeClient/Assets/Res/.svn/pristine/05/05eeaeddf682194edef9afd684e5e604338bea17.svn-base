using edit.pure.resource;
using pure.ui.container;
using pure.ui.core;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace inspectors.ui {
    internal class Drawer_ScrollView {
        private static bool _foldOut;

        internal void Draw(ScrollViewStyle scroll, UICore ui) {
            _foldOut = EditorGUILayout.Foldout(_foldOut, "ScrollView", true);
            if (_foldOut) {
                EditorGUI.indentLevel ++;
                EditorGUI.BeginChangeCheck();
                scroll.useHorizontal = EditorGUILayout.Toggle("Use Horizontal", scroll.useHorizontal);
                scroll.horizontalSpace = EditorGUILayout.DelayedFloatField("Horizontal Space", scroll.horizontalSpace);
                scroll.horizontalBarVisibility =
                    (ScrollRect.ScrollbarVisibility)
                        EditorGUILayout.EnumPopup("Horizontal Visibility", scroll.horizontalBarVisibility);
                scroll.horizontalDirecion = (Scrollbar.Direction)
                    EditorGUILayout.EnumPopup("Horizontal Direction", scroll.horizontalDirecion);
                scroll.horizontalPlacement =
                    (BaseScrollPane.HorizontalBarPlacement)
                        EditorGUILayout.EnumPopup("Horizontal Placement", scroll.horizontalPlacement);
                DrawSeperator();
                scroll.useVertical = EditorGUILayout.Toggle("Use Vertical", scroll.useVertical);
                scroll.verticalSpace = EditorGUILayout.DelayedFloatField("Vertical Space", scroll.verticalSpace);
                scroll.verticalBarVisibility =
                    (ScrollRect.ScrollbarVisibility)
                        EditorGUILayout.EnumPopup("Vertical Visibility", scroll.verticalBarVisibility);
                scroll.verticalDirection = (Scrollbar.Direction)
                    EditorGUILayout.EnumPopup("Vertical Direction", scroll.verticalDirection);
                scroll.verticalPlacement =
                    (BaseScrollPane.VerticalBarPlacement)
                        EditorGUILayout.EnumPopup("Horizontal Placement", scroll.verticalPlacement);
                DrawSeperator();
                scroll.movementType =
                    (ScrollRect.MovementType) EditorGUILayout.EnumPopup("Movement Type", scroll.movementType);
                scroll.elasticity = EditorGUILayout.DelayedFloatField("Elasticity", scroll.elasticity);
                scroll.inertia = EditorGUILayout.Toggle("Inertia", scroll.inertia);
                scroll.decelerationRate = EditorGUILayout.DelayedFloatField("Deceleration Rate", scroll.decelerationRate);
                scroll.sensitivity = EditorGUILayout.DelayedFloatField("Sensitivity", scroll.sensitivity);
                DrawSeperator();
                scroll.stepSize = EditorGUILayout.Vector2Field("Scroll Step Size", scroll.stepSize);
                scroll.stepSensitivity = EditorGUILayout.Vector2Field("Step Sensitivity", scroll.stepSensitivity);
                scroll.maxSteps = EditorGUILayout.IntSlider("Max Steps", scroll.maxSteps, 1, 1000);
                if (EditorGUI.EndChangeCheck()) {
                    EditorUtility.SetDirty(ui);
                }
                EditorGUI.indentLevel--;
            }
        }

        private void DrawSeperator() {
            GUILayout.Space(5);
            GUILayout.Box("", EditStyles.Border1, GUILayout.Height(1));
            GUILayout.Space(2);
        }
    }
}
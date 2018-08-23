using System.Text;
using edit.pure.resource;
using mono.ui.elements;
using pure.ui.uispace3d;
using UnityEditor;
using UnityEngine;

namespace inspectors.ui {
    [CustomEditor(typeof (PortraitCache))]
    internal class Insp_PortraitData : Editor {
        private static bool syncSceneView;


        private static PortraitData copyData;

        private PortraitCache _portrait;
        private PortraitData _data;


        internal void OnEnable() {
            _portrait = (PortraitCache) target;
            _data = _portrait.data;
            _portrait.guiAction -= DrawGameViewRect;
            _portrait.guiAction += DrawGameViewRect;
        }

        public override void OnInspectorGUI() {
            EditStyles.Init();
            Undo.RecordObject(_portrait, "portait data");

            _data.tag = EditorGUILayout.DelayedTextField("Tag", _data.tag);
            _data.size = EditorGUILayout.RectField("Size:", _data.size);

            GUILayout.Label("Viewport Size:" + _data.viewportSize);

            _portrait.editCamera =
                (Camera) EditorGUILayout.ObjectField("Reference Camera", _portrait.editCamera, typeof (Camera), true);

            _portrait.prefab =
                (GameObject) EditorGUILayout.ObjectField("Prefab", _portrait.prefab, typeof (GameObject), false);
            EditStyles.DrawHorizontalSeperator();

            if (_portrait.editCamera) {
                DrawEditCamera();
            } else {
                GUILayout.Label("Protrat Cache Info", EditStyles.boldLabel);
                GUILayout.Label(GetDataString());
                EditorGUILayout.HelpBox("No Edit Camera Assign", MessageType.Error);
            }
            if (!_portrait.prefab) {
                EditorGUILayout.HelpBox("Prefab Lost", MessageType.Error);
            }
        }

        private void DrawEditCamera() {
            GUILayout.Label("Ref Camera", EditStyles.boldLabel);
            Camera cam = _portrait.editCamera;
            cam.fieldOfView = EditorGUILayout.FloatField("Field Of View", cam.fieldOfView);
            cam.farClipPlane = EditorGUILayout.FloatField("Far clip Plane", cam.farClipPlane);
            cam.nearClipPlane = EditorGUILayout.FloatField("Near clip Plane", cam.nearClipPlane);
            cam.transform.position = EditorGUILayout.Vector3Field("Position", cam.transform.position);
            cam.transform.eulerAngles = EditorGUILayout.Vector3Field("Rotate", cam.transform.eulerAngles);
            _portrait.drawRect = EditorGUILayout.Toggle("Draw Rect", _portrait.drawRect);
            syncSceneView = EditorGUILayout.Toggle("Sync to Scene View", syncSceneView);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Copy")) {
                copyData = new PortraitData();
                copyData.CopyFrom(_data);
            }
            if (GUILayout.Button("Paste")) {
                if (copyData != null) {
                    _data.CopyFrom(copyData);
                    EditorUtility.SetDirty(_portrait);
                    SyncToSceneView();
                }
            }

            if (GUILayout.Button("Back To Setting")) {
                PortraitData.DataToCamera(cam, _data);
                SyncToSceneView();
            }
            if (GUILayout.Button("Snap")) {
                PortraitData.CameraToData(cam, _data); 
                EditorUtility.SetDirty(_portrait);
            }

            GUILayout.EndHorizontal();
        }

        private void SyncToSceneView() {
            if (SceneView.lastActiveSceneView) {
                SceneView sv = SceneView.lastActiveSceneView;
                sv.pivot = _data.postion;
                sv.rotation = _data.rotate;
                SceneView.lastActiveSceneView.Repaint();
            }
        }


        internal void OnSceneGUI() {
            SceneView sv = SceneView.currentDrawingSceneView ?? SceneView.lastActiveSceneView;
            if (syncSceneView && sv && _portrait.editCamera) {
                Camera cam = _portrait.editCamera;
                Camera svCam = sv.camera;
                cam.transform.rotation = svCam.transform.rotation;
                cam.transform.position = svCam.transform.position;
            }
        }


        private void DrawGameViewRect() {
            Camera cam = _portrait.editCamera;
            if (!cam) return;
            Rect svPosition = new Rect(0, 0, cam.pixelWidth, cam.pixelHeight);
            Handles.color = Color.red;
            float size = HandleUtility.GetHandleSize(Vector3.zero);
            Handles.SphereCap(0, Vector3.zero, Quaternion.identity, size*0.5f);
            Handles.BeginGUI();
            Vector2 zero = cam.WorldToScreenPoint(Vector3.zero);
            Rect view = svPosition;
            view.x = 0;
            view.y = 0;
            zero.y = view.yMax - zero.y;
            Rect r = _data.size;
            r.position += svPosition.center - svPosition.position;
            PScaleUtility.CheckInit();
            PGUIUtility.DrawLine(new Vector2(r.x, r.y), new Vector2(r.xMax, r.y), Color.yellow, null, 2);
            PGUIUtility.DrawLine(new Vector2(r.xMax, r.y), new Vector2(r.xMax, r.yMax), Color.yellow, null, 2);
            PGUIUtility.DrawLine(new Vector2(r.xMax, r.yMax), new Vector2(r.x, r.yMax), Color.yellow, null, 2);
            PGUIUtility.DrawLine(new Vector2(r.x, r.yMax), new Vector2(r.x, r.y), Color.yellow, null, 2);
            Handles.EndGUI();
        }

        internal void OnDisable() {
            if (_portrait) {
                _portrait.guiAction -= DrawGameViewRect;
            }
        }

        internal void OnDestroy() {
            if (_portrait) {
                _portrait.guiAction -= DrawGameViewRect;
            }
        }

        private string GetDataString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Fov: " + _data.fov);
            sb.AppendLine("Far: " + _data.far);
            sb.AppendLine("Near: " + _data.near);
            sb.AppendLine("Position" + _data.postion);
            sb.AppendLine("Rotate" + _data.rotate.eulerAngles);
            sb.AppendLine("Viewport" + _data.viewportSize);
            return sb.ToString();
        }
    }
}
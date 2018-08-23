using mono.terrain;
using plugins.terrain.undoOpt;
using UnityEditor;
using UnityEngine;

namespace plugins.terrain {
    public class TaPaint : TerrainAction {
        private bool _painting;

        public override bool Execute() {
            if (data != null && data.previewer != null && data.currentSelect != null && TerrainCenter.menuToolBar == 3) {
                Paint();
                return true;
            }
            return false;
        }

        private void Paint() {
            Event e = Event.current;
            UpdateProjector();

            RaycastHit hit = new RaycastHit();
            bool hitted = false;
            bool _isMouseDown = TerrainCenter.Activated &&
                                TerrainCenter.input.IsMouseDown(0) &&
                                !TerrainCenter.input.IsKeyDown(KeyCode.LeftAlt) &&
                                !TerrainCenter.input.IsKeyDown(KeyCode.LeftControl) &&
                                Tools.current != Tool.View;


            MeshCollider c = data.currentSelect.GetComponent<MeshCollider>();
            if (c && c.enabled == false) {
                c.enabled = true;
            }

            if (TerrainCenter.Activated) {
                HandleUtility.AddDefaultControl(0);
                UpdateBrushSize();
                UpdateTools();

                Ray terrain = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                if (Physics.Raycast(terrain, out hit, Mathf.Infinity, TerrainCenter.LAYER_MASK)) {
                    if (hit.collider.transform == data.currentSelect) {
                        DrawProjector(hit);
                        hitted = true;
                    }
                }
            }
            SetPainting(_isMouseDown && hitted);
            if (_painting) {
                DoPaint(hit);
            }
        }

        private void SetPainting(bool value) {
            if (_painting != value) {
                _painting = value;
                if (_painting) {
                    TerrainCenter.undo.Push(new TextureUndoOperation(data.maskTexture));
                } else {
                    data.SaveTexture();
                }
            }
        }


        private void DrawProjector(RaycastHit hit) {
            if (currentSelect.GetComponent<TerrainObject>().convertType != "UT") {
                data.previewer.transform.localEulerAngles = new Vector3(90,
                    data.currentSelect.localEulerAngles.y, 0);
            } else {
                data.previewer.transform.localEulerAngles = new Vector3(90,
                    data.currentSelect.localEulerAngles.y, 0);
            }

            data.previewer.transform.position = hit.point;
            if (data.paintPreview != PaintHandler.Classic &&
                data.paintPreview != PaintHandler.Hide_Preview &&
                data.paintPreview != PaintHandler.Follow_Normal_WireCircle) {
                Handles.color = new Color(1f, 1f, 0f, 0.05f);
                Handles.DrawSolidDisc(hit.point, hit.normal, data.previewer.orthographicSize*0.9f);
            } else if (data.paintPreview != PaintHandler.Classic &&
                       data.paintPreview != PaintHandler.Hide_Preview &&
                       data.paintPreview != PaintHandler.Follow_Normal_Circle) {
                Handles.color = new Color(1f, 1f, 0f, 1f);
                Handles.DrawSolidDisc(hit.point, hit.normal, data.previewer.orthographicSize*0.9f);
            }
        }


        private void DoPaint(RaycastHit hit) {
            Vector2 pixelUV = hit.textureCoord*data.maskTextureUV; // 0.14f; 


            int putx = Mathf.FloorToInt(pixelUV.x*data.maskTexture.width);
            int puty = Mathf.FloorToInt(pixelUV.y*data.maskTexture.height);
            int x = Mathf.Clamp(putx - data.brushSizeInPercenter/2, 0, data.maskTexture.width - 1);
            int y = Mathf.Clamp(puty - data.brushSizeInPercenter/2, 0, data.maskTexture.height - 1);

            int width = Mathf.Clamp(putx + data.brushSizeInPercenter/2, 0, data.maskTexture.width) - x;
            int height = Mathf.Clamp(puty + data.brushSizeInPercenter/2, 0, data.maskTexture.height) - y;

            Color[] terrainRay = data.maskTexture.GetPixels(x, y, width, height, 0);
            float bigStrong = 0;
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    int index = i*width + j;

                    int ceoffx = Mathf.Clamp(x + j - (putx - data.brushSizeInPercenter/2), 0,
                        data.brushSizeInPercenter - 1);

                    int ceoffy = Mathf.Clamp(y + i - (puty - data.brushSizeInPercenter/2), 0,
                        data.brushSizeInPercenter - 1);

                    float stronger = data.brushAlpha[ceoffy*data.brushSizeInPercenter + ceoffx]*
                                     data.brushStronger;
                    if (stronger > bigStrong) {
                        bigStrong = stronger;
                    }
                    Color t = new Color(0, 0, 0, 0);

                    if (data.selectPaintTexture < 3) {
                        t = Color.Lerp(terrainRay[index], data.targetColor, stronger);
                    } else {
                        t = Color.Lerp(terrainRay[index], data.targetColor, stronger);
                    }
                    float total = t.r + t.g + t.b + t.a;
                    t.r /= total;
                    t.g /= total;
                    t.b /= total;
                    t.a /= total;

                    terrainRay[index] = t;
                }
            }

            data.maskTexture.SetPixels(x, y, width, height, terrainRay, 0);
            data.maskTexture.Apply();
        }


        private void UpdateBrushSize() {
            Event e = Event.current;
            if (e.type == EventType.KeyDown) {
                switch (e.keyCode) {
                    case KeyCode.KeypadPlus:
                        data.brushSize += 1;
                        break;
                    case KeyCode.Equals:
                        data.brushSize += 1;
                        break;
                    case KeyCode.KeypadMinus:
                        data.brushSize -= 1;
                        break;
                    case KeyCode.Minus:
                        data.brushSize -= 1;
                        break;
                }
            }
        }

        private void UpdateTools() {
            Tools.current = TerrainCenter.input.IsKeyDown(KeyCode.Q) ? Tool.View : Tool.None;
        }

        private void UpdateProjector() {
            Projector p = data.previewer;
            if (p != null && TerrainCenter.Activated && p.enabled == false) {
                if (data.paintPreview != PaintHandler.Follow_Normal_Circle &&
                    data.paintPreview != PaintHandler.Follow_Normal_WireCircle &&
                    data.paintPreview != PaintHandler.Hide_Preview) {
                    data.previewer.enabled = true;
                }
            } else if (p != null && !TerrainCenter.Activated && p.enabled) {
                if (data.paintPreview != PaintHandler.Classic) {
                    data.previewer.enabled = false;
                }
            }
        }
    }
}
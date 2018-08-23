﻿using pure.ui.uispace3d;
using pure.utils.fileTools;
#if UNITY_EDITOR
using System.IO;
using System.Xml.Linq;
using edit.pure.treespace;
using edit.pure.treespace.core;
using pure.asset.manager.utils;
using pure.refactor.serialize;
using pure.treeComp;
using pure.treeComp.firefx.core;
using pure.utils.debug;
using registerWrap;
using UnityEngine;

#endif

namespace mono.fbx {
    public class PortraitAct_Skill : PortraitSkill_Dll {
#if UNITY_EDITOR
        public override void StartAction(GameObject model, Transform world) {
            TreeWrap.Init();
            FxFileUtils.GetFireSetting = GetFireSetting; 
            FileTools.Start();
            base.StartAction(model, world);
        }

        public static FireFxRoot GetFireSetting(string path) {
            if (File.Exists(path)) {
                TreeEditorFactory.Start();
                XDocument doc = XDocument.Load(path);
                XElement root = doc.Root;
                string canvasType = StringDataTools.XAttribute(root, "type");
                TreeCanvas canvas = TreeEditorFactory.GetCanvas(canvasType);
                canvas.Read(root);
                FireFxRoot r = new FireFxRoot();
                new TreeToRuntimeComp(canvas, TreeFactory.CreateInstance, r).Transfer();
                return r;
            }
            GlobalLogger.LogError(string.Format("{0} is not a valid path", path));
            return null;
        }
#endif
    }
}
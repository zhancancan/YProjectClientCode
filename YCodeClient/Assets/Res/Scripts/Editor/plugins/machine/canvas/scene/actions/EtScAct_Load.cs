﻿using edit.pure.machine.core;
using game.pure.entity.scene.machine;

namespace plugins.machine.canvas.scene.actions {
    [MacActon(false, "Load/Place Data", new[] {typeof (SceneMachineChart)})]
    internal class EtSceneAct_LoadPlace : Mac_Action {
        public override string actionType { get { return SceneMachineType.LOAD_PLACE; } }
        public override string renderContent { get { return "Load place Data"; } set { } }
    }

    [MacActon(false, "Load/Scene", new[] {typeof (SceneMachineChart)})]
    internal class EtSceneAct_Scene : Mac_Action {
        public override string actionType { get { return SceneMachineType.LOAD_SCENE; } }
        public override string renderContent { get { return "Load scene"; } set { } }
    }

    [MacActon(false, "Load/Parse Geometry", new[] {typeof (SceneMachineChart)})]
    internal class EtSceneAct_ParseGeometry : Mac_Action {
        public override string actionType { get { return SceneMachineType.PARSE_GEOMETRY; } }
        public override string renderContent { get { return "Parse geometry"; } set { } }
    }

    [MacActon(false, "Load/Build Place", new[] {typeof (SceneMachineChart)})]
    internal class EtSceneAct_BuildPlace : Mac_Action {
        public override string actionType { get { return SceneMachineType.BUILD_PLACE; } }
        public override string renderContent { get { return "Build Place"; } set { } }
    }

    [MacActon(false, "Complete", new[] {typeof (SceneMachineChart)})]
    internal class EtSceneAct_Complete : Mac_Action {
        public override string actionType { get { return SceneMachineType.LOAD_COMPLETE; } }
        public override string renderContent { get { return "Load Complete"; } set { } }
    }
}
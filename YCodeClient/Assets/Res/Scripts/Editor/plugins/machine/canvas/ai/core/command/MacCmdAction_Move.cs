﻿using edit.pure.inspector;
using edit.pure.machine.core;
using machine.ai;

namespace plugins.machine.canvas.ai.core.command {
    [MacActon(false, "Command/Move", new[] {typeof (AIMachineChart)})]
    public class MacCmdAction_Move : Mac_Action {
        [Inspector(InspectorType.Float, 0)]
        public float threshhold = 10;

        [Inspector(InspectorType.Float, 1)]
        public float duration = 0.6f;

        [Inspector(InspectorType.Float, 2)]
        public float forcastLen = 0.1f;

        [Inspector(InspectorType.Float, 3)]
        public float maxSpeedMulitple = 1.2f;

        public override string actionType { get { return CommandActionType.MOVE; } }

        public override string renderContent { get { return actionType; } set { } }
    }

    [MacActon(false, "Command/Stand", new[] {typeof (AIMachineChart)})]
    public class MacCmdAction_Stand : Mac_Action {
        [Inspector(InspectorType.Float, 0)]
        public float updateInterval = 10;

        [Inspector(InspectorType.Float, 0)]
        public float standModePercent = 0.3f;

        public override string actionType { get { return CommandActionType.STAND; } }

        public override string renderContent { get { return actionType; } set { } }
    }

    [MacActon(false, "Command/Fire Skill", new[] {typeof (AIMachineChart)})]
    public class MacCmdAction_FireSkill : Mac_Action {
        public override string actionType { get { return CommandActionType.FIRE_SKILL; } }

        public override string renderContent { get { return actionType; } set { } }
    }

    [MacActon(false, "Command/Dead", new[] {typeof (AIMachineChart)})]
    public class MacCmdAction_Dead : Mac_Action {
        public override string actionType { get { return CommandActionType.DEAD; } }

        public override string renderContent { get { return actionType; } set { } }
    }

    [MacActon(false, "Command/Float Text", new[] {typeof (AIMachineChart)})]
    public class MacCmdAction_FloatText : Mac_Action {
        public override string actionType { get { return CommandActionType.FLOAT_TEXT; } }

        public override string renderContent { get { return actionType; } set { } }
    }

    [MacActon(false, "Command/Add Buff", new[] {typeof (AIMachineChart)})]
    public class MacCmdAction_Buff : Mac_Action {
        public override string actionType { get { return CommandActionType.ADD_BUFF; } }

        public override string renderContent { get { return actionType; } set { } }
    }
}
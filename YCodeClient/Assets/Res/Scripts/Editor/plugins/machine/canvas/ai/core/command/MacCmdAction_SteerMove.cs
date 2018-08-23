﻿using edit.pure.inspector;
using edit.pure.machine.core;
using machine.ai;

namespace plugins.machine.canvas.ai.core.command {
    [MacActon(false, "Command/Steer Move", new[] {typeof (AIMachineChart)})]
    public class MacCmdAction_SteerMove : Mac_Action {
        [Inspector(InspectorType.Float, 0)]
        public float mass = 1;

        [Inspector(InspectorType.Float, 1)]
        public float maxSpeed = 1.2f;

        [Inspector(InspectorType.Float, 2)]
        public float maxForce = 2;

        [Inspector(InspectorType.Float, 3)]
        public float arriveThreshold = 0.1f;

        [Inspector(InspectorType.Float, 4)]
        public float duration = 0.6f;

        [Inspector(InspectorType.Float, 5)]
        public float lastElasity = 0.1f;

        public override string actionType { get { return CommandActionType.STEER_MOVE; } }

        public override string renderContent { get { return actionType; } set { } }
    }

    [MacActon(false, "Command/Elastic Move", new[] {typeof (AIMachineChart)})]
    public class MacCmdAction_Elastic : Mac_Action {
        [Inspector(InspectorType.Float, 0)]
        public float elastic = 0.1f;

        [Inspector(InspectorType.Float, 1)]
        public float maxSpeed = 1.2f;

        [Inspector(InspectorType.Float, 2)]
        public float duration = 0.6f;

        [Inspector(InspectorType.Float, 3)]
        public float updateThreshold = 5;

        public override string actionType { get { return CommandActionType.ELASIC_MOVE; } }

        public override string renderContent { get { return actionType; } set { } }
    }

    [MacActon(false, "Command/Quit Move", new[] {typeof (AIMachineChart)})]
    public class MacCmdAction_QuitMove : Mac_Action {
        [Inspector(InspectorType.Float, 0)]
        public float elastic = 0.3f;

        [Inspector(InspectorType.Float, 3)]
        public float threshold = 0.05f;

        public override string actionType { get { return CommandActionType.QUIT_MOVE; } }

        public override string renderContent { get { return actionType; } set { } }
    }
}
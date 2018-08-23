using System;
using edit.pure.diagram.core;
using edit.pure.resource;
using plugins.diagram.canvas.mission.node;
using UnityEngine;

namespace plugins.diagram.canvas.mission {
    internal class Connect_Mission : IConnectDeclaration {
        public string identifier {
            get { return GraphType.MISSIOIN; }
        }

        public Type type {
            get { return GetType(); }
        }

        public Color color {
            get { return PGUIUtility.ToColor("0xFFFFC95A"); }
        }

        public string inKnobTex {
            get { return "Textures/In_Knob_1.png"; }
        }

        public string outKnobTex {
            get { return "Textures/Out_Knob_1.png"; }
        }

        public bool Connectable(GraphOutput output, GraphInput input) {
            return input.body is GraphNode_Mission || input.body is GraphNode_MissionNodeBase;
        }
    }

    internal class Connect_Chapter : IConnectDeclaration {
        public string identifier {
            get { return GraphType.CHAPTER; }
        }

        public Type type {
            get { return GetType(); }
        }

        public Color color {
            get { return PGUIUtility.ToColor("0xFFFDF854"); }
        }

        public string inKnobTex {
            get { return "Textures/In_Knob_1.png"; }
        }

        public string outKnobTex {
            get { return "Textures/Out_Knob_1.png"; }
        }

        public bool Connectable(GraphOutput output, GraphInput input) {
            return input.body is GraphNode_Mission;
        }
    }

    internal class Connect_MissionNode : IConnectDeclaration {
        public string identifier {
            get { return GraphType.NODE; }
        }

        public Type type {
            get { return GetType(); }
        }

        public Color color {
            get { return PGUIUtility.ToColor("0xFF65DF5C"); }
        }

        public string inKnobTex {
            get { return "Textures/In_Knob_1.png"; }
        }

        public string outKnobTex {
            get { return "Textures/Out_Knob_1.png"; }
        }

        public bool Connectable(GraphOutput output, GraphInput input) {
            return output != null && input != null && output.connectTypeId == input.connectTypeId &&
                   output.body is GraphNode_Mission;
        }
    }
}
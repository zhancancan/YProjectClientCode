using edit.pure.resource;
using edit.pure.treespace.core;
using UnityEngine;

namespace plugins.tree.canvas.firefx.act {
    public abstract class FireFxCellActBase : FireFxCellBase {
        public override bool IsLeaf { get { return true; } }
        public override Texture2D iconStyle { get { return PGUIStyle.Icon_LightYellow; } }

        public override bool CanInsertTo(TreeCell obj) {
            return base.CanInsertTo(obj) && obj.IsSelfOrDescendOf<FireFxCellBase>();
        }
    }
}
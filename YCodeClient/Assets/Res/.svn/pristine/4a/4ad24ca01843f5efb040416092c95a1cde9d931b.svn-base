using edit.pure.resource;
using UnityEngine;

namespace plugins.tree.canvas.firefx.bullet {
    public abstract class BulletFxCellBase : FireFxCellBase {
        public override Texture2D iconStyle {
            get {
                float g = 0.2f;
                return PResourceManager.GetTintedTexture("Icons/Icon_LightBlue.png", new Color(g, g, g, 1), 0.2f);
            }
        }

        //public override bool CanInsertTo(TreeCell obj) {
        //    return base.CanInsertTo(obj) && obj.IsSelfOrDescendOf<FireFxCellBase>();
        //}

        public override bool IsLeaf { get { return false; } }
    }
}
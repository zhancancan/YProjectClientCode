using UnityEngine;

namespace plugins.terrain {
    public abstract class TerrainAction {
        public abstract bool Execute();


        protected TerrainEditData data {
            get { return TerrainCenter.editData; }
        }

        protected Transform currentSelect {
            get { return data != null ? data.currentSelect : null; }
        }
    }
}
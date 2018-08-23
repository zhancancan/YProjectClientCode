namespace plugins.terrain {
    public class TaModifier : TerrainAction {
        public override bool Execute() {
            if (data != null && data.previewer != null && data.currentSelect != null && TerrainCenter.menuToolBar == 4) {
                PickSelection();
                return true;
            }
            return false;
        }

        private void PickSelection() {
        }
    }
}
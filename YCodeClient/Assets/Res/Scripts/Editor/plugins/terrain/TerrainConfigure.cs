namespace plugins.terrain {
    public enum CreateType {
        Classic,
        Custom
    }

    public enum LodMode {
        Mass_Control,
        Independent_Control
    }

    public enum ViewDistance {
        Near,
        Middle,
        Far,
        Background
    }

    public enum BillBoardAxis {
        Y_Axis,
        All_Axis
    }

    public enum OcclusionMode {
        Max_View_Disance,
        Layer_Cull_Distance
    }


    public enum LodShaderStatus {
        New,
        Exist
    }

    public enum TerrianMaterialType {
        Classic,
        Substances
    }

    //public enum ShaderMode {
    //    GLES1,
    //    GLES2,
    //    GLES3,
    //    CustomShader

    //}

    public enum PaintHandler {
        Classic,
        Follow_Normal_Circle,
        Follow_Normal_WireCircle,
        Hide_Preview
    }
}
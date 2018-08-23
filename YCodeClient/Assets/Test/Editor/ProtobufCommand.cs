using edit.pure.trans;
using UnityEditor;

namespace Test.Editor.testunit {
    public static class ProtobufCommand {
        [MenuItem("Wrapper/Lua Script/Build TransRule Lua")]
        internal static void BuildLuaProtobuf() {
            TransRuleCenter.BuildTransRuleLua();
        }

        [MenuItem("Wrapper/Lua Script/Build Dll Lua")]
        internal static void BuildLuaDll() {
            TransRuleCenter.BuildLuaDll();
        }

        //[MenuItem("Wrapper/Protobuf/Build TransRule")]
        //internal static void BuildProtoFile() {
        //    TransRuleCenter.BuildTransRuleCShape();
        //}

        //[MenuItem("Wrapper/Protobuf/Build TypeData")]
        //internal static void BuildTypeFile() {
        //    TransRuleCenter.BuildTypeData();
        //}

        //[MenuItem("Wrapper/Protobuf/Create Wrap File", false, 2050)]
        //internal static void CreateWrapFile() {
        //    TransRuleCenter.BuildWrapper(true);
        //}

        //[MenuItem("Wrapper/Protobuf/Clear Wrap File", false, 2051)]
        //internal static void ClearWrapperFile() {
        //    TransRuleCenter.ClearWrapper();
        //}

        //[MenuItem("Wrapper/Protobuf/Build GameData Scripts", false, 3000)]
        //internal static void BuildGameData() {
        //    TransRuleCenter.BuildGameData();
        //}

        //[MenuItem("Wrapper/Protobuf/Test socket form", false, 4000)]
        //internal static void TestSocketForm() {
        //    TransRuleCenter.TestSocketForm();
        //}
    }
}
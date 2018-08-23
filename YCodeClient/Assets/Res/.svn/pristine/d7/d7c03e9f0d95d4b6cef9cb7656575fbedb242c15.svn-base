using System;
using System.Text;
using pure.refactor.property;

namespace plugins.wrapper.property {
    internal partial class PropertyWrapperBuilder {
        private StringBuilder _funcBuilder = new StringBuilder();

        private bool Create(Type type, out string code, out string funcName) {
            EditableProperty[] ps = EditableProperty.GetProperties(type);
            if (ps.Length == 0) {
                code = string.Empty;
                funcName = string.Empty;
                return false;
            }
            funcName = type.Name + "_PropertyWrap";
            AppendUsing(type);
            _funcBuilder.AppendFormat("\t\tprivate void {0}(){1}\r\n", funcName, "{");
            _funcBuilder.AppendFormat("\t\t\tType t = typeof({0});\r\n", type.Name);
            _funcBuilder.AppendFormat("\t\t\tEditableProperty[] list = new EditableProperty[{0}];\r\n", ps.Length);
            for (int i = 0; i < ps.Length; i++) {
                string propertyTypeName = WrapperTools.GetTypeName(ps[i].propertyType, _usingList);
                _funcBuilder.AppendFormat("\t\t\t_info.setter=(a,b)=>{0}(({2})a).{3} = ({4}) b;{1};\r\n", "{", "}",
                    type.Name, ps[i].propertyName, propertyTypeName);
                _funcBuilder.AppendFormat("\t\t\t_info.getter = a =>(({0}) a).{1};\r\n", type.Name, ps[i].propertyName);
                _funcBuilder.AppendFormat("\t\t\t_info.propertyType = typeof({0});\r\n", propertyTypeName);
                _funcBuilder.AppendFormat("\t\t\t_info.copier=(a,b)=>{0}(({1})b).{2}=(({1})a).{2};{3};\r\n", "{",
                    type.Name, ps[i].propertyName, "}");
                _funcBuilder.AppendFormat("\t\t\tlist[{0}] = new EditableProperty(\"{1}\",_info);\r\n", i,
                    ps[i].propertyName);
                _funcBuilder.Append("\t\t\tEditableProperty.AddProperties(t,list);\r\n");
            }
            _funcBuilder.Append("\t\t}\r\n");
            code = _funcBuilder.ToString();
            return true;
        }
    }
}
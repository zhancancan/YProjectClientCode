using edit.pure.inspector;
using edit.pure.machine.core;
using game.entity.game.machine;

namespace plugins.machine.canvas.login.condition {
    [MacCondition(false, "Is lower critical", new[] {typeof (LoginMachineChart)})]
    public class LoginCon_IsBuddleVerUpdate : Mac_Condition {
        [Inspector(InspectorType.Boolean, 0)]
        public bool compare;

        protected override void Draw() {
        }

        public override string renderContent { get { return "Is lower critical"; } set { } }

        public override string conditionType { get { return LoginCenter.IS_LOWER_CRITICAL_VER; } }
    }
}
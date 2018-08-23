using edit.pure.inspector;
using edit.pure.machine.core;
using game.entity.game.machine;

namespace plugins.machine.canvas.login.condition {
    [MacCondition(false, "Is New Player", new[] {typeof (LoginMachineChart)})]
    public class LoginCon_IsNewPlayer : Mac_Condition {
        [Inspector(InspectorType.Boolean, 0)]
        public bool compare;

        protected override void Draw() {
        }

        public override string renderContent { get { return "Is New Player"; } set { } }

        public override string conditionType { get { return LoginCenter.IS_NEW_PLAYER; } }
    }
}
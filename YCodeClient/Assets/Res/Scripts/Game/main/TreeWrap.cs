﻿//this source code was auto-generated, do not modify it
using pure.treeComp;
using pure.treeComp.panel;
using pure.treeComp.firefx.sequence;
using pure.treeComp.firefx.emitter;
using pure.treeComp.firefx.core;
using pure.treeComp.firefx.bullet;
using pure.treeComp.firefx.action;
namespace registerWrap {
	public class TreeWrap{

        private static bool register;
        public static void Init(){
            if(!register){  
                new TreeWrap().Register();
            }
            register = true;
        }

        private void Register(){  
            RegTree();
        }
		private void RegTree(){
			TreeFactory.Register<PanelSetting_ChildNormal>("panelChild_normal");
			TreeFactory.Register<PanelSetting_ChildManual>("panelChild_manual");
			TreeFactory.Register<PanelSetting_ChildSelect>("panelChild_select");
			TreeFactory.Register<PanelSetting_ChildStack>("panelChild_stack");
			TreeFactory.Register<PanelSetting_Scene>("panelScene");
			TreeFactory.Register<PanelSetting_AutoClose>("panelAutoClose");
			TreeFactory.Register<PanelSetting_Lua>("panelLua");
			TreeFactory.Register<PanelSetting_OpenToObject>("panelOpenToObject");
			TreeFactory.Register<PanelSetting_Cull>("panelCull");
			TreeFactory.Register<PanelSetting_Module>("panelModule");
			TreeFactory.Register<PanelSetting_Panel>("panelCell");
			TreeFactory.Register<PanelSetting_Root>("panelRoot");
			TreeFactory.Register<FireFx_Parallel>("pal");
			TreeFactory.Register<FireFx_Phase>("fxphase");
			TreeFactory.Register<FireFx_Sequence>("seq");
			TreeFactory.Register<BulletEmitter_Line>("emitline");
			TreeFactory.Register<BulletEmitter_Normal>("emitnormal");
			TreeFactory.Register<FireFxRoot>("firefxroot");
			TreeFactory.Register<BulletFx_BoneBoundler>("bonefx");
			TreeFactory.Register<BulletFx_Chain>("fxchain");
			TreeFactory.Register<BulletFx_Create>("create");
			TreeFactory.Register<BulletFx_Fall>("fall");
			TreeFactory.Register<BulletFx_Fly>("fly");
			TreeFactory.Register<BulletFx_Follow>("fxfollow");
			TreeFactory.Register<BulletFx_LookAt>("lookAt");
			TreeFactory.Register<BulletFx_Parabola>("parabola");
			TreeFactory.Register<BulletFx_Play>("play");
			TreeFactory.Register<BulletFx_Seek>("seek");
			TreeFactory.Register<BulletFx_Tween>("tween");
			TreeFactory.Register<BulletPos_Area>("posarea");
			TreeFactory.Register<BulletPos_DividSpace>("posdividspace");
			TreeFactory.Register<BulletPos_Line>("posline");
			TreeFactory.Register<BulletPos_Ring>("posring");
			TreeFactory.Register<BulletPos_Sector>("possector");
			TreeFactory.Register<FireFx_Animation>("anim");
			TreeFactory.Register<FireFx_ChangeWeapon>("fxchangeweapon");
			TreeFactory.Register<FireFx_Hit>("hit");
			TreeFactory.Register<FireFx_Move>("move");
			TreeFactory.Register<FireFx_Pull>("pull");
			TreeFactory.Register<FireFx_TimeScale>("fxtimescale");
		}
	}
}
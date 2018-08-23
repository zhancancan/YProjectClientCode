﻿//this source code was auto-generated, do not modify it
using pure.stateMachine.machine.core;
using pure.stateMachine.share;
using pure.ai.aimachine.host.condition;
using pure.ai.aimachine.host.actions;
using pure.ai.aimachine.core.condition;
using pure.ai.aimachine.core.action;
using game.entity.scene.machine.actions;
using game.entity.game.machine.condition;
using game.entity.game.machine.actions.login;
using game.entity.game.machine.actions.load;
using game.entity.game.machine.actions.lang;
using game.entity.game.machine.actions.initor;
using game.entity.game.machine.actions.initor.lua;
using game.entity.game.machine.actions.initor.data;
using game.entity.game.machine.actions.http;
using game.entity.game.machine.actions.display;
using game.entity.game.machine.actions.critical;
using game.entity.game.machine.actions.bundle.load;
using game.entity.game.machine.actions.bundle.copy;
using machine.ai;
namespace registerWrap {
	public class MachineWrap{

        private static bool register;
        public static void Init(){
            if(!register)new MachineWrap().Register();
            register = true;
        }

        private void Register(){
            MachineType.Init();
            RegAction();
            RegCondition();
        }
		internal void RegAction(){
			CpxMachineFactory.AddAction<HostAct_Dead>("hdead");
			CpxMachineFactory.AddAction<HostAct_Gather>("hgather");
			CpxMachineFactory.AddAction<HostAct_PathMove>("hpathmove");
			CpxMachineFactory.AddAction<HostAct_PushListener>("hpushlistener");
			CpxMachineFactory.AddAction<HostAct_PushMove>("hpushmove");
			CpxMachineFactory.AddAction<HostAct_Stand>("hstand");
			CpxMachineFactory.AddAction<AIAct_FightStatus>("fightmode");
			CpxMachineFactory.AddAction<AIAct_InitCameraFollow>("aiInitCamFollow");
			CpxMachineFactory.AddAction<AIAct_InitChildRenderer>("aiInitChildRenderer");
			CpxMachineFactory.AddAction<AIAct_InitLabel>("aiInitLabel");
			CpxMachineFactory.AddAction<AIAct_InitRenderer>("aiInitRenderer");
			CpxMachineFactory.AddAction<AIAct_Jump>("jump");
			CpxMachineFactory.AddAction<AIAct_ParameterSet>("paramSetter");
			CpxMachineFactory.AddAction<AIAct_PathFinder>("pathfind");
			CpxMachineFactory.AddAction<AIAct_PathSourcePickup>("pathsourcepick");
			CpxMachineFactory.AddAction<AIAct_SetAnimatorBool>("animBoolSetter");
			CpxMachineFactory.AddAction<SceneAct_BuildPlace>("scBuildPlace");
			CpxMachineFactory.AddAction<SceneAct_ClearData>("scClearData");
			CpxMachineFactory.AddAction<SceneAct_ClearEntity>("scClearEntity");
			CpxMachineFactory.AddAction<SceneAct_ClearFinal>("scFinalDispose");
			CpxMachineFactory.AddAction<SceneAct_ClearMemory>("scClearMemory");
			CpxMachineFactory.AddAction<SceneAct_DestroyPrev>("scDisposePrev");
			CpxMachineFactory.AddAction<SceneAct_LoadComplete>("scLoadComplete");
			CpxMachineFactory.AddAction<SceneAct_LoadPlace>("scLoadingPlace");
			CpxMachineFactory.AddAction<SceneAct_LoadScene>("scLoadingScene");
			CpxMachineFactory.AddAction<SceneAct_OpenPanel>("scOpenPanel");
			CpxMachineFactory.AddAction<SceneAct_ClosePanel>("scClosePanel");
			CpxMachineFactory.AddAction<SceneAct_ParseGeometry>("scParseGeometry");
			CpxMachineFactory.AddAction<SceneAct_TransitionHide>("scHideTransition");
			CpxMachineFactory.AddAction<SceneAct_TransitionShow>("scShowTransition");
			CpxMachineFactory.AddAction<LoginAct_SdkLogin>("actSdkLogin");
			CpxMachineFactory.AddAction<LoginAct_LoadVersion>("actLoadVer");
			CpxMachineFactory.AddAction<LoginAct_InitLang>("actInitLang");
			CpxMachineFactory.AddAction<LoginAct_InitDiagram>("actInitDiagram");
			CpxMachineFactory.AddAction<LoginAct_InitMachine>("actInitMachine");
			CpxMachineFactory.AddAction<LoginAct_InitMainUI>("actInitMainUI");
			CpxMachineFactory.AddAction<LoginAct_InitMediator>("actInitMediator");
			CpxMachineFactory.AddAction<LoginAct_InitScreen>("actInitScreen");
			CpxMachineFactory.AddAction<LoginAct_InitShader>("actInitShader");
			CpxMachineFactory.AddAction<LoginAct_InitStyle>("actInitStyle");
			CpxMachineFactory.AddAction<LoginAct_InitTree>("actInitTree");
			CpxMachineFactory.AddAction<LoginAct_ReadLocalAssetManager>("actLocalAssetManager");
			CpxMachineFactory.AddAction<LoginAct_InitLua>("actInitLua");
			CpxMachineFactory.AddAction<LoginAct_InitData>("actInitStaticData");
			CpxMachineFactory.AddAction<LoginAct_GetConfig>("actGetConfig");
			CpxMachineFactory.AddAction<LoginAct_EnterLogoHide>("actHideEnterLogo");
			CpxMachineFactory.AddAction<LoginAct_EnterLogoShow>("actShowEnterLogo");
			CpxMachineFactory.AddAction<LoginAct_PreloadHide>("hidePreload");
			CpxMachineFactory.AddAction<LoginAct_PreloadShow>("actShowPreload");
			CpxMachineFactory.AddAction<LoginAct_ForceReInstall>("actForceReinstall");
			CpxMachineFactory.AddAction<LoginAct_LoadBundleFiles>("actLoadBundle");
			CpxMachineFactory.AddAction<LoginAct_CopyBundle>("actCopyBundle");
			CpxMachineFactory.AddAction<AICommand_Buff>("cmdAct:addBuff");
			CpxMachineFactory.AddAction<AICommand_Dead>("cmdAct:dead");
			CpxMachineFactory.AddAction<AICommand_FireSkill>("cmdAct:fireSkill");
			CpxMachineFactory.AddAction<AICommand_FloatText>("cmdAct:floatText");
			CpxMachineFactory.AddAction<AICommand_Move>("cmdAct:move");
			CpxMachineFactory.AddAction<AICommand_MoveElastic>("cmdAct:elasticMove");
			CpxMachineFactory.AddAction<AICommand_QuitMove>("cmdAct:quitMove");
			CpxMachineFactory.AddAction<AICommand_Stand>("cmdAct:stand");
			CpxMachineFactory.AddAction<AICommand_SteerMove>("cmdAct:steerMove");
		}
		internal void RegCondition(){
			CpxMachineFactory.AddCondition<HostCon_OnEntityClick>("clickentity");
			CpxMachineFactory.AddCondition<HostCon_OnGroudClick>("clickground");
			CpxMachineFactory.AddCondition<AICon_HasJumpPoint>("hasjumppoint");
			CpxMachineFactory.AddCondition<AICon_RuntimeParameter>("runtimeparameter");
			CpxMachineFactory.AddCondition<LoginCon_IsLowCritical>("isLowerCritical");
			CpxMachineFactory.AddCondition<LoginCon_IsNewPlayer>("isNewPlayer");
			CpxMachineFactory.AddCondition<LoginCon_IsStreamHigh>("IsStreamVerHigh");
			CpxMachineFactory.AddCondition<LoginCon_IsVersionUpdate>("isVerUpdate");
		}
	}
}

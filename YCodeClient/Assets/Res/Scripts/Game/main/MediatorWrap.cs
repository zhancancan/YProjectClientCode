﻿//this source code was auto-generated, do not modify it
using pure.mvc.view;
using game.mvc.view.core;
namespace registerWrap {
	public class MediatorWrap{

        private static bool register;
        public static void Init(){
            if(!register)new MediatorWrap().Register();
            register = true;
        }
		private void Register(){
			MediatorFactory.Add<BaseMediator>(MediatorType.BaseMediator);
			MediatorFactory.Add<FreeMediator>(MediatorType.FreeMediator);
			MediatorFactory.Add<CoverMediator>(MediatorType.CoverMediator);
			MediatorFactory.Add<ChildMediator>(MediatorType.ChildMediator);
		}
	}
}

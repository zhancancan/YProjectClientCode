//this source code was auto-generated, do not modify it
using pure.utils.dlltools;
using mono.scene;
namespace registerWrap {
	public class MonoWrap{

        private static bool register;
        public static void Init(){
            if(!register){  
                new MonoWrap().Register();
            }
            register = true;
        }

        private void Register(){  
            RegMono();
        }
		private void RegMono(){
			MonoDllFactory.Register<CamSetting>("cameraSetting");
			MonoDllFactory.Register<CustomData>("customData");
			MonoDllFactory.Register<EventArea>("eventArea");
			MonoDllFactory.Register<LightmapBake>("lightmapBake");
			MonoDllFactory.Register<LocationData>("locationData");
			MonoDllFactory.Register<Navigator>("navigator");
			MonoDllFactory.Register<PathWayPoint>("pathwaypoint");
		}
	}
}

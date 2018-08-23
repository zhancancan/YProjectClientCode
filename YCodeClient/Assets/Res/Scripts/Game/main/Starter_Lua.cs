using LuaInterface;
using pure.lua;
using pure.utils.coroutine;
using pure.utils.thread;

namespace main {
    public class Starter_Lua : IStartRunner {
        bool IStartRunner.complete { get { return _c; } }
        private bool _c;
        private LuaState _luaState;

        void IStartRunner.Start() {
            GameLuaFileUtils.Start();
            LuaManager.Init();
            _luaState = LuaManager.luaState;
            OpenLibs();
            _luaState.LuaSetTop(0);
            LuaCoroutine.Register(_luaState, CoroutineManager.longLive.behavior);
            ThreadSystem.RunAysn(Bind);
        }

        private void Bind() {
            LuaBinder.Bind(_luaState);
            DelegateFactory.Init();
            _c = true;
        }

        private void OpenLibs() {
            _luaState.OpenLibs(LuaDLL.luaopen_pb);
            _luaState.OpenLibs(LuaDLL.luaopen_struct); 
            //_luaState.OpenLibs(LuaDLL.luaopen_lpeg);
//#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
//             _luaState.OpenLibs(LuaDLL.luaopen_bit);
//#endif
        }
    }
}
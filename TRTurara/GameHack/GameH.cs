using Microsoft.Xna.Framework;
using System;

namespace GameHack
{
    public class HInfo : Attribute
    {
        private string _developer;
        private string _hName;
        private string _version;
        /// <summary>
        /// 须添加此特性以便识别插件
        /// </summary>
        /// <param name="d">开发者</param>
        /// <param name="h">插件名</param>
        /// <param name="v">版本号</param>
        public HInfo(string d, string h, string v)
        {
            _developer = d;
            _hName = h;
            _version = v;
        }
        public string Developer
        {
            get
            {
                return _developer;
            }
        }
        public string HName
        {
            get
            {
                return _hName;
            }
        }
        public string Version
        {
            get
            {
                return _version;
            }
        }
    }
    /// <summary>
    /// 游戏脚本类 继承此类后须声明HInfo特性
    /// </summary>
    [HInfo("Turara", "GameBase", "0.0.2.2")]
    public class GameH
    {
        public virtual void Start()
        {

        }
        public virtual void Update()
        {

        }
        public virtual void OnGUI(GameTime Gametime)
        {

        }
        public virtual void Exit()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CubePDF {
    class Program {
        static void Main(string[] args) {
            UserSetting reset = new UserSetting();
            reset.Save();

            UserSetting setting = new UserSetting(true);
            setting.Save();
        }
    }
}

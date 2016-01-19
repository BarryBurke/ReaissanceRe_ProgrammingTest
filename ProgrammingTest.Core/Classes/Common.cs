using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProgrammingTest.Core.Classes {
    class Common {
        public static bool IsFileAvailable(string filePath){
            return File.Exists(filePath);
        }
    }
}

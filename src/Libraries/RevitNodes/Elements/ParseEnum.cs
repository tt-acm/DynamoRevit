using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;

namespace Revit.Elements
{
    /// <summary>
    /// Parse Revit Enum
    /// </summary>
    [IsVisibleInDynamoLibrary(false)]
    public class ParseEnum
    {
        /// <summary>
        /// Parse any Revit Enum by String
        /// </summary>
        /// <param name="value">enum string value</param>
        /// <param name="typeName">full type name</param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public static object ByString(string value, string typeName)
        {
            var revitAssembly = System.Reflection.Assembly.GetAssembly(typeof(Autodesk.Revit.DB.Document));
            Type type = revitAssembly.GetType(typeName);
            return Enum.Parse(type, value);
        }
    }
}

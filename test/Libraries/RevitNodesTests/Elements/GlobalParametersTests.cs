using System;
using System.Linq;

using Autodesk.DesignScript.Geometry;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;

using NUnit.Framework;

using Revit.Elements;
using Revit.GeometryConversion;
using Revit.GeometryReferences;

using RevitServices.Persistence;

using RevitTestServices;

using RTF.Framework;

using Element = Revit.Elements.Element;
using FamilyType = Revit.Elements.FamilyType;

namespace RevitNodesTests.Elements
{
    [TestFixture]
    class GlobalParametersTests : RevitNodeTestBase
    {
        [Test]
        [TestModel(@".\empty-2017.rvt")]
        public void SetGlobalParameterByName()
        {
            var gp = Revit.Elements.GlobalParameter.ByName("MyGlobal", ParameterType.Text);
            Assert.IsNotNull(gp);
            Assert.IsTrue(typeof(Revit.Elements.GlobalParameter) == gp.GetType());
            Assert.IsTrue("MyGlobal" == gp.Name);
            Assert.IsTrue("Text" == gp.ParameterType);
            
        }


    }
}

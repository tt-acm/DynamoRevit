using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DSRevitNodesUI;
using RVT = Autodesk.Revit.DB;
using RevitServices.Persistence;
using RevitServices.Transactions;

using Dynamo.Utilities;
using Dynamo.Models;
using Dynamo.Nodes;
using ProtoCore.AST.AssociativeAST;
using CoreNodeModels.Properties;
using Dynamo.Graph.Nodes;

namespace DSRevitNodesUI
{

    [NodeName("Global Parameters")]
    [NodeCategory("Revit.Elements")]
    [NodeDescription("Get global parameters from document")]
    [IsDesignScriptCompatible]
    public class GlobalParameters : CustomRevitElementDropDown
    {
        public GlobalParameters() : base("Global Parameters", typeof(Autodesk.Revit.DB.GlobalParameter)) { }
    }

}
using System;

using DynamoServices;

using Revit.GeometryConversion;
using Revit.GeometryReferences;
using RevitServices.Persistence;
using RevitServices.Transactions;

namespace Revit.Elements
{
    /// <summary>
    /// A Revit Grid Element
    /// </summary>
    [RegisterForTrace]
    public class GlobalParameter : Element
    {
        #region Internal properties

        /// <summary>
        /// Internal reference to Element
        /// </summary>
        internal Autodesk.Revit.DB.GlobalParameter InternalGlobalParameter
        {
            get; private set;
        }

        /// <summary>
        /// Reference to the Element
        /// </summary>
        public override Autodesk.Revit.DB.GlobalParameter InternalElement
        {
            get { return InternalGlobalParameter; }
        }

        #endregion

        #region Private constructors

        /// <summary>
        /// Private constructor for wrapping an existing Element
        /// </summary>
        /// <param name="grid"></param>
        private GlobalParameter(Autodesk.Revit.DB.GlobalParameter parameter)
        {
            SafeInit(() => InitGlobalParameter(parameter));
        }

        private GlobalParameter(string name, Autodesk.Revit.DB.ParameterType type)
        {
            SafeInit(() => InitGlobalParameter(name, type));
        }

        #endregion


        #region Helpers for private constructors

        /// <summary>
        /// Initialize a Grid element
        /// </summary>
        /// <param name="grid"></param>
        private void InitGlobalParameter(Autodesk.Revit.DB.GlobalParameter g)
        {
            InternalSetGlobalParameter(g);
        }

        /// <summary>
        /// Initialize a Grid element
        /// </summary>
        /// <param name="line"></param>
        private void InitGlobalParameter(string name, Autodesk.Revit.DB.ParameterType type)
        {
            // Changing the underlying curve requires destroying the Grid
            TransactionManager.Instance.EnsureInTransaction(Document);
            


            Autodesk.Revit.DB.GlobalParameter g = Autodesk.Revit.DB.GlobalParameter.Create(Document, name, type);
            InternalSetGlobalParameter(g);

            TransactionManager.Instance.TransactionTaskDone();

            ElementBinder.CleanupAndSetElementForTrace(Document, this.InternalElement);
        }


        #endregion

        #region Private mutators

        /// <summary>
        /// Set the internal Element, ElementId, and UniqueId
        /// </summary>
        /// <param name="grid"></param>
        private void InternalSetGlobalParameter(Autodesk.Revit.DB.GlobalParameter g)
        {
            this.InternalGlobalParameter = g;
            this.InternalElementId = g.Id;
            this.InternalUniqueId = g.UniqueId;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Get Name
        /// </summary>
        public string Name
        {
            get
            {
                return this.InternalGlobalParameter.GetDefinition().Name;
            }
        }

        /// <summary>
        /// Get Parameter Group
        /// </summary>
        public string ParameterGroup
        {
            get
            {
                return this.InternalGlobalParameter.GetDefinition().ParameterGroup.ToString();
            }
        }

        /// <summary>
        /// Get Parameter Visibility
        /// </summary>
        public bool Visible
        {
            get
            {
                return this.InternalGlobalParameter.GetDefinition().Visible;
            }
        }

        /// <summary>
        /// Get Parameter Type
        /// </summary>
        public string ParameterType
        {
            get
            {
                return this.InternalGlobalParameter.GetDefinition().ParameterType.ToString();
            }
        }


        #endregion

        #region Public static constructors

        /// <summary>
        /// Create a Revit Grid Element in a Project along a Line.  
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static GlobalParameter ByName(string name, Autodesk.Revit.DB.ParameterType type)
        {
            if (!Autodesk.Revit.DB.GlobalParametersManager.AreGlobalParametersAllowed(Document))
            {
                throw new Exception("");
            }

            return new GlobalParameter(name, type);
        }

        #endregion

        #region Internal static constructor

        /// <summary>
        /// Wrap an existing Element in the associated DS type
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="isRevitOwned"></param>
        /// <returns></returns>
        internal static GlobalParameter FromExisting(Autodesk.Revit.DB.GlobalParameter parameter, bool isRevitOwned)
        {
            return new GlobalParameter(parameter)
            {
                IsRevitOwned = isRevitOwned
            };
        }

        #endregion

    }
}

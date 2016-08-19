﻿using System;

using Autodesk.Revit.DB;

using DynamoServices;
using Revit.GeometryConversion;
using RevitServices.Persistence;
using RevitServices.Transactions;
using Curve = Autodesk.Revit.DB.Curve;

namespace Revit.Elements
{
    /// <summary>
    /// FaceWall Element
    /// </summary>
    [RegisterForTrace]
    public class FaceWall : Element
    {
        #region Internal Properties

        /// <summary>
        /// Internal reference to the Revit Element
        /// </summary>
        internal Autodesk.Revit.DB.FaceWall InternalFaceWall
        {
            get; private set;
        }

        /// <summary>
        /// Reference to the Element
        /// </summary>
        public override Autodesk.Revit.DB.Element InternalElement
        {
            get { return InternalFaceWall; }
        }

        #endregion

        #region Private constructors

        /// <summary>
        /// Create from an existing Revit Element
        /// </summary>
        /// <param name="wall"></param>
        private FaceWall(Autodesk.Revit.DB.FaceWall wall)
        {
            SafeInit(() => InitFaceWall(wall));
        }

        /// <summary>
        /// Create a new instance of WallType, deleting the original
        /// </summary>
        /// <param name="location"></param>
        /// <param name="wallType"></param>
        /// <param name="reference"></param>
        private FaceWall(WallLocationLine location, Autodesk.Revit.DB.WallType wallType, Autodesk.Revit.DB.Reference reference)
        {
            SafeInit(() => InitFaceWall(location, wallType, reference));
        }

        #endregion

        #region Helpers for private constructors

        /// <summary>
        /// Initialize a Wall element
        /// </summary>
        /// <param name="wall"></param>
        private void InitFaceWall(Autodesk.Revit.DB.FaceWall wall)
        {
            InternalSetFaceWall(wall);
        }

        /// <summary>
        /// Initialize a Wall element
        /// </summary>
        /// <param name="location"></param>
        /// <param name="wallType"></param>
        /// <param name="reference"></param>
        private void InitFaceWall(WallLocationLine location, Autodesk.Revit.DB.WallType wallType, Autodesk.Revit.DB.Reference reference)
        {
            TransactionManager.Instance.EnsureInTransaction(Document);

            
            var wallElem = ElementBinder.GetElementFromTrace<Autodesk.Revit.DB.FaceWall>(Document);

            // there is no way of comparing the existing element to the new reference.
            // that's why the wall will be recreated.

            var wall = Autodesk.Revit.DB.FaceWall.Create(Document, wallType.Id, location, reference);

            InternalSetFaceWall(wall);
            
            TransactionManager.Instance.TransactionTaskDone();

            ElementBinder.CleanupAndSetElementForTrace(Document, InternalFaceWall);
        }

        #endregion

        #region Private mutators

        /// <summary>
        /// Set the internal Element, ElementId, and UniqueId
        /// </summary>
        /// <param name="wall"></param>
        private void InternalSetFaceWall(Autodesk.Revit.DB.FaceWall wall)
        {
            InternalFaceWall = wall;
            InternalElementId = wall.Id;
            InternalUniqueId = wall.UniqueId;
        }

        #endregion

        #region Public static constructors
        
        [Autodesk.DesignScript.Runtime.IsVisibleInDynamoLibrary(false)]
        public static FaceWall ByFace(WallLocationLine location, WallType wallType, Autodesk.DesignScript.Geometry.Surface surface)
        {
            object reference = surface.Tags.LookupTag("RevitFaceReference");
            if (reference != null && reference.GetType() == typeof(Reference))
            {
                try
                {
                    Reference refr = reference as Reference;
                    return new FaceWall(location, wallType.InternalWallType, refr);
                }
                catch (Exception) { throw new Exception(Properties.Resources.InvalidFace); }
            }
            return null;
        }

        #endregion

        #region Internal static constructors

        /// <summary>
        /// Create a Revit FaceWall from an existing reference
        /// </summary>
        /// <param name="wall"></param>
        /// <param name="isRevitOwned"></param>
        /// <returns></returns>
        internal static FaceWall FromExisting(Autodesk.Revit.DB.FaceWall wall, bool isRevitOwned)
        {
            return new FaceWall(wall)
            {
                IsRevitOwned = isRevitOwned
            };
        }

        #endregion
    }
}

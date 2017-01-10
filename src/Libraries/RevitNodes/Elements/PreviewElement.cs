using System;
using System.Reflection;
using Autodesk.DesignScript.Interfaces;
using Autodesk.Revit.DB;
using Revit.GeometryConversion;
using Revit.GeometryReferences;
using RevitServices.Persistence;
using Autodesk.DesignScript.Runtime;
using DS = Autodesk.DesignScript.Geometry;
using System.Windows.Media;
using System.Windows;

namespace Revit.Elements
{
    public class PreviewElement : IGraphicItem
    {
        private const byte DefR = 0;
        private const byte DefG = 0;
        private const byte DefB = 0;
        private const byte DefA = 255;

        private bool run = true;
        private int ms;

        public void Run(int ms, bool run)
        {
            this.ms = ms;
            this.run = run;
        }

        /// <summary>
        /// Text Tesselation
        /// </summary>
        /// <param name="package"></param>
        /// <param name="parameters"></param>
        [IsVisibleInDynamoLibrary(false)]
        public new void Tessellate(IRenderPackage package, TessellationParameters parameters)
        {

            // Get Conversion factor according to the documents units
            double factor = Revit.GeometryConversion.UnitConverter.DynamoToHostFactor(UnitType.UT_Length);

            while (run)
            {
                for (int i = 0; i < 100; i++)
                {
                    package.AddLineStripVertex(0, 0, 0);
                    package.AddLineStripVertex(i*10, i* 10,0);
                }

            }

        }

        /// <summary>
        /// Rotate point around 0,0
        /// </summary>
        /// <param name="point"></param>
        /// <param name="angle">Angle in degrees</param>
        /// <returns>Rotated point</returns>
        static System.Windows.Point RotatePoint(System.Windows.Point point, double angle)
        {
            // Rotate around 0,0
            System.Windows.Point centerPoint = new System.Windows.Point(0, 0);
            double radians = angle * (Math.PI / 180);
            double cosTheta = Math.Cos(radians);
            double sinTheta = Math.Sin(radians);
            return new System.Windows.Point
            {
                X = (double)(cosTheta * (point.X - centerPoint.X) - sinTheta * (point.Y - centerPoint.Y) + centerPoint.X),
                Y = (double)(sinTheta * (point.X - centerPoint.X) + cosTheta * (point.Y - centerPoint.Y) + centerPoint.Y)
            };
        }


       private static byte[] CreateColorByteArrayOfSize(int size, byte red, byte green, byte blue, byte alpha)
       {
           var arr = new byte[size * 4];
           for (var i = 0; i < arr.Length; i += 4)
           {
               arr[i] = red;
               arr[i + 1] = green;
               arr[i + 2] = blue;
               arr[i + 3] = alpha;
           }
           return arr;
       }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;

namespace ConsoleApp3
{
    public class Program
    {

       

        static void Main(string[] args)
        {
            Model MyModel = new Model();
            Program aaa = new Program();
            // Always remember to check that you really have working connection
            if (MyModel.GetConnectionStatus())
            {
                // Loop through X-axis  (these loops should be changed to match current grid)
                for (double PositionX = 0.0; PositionX <= 12000.0; PositionX += 3000.0)
                {
                    // In first and in last line
                    if (PositionX.Equals(0.0) || PositionX.Equals(12000.0))
                    {
                        // Loop through Y-axis to get pad footings on the longer sides of the grid
                        for (double PositionY = 0.0; PositionY <= 30000.0; PositionY += 6000.0)
                        {
                            

                           aaa.CreateFootingAndColumn(PositionX, PositionY);
                        }
                    }
                    else
                    {
                        aaa.CreateFootingAndColumn(PositionX, 0.0);
                        aaa.CreateFootingAndColumn(PositionX, 30000.0);
                    }
                }
                // Always remember to commit changes to Tekla Structures, otherwise some things might be left in uncertain state
                MyModel.CommitChanges();
            }

        }




        public void CreateFootingAndColumn(double PositionX, double PositionY)
        {
            const double FootingSize = 1500;
            CreatePadFooting(PositionX, PositionY, FootingSize);
            CreateColumn(PositionX, PositionY);
        }

      
        public static ModelObject CreatePadFooting(double PositionX, double PositionY, double FootingSize)
        {
            Beam PadFooting = new Beam();

            PadFooting.Name = "FOOTING";
            PadFooting.Profile.ProfileString = FootingSize + "*" + FootingSize; //"1500*1500";
            PadFooting.Material.MaterialString = "C50/60";
            PadFooting.Class = "8";
            PadFooting.StartPoint.X = PositionX;
            PadFooting.StartPoint.Y = PositionY;
            PadFooting.EndPoint.X = PositionX;
            PadFooting.EndPoint.Y = PositionY;
            PadFooting.EndPoint.Z = -500.0;
            PadFooting.Position.Rotation = Position.RotationEnum.FRONT;
            PadFooting.Position.Plane = Position.PlaneEnum.MIDDLE;
            PadFooting.Position.Depth = Position.DepthEnum.MIDDLE;

            if (!PadFooting.Insert())
            {
                Console.WriteLine("Insertion of pad footing failed.");
            }

            return PadFooting;
        }


        public static ModelObject CreateColumn(double PositionX, double PositionY)
        {
            Beam Column = new Beam();

            Column.Name = "COLUMN";
            Column.Profile.ProfileString = "380*380";
            Column.Material.MaterialString = "C50";
            Column.Class = "2";
            Column.StartPoint.X = PositionX;
            Column.StartPoint.Y = PositionY;
            Column.EndPoint.X = PositionX;
            Column.EndPoint.Y = PositionY;
            Column.EndPoint.Z = 5000.0;
            Column.Position.Rotation = Position.RotationEnum.FRONT;
            Column.Position.Plane = Position.PlaneEnum.MIDDLE;
            Column.Position.Depth = Position.DepthEnum.MIDDLE;

            if (!Column.Insert())
            {
                Console.WriteLine("Insertion of column failed.");
            }

            return Column;
        }


    }
}

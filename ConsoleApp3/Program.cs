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

       
            tekla_class aa = new tekla_class();
           

            aa.CreatePadFootings();
           
            
        }
    }
}

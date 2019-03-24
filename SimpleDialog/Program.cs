using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDialog
{
    class Program
    {
        static void Main(string[] args)
        {
            var dialog = new Dialog(@"..\..\..\..\QA.txt");
            dialog.DoDialog();
        }
    }
}

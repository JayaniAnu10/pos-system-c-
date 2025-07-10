<<<<<<< Updated upstream
﻿using System;
=======
﻿using pos_system.Managemant;
using pos_system.Report;
using System;
>>>>>>> Stashed changes
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pos_system
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new report());
        }
    }
}

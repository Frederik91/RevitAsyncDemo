using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitInteractors
{
    public class RevitInteractorBase
    {
        public static ExternalCommandData ExternalCommandData { get; set; }

        public void ReleaseApplication()
        {
            RevitInteractor.ReleaseApplication();
        }
    }
}

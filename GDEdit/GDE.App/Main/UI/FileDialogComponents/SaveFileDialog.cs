using System;
using System.Collections.Generic;
using System.Text;

namespace GDE.App.Main.UI.FileDialogComponents
{
    public class SaveFileDialog : FileDialog
    {
        protected override string FileDialogAction => "Save";

        public SaveFileDialog() : base() { }
        public SaveFileDialog(string defaultDirectory = null) : base(defaultDirectory) { }
    }
}

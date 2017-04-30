using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatanClient.UI
{
    static class MessageBoxHelper
    {
        public static void ShowErrorMessage(Exception ex)
        {
            MessageBox.Show( ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

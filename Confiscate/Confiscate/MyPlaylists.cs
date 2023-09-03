using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Confiscate
{
    public partial class MyPlaylists : UserControl
    {
        public string _accessToken;
        public MyPlaylists(string accessToken)
        {
            InitializeComponent();
            _accessToken = accessToken;
        }
    }
}

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
    public partial class MyMusicFiles : UserControl
    {
        public string _accessToken;
        public MyMusicFiles(string accessToken)
        {
            InitializeComponent();
            _accessToken = accessToken;
        }
    }
}

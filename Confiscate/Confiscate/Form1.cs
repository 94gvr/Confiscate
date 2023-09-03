using Confiscate;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Confiscate
{

    public partial class Confiscate : Form
    {
        private string _accessToken;

        private ImageInfo[] imageInfos = new ImageInfo[]
        {
            new ImageInfo("E:\\ПЛОХОЕ ХОББИ ДЛЯ ПЛОХИХ ЛЮДЕЙ\\Confiscate\\Images\\LikedSongs.jpg", 0),
            new ImageInfo("E:\\ПЛОХОЕ ХОББИ ДЛЯ ПЛОХИХ ЛЮДЕЙ\\Confiscate\\Images\\Search.png", 1),
            new ImageInfo("E:\\ПЛОХОЕ ХОББИ ДЛЯ ПЛОХИХ ЛЮДЕЙ\\Confiscate\\Images\\Playlist.png", 2),
            new ImageInfo("E:\\ПЛОХОЕ ХОББИ ДЛЯ ПЛОХИХ ЛЮДЕЙ\\Confiscate\\Images\\Music File.png", 3)
        };

        Dictionary<PictureBox, ImageInfo> iconPictureBoxes = new Dictionary<PictureBox, ImageInfo>();
        public Confiscate(string accessToken)
        {
            InitializeComponent();
            _accessToken = accessToken;
            InitializeInterface(imageInfos);
            savedTracks._accessToken = this._accessToken;
            ssearch._accessToken = this._accessToken;
            myPlaylists._accessToken = this._accessToken;
            myMusicFiles._accessToken = this._accessToken;
        }
        public void InitializeInterface(ImageInfo[] imageInfos)
        {

            int y = 20;
            int x = 10;
            int yStep = 172;

            foreach (var imageInfo in imageInfos)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Location = new Point(x, y),
                    Size = new Size(50, 50),
                    ImageLocation = imageInfo.ImagePath,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                pictureBox.Click += IconPictureBox_Click;

                Controls.Add(pictureBox);

                iconPictureBoxes.Add(pictureBox, imageInfo);

                y += yStep;
            }
        }

        private void IconPictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox != null && iconPictureBoxes.TryGetValue(pictureBox, out ImageInfo imageInfo))
            {
                int iconId = imageInfo.Id;

                switch (iconId)
                {
                    case 0:
                        savedTracks.Visible = true;
                        ssearch.Visible = false;
                        myPlaylists.Visible = false;
                        myMusicFiles.Visible = false;
                        break;
                    case 1:
                        savedTracks.Visible = false;
                        ssearch.Visible = true;
                        myPlaylists.Visible = false;
                        myMusicFiles.Visible = false;
                        break;
                    case 2:
                        savedTracks.Visible = false;
                        ssearch.Visible = false;
                        myPlaylists.Visible = true;
                        myMusicFiles.Visible = false;
                        break;
                    case 3:
                        savedTracks.Visible = false;
                        ssearch.Visible = false;
                        myPlaylists.Visible = false;
                        myMusicFiles.Visible = true;
                        break;
                }
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
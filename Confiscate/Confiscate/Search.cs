using Confiscate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Confiscate
{
    public partial class Search : UserControl
    {
        public string _accessToken;

        public bool isInitialized = false;

        private Dictionary<PictureBox, ArtistInfo> artistPictureBoxes;

        private List<Control> generatedControls = new List<Control>();

        private const int albumWidth = 150;
        private const int albumHeight = 150;
        private const int albumSpacing = 20;
        private const int labelMaxWidth = 400;
        private const int fontSize = 12;
        private const int albumsPerRow = 4;
        private const int singlesPerRow = 4;
        private const int controlHeight = 50;
        public Search(string accessToken)
        {
            InitializeComponent();
            _accessToken = accessToken;
        }

        public void ClearContent()
        {
            foreach (var control in generatedControls)
            {
                Controls.Remove(control);
                control.Dispose();
            }
            generatedControls.Clear();
        }
        private async void SearcherKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Dictionary<string, (string Name, string ImageUrl, string Id)> artistsInfo = Parse.ParseSearchArtistsInfo(Convert.ToString(await Get.SearchArtist(Searcher.Text, _accessToken)));
                string stringInfo = "";
                foreach (var artist in artistsInfo)
                {
                    stringInfo += $"ID: {artist.Key} ";
                    stringInfo += $"Name: {artist.Value.Name} ";
                    stringInfo += $"Image URL: {artist.Value.ImageUrl}\n";

                }
                if(isInitialized == false)
                {
                    InitializeArtists(artistsInfo);
                    isInitialized = true;
                } else
                {
                    UpdateArtists(artistsInfo);
                }
            }
        }
        private void InitializeArtists(Dictionary<string, (string Name, string ImageUrl, string Id)> artistInfo)
        {
            artistPictureBoxes = new Dictionary<PictureBox, ArtistInfo>();

            int y = 175;
            int x = 100;
            int xStep = 120;

            foreach (var artistEntry in artistInfo)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Location = new Point(x, y),
                    Size = new Size(100, 100),
                    ImageLocation = artistEntry.Value.ImageUrl,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                Label nameLabel = new Label
                {
                    Location = new Point(x, y + 100),
                    Size = new Size(100, 60),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = artistEntry.Value.Name,
                    Font = new Font("Comic Sans MS", 11)
                };

                pictureBox.Click += ArtistPictureBox_Click;

                Controls.Add(pictureBox);
                Controls.Add(nameLabel);

                generatedControls.Add(pictureBox);
                generatedControls.Add(nameLabel);


                artistPictureBoxes.Add(pictureBox, new ArtistInfo
                {
                    Name = artistEntry.Value.Name,
                    ImageUrl = artistEntry.Value.ImageUrl,
                    Id = artistEntry.Value.Id
                });

                x += xStep;
            }
        }

        private void UpdateArtists(Dictionary<string, (string Name, string ImageUrl, string Id)> artistInfo)
        {
            ClearContent();
            InitializeArtists(artistInfo);
        }
        private void ArtistPictureBox_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox && artistPictureBoxes.ContainsKey(pictureBox))
            {
                ArtistInfo musicianInfo = artistPictureBoxes[pictureBox];
                gotoArtistPage(musicianInfo.Id);
            }
        }
        private void gotoArtistPage(string id)
        {
            generatedControls.Add(Searcher);
            ClearContent();
            InitializeArtistPage(id);
        }

        private async void InitializeArtistPage(string ID)
        {


            // Initializing Avatar and Nickname


            (string name, string imageUrl) = Parse.ParseArtistInfo(Convert.ToString(await Get.GetArtist(ID, _accessToken)));

            PictureBox pictureBox = new PictureBox
            {
                Location = new Point(30, 30),
                Size = new Size(150, 150),
                ImageLocation = imageUrl,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            using (Graphics g = CreateGraphics())
            {
                SizeF textSize = g.MeasureString(name, new Font("Comic Sans MS", 36));

                Label nameLabel = new Label
                {
                    Location = new Point(180, 67),
                    Size = new Size((int)textSize.Width + 10, (int)textSize.Height),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Text = name,
                    Font = new Font("Comic Sans MS", 36),
                };

                Controls.Add(pictureBox);
                Controls.Add(nameLabel);

                generatedControls.Add(pictureBox);
                generatedControls.Add(nameLabel);
            }

            // Initializing Popular Tracks


            List<TrackInfo> trackInfoList = Parse.ParseArtistTopTracks(await Get.GetArtistTopTracks(ID, _accessToken));

            int yOffset = 180;
            string playImageUrl = "E:\\ПЛОХОЕ ХОББИ ДЛЯ ПЛОХИХ ЛЮДЕЙ\\Confiscate\\Images\\Play.png";

            Label titleLabel = new Label
            {
                Location = new Point(30, yOffset),
                Size = new Size(labelMaxWidth, 60),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Popular Tracks",
                Font = new Font("Comic Sans MS", 24),
            };
            Controls.Add(titleLabel);

            generatedControls.Add(titleLabel);

            yOffset += 70;

            int trackNumber = 1;

            foreach (var trackInfo in trackInfoList)
            {
                Label numberLabel = new Label
                {
                    Location = new Point(30, yOffset + (controlHeight - MeasureTextHeight(trackInfo.Name, new Font("Comic Sans MS", fontSize))) / 2),
                    Size = new Size(30, MeasureTextHeight(trackInfo.Name, new Font("Comic Sans MS", fontSize))),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Text = trackNumber.ToString(),
                    Font = new Font("Comic Sans MS", fontSize),
                    AutoSize = false,
                    AutoEllipsis = true
                };

                PictureBox spictureBox = new PictureBox
                {
                    Location = new Point(60, yOffset + (controlHeight - 50) / 2),
                    Size = new Size(50, 50),
                    ImageLocation = trackInfo.ImageUrl,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                Label nameLabel = new Label
                {
                    Location = new Point(120, yOffset + (controlHeight - MeasureTextHeight(trackInfo.Name, new Font("Comic Sans MS", fontSize))) / 2),
                    Size = new Size(labelMaxWidth, MeasureTextHeight(trackInfo.Name, new Font("Comic Sans MS", fontSize))),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Text = trackInfo.Name,
                    Font = new Font("Comic Sans MS", fontSize),
                    AutoSize = false,
                    AutoEllipsis = true
                };

                Label durationLabel = new Label
                {
                    Location = new Point(610, yOffset + (controlHeight - MeasureTextHeight($"{(int)trackInfo.Duration.TotalMinutes}:{trackInfo.Duration.Seconds:D2}", new Font("Comic Sans MS", fontSize))) / 2),
                    Size = new Size(50, MeasureTextHeight($"{(int)trackInfo.Duration.TotalMinutes}:{trackInfo.Duration.Seconds:D2}", new Font("Comic Sans MS", fontSize))),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Text = $"{(int)trackInfo.Duration.TotalMinutes}:{trackInfo.Duration.Seconds:D2}",
                    Font = new Font("Comic Sans MS", fontSize)
                };

                PictureBox trackImagePictureBox = new PictureBox
                {
                    Location = new Point(660, yOffset + (controlHeight - 50) / 2),
                    Size = new Size(50, 50),
                    ImageLocation = playImageUrl,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                trackImagePictureBox.Click += (sender, e) => Play.PlayTrack(trackInfo.Id);

                Controls.Add(numberLabel);
                Controls.Add(spictureBox);
                Controls.Add(nameLabel);
                Controls.Add(durationLabel);
                Controls.Add(trackImagePictureBox);

                generatedControls.Add(numberLabel);
                generatedControls.Add(spictureBox);
                generatedControls.Add(nameLabel);
                generatedControls.Add(durationLabel);
                generatedControls.Add(trackImagePictureBox);

                yOffset += 70;
                trackNumber++;
            }


            // Initializing Albums


            List<AlbumInfo> albumInfoList = Parse.ParseArtistAlbums(await Get.GetArtistAlbums(ID, "8", _accessToken));
            Control lastAddedControl = Controls[Controls.Count - 1];
            yOffset = lastAddedControl.Bottom;
            //MessageBox.Show(yOffset.ToString(), "anotherYOffset");
            int albumCount = 0;

            Label albumLabel = new Label
            {
                Location = new Point(30, yOffset),
                Size = new Size(labelMaxWidth, 60),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Albums",
                Font = new Font("Comic Sans MS", 24),
            };
            Controls.Add(albumLabel);

            generatedControls.Add(albumLabel);

            Label viewAllAlbums = new Label
            {
                Location = new Point(560, yOffset),
                Size = new Size(140, 60),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "View all",
                Font = new Font("Comic Sans MS", 24),
            };
            Controls.Add(viewAllAlbums);

            generatedControls.Add(viewAllAlbums);

            viewAllAlbums.Click += (sender, e) => gotoAllAlbums(ID);

            yOffset += 70;

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < albumsPerRow; col++)
                {
                    if (albumCount >= albumInfoList.Count)
                        break;

                    int x = 30 + col * (albumWidth + albumSpacing);
                    int y = yOffset + (row * (albumHeight + albumSpacing + 50));

                    var albumInfo = albumInfoList[albumCount];

                    PictureBox albumPictureBox = new PictureBox
                    {
                        Location = new Point(x, y),
                        Size = new Size(albumWidth, albumHeight),
                        ImageLocation = albumInfo.ImageUrl,
                        SizeMode = PictureBoxSizeMode.Zoom,
                    };

                    albumPictureBox.Click += (sender, e) => gotoAlbumPage(albumInfo.Id);

                    Label albumNameLabel = new Label
                    {
                        Location = new Point(x, y + albumHeight),
                        Size = new Size(albumWidth, 50),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Text = albumInfo.Name,
                        Font = new Font("Comic Sans MS", fontSize),
                        AutoSize = false,
                        AutoEllipsis = true
                    };

                    Controls.Add(albumPictureBox);
                    Controls.Add(albumNameLabel);

                    generatedControls.Add(albumPictureBox);
                    generatedControls.Add(albumNameLabel);

                    albumCount++;
                }
            }

            // Initializing Singles


            List<SingleInfo> singleInfoList = Parse.ParseArtistSingles(await Get.GetArtistSingles(ID, "8", _accessToken));
            lastAddedControl = Controls[Controls.Count - 1];
            yOffset = lastAddedControl.Bottom;
            //MessageBox.Show(yOffset.ToString(), "anotherYOffset");
            int singleCount = 0;

            Label singleLabel = new Label
            {
                Location = new Point(30, yOffset),
                Size = new Size(labelMaxWidth, 60),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Singles",
                Font = new Font("Comic Sans MS", 24),
            };
            Controls.Add(singleLabel);

            generatedControls.Add(singleLabel);

            Label viewAllSingles = new Label
            {
                Location = new Point(560, yOffset),
                Size = new Size(140, 60),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "View all",
                Font = new Font("Comic Sans MS", 24),
            };
            Controls.Add(viewAllSingles);

            generatedControls.Add(viewAllSingles);

            viewAllSingles.Click += (sender, e) => gotoAllSingles(ID);

            yOffset += 70;

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < singlesPerRow; col++)
                {
                    if (singleCount >= singleInfoList.Count)
                        break;

                    int x = 30 + col * (albumWidth + albumSpacing);
                    int y = yOffset + (row * (albumHeight + albumSpacing + 50));

                    var singleInfo = singleInfoList[singleCount];

                    PictureBox singlePictureBox = new PictureBox
                    {
                        Location = new Point(x, y),
                        Size = new Size(albumWidth, albumHeight),
                        ImageLocation = singleInfo.ImageUrl,
                        SizeMode = PictureBoxSizeMode.Zoom,
                    };

                    singlePictureBox.Click += (sender, e) => Play.PlayTrack(singleInfo.Id);

                    Label singleNameLabel = new Label
                    {
                        Location = new Point(x, y + albumHeight),
                        Size = new Size(albumWidth, 50),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Text = singleInfo.Name,
                        Font = new Font("Comic Sans MS", fontSize),
                        AutoSize = false,
                        AutoEllipsis = true
                    };

                    Controls.Add(singlePictureBox);
                    Controls.Add(singleNameLabel);

                    generatedControls.Add(singlePictureBox);
                    generatedControls.Add(singleNameLabel);

                    singleCount++;
                }
            }


        }
        int MeasureTextHeight(string text, Font font)
        {
            using (Graphics g = CreateGraphics())
            {
                SizeF textSize = g.MeasureString(text, font);
                return (int)Math.Ceiling(textSize.Height);
            }
        }

        private void gotoAlbumPage(string id)
        {

        }
        private async void gotoAllAlbums(string ID)
        {
            ClearContent();
            List<AlbumInfo> albumInfoList = Parse.ParseArtistAlbums(await Get.GetArtistAlbums(ID, "0", _accessToken));
            int yOffset = 0;
            //MessageBox.Show(yOffset.ToString(), "anotherYOffset");
            int albumCount = 0;

            Label albumLabel = new Label
            {
                Location = new Point(30, yOffset),
                Size = new Size(labelMaxWidth, 60),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Albums",
                Font = new Font("Comic Sans MS", 24),
            };
            Controls.Add(albumLabel);

            yOffset += 70;

            for (int row = 0;; row++)
            {
                if (albumCount >= albumInfoList.Count)
                {
                    break;
                }
                for (int col = 0; col < albumsPerRow; col++)
                {
                    if (albumCount >= albumInfoList.Count)
                    {
                        break;
                    }

                    int x = 30 + col * (albumWidth + albumSpacing);
                    int y = yOffset + (row * (albumHeight + albumSpacing + 50));

                    var albumInfo = albumInfoList[albumCount];

                    PictureBox albumPictureBox = new PictureBox
                    {
                        Location = new Point(x, y),
                        Size = new Size(albumWidth, albumHeight),
                        ImageLocation = albumInfo.ImageUrl,
                        SizeMode = PictureBoxSizeMode.Zoom,
                    };

                    albumPictureBox.Click += (sender, e) => gotoAlbumPage(albumInfo.Id);

                    Label albumNameLabel = new Label
                    {
                        Location = new Point(x, y + albumHeight),
                        Size = new Size(albumWidth, 50),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Text = albumInfo.Name,
                        Font = new Font("Comic Sans MS", fontSize),
                        AutoSize = false,
                        AutoEllipsis = true
                    };

                    Controls.Add(albumPictureBox);
                    Controls.Add(albumNameLabel);

                    generatedControls.Add(albumPictureBox);
                    generatedControls.Add(albumNameLabel);

                    albumCount++;
                }
            }
        }
        private async void gotoAllSingles(string ID)
        {
            ClearContent();
            List<SingleInfo> singleInfoList = Parse.ParseArtistSingles(await Get.GetArtistSingles(ID, "0", _accessToken));
            int yOffset = 0;
            //MessageBox.Show(yOffset.ToString(), "anotherYOffset");
            int singleCount = 0;

            Label singleLabel = new Label
            {
                Location = new Point(30, yOffset),
                Size = new Size(labelMaxWidth, 60),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Singles",
                Font = new Font("Comic Sans MS", 24),
            };
            Controls.Add(singleLabel);

            yOffset += 70;

            for (int row = 0; ; row++)
            {
                if (singleCount >= singleInfoList.Count)
                {
                    break;
                }
                for (int col = 0; col < albumsPerRow; col++)
                {
                    if (singleCount >= singleInfoList.Count)
                    {
                        break;
                    }

                    int x = 30 + col * (albumWidth + albumSpacing);
                    int y = yOffset + (row * (albumHeight + albumSpacing + 50));

                    var singleInfo = singleInfoList[singleCount];

                    PictureBox singlePictureBox = new PictureBox
                    {
                        Location = new Point(x, y),
                        Size = new Size(albumWidth, albumHeight),
                        ImageLocation = singleInfo.ImageUrl,
                        SizeMode = PictureBoxSizeMode.Zoom,
                    };

                    singlePictureBox.Click += (sender, e) => Play.PlayTrack(singleInfo.Id);

                    Label singleNameLabel = new Label
                    {
                        Location = new Point(x, y + albumHeight),
                        Size = new Size(albumWidth, 50),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Text = singleInfo.Name,
                        Font = new Font("Comic Sans MS", fontSize),
                        AutoSize = false,
                        AutoEllipsis = true
                    };

                    Controls.Add(singlePictureBox);
                    Controls.Add(singleNameLabel);

                    generatedControls.Add(singlePictureBox);
                    generatedControls.Add(singleNameLabel);

                    singleCount++;
                }
            }
        }
    }
}
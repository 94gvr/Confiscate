namespace Confiscate
{
    partial class Confiscate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Confiscate));
            this.savedTracks = new SavedTracks("");
            this.ssearch = new Search("");
            this.myPlaylists = new MyPlaylists("");
            this.myMusicFiles = new MyMusicFiles("");
            this.SuspendLayout();
            // 
            // savedTracks
            // 
            this.savedTracks.Location = new System.Drawing.Point(70, 0);
            this.savedTracks.Name = "savedTracks";
            this.savedTracks.Size = new System.Drawing.Size(730, 600);
            this.savedTracks.TabIndex = 0;
            // 
            // ssearch
            // 
            this.ssearch.Location = new System.Drawing.Point(70, 0);
            this.ssearch.Name = "ssearch";
            this.ssearch.Size = new System.Drawing.Size(730, 600);
            this.ssearch.TabIndex = 0;
            // 
            // myPlaylists
            // 
            this.myPlaylists.Location = new System.Drawing.Point(70, 0);
            this.myPlaylists.Name = "myPlaylists";
            this.myPlaylists.Size = new System.Drawing.Size(730, 600);
            this.myPlaylists.TabIndex = 0;
            // 
            // myMusicFiles
            // 
            this.myMusicFiles.Location = new System.Drawing.Point(70, 0);
            this.myMusicFiles.Name = "myMusicFiles";
            this.myMusicFiles.Size = new System.Drawing.Size(730, 600);
            this.myMusicFiles.TabIndex = 0;
            // 
            // Confiscate
            // 
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.savedTracks);
            this.Controls.Add(this.ssearch);
            this.Controls.Add(this.myPlaylists);
            this.Controls.Add(this.myMusicFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Confiscate";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);

        }

        #endregion
        private SavedTracks savedTracks;
        private Search ssearch;
        private MyPlaylists myPlaylists;
        private MyMusicFiles myMusicFiles;
    }
}


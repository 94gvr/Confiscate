namespace Confiscate
{
    partial class Search
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Searcher = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Searcher
            // 
            this.Searcher.Location = new System.Drawing.Point(285, 30);
            this.Searcher.Name = "Searcher";
            this.Searcher.Size = new System.Drawing.Size(160, 20);
            this.Searcher.TabIndex = 0;
            this.Searcher.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearcherKeyPress);
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.Searcher);
            this.Name = "Search";
            this.Size = new System.Drawing.Size(730, 600);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Searcher;
    }
}

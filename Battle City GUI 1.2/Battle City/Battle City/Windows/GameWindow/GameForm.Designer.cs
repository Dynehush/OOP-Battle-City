namespace Battle_City
{
    partial class GameForm
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
            PictureBoxField = new PictureBox();
            GameInfo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)PictureBoxField).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GameInfo).BeginInit();
            SuspendLayout();
            // 
            // PictureBoxField
            // 
            PictureBoxField.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PictureBoxField.BackColor = Color.Black;
            PictureBoxField.Location = new Point(1, -2);
            PictureBoxField.Name = "PictureBoxField";
            PictureBoxField.Size = new Size(949, 598);
            PictureBoxField.TabIndex = 0;
            PictureBoxField.TabStop = false;
            // 
            // GameInfo
            // 
            GameInfo.Location = new Point(999, 12);
            GameInfo.Name = "GameInfo";
            GameInfo.Size = new Size(400, 150);
            GameInfo.TabIndex = 1;
            GameInfo.TabStop = false;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1588, 597);
            Controls.Add(GameInfo);
            Controls.Add(PictureBoxField);
            ForeColor = SystemColors.ActiveBorder;
            Name = "GameForm";
            Text = " Battle City";
            WindowState = FormWindowState.Maximized;
            Load += GameForm_Load;
            ((System.ComponentModel.ISupportInitialize)PictureBoxField).EndInit();
            ((System.ComponentModel.ISupportInitialize)GameInfo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public static PictureBox GameInfo;
        public static PictureBox PictureBoxField;
    }
}
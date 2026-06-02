namespace Battle_City.Windows
{
    partial class EscapeForm
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
            EscapeLabel = new Label();
            MenuButton = new Button();
            DenyButton = new Button();
            ConfirmButton = new Button();
            SuspendLayout();
            // 
            // EscapeLabel
            // 
            EscapeLabel.Dock = DockStyle.Top;
            EscapeLabel.Font = new Font("Palatino Linotype", 32F);
            EscapeLabel.Location = new Point(0, 0);
            EscapeLabel.Name = "EscapeLabel";
            EscapeLabel.Padding = new Padding(0, 20, 0, 0);
            EscapeLabel.Size = new Size(574, 95);
            EscapeLabel.TabIndex = 0;
            EscapeLabel.Text = "Do you really want to exit?";
            EscapeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MenuButton
            // 
            MenuButton.Anchor = AnchorStyles.Top;
            MenuButton.Font = new Font("Segoe UI", 18F);
            MenuButton.Location = new Point(118, 237);
            MenuButton.Name = "MenuButton";
            MenuButton.Size = new Size(300, 45);
            MenuButton.TabIndex = 4;
            MenuButton.Text = "Go to main menu";
            MenuButton.UseVisualStyleBackColor = true;
            MenuButton.Click += MenuButton_Click;
            // 
            // DenyButton
            // 
            DenyButton.Anchor = AnchorStyles.Top;
            DenyButton.Font = new Font("Segoe UI", 18F);
            DenyButton.Location = new Point(118, 175);
            DenyButton.Name = "DenyButton";
            DenyButton.Size = new Size(300, 45);
            DenyButton.TabIndex = 5;
            DenyButton.Text = "No";
            DenyButton.UseVisualStyleBackColor = true;
            DenyButton.Click += DenyButton_Click;
            // 
            // ConfirmButton
            // 
            ConfirmButton.Anchor = AnchorStyles.Top;
            ConfirmButton.Font = new Font("Segoe UI", 18F);
            ConfirmButton.Location = new Point(118, 112);
            ConfirmButton.Name = "ConfirmButton";
            ConfirmButton.Size = new Size(300, 45);
            ConfirmButton.TabIndex = 6;
            ConfirmButton.Text = "Yes";
            ConfirmButton.UseVisualStyleBackColor = true;
            ConfirmButton.Click += ConfirmButton_Click;
            // 
            // EscapeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(574, 332);
            Controls.Add(ConfirmButton);
            Controls.Add(DenyButton);
            Controls.Add(MenuButton);
            Controls.Add(EscapeLabel);
            Name = "EscapeForm";
            Text = "Exit";
            Load += EscapeForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label EscapeLabel;
        private Button MenuButton;
        private Button DenyButton;
        private Button ConfirmButton;
    }
}
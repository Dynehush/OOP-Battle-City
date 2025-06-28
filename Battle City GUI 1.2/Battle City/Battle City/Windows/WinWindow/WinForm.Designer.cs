namespace Battle_City
{
    partial class WinForm
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
            WinTitle = new Label();
            Start = new Button();
            Exit = new Button();
            MenuButton = new Button();
            SuspendLayout();
            // 
            // WinTitle
            // 
            WinTitle.Dock = DockStyle.Top;
            WinTitle.Font = new Font("Palatino Linotype", 36F);
            WinTitle.Location = new Point(0, 0);
            WinTitle.Name = "WinTitle";
            WinTitle.Padding = new Padding(0, 20, 0, 0);
            WinTitle.Size = new Size(784, 100);
            WinTitle.TabIndex = 0;
            WinTitle.Text = "You have won!";
            WinTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Start
            // 
            Start.Anchor = AnchorStyles.Top;
            Start.FlatStyle = FlatStyle.System;
            Start.Font = new Font("Segoe UI", 18F);
            Start.ForeColor = SystemColors.ActiveCaptionText;
            Start.Location = new Point(243, 130);
            Start.Name = "Start";
            Start.Size = new Size(300, 45);
            Start.TabIndex = 2;
            Start.Text = "Start a new game";
            Start.UseVisualStyleBackColor = true;
            Start.Click += Start_Click;
            // 
            // Exit
            // 
            Exit.Anchor = AnchorStyles.Top;
            Exit.Font = new Font("Segoe UI", 18F);
            Exit.ForeColor = SystemColors.ActiveCaptionText;
            Exit.Location = new Point(243, 254);
            Exit.Margin = new Padding(3, 3, 100, 3);
            Exit.Name = "Exit";
            Exit.Size = new Size(300, 45);
            Exit.TabIndex = 5;
            Exit.Text = "Exit";
            Exit.UseVisualStyleBackColor = true;
            Exit.Click += Exit_Click;
            // 
            // MenuButton
            // 
            MenuButton.Anchor = AnchorStyles.Top;
            MenuButton.Font = new Font("Segoe UI", 18F);
            MenuButton.ForeColor = SystemColors.ActiveCaptionText;
            MenuButton.Location = new Point(243, 194);
            MenuButton.Margin = new Padding(3, 3, 100, 3);
            MenuButton.Name = "MenuButton";
            MenuButton.Size = new Size(300, 45);
            MenuButton.TabIndex = 6;
            MenuButton.Text = "Go to main menu";
            MenuButton.UseVisualStyleBackColor = true;
            MenuButton.Click += MenuButton_Click;
            // 
            // WinForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(784, 511);
            Controls.Add(MenuButton);
            Controls.Add(Exit);
            Controls.Add(Start);
            Controls.Add(WinTitle);
            Name = "WinForm";
            Text = "Battle City";
            ResumeLayout(false);
        }

        #endregion

        private Label WinTitle;
        private Button Start;
        private Button Exit;
        private Button MenuButton;
    }
}
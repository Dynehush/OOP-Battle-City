
namespace Battle_City
{
    partial class MainMenu
    {
        private System.ComponentModel.IContainer components = null;
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MenuLabel = new Label();
            Start = new Button();
            Load = new Button();
            Instructions = new Button();
            Exit = new Button();
            SuspendLayout();
            // 
            // MenuLabel
            // 
            MenuLabel.Dock = DockStyle.Top;
            MenuLabel.Font = new Font("Palatino Linotype", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MenuLabel.Location = new Point(0, 0);
            MenuLabel.Name = "MenuLabel";
            MenuLabel.Padding = new Padding(0, 20, 0, 0);
            MenuLabel.Size = new Size(784, 100);
            MenuLabel.TabIndex = 0;
            MenuLabel.Text = "Welcome to The Battle City!";
            MenuLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Start
            // 
            Start.Anchor = AnchorStyles.Top;
            Start.FlatStyle = FlatStyle.System;
            Start.Font = new Font("Segoe UI", 18F);
            Start.ForeColor = SystemColors.ActiveCaptionText;
            Start.Location = new Point(215, 125);
            Start.Name = "Start";
            Start.Size = new Size(300, 45);
            Start.TabIndex = 1;
            Start.Text = "Start a new game";
            Start.UseVisualStyleBackColor = true;
            Start.Click += Start_Click;
            // 
            // Load
            // 
            Load.Anchor = AnchorStyles.Top;
            Load.Font = new Font("Segoe UI", 18F);
            Load.ForeColor = SystemColors.ActiveCaptionText;
            Load.Location = new Point(215, 189);
            Load.Margin = new Padding(3, 3, 100, 3);
            Load.Name = "Load";
            Load.Size = new Size(300, 45);
            Load.TabIndex = 2;
            Load.Text = "Load";
            Load.UseVisualStyleBackColor = true;
            Load.Click += Load_Click;
            // 
            // Instructions
            // 
            Instructions.Anchor = AnchorStyles.Top;
            Instructions.Font = new Font("Segoe UI", 18F);
            Instructions.ForeColor = SystemColors.ActiveCaptionText;
            Instructions.Location = new Point(215, 252);
            Instructions.Margin = new Padding(3, 3, 100, 3);
            Instructions.Name = "Instructions";
            Instructions.Size = new Size(300, 45);
            Instructions.TabIndex = 3;
            Instructions.Text = "Instructions";
            Instructions.UseVisualStyleBackColor = true;
            Instructions.Click += Instructions_Click;
            // 
            // Exit
            // 
            Exit.Anchor = AnchorStyles.Top;
            Exit.Font = new Font("Segoe UI", 18F);
            Exit.ForeColor = SystemColors.ActiveCaptionText;
            Exit.Location = new Point(215, 316);
            Exit.Margin = new Padding(3, 3, 100, 3);
            Exit.Name = "Exit";
            Exit.Size = new Size(300, 45);
            Exit.TabIndex = 4;
            Exit.Text = "Exit";
            Exit.UseVisualStyleBackColor = true;
            Exit.Click += Exit_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(784, 511);
            Controls.Add(Exit);
            Controls.Add(Instructions);
            Controls.Add(Load);
            Controls.Add(Start);
            Controls.Add(MenuLabel);
            Name = "MainMenu";
            Text = "Battle City";
            ResumeLayout(false);
        }

        #endregion

        private Label MenuLabel;
        private Button Start;
        private Button Load;
        private Button Instructions;
        private Button Exit;
    }
}

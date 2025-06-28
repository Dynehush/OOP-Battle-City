namespace Battle_City
{
    partial class DefeatForm
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
            Label = new Label();
            RestartButton = new Button();
            LoadButton = new Button();
            MenuButton = new Button();
            ExitButton = new Button();
            SuspendLayout();
            // 
            // Label
            // 
            Label.Dock = DockStyle.Top;
            Label.Font = new Font("Palatino Linotype", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label.Location = new Point(0, 0);
            Label.Name = "Label";
            Label.Size = new Size(784, 141);
            Label.TabIndex = 0;
            Label.Text = "Defeat!";
            Label.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // RestartButton
            // 
            RestartButton.Anchor = AnchorStyles.Top;
            RestartButton.Font = new Font("Segoe UI", 18F);
            RestartButton.Location = new Point(242, 144);
            RestartButton.Name = "RestartButton";
            RestartButton.Size = new Size(300, 45);
            RestartButton.TabIndex = 1;
            RestartButton.Text = "Restart";
            RestartButton.UseVisualStyleBackColor = true;
            RestartButton.Click += RestartButton_Click;
            // 
            // LoadButton
            // 
            LoadButton.Anchor = AnchorStyles.Top;
            LoadButton.Font = new Font("Segoe UI", 18F);
            LoadButton.Location = new Point(242, 206);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(300, 45);
            LoadButton.TabIndex = 2;
            LoadButton.Text = "Load";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click_1;
            // 
            // MenuButton
            // 
            MenuButton.Anchor = AnchorStyles.Top;
            MenuButton.Font = new Font("Segoe UI", 18F);
            MenuButton.Location = new Point(242, 266);
            MenuButton.Name = "MenuButton";
            MenuButton.Size = new Size(300, 45);
            MenuButton.TabIndex = 3;
            MenuButton.Text = "Go to main menu";
            MenuButton.UseVisualStyleBackColor = true;
            MenuButton.Click += MenuButton_Click;
            // 
            // ExitButton
            // 
            ExitButton.Anchor = AnchorStyles.Top;
            ExitButton.Font = new Font("Segoe UI", 18F);
            ExitButton.Location = new Point(242, 326);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(300, 45);
            ExitButton.TabIndex = 4;
            ExitButton.Text = "Exit";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // DefeatForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(784, 511);
            Controls.Add(ExitButton);
            Controls.Add(MenuButton);
            Controls.Add(LoadButton);
            Controls.Add(RestartButton);
            Controls.Add(Label);
            Name = "DefeatForm";
            Text = "Battle City";
            Load += DefeatForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label Label;
        private Button RestartButton;
        private Button LoadButton;
        private Button MenuButton;
        private Button ExitButton;
    }
}
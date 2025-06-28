namespace Battle_City
{
    partial class InstructionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstructionsForm));
            InstructionsLabel = new Label();
            InstructionsText = new TextBox();
            SuspendLayout();
            // 
            // InstructionsLabel
            // 
            InstructionsLabel.Dock = DockStyle.Top;
            InstructionsLabel.Font = new Font("Palatino Linotype", 36F);
            InstructionsLabel.Location = new Point(0, 0);
            InstructionsLabel.Name = "InstructionsLabel";
            InstructionsLabel.Size = new Size(784, 100);
            InstructionsLabel.TabIndex = 0;
            InstructionsLabel.Text = "Instructions";
            InstructionsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // InstructionsText
            // 
            InstructionsText.BackColor = SystemColors.ActiveCaption;
            InstructionsText.BorderStyle = BorderStyle.None;
            InstructionsText.Dock = DockStyle.Fill;
            InstructionsText.Font = new Font("Palatino Linotype", 24F);
            InstructionsText.Location = new Point(0, 100);
            InstructionsText.Multiline = true;
            InstructionsText.Name = "InstructionsText";
            InstructionsText.Size = new Size(784, 411);
            InstructionsText.TabIndex = 1;
            InstructionsText.Text = resources.GetString("InstructionsText.Text");
            InstructionsText.TextAlign = HorizontalAlignment.Center;
            // 
            // InstructionsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(784, 511);
            Controls.Add(InstructionsText);
            Controls.Add(InstructionsLabel);
            Name = "InstructionsForm";
            Text = "Battle City";
            Load += InstructionsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label InstructionsLabel;
        private TextBox InstructionsText;
    }
}
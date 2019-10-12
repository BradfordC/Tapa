namespace Tapa
{
    partial class MainForm
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
            this.DrawingPanel = new System.Windows.Forms.Panel();
            this.ClueLabel = new System.Windows.Forms.Label();
            this.CluePanel = new System.Windows.Forms.Panel();
            this.SquareLabel = new System.Windows.Forms.Label();
            this.SquarePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // DrawingPanel
            // 
            this.DrawingPanel.BackColor = System.Drawing.Color.Silver;
            this.DrawingPanel.Location = new System.Drawing.Point(12, 59);
            this.DrawingPanel.Name = "DrawingPanel";
            this.DrawingPanel.Size = new System.Drawing.Size(544, 540);
            this.DrawingPanel.TabIndex = 0;
            this.DrawingPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingPanel_Paint);
            this.DrawingPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DrawingPanel_MouseClick);
            this.DrawingPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DrawingPanel_MouseClick);
            // 
            // ClueLabel
            // 
            this.ClueLabel.AutoSize = true;
            this.ClueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClueLabel.Location = new System.Drawing.Point(41, 18);
            this.ClueLabel.Name = "ClueLabel";
            this.ClueLabel.Size = new System.Drawing.Size(49, 20);
            this.ClueLabel.TabIndex = 1;
            this.ClueLabel.Text = "Clues";
            // 
            // CluePanel
            // 
            this.CluePanel.BackColor = System.Drawing.Color.Red;
            this.CluePanel.Location = new System.Drawing.Point(96, 18);
            this.CluePanel.Name = "CluePanel";
            this.CluePanel.Size = new System.Drawing.Size(20, 20);
            this.CluePanel.TabIndex = 2;
            // 
            // SquareLabel
            // 
            this.SquareLabel.AutoSize = true;
            this.SquareLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SquareLabel.Location = new System.Drawing.Point(155, 18);
            this.SquareLabel.Name = "SquareLabel";
            this.SquareLabel.Size = new System.Drawing.Size(69, 20);
            this.SquareLabel.TabIndex = 3;
            this.SquareLabel.Text = "Squares";
            // 
            // SquarePanel
            // 
            this.SquarePanel.BackColor = System.Drawing.Color.Green;
            this.SquarePanel.Location = new System.Drawing.Point(230, 18);
            this.SquarePanel.Name = "SquarePanel";
            this.SquarePanel.Size = new System.Drawing.Size(20, 20);
            this.SquarePanel.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 611);
            this.Controls.Add(this.SquarePanel);
            this.Controls.Add(this.SquareLabel);
            this.Controls.Add(this.CluePanel);
            this.Controls.Add(this.ClueLabel);
            this.Controls.Add(this.DrawingPanel);
            this.Name = "MainForm";
            this.Text = "Tapa";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DrawingPanel;
        private System.Windows.Forms.Label ClueLabel;
        private System.Windows.Forms.Panel CluePanel;
        private System.Windows.Forms.Label SquareLabel;
        private System.Windows.Forms.Panel SquarePanel;
    }
}


namespace Lab2_Paint
{
    partial class ChildForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.drawPanel = new Lab2_Paint.BufferedPanel();
            this.SuspendLayout();
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.SystemColors.Window;
            this.drawPanel.Location = new System.Drawing.Point(0, 0);
            this.drawPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.drawPanel.MinimumSize = new System.Drawing.Size(100, 65);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(853, 468);
            this.drawPanel.TabIndex = 0;
            this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_Paint);
            this.drawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseDown);
            this.drawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseMove);
            this.drawPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseUp);
            this.drawPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseWheel);
            this.drawPanel.Resize += new System.EventHandler(this.drawPanel_Resize);
            // 
            // ChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(840, 434);
            this.Controls.Add(this.drawPanel);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ChildForm";
            this.Text = "Child Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.ChildForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChildForm_FormClosing);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseWheel);
            this.Resize += new System.EventHandler(this.ChildForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        public BufferedPanel drawPanel;
    }
}


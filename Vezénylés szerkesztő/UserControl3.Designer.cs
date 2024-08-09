
namespace Vezénylés_szerkesztő
{
    partial class UserControl3
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.járatHozzáadásaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.járatTörléseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getNonDefaultShiftsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(8, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "#1\r\n#2";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseEnter += new System.EventHandler(this.UserControl3_MouseEnter);
            this.label1.MouseLeave += new System.EventHandler(this.UserControl3_MouseLeave);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.járatHozzáadásaToolStripMenuItem,
            this.járatTörléseToolStripMenuItem,
            this.getNonDefaultShiftsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(183, 92);
            // 
            // járatHozzáadásaToolStripMenuItem
            // 
            this.járatHozzáadásaToolStripMenuItem.Name = "járatHozzáadásaToolStripMenuItem";
            this.járatHozzáadásaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.járatHozzáadásaToolStripMenuItem.Text = "Járat hozzáadása";
            this.járatHozzáadásaToolStripMenuItem.Click += new System.EventHandler(this.járatHozzáadásaToolStripMenuItem_Click);
            // 
            // járatTörléseToolStripMenuItem
            // 
            this.járatTörléseToolStripMenuItem.Enabled = false;
            this.járatTörléseToolStripMenuItem.Name = "járatTörléseToolStripMenuItem";
            this.járatTörléseToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.járatTörléseToolStripMenuItem.Text = "Járat törlése";
            this.járatTörléseToolStripMenuItem.Click += new System.EventHandler(this.járatTörléseToolStripMenuItem_Click);
            // 
            // getNonDefaultShiftsToolStripMenuItem
            // 
            this.getNonDefaultShiftsToolStripMenuItem.Name = "getNonDefaultShiftsToolStripMenuItem";
            this.getNonDefaultShiftsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.getNonDefaultShiftsToolStripMenuItem.Text = "GetNonDefaultShifts";
            this.getNonDefaultShiftsToolStripMenuItem.Click += new System.EventHandler(this.getNonDefaultShiftsToolStripMenuItem_Click);
            // 
            // UserControl3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "UserControl3";
            this.Size = new System.Drawing.Size(35, 52);
            this.toolTip1.SetToolTip(this, "asd");
            this.Load += new System.EventHandler(this.UserControl3_Load);
            this.MouseEnter += new System.EventHandler(this.UserControl3_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.UserControl3_MouseLeave);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem járatHozzáadásaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem járatTörléseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getNonDefaultShiftsToolStripMenuItem;
    }
}


namespace Vezénylés_szerkesztő
{
    partial class UserControl2
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
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.szerkesztésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.betegségToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bejelentésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.törlésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kérésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hozzáadásToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.törlésToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "39";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(28, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hídvégi Árpád";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.ContextMenuStrip = this.contextMenuStrip1;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(186, 52);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.szerkesztésToolStripMenuItem,
            this.betegségToolStripMenuItem,
            this.kérésToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            // 
            // szerkesztésToolStripMenuItem
            // 
            this.szerkesztésToolStripMenuItem.Name = "szerkesztésToolStripMenuItem";
            this.szerkesztésToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.szerkesztésToolStripMenuItem.Text = "Szerkesztés";
            this.szerkesztésToolStripMenuItem.Click += new System.EventHandler(this.szerkesztésToolStripMenuItem_Click);
            // 
            // betegségToolStripMenuItem
            // 
            this.betegségToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bejelentésToolStripMenuItem,
            this.törlésToolStripMenuItem});
            this.betegségToolStripMenuItem.Name = "betegségToolStripMenuItem";
            this.betegségToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.betegségToolStripMenuItem.Text = "Betegség";
            // 
            // bejelentésToolStripMenuItem
            // 
            this.bejelentésToolStripMenuItem.Name = "bejelentésToolStripMenuItem";
            this.bejelentésToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.bejelentésToolStripMenuItem.Text = "Bejelentés";
            this.bejelentésToolStripMenuItem.Click += new System.EventHandler(this.bejelentésToolStripMenuItem_Click);
            // 
            // törlésToolStripMenuItem
            // 
            this.törlésToolStripMenuItem.Enabled = false;
            this.törlésToolStripMenuItem.Name = "törlésToolStripMenuItem";
            this.törlésToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.törlésToolStripMenuItem.Text = "Törlés";
            // 
            // kérésToolStripMenuItem
            // 
            this.kérésToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hozzáadásToolStripMenuItem,
            this.törlésToolStripMenuItem1,
            this.toolStripSeparator1,
            this.toolStripComboBox1});
            this.kérésToolStripMenuItem.Name = "kérésToolStripMenuItem";
            this.kérésToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.kérésToolStripMenuItem.Text = "Kérés";
            // 
            // hozzáadásToolStripMenuItem
            // 
            this.hozzáadásToolStripMenuItem.Name = "hozzáadásToolStripMenuItem";
            this.hozzáadásToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.hozzáadásToolStripMenuItem.Text = "Hozzáadás";
            this.hozzáadásToolStripMenuItem.Click += new System.EventHandler(this.hozzáadásToolStripMenuItem_Click);
            // 
            // törlésToolStripMenuItem1
            // 
            this.törlésToolStripMenuItem1.Enabled = false;
            this.törlésToolStripMenuItem1.Name = "törlésToolStripMenuItem1";
            this.törlésToolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
            this.törlésToolStripMenuItem1.Text = "Törlés";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "Normál órák",
            "Több óra a hónapban"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // UserControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "UserControl2";
            this.Size = new System.Drawing.Size(186, 52);
            this.Load += new System.EventHandler(this.UserControl2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem szerkesztésToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem betegségToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bejelentésToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem törlésToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kérésToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hozzáadásToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem törlésToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
    }
}

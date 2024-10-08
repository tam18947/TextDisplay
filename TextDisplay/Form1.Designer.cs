﻿namespace TextDisplay
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            contextMenuStrip1 = new ContextMenuStrip(components);
            toolStripTextBox1 = new ToolStripTextBox();
            fontToolStripMenuItem = new ToolStripMenuItem();
            foreColorToolStripMenuItem = new ToolStripMenuItem();
            backgroundColorToolStripMenuItem = new ToolStripMenuItem();
            paddingToolStripMenuItem = new ToolStripMenuItem();
            toolStripTextBoxPadding = new ToolStripTextBox();
            toolStripSeparator1 = new ToolStripSeparator();
            snapAssistToolStripMenuItem = new ToolStripMenuItem();
            topmostToolStripMenuItem = new ToolStripMenuItem();
            blinkToolStripMenuItem = new ToolStripMenuItem();
            autoSaveToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            resetToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            closeToolStripMenuItem = new ToolStripMenuItem();
            fontDialog1 = new FontDialog();
            colorDialog1 = new ColorDialog();
            label1 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { toolStripTextBox1, fontToolStripMenuItem, foreColorToolStripMenuItem, backgroundColorToolStripMenuItem, paddingToolStripMenuItem, toolStripSeparator1, snapAssistToolStripMenuItem, topmostToolStripMenuItem, blinkToolStripMenuItem, autoSaveToolStripMenuItem, toolStripSeparator2, saveToolStripMenuItem, resetToolStripMenuItem, toolStripSeparator3, closeToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(161, 289);
            // 
            // toolStripTextBox1
            // 
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.Size = new Size(100, 23);
            toolStripTextBox1.KeyDown += ToolStripTextBox1_KeyDown;
            toolStripTextBox1.TextChanged += ToolStripTextBox1_TextChanged;
            // 
            // fontToolStripMenuItem
            // 
            fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            fontToolStripMenuItem.Size = new Size(160, 22);
            fontToolStripMenuItem.Text = "フォント...";
            fontToolStripMenuItem.Click += FontToolStripMenuItem_Click;
            // 
            // foreColorToolStripMenuItem
            // 
            foreColorToolStripMenuItem.Name = "foreColorToolStripMenuItem";
            foreColorToolStripMenuItem.Size = new Size(160, 22);
            foreColorToolStripMenuItem.Text = "文字色...";
            foreColorToolStripMenuItem.Click += ForeColorToolStripMenuItem_Click;
            // 
            // backgroundColorToolStripMenuItem
            // 
            backgroundColorToolStripMenuItem.Name = "backgroundColorToolStripMenuItem";
            backgroundColorToolStripMenuItem.Size = new Size(160, 22);
            backgroundColorToolStripMenuItem.Text = "背景色...";
            backgroundColorToolStripMenuItem.Click += BackColorToolStripMenuItem_Click;
            // 
            // paddingToolStripMenuItem
            // 
            paddingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripTextBoxPadding });
            paddingToolStripMenuItem.Name = "paddingToolStripMenuItem";
            paddingToolStripMenuItem.Size = new Size(160, 22);
            paddingToolStripMenuItem.Text = "パディング";
            // 
            // toolStripTextBoxPadding
            // 
            toolStripTextBoxPadding.Name = "toolStripTextBoxPadding";
            toolStripTextBoxPadding.Size = new Size(100, 23);
            toolStripTextBoxPadding.Text = "0";
            toolStripTextBoxPadding.KeyDown += ToolStripTextBoxPadding_KeyDown;
            toolStripTextBoxPadding.TextChanged += ToolStripTextBoxPadding_TextChanged;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(157, 6);
            // 
            // snapAssistToolStripMenuItem
            // 
            snapAssistToolStripMenuItem.Checked = true;
            snapAssistToolStripMenuItem.CheckState = CheckState.Checked;
            snapAssistToolStripMenuItem.Name = "snapAssistToolStripMenuItem";
            snapAssistToolStripMenuItem.Size = new Size(160, 22);
            snapAssistToolStripMenuItem.Text = "スナップアシスト";
            snapAssistToolStripMenuItem.Click += SnapAssistToolStripMenuItem_Click;
            // 
            // topmostToolStripMenuItem
            // 
            topmostToolStripMenuItem.Checked = true;
            topmostToolStripMenuItem.CheckState = CheckState.Checked;
            topmostToolStripMenuItem.Name = "topmostToolStripMenuItem";
            topmostToolStripMenuItem.Size = new Size(160, 22);
            topmostToolStripMenuItem.Text = "最前面";
            topmostToolStripMenuItem.Click += TopmostToolStripMenuItem_Click;
            // 
            // blinkToolStripMenuItem
            // 
            blinkToolStripMenuItem.Name = "blinkToolStripMenuItem";
            blinkToolStripMenuItem.Size = new Size(160, 22);
            blinkToolStripMenuItem.Text = "点滅";
            blinkToolStripMenuItem.Click += BlinkToolStripMenuItem_Click;
            // 
            // autoSaveToolStripMenuItem
            // 
            autoSaveToolStripMenuItem.Name = "autoSaveToolStripMenuItem";
            autoSaveToolStripMenuItem.Size = new Size(160, 22);
            autoSaveToolStripMenuItem.Text = "自動保存";
            autoSaveToolStripMenuItem.Click += AutoSaveToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(157, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(160, 22);
            saveToolStripMenuItem.Text = "設定の保存";
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            // 
            // resetToolStripMenuItem
            // 
            resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            resetToolStripMenuItem.Size = new Size(160, 22);
            resetToolStripMenuItem.Text = "設定のリセット";
            resetToolStripMenuItem.Click += ResetToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(157, 6);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(160, 22);
            closeToolStripMenuItem.Text = "終了(&X)";
            closeToolStripMenuItem.Click += CloseToolStripMenuItem_Click;
            // 
            // fontDialog1
            // 
            fontDialog1.ShowColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ContextMenuStrip = contextMenuStrip1;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Yu Gothic UI", 24F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(491, 45);
            label1.TabIndex = 2;
            label1.Text = "右クリックしてこのメッセージを編集する";
            label1.MouseDown += Form1_MouseDown;
            label1.MouseMove += Form1_MouseMove;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MediumBlue;
            ClientSize = new Size(500, 50);
            ContextMenuStrip = contextMenuStrip1;
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Text display";
            TopMost = true;
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            contextMenuStrip1.ResumeLayout(false);
            contextMenuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem snapAssistToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem topmostToolStripMenuItem;
        private ToolStripMenuItem fontToolStripMenuItem;
        private FontDialog fontDialog1;
        private ToolStripMenuItem backgroundColorToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;
        private ColorDialog colorDialog1;
        private ToolStripMenuItem foreColorToolStripMenuItem;
        private Label label1;
        private ToolStripMenuItem resetToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem paddingToolStripMenuItem;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripMenuItem blinkToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private ToolStripTextBox toolStripTextBoxPadding;
        private ToolStripMenuItem autoSaveToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
    }
}
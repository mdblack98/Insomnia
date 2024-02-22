namespace Insomnia
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
            buttonKeepAwake = new Button();
            richTextBox1 = new RichTextBox();
            linkLabel1 = new LinkLabel();
            toolTip1 = new ToolTip(components);
            buttonList = new Button();
            linkLabelHelp = new LinkLabel();
            SuspendLayout();
            // 
            // buttonKeepAwake
            // 
            buttonKeepAwake.Location = new Point(120, 6);
            buttonKeepAwake.Name = "buttonKeepAwake";
            buttonKeepAwake.Size = new Size(104, 23);
            buttonKeepAwake.TabIndex = 0;
            buttonKeepAwake.Text = "Keep Awake";
            toolTip1.SetToolTip(buttonKeepAwake, "Set all USB devices to stay awake");
            buttonKeepAwake.UseVisualStyleBackColor = true;
            buttonKeepAwake.Click += buttonKeepAwake_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.Location = new Point(10, 35);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(778, 403);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(232, 10);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(99, 15);
            linkLabel1.TabIndex = 3;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "USB Devices Only";
            toolTip1.SetToolTip(linkLabel1, "QRZ Link to W9MDB");
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // buttonList
            // 
            buttonList.Location = new Point(10, 6);
            buttonList.Name = "buttonList";
            buttonList.Size = new Size(104, 23);
            buttonList.TabIndex = 4;
            buttonList.Text = "List Devices";
            toolTip1.SetToolTip(buttonList, "Set all USB devices to stay awake");
            buttonList.UseVisualStyleBackColor = true;
            buttonList.Click += buttonList_Click;
            // 
            // linkLabelHelp
            // 
            linkLabelHelp.AutoSize = true;
            linkLabelHelp.Location = new Point(689, 9);
            linkLabelHelp.Name = "linkLabelHelp";
            linkLabelHelp.Size = new Size(32, 15);
            linkLabelHelp.TabIndex = 5;
            linkLabelHelp.TabStop = true;
            linkLabelHelp.Text = "Help";
            linkLabelHelp.LinkClicked += linkLabelHelp_LinkClicked;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(linkLabelHelp);
            Controls.Add(buttonList);
            Controls.Add(linkLabel1);
            Controls.Add(richTextBox1);
            Controls.Add(buttonKeepAwake);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Insomnia V1.0";
            Load += Form1_Load;
            Shown += Form1_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonKeepAwake;
        private RichTextBox richTextBox1;
        private LinkLabel linkLabel1;
        private ToolTip toolTip1;
        private Button buttonList;
        private LinkLabel linkLabelHelp;
    }
}

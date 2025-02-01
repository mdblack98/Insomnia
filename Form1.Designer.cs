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
            toolTip1 = new ToolTip(components);
            buttonList = new Button();
            checkBoxAutoOff = new CheckBox();
            checkBoxAutoExit = new CheckBox();
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
            richTextBox1.Size = new Size(593, 403);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            richTextBox1.WordWrap = false;
            // 
            // buttonList
            // 
            buttonList.Location = new Point(10, 6);
            buttonList.Name = "buttonList";
            buttonList.Size = new Size(104, 23);
            buttonList.TabIndex = 4;
            buttonList.Text = "List Devices";
            toolTip1.SetToolTip(buttonList, "List sleep status of all USB devices");
            buttonList.UseVisualStyleBackColor = true;
            buttonList.Click += buttonList_Click;
            // 
            // checkBoxAutoOff
            // 
            checkBoxAutoOff.AutoSize = true;
            checkBoxAutoOff.Location = new Point(240, 10);
            checkBoxAutoOff.Name = "checkBoxAutoOff";
            checkBoxAutoOff.Size = new Size(72, 19);
            checkBoxAutoOff.TabIndex = 6;
            checkBoxAutoOff.Text = "Auto Off";
            toolTip1.SetToolTip(checkBoxAutoOff, "Auto turn off of sleep if needed");
            checkBoxAutoOff.UseVisualStyleBackColor = true;
            checkBoxAutoOff.CheckedChanged += checkBoxAutoOff_CheckedChanged;
            // 
            // checkBoxAutoExit
            // 
            checkBoxAutoExit.AutoSize = true;
            checkBoxAutoExit.Location = new Point(325, 10);
            checkBoxAutoExit.Name = "checkBoxAutoExit";
            checkBoxAutoExit.Size = new Size(73, 19);
            checkBoxAutoExit.TabIndex = 7;
            checkBoxAutoExit.Text = "Auto Exit";
            toolTip1.SetToolTip(checkBoxAutoExit, "Auto exit program");
            checkBoxAutoExit.UseVisualStyleBackColor = true;
            checkBoxAutoExit.CheckedChanged += checkBoxAutoExit_CheckedChanged;
            // 
            // linkLabelHelp
            // 
            linkLabelHelp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkLabelHelp.AutoSize = true;
            linkLabelHelp.Location = new Point(504, 9);
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
            ClientSize = new Size(615, 450);
            Controls.Add(checkBoxAutoExit);
            Controls.Add(checkBoxAutoOff);
            Controls.Add(linkLabelHelp);
            Controls.Add(buttonList);
            Controls.Add(richTextBox1);
            Controls.Add(buttonKeepAwake);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Insomnia V1.3.1";
            Load += Form1_Load;
            Shown += Form1_Shown;
            ResizeEnd += Form1_ResizeEnd;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonKeepAwake;
        private RichTextBox richTextBox1;
        private ToolTip toolTip1;
        private Button buttonList;
        private LinkLabel linkLabelHelp;
        private CheckBox checkBoxAutoOff;
        private CheckBox checkBoxAutoExit;
    }
}

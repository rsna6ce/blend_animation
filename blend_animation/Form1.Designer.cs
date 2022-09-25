namespace blend_animation
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericUpDownDelayBlend = new System.Windows.Forms.NumericUpDown();
            this.comboBoxBlendStep = new System.Windows.Forms.ComboBox();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.checkBoxPreview = new System.Windows.Forms.CheckBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.comboBoxLoopType = new System.Windows.Forms.ComboBox();
            this.comboBoxBlendType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBoxResult = new System.Windows.Forms.PictureBox();
            this.timerRender = new System.Windows.Forms.Timer(this.components);
            this.numericUpDownDelayOriginal = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayBlend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayOriginal)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.numericUpDownDelayOriginal);
            this.panel1.Controls.Add(this.numericUpDownDelayBlend);
            this.panel1.Controls.Add(this.comboBoxBlendStep);
            this.panel1.Controls.Add(this.buttonDown);
            this.panel1.Controls.Add(this.checkBoxPreview);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonUp);
            this.panel1.Controls.Add(this.comboBoxBlendType);
            this.panel1.Controls.Add(this.buttonRemove);
            this.panel1.Controls.Add(this.comboBoxLoopType);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Controls.Add(this.listBoxFiles);
            this.panel1.Controls.Add(this.buttonStart);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(152, 553);
            this.panel1.TabIndex = 0;
            // 
            // numericUpDownDelayBlend
            // 
            this.numericUpDownDelayBlend.Location = new System.Drawing.Point(8, 344);
            this.numericUpDownDelayBlend.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownDelayBlend.Name = "numericUpDownDelayBlend";
            this.numericUpDownDelayBlend.Size = new System.Drawing.Size(80, 19);
            this.numericUpDownDelayBlend.TabIndex = 8;
            this.numericUpDownDelayBlend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownDelayBlend.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // comboBoxBlendStep
            // 
            this.comboBoxBlendStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBlendStep.FormattingEnabled = true;
            this.comboBoxBlendStep.Items.AddRange(new object[] {
            "2 %",
            "5 %",
            "10 %",
            "20 %",
            "25 %"});
            this.comboBoxBlendStep.Location = new System.Drawing.Point(8, 300);
            this.comboBoxBlendStep.Name = "comboBoxBlendStep";
            this.comboBoxBlendStep.Size = new System.Drawing.Size(121, 20);
            this.comboBoxBlendStep.TabIndex = 7;
            // 
            // buttonDown
            // 
            this.buttonDown.Location = new System.Drawing.Point(76, 152);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(68, 23);
            this.buttonDown.TabIndex = 4;
            this.buttonDown.Text = "▼";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Location = new System.Drawing.Point(8, 152);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(64, 23);
            this.buttonUp.TabIndex = 3;
            this.buttonUp.Text = "▲";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(76, 36);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(68, 23);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(8, 36);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(64, 23);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.AllowDrop = true;
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.ItemHeight = 12;
            this.listBoxFiles.Location = new System.Drawing.Point(8, 60);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(136, 88);
            this.listBoxFiles.TabIndex = 2;
            this.listBoxFiles.SelectedIndexChanged += new System.EventHandler(this.listBoxFiles_SelectedIndexChanged);
            this.listBoxFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBoxFiles_DragDrop);
            this.listBoxFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBoxFiles_DragEnter);
            // 
            // checkBoxPreview
            // 
            this.checkBoxPreview.AutoSize = true;
            this.checkBoxPreview.Location = new System.Drawing.Point(8, 420);
            this.checkBoxPreview.Name = "checkBoxPreview";
            this.checkBoxPreview.Size = new System.Drawing.Size(88, 16);
            this.checkBoxPreview.TabIndex = 10;
            this.checkBoxPreview.Text = "preview only";
            this.checkBoxPreview.UseVisualStyleBackColor = true;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(8, 444);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(124, 23);
            this.buttonStart.TabIndex = 11;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // comboBoxLoopType
            // 
            this.comboBoxLoopType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLoopType.FormattingEnabled = true;
            this.comboBoxLoopType.Items.AddRange(new object[] {
            "Loop",
            "Round trip"});
            this.comboBoxLoopType.Location = new System.Drawing.Point(8, 256);
            this.comboBoxLoopType.Name = "comboBoxLoopType";
            this.comboBoxLoopType.Size = new System.Drawing.Size(121, 20);
            this.comboBoxLoopType.TabIndex = 6;
            // 
            // comboBoxBlendType
            // 
            this.comboBoxBlendType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBlendType.FormattingEnabled = true;
            this.comboBoxBlendType.Items.AddRange(new object[] {
            "Normal",
            "H-Gradation"});
            this.comboBoxBlendType.Location = new System.Drawing.Point(8, 212);
            this.comboBoxBlendType.Name = "comboBoxBlendType";
            this.comboBoxBlendType.Size = new System.Drawing.Size(121, 20);
            this.comboBoxBlendType.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 372);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "Delay of original frame";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 328);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "Delay of blend frame";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "Blend step / frame";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Loop type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "Animation type";
            // 
            // pictureBoxResult
            // 
            this.pictureBoxResult.Location = new System.Drawing.Point(160, 4);
            this.pictureBoxResult.Name = "pictureBoxResult";
            this.pictureBoxResult.Size = new System.Drawing.Size(3, 3);
            this.pictureBoxResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxResult.TabIndex = 1;
            this.pictureBoxResult.TabStop = false;
            // 
            // timerRender
            // 
            this.timerRender.Tick += new System.EventHandler(this.timerRender_Tick);
            // 
            // numericUpDownDelayOriginal
            // 
            this.numericUpDownDelayOriginal.Location = new System.Drawing.Point(8, 388);
            this.numericUpDownDelayOriginal.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownDelayOriginal.Name = "numericUpDownDelayOriginal";
            this.numericUpDownDelayOriginal.Size = new System.Drawing.Size(80, 19);
            this.numericUpDownDelayOriginal.TabIndex = 9;
            this.numericUpDownDelayOriginal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownDelayOriginal.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 348);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "x10ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 392);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "x10ms";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 24);
            this.label8.TabIndex = 1;
            this.label8.Text = "Add image file or drag\r\ndrop image files to list.\r\n";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.pictureBoxResult);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "blend_animation";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayBlend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayOriginal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxResult;
        private System.Windows.Forms.CheckBox checkBoxPreview;
        private System.Windows.Forms.ComboBox comboBoxBlendType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.NumericUpDown numericUpDownDelayBlend;
        private System.Windows.Forms.ComboBox comboBoxBlendStep;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ComboBox comboBoxLoopType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerRender;
        private System.Windows.Forms.NumericUpDown numericUpDownDelayOriginal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
    }
}


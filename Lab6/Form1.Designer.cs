namespace Lab6 {
    partial class Form1 {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.draw_btn = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.transform = new System.Windows.Forms.Button();
            this.numericMoveX = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericMoveY = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericMoveZ = new System.Windows.Forms.NumericUpDown();
            this.numericScaleX = new System.Windows.Forms.NumericUpDown();
            this.numericScaleY = new System.Windows.Forms.NumericUpDown();
            this.numericScaleZ = new System.Windows.Forms.NumericUpDown();
            this.rotationValueX = new System.Windows.Forms.TrackBar();
            this.rotationValueY = new System.Windows.Forms.TrackBar();
            this.rotationValueZ = new System.Windows.Forms.TrackBar();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMoveX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMoveY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMoveZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotationValueX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotationValueY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotationValueZ)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(776, 326);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // draw_btn
            // 
            this.draw_btn.Location = new System.Drawing.Point(13, 345);
            this.draw_btn.Name = "draw_btn";
            this.draw_btn.Size = new System.Drawing.Size(80, 23);
            this.draw_btn.TabIndex = 1;
            this.draw_btn.Text = "Нарисовать";
            this.draw_btn.UseVisualStyleBackColor = true;
            this.draw_btn.Click += new System.EventHandler(this.draw_btn_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(13, 374);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(73, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Тетраэдр";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(13, 398);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(73, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "Гексаэдр";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Enabled = false;
            this.radioButton3.Location = new System.Drawing.Point(13, 421);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(68, 17);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.Text = "Октаэдр";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // transform
            // 
            this.transform.Location = new System.Drawing.Point(111, 345);
            this.transform.Name = "transform";
            this.transform.Size = new System.Drawing.Size(107, 23);
            this.transform.TabIndex = 5;
            this.transform.Text = "Преобразовать";
            this.transform.UseVisualStyleBackColor = true;
            this.transform.Click += new System.EventHandler(this.transform_Click);
            // 
            // numericMoveX
            // 
            this.numericMoveX.Location = new System.Drawing.Point(131, 374);
            this.numericMoveX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericMoveX.Name = "numericMoveX";
            this.numericMoveX.Size = new System.Drawing.Size(44, 20);
            this.numericMoveX.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 411);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Y";
            // 
            // numericMoveY
            // 
            this.numericMoveY.Location = new System.Drawing.Point(131, 409);
            this.numericMoveY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericMoveY.Name = "numericMoveY";
            this.numericMoveY.Size = new System.Drawing.Size(44, 20);
            this.numericMoveY.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 449);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Z";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // numericMoveZ
            // 
            this.numericMoveZ.Location = new System.Drawing.Point(131, 447);
            this.numericMoveZ.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericMoveZ.Name = "numericMoveZ";
            this.numericMoveZ.Size = new System.Drawing.Size(44, 20);
            this.numericMoveZ.TabIndex = 12;
            // 
            // numericScaleX
            // 
            this.numericScaleX.DecimalPlaces = 1;
            this.numericScaleX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericScaleX.Location = new System.Drawing.Point(181, 374);
            this.numericScaleX.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericScaleX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericScaleX.Name = "numericScaleX";
            this.numericScaleX.Size = new System.Drawing.Size(36, 20);
            this.numericScaleX.TabIndex = 14;
            this.numericScaleX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericScaleY
            // 
            this.numericScaleY.DecimalPlaces = 1;
            this.numericScaleY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericScaleY.Location = new System.Drawing.Point(181, 409);
            this.numericScaleY.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericScaleY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericScaleY.Name = "numericScaleY";
            this.numericScaleY.Size = new System.Drawing.Size(36, 20);
            this.numericScaleY.TabIndex = 15;
            this.numericScaleY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericScaleZ
            // 
            this.numericScaleZ.DecimalPlaces = 1;
            this.numericScaleZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericScaleZ.Location = new System.Drawing.Point(181, 447);
            this.numericScaleZ.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericScaleZ.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericScaleZ.Name = "numericScaleZ";
            this.numericScaleZ.Size = new System.Drawing.Size(36, 20);
            this.numericScaleZ.TabIndex = 16;
            this.numericScaleZ.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rotationValueX
            // 
            this.rotationValueX.Location = new System.Drawing.Point(224, 374);
            this.rotationValueX.Maximum = 360;
            this.rotationValueX.Name = "rotationValueX";
            this.rotationValueX.Size = new System.Drawing.Size(208, 45);
            this.rotationValueX.SmallChange = 20;
            this.rotationValueX.TabIndex = 17;
            this.rotationValueX.ValueChanged += new System.EventHandler(this.rotationValueX_ValueChanged);
            // 
            // rotationValueY
            // 
            this.rotationValueY.Location = new System.Drawing.Point(224, 409);
            this.rotationValueY.Maximum = 360;
            this.rotationValueY.Name = "rotationValueY";
            this.rotationValueY.Size = new System.Drawing.Size(208, 45);
            this.rotationValueY.SmallChange = 20;
            this.rotationValueY.TabIndex = 18;
            this.rotationValueY.ValueChanged += new System.EventHandler(this.rotationValueY_ValueChanged);
            // 
            // rotationValueZ
            // 
            this.rotationValueZ.Location = new System.Drawing.Point(223, 447);
            this.rotationValueZ.Maximum = 360;
            this.rotationValueZ.Name = "rotationValueZ";
            this.rotationValueZ.Size = new System.Drawing.Size(208, 45);
            this.rotationValueZ.SmallChange = 20;
            this.rotationValueZ.TabIndex = 19;
            this.rotationValueZ.ValueChanged += new System.EventHandler(this.rotationValueZ_ValueChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "XY",
            "YZ",
            "ZX"});
            this.comboBox1.Location = new System.Drawing.Point(309, 347);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(70, 21);
            this.comboBox1.TabIndex = 20;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(239, 350);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Отражение";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(442, 345);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(442, 374);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 484);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.rotationValueZ);
            this.Controls.Add(this.rotationValueY);
            this.Controls.Add(this.rotationValueX);
            this.Controls.Add(this.numericScaleZ);
            this.Controls.Add(this.numericScaleY);
            this.Controls.Add(this.numericScaleX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericMoveZ);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericMoveY);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericMoveX);
            this.Controls.Add(this.transform);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.draw_btn);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMoveX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMoveY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMoveZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericScaleZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotationValueX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotationValueY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotationValueZ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button draw_btn;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Button transform;
        private System.Windows.Forms.NumericUpDown numericMoveX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericMoveY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericMoveZ;
        private System.Windows.Forms.NumericUpDown numericScaleX;
        private System.Windows.Forms.NumericUpDown numericScaleY;
        private System.Windows.Forms.NumericUpDown numericScaleZ;
        private System.Windows.Forms.TrackBar rotationValueX;
        private System.Windows.Forms.TrackBar rotationValueY;
        private System.Windows.Forms.TrackBar rotationValueZ;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}


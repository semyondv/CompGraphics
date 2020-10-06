namespace lab04
{
    partial class Form1
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
            this.picArea = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dot = new System.Windows.Forms.RadioButton();
            this.line = new System.Windows.Forms.RadioButton();
            this.poly = new System.Windows.Forms.RadioButton();
            this.lineActions = new System.Windows.Forms.ComboBox();
            this.interOut = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.dx = new System.Windows.Forms.NumericUpDown();
            this.dy = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.polyAcions = new System.Windows.Forms.ComboBox();
            this.angleVal = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.pointPositionInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleVal)).BeginInit();
            this.SuspendLayout();
            // 
            // picArea
            // 
            this.picArea.Location = new System.Drawing.Point(12, 12);
            this.picArea.Name = "picArea";
            this.picArea.Size = new System.Drawing.Size(674, 372);
            this.picArea.TabIndex = 0;
            this.picArea.TabStop = false;
            this.picArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picArea_MouseDown);
            this.picArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picArea_MouseUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 409);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 21);
            this.button1.TabIndex = 1;
            this.button1.Text = "Очистить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dot
            // 
            this.dot.AutoSize = true;
            this.dot.Location = new System.Drawing.Point(14, 443);
            this.dot.Name = "dot";
            this.dot.Size = new System.Drawing.Size(55, 17);
            this.dot.TabIndex = 2;
            this.dot.Text = "Точка";
            this.dot.UseVisualStyleBackColor = true;
            // 
            // line
            // 
            this.line.AutoSize = true;
            this.line.Checked = true;
            this.line.Location = new System.Drawing.Point(92, 443);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(56, 17);
            this.line.TabIndex = 2;
            this.line.TabStop = true;
            this.line.Text = "Ребро";
            this.line.UseVisualStyleBackColor = true;
            // 
            // poly
            // 
            this.poly.AutoSize = true;
            this.poly.Location = new System.Drawing.Point(171, 443);
            this.poly.Name = "poly";
            this.poly.Size = new System.Drawing.Size(68, 17);
            this.poly.TabIndex = 2;
            this.poly.Text = "Полигон";
            this.poly.UseVisualStyleBackColor = true;
            // 
            // lineActions
            // 
            this.lineActions.FormattingEnabled = true;
            this.lineActions.Items.AddRange(new object[] {
            "Пересечение ребер",
            "Смещение",
            "Поворот 90",
            "Положение точки"});
            this.lineActions.Location = new System.Drawing.Point(280, 409);
            this.lineActions.Name = "lineActions";
            this.lineActions.Size = new System.Drawing.Size(153, 21);
            this.lineActions.TabIndex = 3;
            // 
            // interOut
            // 
            this.interOut.AutoSize = true;
            this.interOut.Location = new System.Drawing.Point(277, 443);
            this.interOut.Name = "interOut";
            this.interOut.Size = new System.Drawing.Size(0, 13);
            this.interOut.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(106, 409);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 21);
            this.button2.TabIndex = 5;
            this.button2.Text = "Преобразовать";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dx
            // 
            this.dx.Location = new System.Drawing.Point(40, 467);
            this.dx.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.dx.Name = "dx";
            this.dx.Size = new System.Drawing.Size(37, 20);
            this.dx.TabIndex = 6;
            // 
            // dy
            // 
            this.dy.Location = new System.Drawing.Point(40, 491);
            this.dy.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.dy.Name = "dy";
            this.dy.Size = new System.Drawing.Size(37, 20);
            this.dy.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 469);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 493);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Y";
            // 
            // polyAcions
            // 
            this.polyAcions.FormattingEnabled = true;
            this.polyAcions.Items.AddRange(new object[] {
            "Смещение",
            "Поворот",
            "Масштабирование",
            "Принадлежит ли точка выпуклому многоугольнику ",
            "Принадлежит ли точка невыпуклому многоугольнику"});
            this.polyAcions.Location = new System.Drawing.Point(439, 409);
            this.polyAcions.Name = "polyAcions";
            this.polyAcions.Size = new System.Drawing.Size(153, 21);
            this.polyAcions.TabIndex = 10;
            // 
            // angleVal
            // 
            this.angleVal.Location = new System.Drawing.Point(171, 466);
            this.angleVal.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.angleVal.Name = "angleVal";
            this.angleVal.Size = new System.Drawing.Size(36, 20);
            this.angleVal.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(83, 468);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Угол поворота";
            // 
            // pointPositionInfo
            // 
            this.pointPositionInfo.AutoSize = true;
            this.pointPositionInfo.Location = new System.Drawing.Point(523, 493);
            this.pointPositionInfo.Name = "pointPositionInfo";
            this.pointPositionInfo.Size = new System.Drawing.Size(0, 13);
            this.pointPositionInfo.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 515);
            this.Controls.Add(this.pointPositionInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.angleVal);
            this.Controls.Add(this.polyAcions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dy);
            this.Controls.Add(this.dx);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.interOut);
            this.Controls.Add(this.lineActions);
            this.Controls.Add(this.poly);
            this.Controls.Add(this.line);
            this.Controls.Add(this.dot);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.picArea);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleVal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picArea;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton dot;
        private System.Windows.Forms.RadioButton line;
        private System.Windows.Forms.RadioButton poly;
        private System.Windows.Forms.ComboBox lineActions;
        private System.Windows.Forms.Label interOut;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown dx;
        private System.Windows.Forms.NumericUpDown dy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox polyAcions;
        private System.Windows.Forms.NumericUpDown angleVal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label pointPositionInfo;
    }
}


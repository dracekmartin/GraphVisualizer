namespace Graphs
{
    partial class FormSettings
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
            this.vertexBaseColorButton = new System.Windows.Forms.Button();
            this.edgeBaseColorButton = new System.Windows.Forms.Button();
            this.textBaseColorButton = new System.Windows.Forms.Button();
            this.smallHiglightColor = new System.Windows.Forms.Button();
            this.mediumHiglightColor = new System.Windows.Forms.Button();
            this.bigHiglightColor = new System.Windows.Forms.Button();
            this.nodeUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.edgeUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.textUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.arrowUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nodeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edgeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arrowUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // vertexBaseColorButton
            // 
            this.vertexBaseColorButton.Location = new System.Drawing.Point(12, 12);
            this.vertexBaseColorButton.Name = "vertexBaseColorButton";
            this.vertexBaseColorButton.Size = new System.Drawing.Size(123, 23);
            this.vertexBaseColorButton.TabIndex = 0;
            this.vertexBaseColorButton.Text = "Vertex base color";
            this.vertexBaseColorButton.UseVisualStyleBackColor = true;
            this.vertexBaseColorButton.Click += new System.EventHandler(this.VertexBaseColorButton_Click);
            // 
            // edgeBaseColorButton
            // 
            this.edgeBaseColorButton.Location = new System.Drawing.Point(12, 41);
            this.edgeBaseColorButton.Name = "edgeBaseColorButton";
            this.edgeBaseColorButton.Size = new System.Drawing.Size(123, 23);
            this.edgeBaseColorButton.TabIndex = 1;
            this.edgeBaseColorButton.Text = "Edge base color";
            this.edgeBaseColorButton.UseVisualStyleBackColor = true;
            this.edgeBaseColorButton.Click += new System.EventHandler(this.edgeBaseColorButton_Click);
            // 
            // textBaseColorButton
            // 
            this.textBaseColorButton.Location = new System.Drawing.Point(12, 70);
            this.textBaseColorButton.Name = "textBaseColorButton";
            this.textBaseColorButton.Size = new System.Drawing.Size(123, 23);
            this.textBaseColorButton.TabIndex = 2;
            this.textBaseColorButton.Text = "Text base color";
            this.textBaseColorButton.UseVisualStyleBackColor = true;
            this.textBaseColorButton.Click += new System.EventHandler(this.textBaseColorButton_Click);
            // 
            // smallHiglightColor
            // 
            this.smallHiglightColor.Location = new System.Drawing.Point(12, 99);
            this.smallHiglightColor.Name = "smallHiglightColor";
            this.smallHiglightColor.Size = new System.Drawing.Size(123, 23);
            this.smallHiglightColor.TabIndex = 3;
            this.smallHiglightColor.Text = "Small highlight color";
            this.smallHiglightColor.UseVisualStyleBackColor = true;
            this.smallHiglightColor.Click += new System.EventHandler(this.smallHiglightColor_Click);
            // 
            // mediumHiglightColor
            // 
            this.mediumHiglightColor.Location = new System.Drawing.Point(12, 128);
            this.mediumHiglightColor.Name = "mediumHiglightColor";
            this.mediumHiglightColor.Size = new System.Drawing.Size(123, 23);
            this.mediumHiglightColor.TabIndex = 4;
            this.mediumHiglightColor.Text = "Medium highlight color";
            this.mediumHiglightColor.UseVisualStyleBackColor = true;
            this.mediumHiglightColor.Click += new System.EventHandler(this.mediumHiglightColor_Click);
            // 
            // bigHiglightColor
            // 
            this.bigHiglightColor.Location = new System.Drawing.Point(12, 157);
            this.bigHiglightColor.Name = "bigHiglightColor";
            this.bigHiglightColor.Size = new System.Drawing.Size(123, 23);
            this.bigHiglightColor.TabIndex = 5;
            this.bigHiglightColor.Text = "Big highlight color";
            this.bigHiglightColor.UseVisualStyleBackColor = true;
            this.bigHiglightColor.Click += new System.EventHandler(this.bigHiglightColor_Click);
            // 
            // nodeUpDown
            // 
            this.nodeUpDown.Location = new System.Drawing.Point(204, 15);
            this.nodeUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nodeUpDown.Name = "nodeUpDown";
            this.nodeUpDown.Size = new System.Drawing.Size(61, 20);
            this.nodeUpDown.TabIndex = 6;
            this.nodeUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nodeUpDown.ValueChanged += new System.EventHandler(this.nodeUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(141, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Node size:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Edge size:";
            // 
            // edgeUpDown
            // 
            this.edgeUpDown.Location = new System.Drawing.Point(204, 44);
            this.edgeUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edgeUpDown.Name = "edgeUpDown";
            this.edgeUpDown.Size = new System.Drawing.Size(61, 20);
            this.edgeUpDown.TabIndex = 8;
            this.edgeUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edgeUpDown.ValueChanged += new System.EventHandler(this.edgeUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(141, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Text size:";
            // 
            // textUpDown
            // 
            this.textUpDown.Location = new System.Drawing.Point(204, 73);
            this.textUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textUpDown.Name = "textUpDown";
            this.textUpDown.Size = new System.Drawing.Size(61, 20);
            this.textUpDown.TabIndex = 10;
            this.textUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textUpDown.ValueChanged += new System.EventHandler(this.textUpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Arrow size:";
            // 
            // arrowUpDown
            // 
            this.arrowUpDown.Location = new System.Drawing.Point(204, 102);
            this.arrowUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.arrowUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.arrowUpDown.Name = "arrowUpDown";
            this.arrowUpDown.Size = new System.Drawing.Size(61, 20);
            this.arrowUpDown.TabIndex = 12;
            this.arrowUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.arrowUpDown.ValueChanged += new System.EventHandler(this.arrowUpDown_ValueChanged);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 193);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.arrowUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edgeUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nodeUpDown);
            this.Controls.Add(this.vertexBaseColorButton);
            this.Controls.Add(this.edgeBaseColorButton);
            this.Controls.Add(this.textBaseColorButton);
            this.Controls.Add(this.bigHiglightColor);
            this.Controls.Add(this.smallHiglightColor);
            this.Controls.Add(this.mediumHiglightColor);
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSettings_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.nodeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edgeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arrowUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button vertexBaseColorButton;
        private System.Windows.Forms.Button edgeBaseColorButton;
        private System.Windows.Forms.Button textBaseColorButton;
        private System.Windows.Forms.Button smallHiglightColor;
        private System.Windows.Forms.Button mediumHiglightColor;
        private System.Windows.Forms.Button bigHiglightColor;
        private System.Windows.Forms.NumericUpDown nodeUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown edgeUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown textUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown arrowUpDown;
    }
}
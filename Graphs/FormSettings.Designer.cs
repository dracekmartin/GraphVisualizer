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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.vertexBaseColorButton = new System.Windows.Forms.Button();
            this.edgeBaseColorButton = new System.Windows.Forms.Button();
            this.textBaseColorButton = new System.Windows.Forms.Button();
            this.smallHiglightColor = new System.Windows.Forms.Button();
            this.mediumHiglightColor = new System.Windows.Forms.Button();
            this.bigHiglightColor = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.vertexBaseColorButton);
            this.flowLayoutPanel1.Controls.Add(this.edgeBaseColorButton);
            this.flowLayoutPanel1.Controls.Add(this.textBaseColorButton);
            this.flowLayoutPanel1.Controls.Add(this.smallHiglightColor);
            this.flowLayoutPanel1.Controls.Add(this.mediumHiglightColor);
            this.flowLayoutPanel1.Controls.Add(this.bigHiglightColor);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(285, 296);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // vertexBaseColorButton
            // 
            this.vertexBaseColorButton.Location = new System.Drawing.Point(3, 3);
            this.vertexBaseColorButton.Name = "vertexBaseColorButton";
            this.vertexBaseColorButton.Size = new System.Drawing.Size(123, 23);
            this.vertexBaseColorButton.TabIndex = 0;
            this.vertexBaseColorButton.Text = "Vertex base color";
            this.vertexBaseColorButton.UseVisualStyleBackColor = true;
            this.vertexBaseColorButton.Click += new System.EventHandler(this.VertexBaseColorButton_Click);
            // 
            // edgeBaseColorButton
            // 
            this.edgeBaseColorButton.Location = new System.Drawing.Point(3, 32);
            this.edgeBaseColorButton.Name = "edgeBaseColorButton";
            this.edgeBaseColorButton.Size = new System.Drawing.Size(123, 23);
            this.edgeBaseColorButton.TabIndex = 1;
            this.edgeBaseColorButton.Text = "Edge base color";
            this.edgeBaseColorButton.UseVisualStyleBackColor = true;
            this.edgeBaseColorButton.Click += new System.EventHandler(this.edgeBaseColorButton_Click);
            // 
            // textBaseColorButton
            // 
            this.textBaseColorButton.Location = new System.Drawing.Point(3, 61);
            this.textBaseColorButton.Name = "textBaseColorButton";
            this.textBaseColorButton.Size = new System.Drawing.Size(123, 23);
            this.textBaseColorButton.TabIndex = 2;
            this.textBaseColorButton.Text = "Text base color";
            this.textBaseColorButton.UseVisualStyleBackColor = true;
            this.textBaseColorButton.Click += new System.EventHandler(this.textBaseColorButton_Click);
            // 
            // smallHiglightColor
            // 
            this.smallHiglightColor.Location = new System.Drawing.Point(3, 90);
            this.smallHiglightColor.Name = "smallHiglightColor";
            this.smallHiglightColor.Size = new System.Drawing.Size(123, 23);
            this.smallHiglightColor.TabIndex = 3;
            this.smallHiglightColor.Text = "Small highlight color";
            this.smallHiglightColor.UseVisualStyleBackColor = true;
            this.smallHiglightColor.Click += new System.EventHandler(this.smallHiglightColor_Click);
            // 
            // mediumHiglightColor
            // 
            this.mediumHiglightColor.Location = new System.Drawing.Point(3, 119);
            this.mediumHiglightColor.Name = "mediumHiglightColor";
            this.mediumHiglightColor.Size = new System.Drawing.Size(123, 23);
            this.mediumHiglightColor.TabIndex = 4;
            this.mediumHiglightColor.Text = "Medium highlight color";
            this.mediumHiglightColor.UseVisualStyleBackColor = true;
            this.mediumHiglightColor.Click += new System.EventHandler(this.mediumHiglightColor_Click);
            // 
            // bigHiglightColor
            // 
            this.bigHiglightColor.Location = new System.Drawing.Point(3, 148);
            this.bigHiglightColor.Name = "bigHiglightColor";
            this.bigHiglightColor.Size = new System.Drawing.Size(123, 23);
            this.bigHiglightColor.TabIndex = 5;
            this.bigHiglightColor.Text = "Big highlight color";
            this.bigHiglightColor.UseVisualStyleBackColor = true;
            this.bigHiglightColor.Click += new System.EventHandler(this.bigHiglightColor_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 296);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "FormSettings";
            this.Text = "FormSettings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSettings_FormClosed);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button vertexBaseColorButton;
        private System.Windows.Forms.Button edgeBaseColorButton;
        private System.Windows.Forms.Button textBaseColorButton;
        private System.Windows.Forms.Button smallHiglightColor;
        private System.Windows.Forms.Button mediumHiglightColor;
        private System.Windows.Forms.Button bigHiglightColor;
    }
}
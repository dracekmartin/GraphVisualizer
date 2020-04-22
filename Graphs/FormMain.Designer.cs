namespace Graphs
{
    partial class FormMain
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
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutControls = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxGraphCreation = new System.Windows.Forms.GroupBox();
            this.clickFunctionEdge = new System.Windows.Forms.ListBox();
            this.clickFunctionVertex = new System.Windows.Forms.ListBox();
            this.edgeValueInput = new System.Windows.Forms.MaskedTextBox();
            this.clickFunctionClick = new System.Windows.Forms.ListBox();
            this.groupBoxAlg = new System.Windows.Forms.GroupBox();
            this.waitTimeInput = new System.Windows.Forms.TrackBar();
            this.algorithmSelection = new System.Windows.Forms.ListBox();
            this.startAlgButton = new System.Windows.Forms.Button();
            this.clickFunctionStartingVertex = new System.Windows.Forms.ListBox();
            this.graph = new System.Windows.Forms.PictureBox();
            this.tableLayoutMain.SuspendLayout();
            this.flowLayoutControls.SuspendLayout();
            this.groupBoxGraphCreation.SuspendLayout();
            this.groupBoxAlg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waitTimeInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutMain.ColumnCount = 1;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Controls.Add(this.flowLayoutControls, 0, 0);
            this.tableLayoutMain.Controls.Add(this.graph, 0, 1);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 2;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Size = new System.Drawing.Size(944, 501);
            this.tableLayoutMain.TabIndex = 2;
            // 
            // flowLayoutControls
            // 
            this.flowLayoutControls.AutoScroll = true;
            this.flowLayoutControls.BackColor = System.Drawing.SystemColors.ControlDark;
            this.flowLayoutControls.Controls.Add(this.groupBoxGraphCreation);
            this.flowLayoutControls.Controls.Add(this.groupBoxAlg);
            this.flowLayoutControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutControls.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutControls.Name = "flowLayoutControls";
            this.flowLayoutControls.Size = new System.Drawing.Size(938, 115);
            this.flowLayoutControls.TabIndex = 1;
            // 
            // groupBoxGraphCreation
            // 
            this.groupBoxGraphCreation.AutoSize = true;
            this.groupBoxGraphCreation.Controls.Add(this.clickFunctionEdge);
            this.groupBoxGraphCreation.Controls.Add(this.clickFunctionVertex);
            this.groupBoxGraphCreation.Controls.Add(this.edgeValueInput);
            this.groupBoxGraphCreation.Controls.Add(this.clickFunctionClick);
            this.groupBoxGraphCreation.Location = new System.Drawing.Point(3, 3);
            this.groupBoxGraphCreation.Name = "groupBoxGraphCreation";
            this.groupBoxGraphCreation.Size = new System.Drawing.Size(443, 91);
            this.groupBoxGraphCreation.TabIndex = 4;
            this.groupBoxGraphCreation.TabStop = false;
            this.groupBoxGraphCreation.Text = "Graph creation";
            // 
            // clickFunctionEdge
            // 
            this.clickFunctionEdge.FormattingEnabled = true;
            this.clickFunctionEdge.Items.AddRange(new object[] {
            "Add edge",
            "Change edge value",
            "Delete edge",
            "Set edge direction"});
            this.clickFunctionEdge.Location = new System.Drawing.Point(277, 16);
            this.clickFunctionEdge.Name = "clickFunctionEdge";
            this.clickFunctionEdge.Size = new System.Drawing.Size(131, 56);
            this.clickFunctionEdge.TabIndex = 3;
            this.clickFunctionEdge.SelectedIndexChanged += new System.EventHandler(this.clickFunctionChange);
            this.clickFunctionEdge.Leave += new System.EventHandler(this.clickFunction_Leave);
            // 
            // clickFunctionVertex
            // 
            this.clickFunctionVertex.FormattingEnabled = true;
            this.clickFunctionVertex.Items.AddRange(new object[] {
            "Add vertex",
            "Move vertex",
            "Delte vertex"});
            this.clickFunctionVertex.Location = new System.Drawing.Point(140, 16);
            this.clickFunctionVertex.Name = "clickFunctionVertex";
            this.clickFunctionVertex.Size = new System.Drawing.Size(131, 43);
            this.clickFunctionVertex.TabIndex = 2;
            this.clickFunctionVertex.SelectedIndexChanged += new System.EventHandler(this.clickFunctionChange);
            this.clickFunctionVertex.Leave += new System.EventHandler(this.clickFunction_Leave);
            // 
            // edgeValueInput
            // 
            this.edgeValueInput.Location = new System.Drawing.Point(414, 28);
            this.edgeValueInput.Mask = "000";
            this.edgeValueInput.Name = "edgeValueInput";
            this.edgeValueInput.Size = new System.Drawing.Size(23, 20);
            this.edgeValueInput.TabIndex = 4;
            this.edgeValueInput.Text = "10";
            this.edgeValueInput.ValidatingType = typeof(int);
            this.edgeValueInput.Leave += new System.EventHandler(this.edgeValueInput_Leave);
            // 
            // clickFunctionClick
            // 
            this.clickFunctionClick.FormattingEnabled = true;
            this.clickFunctionClick.Items.AddRange(new object[] {
            "Click"});
            this.clickFunctionClick.Location = new System.Drawing.Point(3, 16);
            this.clickFunctionClick.Name = "clickFunctionClick";
            this.clickFunctionClick.Size = new System.Drawing.Size(131, 17);
            this.clickFunctionClick.TabIndex = 1;
            this.clickFunctionClick.SelectedIndexChanged += new System.EventHandler(this.clickFunctionChange);
            this.clickFunctionClick.Leave += new System.EventHandler(this.clickFunction_Leave);
            // 
            // groupBoxAlg
            // 
            this.groupBoxAlg.AutoSize = true;
            this.groupBoxAlg.Controls.Add(this.waitTimeInput);
            this.groupBoxAlg.Controls.Add(this.algorithmSelection);
            this.groupBoxAlg.Controls.Add(this.startAlgButton);
            this.groupBoxAlg.Controls.Add(this.clickFunctionStartingVertex);
            this.groupBoxAlg.Location = new System.Drawing.Point(452, 3);
            this.groupBoxAlg.Name = "groupBoxAlg";
            this.groupBoxAlg.Size = new System.Drawing.Size(361, 91);
            this.groupBoxAlg.TabIndex = 5;
            this.groupBoxAlg.TabStop = false;
            this.groupBoxAlg.Text = "Algorithms";
            // 
            // waitTimeInput
            // 
            this.waitTimeInput.AutoSize = false;
            this.waitTimeInput.BackColor = System.Drawing.SystemColors.ControlDark;
            this.waitTimeInput.LargeChange = 100;
            this.waitTimeInput.Location = new System.Drawing.Point(143, 39);
            this.waitTimeInput.Maximum = 1000;
            this.waitTimeInput.Minimum = 1;
            this.waitTimeInput.Name = "waitTimeInput";
            this.waitTimeInput.Size = new System.Drawing.Size(131, 33);
            this.waitTimeInput.TabIndex = 7;
            this.waitTimeInput.Value = 100;
            // 
            // algorithmSelection
            // 
            this.algorithmSelection.FormattingEnabled = true;
            this.algorithmSelection.Items.AddRange(new object[] {
            "Dijsktra",
            "Kruskal",
            "Jarník"});
            this.algorithmSelection.Location = new System.Drawing.Point(6, 16);
            this.algorithmSelection.Name = "algorithmSelection";
            this.algorithmSelection.Size = new System.Drawing.Size(131, 43);
            this.algorithmSelection.TabIndex = 5;
            // 
            // startAlgButton
            // 
            this.startAlgButton.AutoSize = true;
            this.startAlgButton.Location = new System.Drawing.Point(280, 16);
            this.startAlgButton.Name = "startAlgButton";
            this.startAlgButton.Size = new System.Drawing.Size(75, 23);
            this.startAlgButton.TabIndex = 8;
            this.startAlgButton.Text = "Start";
            this.startAlgButton.UseVisualStyleBackColor = true;
            this.startAlgButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // clickFunctionStartingVertex
            // 
            this.clickFunctionStartingVertex.FormattingEnabled = true;
            this.clickFunctionStartingVertex.Items.AddRange(new object[] {
            "Choose starting point"});
            this.clickFunctionStartingVertex.Location = new System.Drawing.Point(143, 16);
            this.clickFunctionStartingVertex.Name = "clickFunctionStartingVertex";
            this.clickFunctionStartingVertex.Size = new System.Drawing.Size(131, 17);
            this.clickFunctionStartingVertex.TabIndex = 6;
            this.clickFunctionStartingVertex.SelectedIndexChanged += new System.EventHandler(this.clickFunctionChange);
            this.clickFunctionStartingVertex.Leave += new System.EventHandler(this.clickFunction_Leave);
            // 
            // graph
            // 
            this.graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graph.Location = new System.Drawing.Point(3, 124);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(938, 374);
            this.graph.TabIndex = 2;
            this.graph.TabStop = false;
            this.graph.Paint += new System.Windows.Forms.PaintEventHandler(this.graph_Paint);
            this.graph.MouseClick += new System.Windows.Forms.MouseEventHandler(this.graph_MouseClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.tableLayoutMain);
            this.Name = "FormMain";
            this.Text = "Graphs";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutMain.ResumeLayout(false);
            this.flowLayoutControls.ResumeLayout(false);
            this.flowLayoutControls.PerformLayout();
            this.groupBoxGraphCreation.ResumeLayout(false);
            this.groupBoxGraphCreation.PerformLayout();
            this.groupBoxAlg.ResumeLayout(false);
            this.groupBoxAlg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waitTimeInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutControls;
        private System.Windows.Forms.GroupBox groupBoxGraphCreation;
        private System.Windows.Forms.ListBox clickFunctionEdge;
        private System.Windows.Forms.ListBox clickFunctionVertex;
        private System.Windows.Forms.MaskedTextBox edgeValueInput;
        private System.Windows.Forms.ListBox clickFunctionClick;
        private System.Windows.Forms.GroupBox groupBoxAlg;
        private System.Windows.Forms.ListBox algorithmSelection;
        private System.Windows.Forms.Button startAlgButton;
        private System.Windows.Forms.ListBox clickFunctionStartingVertex;
        private System.Windows.Forms.PictureBox graph;
        private System.Windows.Forms.TrackBar waitTimeInput;
    }
}


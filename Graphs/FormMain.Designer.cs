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
            this.clickFunctionNode = new System.Windows.Forms.ListBox();
            this.edgeValueInput = new System.Windows.Forms.MaskedTextBox();
            this.clickFunctionClick = new System.Windows.Forms.ListBox();
            this.groupBoxAlg = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.algorithmSelectionMF = new System.Windows.Forms.ListBox();
            this.algorithmSelectionMST = new System.Windows.Forms.ListBox();
            this.backstepButton = new System.Windows.Forms.Button();
            this.stepButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.waitTimeInput = new System.Windows.Forms.TrackBar();
            this.algorithmSelectionSP = new System.Windows.Forms.ListBox();
            this.startAlgButton = new System.Windows.Forms.Button();
            this.clickFunctionStartingNode = new System.Windows.Forms.ListBox();
            this.groupBoxOthers = new System.Windows.Forms.GroupBox();
            this.clearGraphButton = new System.Windows.Forms.Button();
            this.saveGraphButton = new System.Windows.Forms.Button();
            this.directedCheckBox = new System.Windows.Forms.CheckBox();
            this.euclideanSpaceCheckBox = new System.Windows.Forms.CheckBox();
            this.loadGraphButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.graph = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutMain.SuspendLayout();
            this.flowLayoutControls.SuspendLayout();
            this.groupBoxGraphCreation.SuspendLayout();
            this.groupBoxAlg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waitTimeInput)).BeginInit();
            this.groupBoxOthers.SuspendLayout();
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
            this.tableLayoutMain.Size = new System.Drawing.Size(1426, 501);
            this.tableLayoutMain.TabIndex = 2;
            // 
            // flowLayoutControls
            // 
            this.flowLayoutControls.AutoSize = true;
            this.flowLayoutControls.BackColor = System.Drawing.SystemColors.ControlDark;
            this.flowLayoutControls.Controls.Add(this.groupBoxGraphCreation);
            this.flowLayoutControls.Controls.Add(this.groupBoxAlg);
            this.flowLayoutControls.Controls.Add(this.groupBoxOthers);
            this.flowLayoutControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutControls.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutControls.Name = "flowLayoutControls";
            this.flowLayoutControls.Size = new System.Drawing.Size(1420, 116);
            this.flowLayoutControls.TabIndex = 1;
            // 
            // groupBoxGraphCreation
            // 
            this.groupBoxGraphCreation.AutoSize = true;
            this.groupBoxGraphCreation.Controls.Add(this.clickFunctionEdge);
            this.groupBoxGraphCreation.Controls.Add(this.clickFunctionNode);
            this.groupBoxGraphCreation.Controls.Add(this.edgeValueInput);
            this.groupBoxGraphCreation.Controls.Add(this.clickFunctionClick);
            this.groupBoxGraphCreation.Location = new System.Drawing.Point(3, 3);
            this.groupBoxGraphCreation.Name = "groupBoxGraphCreation";
            this.groupBoxGraphCreation.Size = new System.Drawing.Size(443, 78);
            this.groupBoxGraphCreation.TabIndex = 4;
            this.groupBoxGraphCreation.TabStop = false;
            this.groupBoxGraphCreation.Text = "Graph creation";
            // 
            // clickFunctionEdge
            // 
            this.clickFunctionEdge.FormattingEnabled = true;
            this.clickFunctionEdge.Items.AddRange(new object[] {
            "Add/delete edge",
            "Change edge value",
            "Set edge direction"});
            this.clickFunctionEdge.Location = new System.Drawing.Point(277, 16);
            this.clickFunctionEdge.Name = "clickFunctionEdge";
            this.clickFunctionEdge.Size = new System.Drawing.Size(131, 43);
            this.clickFunctionEdge.TabIndex = 3;
            this.clickFunctionEdge.SelectedIndexChanged += new System.EventHandler(this.ClickFunctionChange);
            this.clickFunctionEdge.Leave += new System.EventHandler(this.ClickFunctionLeaveFocus);
            // 
            // clickFunctionNode
            // 
            this.clickFunctionNode.FormattingEnabled = true;
            this.clickFunctionNode.Items.AddRange(new object[] {
            "Add/delete node",
            "Move node"});
            this.clickFunctionNode.Location = new System.Drawing.Point(140, 16);
            this.clickFunctionNode.Name = "clickFunctionNode";
            this.clickFunctionNode.Size = new System.Drawing.Size(131, 30);
            this.clickFunctionNode.TabIndex = 2;
            this.clickFunctionNode.SelectedIndexChanged += new System.EventHandler(this.ClickFunctionChange);
            this.clickFunctionNode.Leave += new System.EventHandler(this.ClickFunctionLeaveFocus);
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
            this.edgeValueInput.Leave += new System.EventHandler(this.EdgeValueInput_Leave);
            // 
            // clickFunctionClick
            // 
            this.clickFunctionClick.FormattingEnabled = true;
            this.clickFunctionClick.Items.AddRange(new object[] {
            "Click",
            "Drag"});
            this.clickFunctionClick.Location = new System.Drawing.Point(3, 16);
            this.clickFunctionClick.Name = "clickFunctionClick";
            this.clickFunctionClick.Size = new System.Drawing.Size(131, 30);
            this.clickFunctionClick.TabIndex = 1;
            this.clickFunctionClick.SelectedIndexChanged += new System.EventHandler(this.ClickFunctionChange);
            this.clickFunctionClick.Leave += new System.EventHandler(this.ClickFunctionLeaveFocus);
            // 
            // groupBoxAlg
            // 
            this.groupBoxAlg.AutoSize = true;
            this.groupBoxAlg.Controls.Add(this.label3);
            this.groupBoxAlg.Controls.Add(this.label2);
            this.groupBoxAlg.Controls.Add(this.label1);
            this.groupBoxAlg.Controls.Add(this.algorithmSelectionMF);
            this.groupBoxAlg.Controls.Add(this.algorithmSelectionMST);
            this.groupBoxAlg.Controls.Add(this.backstepButton);
            this.groupBoxAlg.Controls.Add(this.stepButton);
            this.groupBoxAlg.Controls.Add(this.pauseButton);
            this.groupBoxAlg.Controls.Add(this.waitTimeInput);
            this.groupBoxAlg.Controls.Add(this.algorithmSelectionSP);
            this.groupBoxAlg.Controls.Add(this.startAlgButton);
            this.groupBoxAlg.Controls.Add(this.clickFunctionStartingNode);
            this.groupBoxAlg.Location = new System.Drawing.Point(452, 3);
            this.groupBoxAlg.Name = "groupBoxAlg";
            this.groupBoxAlg.Size = new System.Drawing.Size(635, 110);
            this.groupBoxAlg.TabIndex = 5;
            this.groupBoxAlg.TabStop = false;
            this.groupBoxAlg.Text = "Algorithms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(277, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Maximum flow";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Minimum spanning tree";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Shortest path";
            // 
            // algorithmSelectionMF
            // 
            this.algorithmSelectionMF.FormattingEnabled = true;
            this.algorithmSelectionMF.Items.AddRange(new object[] {
            "Edmonds-Karp",
            "Dinic"});
            this.algorithmSelectionMF.Location = new System.Drawing.Point(280, 32);
            this.algorithmSelectionMF.Name = "algorithmSelectionMF";
            this.algorithmSelectionMF.Size = new System.Drawing.Size(131, 30);
            this.algorithmSelectionMF.TabIndex = 7;
            this.algorithmSelectionMF.Enter += new System.EventHandler(this.AlgorithmSelection_Enter);
            // 
            // algorithmSelectionMST
            // 
            this.algorithmSelectionMST.FormattingEnabled = true;
            this.algorithmSelectionMST.Items.AddRange(new object[] {
            "Jarník",
            "Borůvka",
            "Kruskal"});
            this.algorithmSelectionMST.Location = new System.Drawing.Point(143, 32);
            this.algorithmSelectionMST.Name = "algorithmSelectionMST";
            this.algorithmSelectionMST.Size = new System.Drawing.Size(131, 43);
            this.algorithmSelectionMST.TabIndex = 6;
            this.algorithmSelectionMST.Enter += new System.EventHandler(this.AlgorithmSelection_Enter);
            // 
            // backstepButton
            // 
            this.backstepButton.Location = new System.Drawing.Point(554, 68);
            this.backstepButton.Name = "backstepButton";
            this.backstepButton.Size = new System.Drawing.Size(34, 23);
            this.backstepButton.TabIndex = 12;
            this.backstepButton.Text = "<-";
            this.backstepButton.UseVisualStyleBackColor = true;
            this.backstepButton.Click += new System.EventHandler(this.BackstepButton_Click);
            // 
            // stepButton
            // 
            this.stepButton.Location = new System.Drawing.Point(594, 68);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(34, 23);
            this.stepButton.TabIndex = 13;
            this.stepButton.Text = "->";
            this.stepButton.UseVisualStyleBackColor = true;
            this.stepButton.Click += new System.EventHandler(this.StepButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.AutoSize = true;
            this.pauseButton.Location = new System.Drawing.Point(554, 45);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(75, 23);
            this.pauseButton.TabIndex = 11;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // waitTimeInput
            // 
            this.waitTimeInput.AutoSize = false;
            this.waitTimeInput.BackColor = System.Drawing.SystemColors.ControlDark;
            this.waitTimeInput.LargeChange = 100;
            this.waitTimeInput.Location = new System.Drawing.Point(417, 52);
            this.waitTimeInput.Maximum = 1001;
            this.waitTimeInput.Minimum = 1;
            this.waitTimeInput.Name = "waitTimeInput";
            this.waitTimeInput.Size = new System.Drawing.Size(131, 33);
            this.waitTimeInput.TabIndex = 10;
            this.waitTimeInput.Value = 100;
            // 
            // algorithmSelectionSP
            // 
            this.algorithmSelectionSP.FormattingEnabled = true;
            this.algorithmSelectionSP.Items.AddRange(new object[] {
            "Dijsktra"});
            this.algorithmSelectionSP.Location = new System.Drawing.Point(6, 32);
            this.algorithmSelectionSP.Name = "algorithmSelectionSP";
            this.algorithmSelectionSP.Size = new System.Drawing.Size(131, 17);
            this.algorithmSelectionSP.TabIndex = 5;
            this.algorithmSelectionSP.Enter += new System.EventHandler(this.AlgorithmSelection_Enter);
            // 
            // startAlgButton
            // 
            this.startAlgButton.AutoSize = true;
            this.startAlgButton.Location = new System.Drawing.Point(554, 16);
            this.startAlgButton.Name = "startAlgButton";
            this.startAlgButton.Size = new System.Drawing.Size(75, 23);
            this.startAlgButton.TabIndex = 9;
            this.startAlgButton.Text = "Start";
            this.startAlgButton.UseVisualStyleBackColor = true;
            this.startAlgButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // clickFunctionStartingNode
            // 
            this.clickFunctionStartingNode.FormattingEnabled = true;
            this.clickFunctionStartingNode.Items.AddRange(new object[] {
            "Choose starting point",
            "Choose sink"});
            this.clickFunctionStartingNode.Location = new System.Drawing.Point(417, 16);
            this.clickFunctionStartingNode.Name = "clickFunctionStartingNode";
            this.clickFunctionStartingNode.Size = new System.Drawing.Size(131, 30);
            this.clickFunctionStartingNode.TabIndex = 8;
            this.clickFunctionStartingNode.SelectedIndexChanged += new System.EventHandler(this.ClickFunctionChange);
            this.clickFunctionStartingNode.Leave += new System.EventHandler(this.ClickFunctionLeaveFocus);
            // 
            // groupBoxOthers
            // 
            this.groupBoxOthers.AutoSize = true;
            this.groupBoxOthers.Controls.Add(this.clearGraphButton);
            this.groupBoxOthers.Controls.Add(this.saveGraphButton);
            this.groupBoxOthers.Controls.Add(this.directedCheckBox);
            this.groupBoxOthers.Controls.Add(this.euclideanSpaceCheckBox);
            this.groupBoxOthers.Controls.Add(this.loadGraphButton);
            this.groupBoxOthers.Controls.Add(this.settingsButton);
            this.groupBoxOthers.Location = new System.Drawing.Point(1093, 3);
            this.groupBoxOthers.Name = "groupBoxOthers";
            this.groupBoxOthers.Size = new System.Drawing.Size(295, 87);
            this.groupBoxOthers.TabIndex = 7;
            this.groupBoxOthers.TabStop = false;
            this.groupBoxOthers.Text = "Others";
            // 
            // clearGraphButton
            // 
            this.clearGraphButton.Location = new System.Drawing.Point(133, 45);
            this.clearGraphButton.Name = "clearGraphButton";
            this.clearGraphButton.Size = new System.Drawing.Size(75, 23);
            this.clearGraphButton.TabIndex = 17;
            this.clearGraphButton.Text = "Clear";
            this.clearGraphButton.UseVisualStyleBackColor = true;
            this.clearGraphButton.Click += new System.EventHandler(this.ClearGraphButton_Click);
            // 
            // saveGraphButton
            // 
            this.saveGraphButton.Location = new System.Drawing.Point(214, 16);
            this.saveGraphButton.Name = "saveGraphButton";
            this.saveGraphButton.Size = new System.Drawing.Size(75, 23);
            this.saveGraphButton.TabIndex = 18;
            this.saveGraphButton.Text = "Save graph";
            this.saveGraphButton.UseVisualStyleBackColor = true;
            this.saveGraphButton.Click += new System.EventHandler(this.SaveGraphButton_Click);
            // 
            // directedCheckBox
            // 
            this.directedCheckBox.AutoSize = true;
            this.directedCheckBox.Location = new System.Drawing.Point(6, 42);
            this.directedCheckBox.Name = "directedCheckBox";
            this.directedCheckBox.Size = new System.Drawing.Size(96, 17);
            this.directedCheckBox.TabIndex = 15;
            this.directedCheckBox.Text = "Directed graph";
            this.directedCheckBox.UseVisualStyleBackColor = true;
            this.directedCheckBox.CheckedChanged += new System.EventHandler(this.DirectedCheckBox_CheckedChanged);
            // 
            // euclideanSpaceCheckBox
            // 
            this.euclideanSpaceCheckBox.AutoSize = true;
            this.euclideanSpaceCheckBox.Location = new System.Drawing.Point(6, 20);
            this.euclideanSpaceCheckBox.Name = "euclideanSpaceCheckBox";
            this.euclideanSpaceCheckBox.Size = new System.Drawing.Size(121, 17);
            this.euclideanSpaceCheckBox.TabIndex = 14;
            this.euclideanSpaceCheckBox.Text = "Euclidean distances";
            this.euclideanSpaceCheckBox.UseVisualStyleBackColor = true;
            this.euclideanSpaceCheckBox.CheckedChanged += new System.EventHandler(this.EuclideanSpaceCheckBox_CheckedChanged);
            // 
            // loadGraphButton
            // 
            this.loadGraphButton.Location = new System.Drawing.Point(214, 45);
            this.loadGraphButton.Name = "loadGraphButton";
            this.loadGraphButton.Size = new System.Drawing.Size(75, 23);
            this.loadGraphButton.TabIndex = 19;
            this.loadGraphButton.Text = "Load graph";
            this.loadGraphButton.UseVisualStyleBackColor = true;
            this.loadGraphButton.Click += new System.EventHandler(this.LoadGraphButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(133, 16);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(75, 23);
            this.settingsButton.TabIndex = 16;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // graph
            // 
            this.graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graph.Location = new System.Drawing.Point(3, 125);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(1420, 373);
            this.graph.TabIndex = 2;
            this.graph.TabStop = false;
            this.graph.Paint += new System.Windows.Forms.PaintEventHandler(this.Graph_Paint);
            this.graph.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Graph_MouseClick);
            this.graph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Graph_MouseDown);
            this.graph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Graph_MouseMove);
            this.graph.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Graph_MouseUp);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "GraphML file|*.graphml";
            this.saveFileDialog1.InitialDirectory = "c:\\Users\\mhuba\\Documents\\Graphs\\";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "GraphML file|*.graphml";
            this.openFileDialog1.InitialDirectory = "c:\\Users\\mhuba\\Documents\\Graphs\\";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1426, 501);
            this.Controls.Add(this.tableLayoutMain);
            this.Name = "FormMain";
            this.Text = "Graphs";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutMain.ResumeLayout(false);
            this.tableLayoutMain.PerformLayout();
            this.flowLayoutControls.ResumeLayout(false);
            this.flowLayoutControls.PerformLayout();
            this.groupBoxGraphCreation.ResumeLayout(false);
            this.groupBoxGraphCreation.PerformLayout();
            this.groupBoxAlg.ResumeLayout(false);
            this.groupBoxAlg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waitTimeInput)).EndInit();
            this.groupBoxOthers.ResumeLayout(false);
            this.groupBoxOthers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutControls;
        private System.Windows.Forms.GroupBox groupBoxGraphCreation;
        private System.Windows.Forms.ListBox clickFunctionEdge;
        private System.Windows.Forms.ListBox clickFunctionNode;
        private System.Windows.Forms.MaskedTextBox edgeValueInput;
        private System.Windows.Forms.ListBox clickFunctionClick;
        private System.Windows.Forms.GroupBox groupBoxAlg;
        private System.Windows.Forms.ListBox algorithmSelectionSP;
        private System.Windows.Forms.Button startAlgButton;
        private System.Windows.Forms.ListBox clickFunctionStartingNode;
        private System.Windows.Forms.PictureBox graph;
        private System.Windows.Forms.TrackBar waitTimeInput;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.GroupBox groupBoxOthers;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button backstepButton;
        private System.Windows.Forms.Button stepButton;
        private System.Windows.Forms.CheckBox euclideanSpaceCheckBox;
        private System.Windows.Forms.CheckBox directedCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox algorithmSelectionMF;
        private System.Windows.Forms.ListBox algorithmSelectionMST;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button saveGraphButton;
        private System.Windows.Forms.Button loadGraphButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button clearGraphButton;
    }
}


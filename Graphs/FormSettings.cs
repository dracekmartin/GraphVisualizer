using System;
using System.Windows.Forms;

namespace Graphs
{
    public partial class FormSettings : Form
    {
        FormMain mainForm;
        public FormSettings(FormMain form)
        {
            InitializeComponent();
            mainForm = form;
            nodeUpDown.Value = mainForm.nodesize;
            edgeUpDown.Value = mainForm.edgesize;
            textUpDown.Value = mainForm.textsize;
            arrowUpDown.Value = (decimal)mainForm.arrowsize;
        }

        private void FormSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.Enabled = true;
            mainForm.RecolorToBase();
        }

        private void VertexBaseColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            cd.Color = mainForm.nodeBaseColor;
            cd.ShowDialog();
            mainForm.nodeBaseColor = cd.Color;
            mainForm.RecolorToBase();
        }

        private void edgeBaseColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            cd.Color = mainForm.edgeBaseColor;
            cd.ShowDialog();
            mainForm.edgeBaseColor = cd.Color;
            mainForm.RecolorToBase();
        }

        private void textBaseColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            cd.Color = mainForm.textBaseColor;
            cd.ShowDialog();
            mainForm.textBaseColor = cd.Color;
            mainForm.RecolorToBase();
        }

        private void smallHiglightColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            cd.Color = mainForm.smallHiglightColor;
            cd.ShowDialog();
            mainForm.smallHiglightColor = cd.Color;
            mainForm.RecolorToBase();
        }

        private void mediumHiglightColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            cd.Color = mainForm.mediumHiglightColor;
            cd.ShowDialog();
            mainForm.mediumHiglightColor = cd.Color;
            mainForm.RecolorToBase();
        }

        private void bigHiglightColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            cd.Color = mainForm.bigHiglightColor;
            cd.ShowDialog();
            mainForm.bigHiglightColor = cd.Color;
            mainForm.RecolorToBase();
        }

        private void textUpDown_ValueChanged(object sender, EventArgs e)
        {
            int fn = (int)textUpDown.Value;
            mainForm.textsize = fn;
            foreach(Edge edge in mainForm.edges)
            {
                edge.TextSize = fn;
            }
            foreach (Node node in mainForm.nodes)
            {
                node.TextSize = fn;
            }
            mainForm.Refresh();
        }

        private void edgeUpDown_ValueChanged(object sender, EventArgs e)
        {
            int fn = (int)edgeUpDown.Value;
            mainForm.edgesize = fn;
            foreach (Edge edge in mainForm.edges)
            {
                edge.Size = fn;
            }
            mainForm.Refresh();
        }

        private void nodeUpDown_ValueChanged(object sender, EventArgs e)
        {
            int fn = (int)nodeUpDown.Value;
            mainForm.nodesize = fn;
            foreach (Node node in mainForm.nodes)
            {
                node.Size = fn;
            }
            mainForm.Refresh();
        }

        bool first = true;
        private void arrowUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!first && mainForm.directed == false)
            {
                mainForm.Check();
            }
            first = false;
            int fn = (int)arrowUpDown.Value;
            mainForm.arrowsize = fn;
            foreach (Edge edge in mainForm.edges)
            {
                edge.ArrowSize = fn;
            }
            mainForm.Refresh();
        }
    }
}

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
        }

        private void FormSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.Enabled = true;
            mainForm.Recolor();
        }

        private void VertexBaseColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            cd.Color = mainForm.nodeBaseColor;
            cd.ShowDialog();
            mainForm.nodeBaseColor = cd.Color;
            mainForm.Recolor();
        }

        private void edgeBaseColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            cd.Color = mainForm.edgeBaseColor;
            cd.ShowDialog();
            mainForm.edgeBaseColor = cd.Color;
            mainForm.Recolor();
        }

        private void textBaseColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            cd.Color = mainForm.textBaseColor;
            cd.ShowDialog();
            mainForm.textBaseColor = cd.Color;
            mainForm.Recolor();
        }

        private void smallHiglightColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            cd.Color = mainForm.smallHiglightColor;
            cd.ShowDialog();
            mainForm.smallHiglightColor = cd.Color;
            mainForm.Recolor();
        }

        private void mediumHiglightColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            cd.Color = mainForm.mediumHiglightColor;
            cd.ShowDialog();
            mainForm.mediumHiglightColor = cd.Color;
            mainForm.Recolor();
        }

        private void bigHiglightColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;
            cd.Color = mainForm.bigHiglightColor;
            cd.ShowDialog();
            mainForm.bigHiglightColor = cd.Color;
            mainForm.Recolor();
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace blend_animation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadImage(TextBox targetText, PictureBox targetPicture)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "すべてのファイル(*.*)|*.*";
                ofd.Title = "select image file";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    targetText.Text = Path.GetFileName(ofd.FileName);
                    targetPicture.ImageLocation = ofd.FileName;
                    targetPicture.Visible = true;
                }
            }
            catch
            {
                MessageBox.Show("error");
            }
        }

        private void buttonImage1_Click(object sender, EventArgs e)
        {
            LoadImage(this.textBox1, this.pictureBox1);
        }
        private void buttonImage2_Click(object sender, EventArgs e)
        {
            LoadImage(this.textBox2, this.pictureBox2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxDisplay.SelectedIndex = 0;
            comboBoxBlendType.SelectedIndex = 0;
        }

        private void comboBoxDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxDisplay.SelectedIndex)
            {
                case 0: // image1
                    pictureBox1.BringToFront();
                    break;
                case 1: // image2
                    pictureBox2.BringToFront();
                    break;
                case 2: // result
                    pictureBoxResult.BringToFront();
                    break;
            }
        }

        enum phase{
            DisplaySrc1,
            BlendToSrc1,
            DisplaySrc2,
            BlendToSrc2,
        }
        const int stepPercent = 10;
        int currPercent = 0;
        private void timerAnimation_Tick(object sender, EventArgs e)
        {

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            currPercent = 0;
            timerAnimation.Start();
        }

    }
}

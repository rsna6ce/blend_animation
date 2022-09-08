using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
                MessageBox.Show("Error: cannot load image file.");
            }
        }

        private void buttonImage1_Click(object sender, EventArgs e)
        {
            LoadImage(this.textBox1, this.pictureBox1);
            comboBoxDisplay.SelectedIndex = 0;
        }
        private void buttonImage2_Click(object sender, EventArgs e)
        {
            LoadImage(this.textBox2, this.pictureBox2);
            comboBoxDisplay.SelectedIndex = 1;
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
        const int originalFrame = 5;
        phase currPhase = phase.DisplaySrc1;
        int currPercent = 0;
        int currFrame = 0;
        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            DrawResult();
            switch (currPhase)
            {
                case phase.DisplaySrc1:
                    currFrame ++;
                    if (currFrame > originalFrame)
                    {
                        currPhase = phase.BlendToSrc1;
                        currPercent = 100;
                    }
                    break;

                case phase.BlendToSrc1:
                    currPercent-= stepPercent;
                    if (currPercent < 0)
                    {
                        currPhase = phase.DisplaySrc2;
                        currFrame = 0;
                    }
                    break;

                case phase.DisplaySrc2:
                    currFrame ++;
                    if (currFrame > originalFrame)
                    {
                        currPhase = phase.BlendToSrc2;
                        currPercent = 0;
                    }
                    break;
                case phase.BlendToSrc2:
                    currPercent += stepPercent;
                    if (100 < currPercent)
                    {
                        timerAnimation.Stop();
                        buttonStart.Enabled = true;
                    }
                    break;
            }
        }

        private void DrawResultBlend()
        {
            //Bitmapをロックする
            Bitmap bmpImg1 = (Bitmap)pictureBox1.Image;
            Bitmap bmpImg2 = (Bitmap)pictureBox2.Image;
            Bitmap bmpImg3 = (Bitmap)pictureBoxResult.Image;
            BitmapData bmpData1 = bmpImg1.LockBits(new Rectangle(0, 0, bmpImg1.Width, bmpImg1.Height), ImageLockMode.ReadOnly, bmpImg1.PixelFormat);
            BitmapData bmpData2 = bmpImg2.LockBits(new Rectangle(0, 0, bmpImg2.Width, bmpImg2.Height), ImageLockMode.ReadOnly, bmpImg2.PixelFormat);
            BitmapData bmpData3 = bmpImg3.LockBits(new Rectangle(0, 0, bmpImg3.Width, bmpImg3.Height), ImageLockMode.ReadWrite, bmpImg3.PixelFormat);

            if (bmpData1.Stride < 0)
            {
                bmpImg1.UnlockBits(bmpData1);
                bmpImg2.UnlockBits(bmpData2);
                bmpImg3.UnlockBits(bmpData3);
                MessageBox.Show("Error: invalid format bottom to up.");
            }

            IntPtr ptr1 = bmpData1.Scan0;
            IntPtr ptr2 = bmpData2.Scan0;
            IntPtr ptr3 = bmpData3.Scan0;
            byte[] pixels1 = new byte[bmpData1.Stride * bmpImg1.Height];
            byte[] pixels2 = new byte[bmpData2.Stride * bmpImg2.Height];
            byte[] pixels3 = new byte[bmpData3.Stride * bmpImg3.Height];
            System.Runtime.InteropServices.Marshal.Copy(ptr1, pixels1, 0, pixels1.Length);
            System.Runtime.InteropServices.Marshal.Copy(ptr2, pixels2, 0, pixels2.Length);
            //System.Runtime.InteropServices.Marshal.Copy(ptr3, pixels3, 0, pixels3.Length);

            int pixelSize1 = Image.GetPixelFormatSize(bmpImg1.PixelFormat) / 8;
            double alpha1 = (double)currPercent / 100.0;
            double alpha2 = 1.0 - alpha1;
            for (int y = 0; y < bmpData1.Height; y++)
            {
                if (this.comboBoxBlendType.Text == "H-Gradation")
                {
                    alpha1 = Math.Min(Math.Max(alpha1 - ((double)(y-bmpData1.Height/2) / (double)bmpData1.Height * 0.0005), 0.0),1.0);
                    alpha2 = 1.0 - alpha1;
                }
                for (int x = 0; x < bmpData1.Width; x++)
                {
                    int pos = y * bmpData1.Stride + x * pixelSize1;
                    //青、緑、赤の色を変更する
                    pixels3[pos + 0] = (byte)Math.Min(Math.Max((int)((double)pixels1[pos + 0] * alpha1 + (double)pixels2[pos + 0] * alpha2 + 0.5), 0), 255);
                    pixels3[pos + 1] = (byte)Math.Min(Math.Max((int)((double)pixels1[pos + 1] * alpha1 + (double)pixels2[pos + 1] * alpha2 + 0.5), 0), 255);
                    pixels3[pos + 2] = (byte)Math.Min(Math.Max((int)((double)pixels1[pos + 2] * alpha1 + (double)pixels2[pos + 2] * alpha2 + 0.5), 0), 255);
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(pixels3, 0, ptr3, pixels3.Length);

            bmpImg1.UnlockBits(bmpData1);
            bmpImg2.UnlockBits(bmpData2);
            bmpImg3.UnlockBits(bmpData3);
            pictureBoxResult.Refresh();

        }

        private void DrawResult()
        {
            switch (currPhase)
            {
                case phase.DisplaySrc1:
                    pictureBoxResult.Image = (Image)pictureBox1.Image.Clone();
                    break;
                case phase.DisplaySrc2:
                    pictureBoxResult.Image = (Image)pictureBox2.Image.Clone();
                    break;
                default:
                    DrawResultBlend();
                    break;
            }
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Size != pictureBox2.Size)
            {
                MessageBox.Show("ERROR: image1 and image2 are different size.");
                return;
            }

            PixelFormat pixelFormat1 = this.pictureBox1.Image.PixelFormat;
            PixelFormat pixelFormat2 = this.pictureBox2.Image.PixelFormat;
            int pixelSize1 = Image.GetPixelFormatSize(pixelFormat1) / 8;
            int pixelSize2 = Image.GetPixelFormatSize(pixelFormat2) / 8;
            if (pixelSize1 != pixelSize2 || 
                pixelSize1 < 3 || 4 < pixelSize1 || 
                pixelSize2 < 3 || 4 < pixelSize2)
            {
                MessageBox.Show("Error: invalid image color format.");
            }

            currFrame = 0;
            currPercent = 0;
            currPhase = phase.DisplaySrc1;
            pictureBoxResult.Visible = true;
            pictureBoxResult.Size = pictureBox1.Size;
            comboBoxDisplay.SelectedIndex = 2; // result
            timerAnimation.Start();
            buttonStart.Enabled = false;
        }

    }
}

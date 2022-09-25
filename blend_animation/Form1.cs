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
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        public struct FileInfo
        {
            public string fullpath;
            public string path;
            public Bitmap bmp;
        }
        private List<FileInfo> files;

        private struct FrameInfo
        {
            public bool originalFrame;
            public Bitmap bmp0;
            public Bitmap bmp1;
            public int rate_percent;
        }
        Queue<FrameInfo> queue_frames;

        List<MyGifEncorder.BitmapAndDelayTime> gif_frames;

        public Form1()
        {
            InitializeComponent();
#if DEBUG
            AllocConsole();
#endif
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxBlendType.SelectedIndex = 0;
            comboBoxBlendStep.SelectedIndex = 2;
            comboBoxLoopType.SelectedIndex = 0;
            files = new List<FileInfo>();
            queue_frames = new Queue<FrameInfo>();
            gif_frames = new List<MyGifEncorder.BitmapAndDelayTime>();
        }

        private bool add_file(string filename)
        {
            Bitmap bmptarget = new Bitmap(filename);

            int pixelSize = Image.GetPixelFormatSize(bmptarget.PixelFormat) / 8;
            if (pixelSize < 3 || 4 < pixelSize)
            {
                MessageBox.Show("Error: invalid color format, this program supports RGB or RGBA format.");
                return false;
            }
            if (files.Count > 0)
            {
                if (files[0].bmp.Width != bmptarget.Width || files[0].bmp.Height != bmptarget.Height)
                {
                    MessageBox.Show("Error: differnet image size.");
                    return false;
                }
            }
            FileInfo finfo = new FileInfo();
            finfo.fullpath = filename;
            finfo.path = Path.GetFileName(filename);
            finfo.bmp = bmptarget;
            files.Add(finfo);
            listBoxFiles.Items.Add(finfo.path);
            listBoxFiles.SelectedIndex = listBoxFiles.Items.Count - 1;
            return true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
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
                    add_file(ofd.FileName);
                }
            }
            catch
            {
                MessageBox.Show("Error: cannot load image file.");
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxFiles.Items.Count == 0)
            {
                return;
            }
            int index = listBoxFiles.SelectedIndex;
            listBoxFiles.Items.Remove(listBoxFiles.Items[index]);
            files.RemoveAt(index);
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedIndex <= 0)
            {
                return;
            }
            int index0 = listBoxFiles.SelectedIndex -1;
            int index1 = listBoxFiles.SelectedIndex;
            string temp_item = listBoxFiles.Items[index0].ToString();
            listBoxFiles.Items[index0] = listBoxFiles.Items[index1];
            listBoxFiles.Items[index1] = temp_item;
            FileInfo temp_info = files[index0];
            files[index0] = files[index1];
            files[index1] = temp_info;
            listBoxFiles.SelectedIndex = listBoxFiles.SelectedIndex - 1;
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedIndex > listBoxFiles.Items.Count - 2)
            {
                return;
            }
            int index0 = listBoxFiles.SelectedIndex;
            int index1 = listBoxFiles.SelectedIndex + 1;
            string temp_item = listBoxFiles.Items[index0].ToString();
            listBoxFiles.Items[index0] = listBoxFiles.Items[index1];
            listBoxFiles.Items[index1] = temp_item;
            FileInfo temp_info = files[index0];
            files[index0] = files[index1];
            files[index1] = temp_info;
            listBoxFiles.SelectedIndex = listBoxFiles.SelectedIndex + 1;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (files.Count < 2)
            {
                return;
            }
            listBoxFiles.SelectedIndex = -1;
            queue_frames.Clear();
            gif_frames.Clear();

            int loopCount = files.Count;
            if (comboBoxLoopType.Text == "Round trip")
            {
                // 往復は折り返す一つ手前まで
                loopCount -= 1;
            }

            //ループと往復共通
            for (int i = 0; i < loopCount; i++)
            {
                int index_curr = i;
                int index_next = i < files.Count-1 ? i+1 : 0;

                FrameInfo fi = new FrameInfo();
                fi.originalFrame = true;
                fi.bmp0 = files[index_curr].bmp;
                fi.bmp1 = null;
                fi.rate_percent = 0;
                queue_frames.Enqueue(fi);
                Console.WriteLine(string.Format("orig:{0} curr:{1}, next:{2}, rate:{3}",fi.originalFrame, index_curr, index_next, fi.rate_percent));

                fi.originalFrame = false;
                fi.bmp1 = files[index_next].bmp;
                int blend_step = int.Parse(comboBoxBlendStep.Text.Replace("%", ""));
                for (int step = 100 - blend_step; 0 < step; step -= blend_step)
                {
                    fi.rate_percent = step;
                    queue_frames.Enqueue(fi);
                    Console.WriteLine(string.Format("orig:{0} curr:{1}, next:{2}, rate:{3}", fi.originalFrame, index_curr, index_next, fi.rate_percent));
                }
            }

            //　往復の往路
            if (comboBoxLoopType.Text == "Round trip")
            {
                Console.WriteLine("------------------------------------------------");
                for (int i = files.Count-1; 1<= i; i--)
                {
                    int index_curr = i;
                    int index_next = i - 1;

                    FrameInfo fi = new FrameInfo();
                    fi.originalFrame = true;
                    fi.bmp0 = files[index_curr].bmp;
                    fi.bmp1 = null;
                    fi.rate_percent = 0;
                    queue_frames.Enqueue(fi);
                    Console.WriteLine(string.Format("orig:{0} curr:{1}, next:{2}, rate:{3}", fi.originalFrame, index_curr, index_next, fi.rate_percent));

                    fi.originalFrame = false;
                    fi.bmp0 = files[index_next].bmp;
                    fi.bmp1 = files[index_curr].bmp;
                    int blend_step = int.Parse(comboBoxBlendStep.Text.Replace("%", ""));
                    for (int step = blend_step; step < 100; step += blend_step)
                    {
                        fi.rate_percent = step;
                        queue_frames.Enqueue(fi);
                        Console.WriteLine(string.Format("orig:{0} curr:{1}, next:{2}, rate:{3}", fi.originalFrame, index_curr, index_next, fi.rate_percent));
                    }
                }
            }

            timerRender.Start();
        }

        private void timerRender_Tick(object sender, EventArgs e)
        {
            timerRender.Enabled = false;
            FrameInfo fi = queue_frames.Dequeue();

            if (fi.originalFrame)
            {
                gif_frames.Add(new MyGifEncorder.BitmapAndDelayTime(fi.bmp0, (ushort)numericUpDownDelayOriginal.Value));
                pictureBoxResult.Image = (Image)fi.bmp0.Clone();
            }
            else
            {
                //Bitmapをロックする
                Bitmap bmpImg1 = fi.bmp0;
                Bitmap bmpImg2 = fi.bmp1;
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
                double alpha1 = (double)fi.rate_percent / 100.0;
                double alpha2 = 1.0 - alpha1;
                for (int y = 0; y < bmpData1.Height; y++)
                {
                    if (this.comboBoxBlendType.Text == "H-Gradation")
                    {
                        const int blendAreaHeight = 100;
                        int baseY = bmpData1.Height * (fi.rate_percent * (100 + blendAreaHeight * 2) / 100) / (100 + blendAreaHeight * 2);
                        int diffY = baseY - y;
                        alpha1 = Math.Min(Math.Max((double)diffY / (double)blendAreaHeight * 3, 0.0), 1.0);
                        alpha2 = 1.0 - alpha1;
                    }
                    for (int x = 0; x < bmpData1.Width; x++)
                    {
                        int pos = y * bmpData1.Stride + x * pixelSize1;
                        //青、緑、赤の色を変更する
                        pixels3[pos + 0] = (byte)Math.Min(Math.Max((int)((double)pixels1[pos + 0] * alpha1 + (double)pixels2[pos + 0] * alpha2 + 0.5), 0), 255);
                        pixels3[pos + 1] = (byte)Math.Min(Math.Max((int)((double)pixels1[pos + 1] * alpha1 + (double)pixels2[pos + 1] * alpha2 + 0.5), 0), 255);
                        pixels3[pos + 2] = (byte)Math.Min(Math.Max((int)((double)pixels1[pos + 2] * alpha1 + (double)pixels2[pos + 2] * alpha2 + 0.5), 0), 255);
                        if (pixelSize1 == 4)
                        {
                            // Alphaチャネル対応
                            pixels3[pos + 3] = 255;
                        }
                    }
                }

                System.Runtime.InteropServices.Marshal.Copy(pixels3, 0, ptr3, pixels3.Length);

                bmpImg1.UnlockBits(bmpData1);
                bmpImg2.UnlockBits(bmpData2);
                bmpImg3.UnlockBits(bmpData3);

                gif_frames.Add(new MyGifEncorder.BitmapAndDelayTime(new Bitmap(bmpImg3), (ushort)numericUpDownDelayBlend.Value));
            }

            pictureBoxResult.Refresh();


            if (queue_frames.Count > 0)
            {
                timerRender.Enabled = true;
            }
            else
            {
                // queue 全処理完了
                Console.WriteLine("================================================");
                if (!checkBoxPreview.Checked)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Title = "Blend Animation GIF";
                    DateTime dt = DateTime.Now;
                    saveFileDialog.FileName = dt.ToString("yyyyMMdd_HHmmss") + ".gif";
                    saveFileDialog.Filter = "GIFファイル(*.gif)|*.gif";
                    DialogResult result = saveFileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        MyGifEncorder.SaveAnimatedGif(saveFileDialog.FileName, gif_frames, 0);
                    }
                }
            }
        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (0 <= listBoxFiles.SelectedIndex && listBoxFiles.SelectedIndex < listBoxFiles.Items.Count)
            {
                pictureBoxResult.Image = (Image)files[listBoxFiles.SelectedIndex].bmp;
            }
            else
            {
                pictureBoxResult.Image = null;
                pictureBoxResult.Height = 0;
                pictureBoxResult.Width = 0;
            }
        }

        private void listBoxFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void listBoxFiles_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileNames =(string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (var fileName in fileNames) {
                if (!add_file(fileName)) {
                    break;
                }
            }
        }
    }
}

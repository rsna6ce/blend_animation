using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


// from https://qiita.com/kob58im/items/d76ef98de48a5a17165e

public static class MyGifEncorder
{
    public class BitmapAndDelayTime
    {
        public Bitmap bitmap;
        public ushort delayTime;
        public BitmapAndDelayTime(Bitmap _bitmap, ushort _delayTime)
        {
            bitmap = _bitmap;
            delayTime = _delayTime;
        }
    }
    public static void SaveAnimatedGif(string fileName, List<BitmapAndDelayTime> baseImages, ushort loopCount)
    {
        if (baseImages.Count == 0)
        {
            return;
        }

        //�������ݐ�̃t�@�C�����J��
        var writerFs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
        //BinaryWriter�ŏ�������
        var writer = new BinaryWriter(writerFs);
        var ms = new MemoryStream();

        try
        {
            bool hasGlobalColorTable = false;
            int colorTableSize = 0;
            int imagesCount = baseImages.Count;

            for (int i = 0; i < imagesCount; i++)
            {
                //�摜��GIF�ɕϊ����āAMemoryStream�ɓ����
                Bitmap bmp = baseImages[i].bitmap;
                bmp.Save(ms, ImageFormat.Gif);
                ms.Position = 0;

                if (i == 0)
                {
                    writer.Write(ReadBytes(ms, 6));   //�w�b�_���������� // "GIF89a" �̂͂�

                    // http://www.tohoho-web.com/wwwgif.htm#GIFHeader

                    //Logical Screen Descriptor
                    byte[] screenDescriptor = ReadBytes(ms, 7);
                    //Global Color Table�����邩�m�F
                    if ((screenDescriptor[4] & 0x80) != 0)
                    {
                        //Color Table�̃T�C�Y���擾
                        colorTableSize = screenDescriptor[4] & 0x07;
                        hasGlobalColorTable = true;
                    }
                    else
                    {
                        hasGlobalColorTable = false;
                    }
                    //Global Color Table���g��Ȃ�
                    //�L��z�F�\�t���O�ƍL��z�F�\�̐��@������
                    screenDescriptor[4] &= 0x78;
                    writer.Write(screenDescriptor);

                    //Application Extension
                    writer.Write(GetApplicationExtension(loopCount));
                }
                else
                {
                    //Header��Logical Screen Descriptor���X�L�b�v
                    ms.Position += 6 + 7;
                }

                byte[] colorTable = null;
                if (hasGlobalColorTable)
                {
                    //Color Table���擾
                    colorTable = ReadBytes(ms, (1 << (colorTableSize + 1)) * 3);
                }

                //Graphics Control Extension
                writer.Write(GetGraphicControlExtension(baseImages[i].delayTime));

                {
                    byte[] tmp = PeekBytes(ms, 2);
                    if (tmp[0] == 0x21 && tmp[1] == 0xF9)
                    {
                        //���Graphics Control Extension���X�L�b�v
                        ms.Position += 8;
                    }
                }

                //Image Descriptor
                byte[] imageDescriptor = ReadBytes(ms, 10);
                if (imageDescriptor[0] != 0x2C)
                { // Image Separator
                    throw new Exception("Unexpected format.");
                }
                if (!hasGlobalColorTable)
                {
                    //Local Color Table�������Ă��邩�m�F
                    if ((imageDescriptor[9] & 0x80) == 0)
                    {
                        throw new Exception("Not found local color table."); // not support
                    }
                    colorTableSize = imageDescriptor[9] & 0x07;//Color Table�̃T�C�Y���擾
                    //Color Table���擾
                    colorTable = ReadBytes(ms, (1 << (colorTableSize + 1)) * 3);
                }
                //����z�F�\�t���O (Local Color Table Flag) �Ƌ���z�F�\�̐��@��ǉ�
                imageDescriptor[9] |= (byte)(0x80 | colorTableSize);
                writer.Write(imageDescriptor);
                writer.Write(colorTable);                   //Local Color Table����������

                //Image Data���������� (�I�����͏������܂Ȃ�)
                writer.Write(ReadBytes(ms, (int)(ms.Length - ms.Position - 1)));

                if (i == imagesCount - 1)
                {
                    writer.Write((byte)0x3B);               //�I���� (Trailer)
                }

                ms.SetLength(0);                            //MemoryStream�����Z�b�g
            }
        }
        finally
        {
            ms.Close();
            writer.Close();
            writerFs.Close();
        }
    }

    private static byte[] ReadBytes(MemoryStream ms, int count)
    {
        byte[] bs = new byte[count];
        int n = ms.Read(bs, 0, count);
        if (n < count)
        {
            throw new Exception("ReadBytes failed.");
        }
        return bs;
    }

    private static byte[] PeekBytes(MemoryStream ms, int count)
    {
        byte[] bs = new byte[count];
        long pos = ms.Position;
        int n = ms.Read(bs, 0, count);
        ms.Position = pos; // position��߂�
        if (n < count)
        {
            throw new Exception("PeekBytes failed.");
        }
        return bs;
    }

    // loopCount: �J��Ԃ���(0 = ����)
    private static byte[] GetApplicationExtension(ushort loopCount)
    {
        byte[] bs = new byte[19] {
            0x21,               // [0] �g�������� (Extension Introducer)
            0xFF,               // [1] �A�v���P�[�V�����g�����x�� (Application Extension Label)
            0x0B,               // [2] �u���b�N���@ (Block Size)
                                // [3..10] "NETSCAPE"  �A�v���P�[�V�������ʖ� (Application Identifier)
            0x4E, 0x45, 0x54, 0x53, 0x43, 0x41, 0x50, 0x45,
            0x32, 0x2E, 0x30,   // [11..13] "2.0"  �A�v���P�[�V�����m�ؕ��� (Application Authentication Code)
            0x03,               // [14] �f�[�^���u���b�N���@ (Data Sub-block Size)
            0x01,               // [15] �l�ߍ��ݗ� [�l�b�g�X�P�[�v�g���R�[�h (Netscape Extension Code)]
            0x00, 0x00,         // [16..17] ���ȍ~�̏����ő��  �J��Ԃ��� (Loop Count)
            0x00                // [18] �u���b�N�I���� (Block Terminator)
        };
        bs[16] = (byte)(loopCount & 0xFF);
        bs[17] = (byte)((loopCount >> 8) & 0xFF);
        return bs;
    }

    // delayTime: �x������ (�P��:10ms)
    private static byte[] GetGraphicControlExtension(ushort delayTime)
    {
        byte[] bs = new byte[8]{
            0x21,       // [0] �g�������� (Extension Introducer)
            0xF9,       // [1] �O���t�B�b�N���䃉�x�� (Graphic Control Label)
            0x04,       // [2] �u���b�N���@ (Block Size, Byte Size)

            0x00,       // [3] �l�ߍ��ݗ� (Packed Field)
                        //     bit 0:            1=���ߐF�w�W���g��
                        //     bit 3-2: �������@: 1=���̂܂܎c��  2=�w�i�F�łԂ�  3=���O�̉摜�ɖ߂�

            0x00, 0x00, // [4..5] ���ȍ~�̏����ő��   �x������ (Delay Time)
            0x00,       // [6] ���ߐF�w�W (Transparency Index, Transparent Color Index)
            0x00        // [7] �u���b�N�I���� (Block Terminator)
        };
        bs[4] = (byte)(delayTime & 0xFF);
        bs[5] = (byte)((delayTime >> 8) & 0xFF);
        return bs;
    }
}


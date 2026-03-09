using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳APP.Models.DTOs;

namespace 記帳APP.Utility
{
    internal class ImageCompress
    {
        public static Bitmap CompressSmallImage(Image Pic, int newWidth, int newHeight)
        {
            Bitmap originalImage = new Bitmap(Pic);
            Bitmap resizedImage = new Bitmap(newWidth, newHeight);

            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }

            return resizedImage;
        }
        public static Bitmap CompressImage(Image Pic)
        {
            using (Bitmap bmp1 = new Bitmap(Pic))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID  
                // for the Quality parameter category.  
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object.  
                // An EncoderParameters object has an array of EncoderParameter  
                // objects. In this case, there is only one  
                // EncoderParameter object in the array.  
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 15L);//品質0到100分的中間值50分
                myEncoderParameters.Param[0] = myEncoderParameter;
                //bmp1.Save(pic1, jpgEncoder, myEncoderParameters);

                MemoryStream myStream = new MemoryStream();
                bmp1.Save(myStream, jpgEncoder, myEncoderParameters);
                Bitmap bmp2 = new Bitmap(myStream);
                return bmp2;
            }
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

    }
}

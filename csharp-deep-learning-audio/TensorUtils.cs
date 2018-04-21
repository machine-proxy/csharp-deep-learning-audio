using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TensorFlow;

namespace csharp_deep_learning_audio
{
    public class TensorUtils
    {
        public static TFTensor GetImageTensor(Bitmap image, int imgWidth, int imgHeight)
        {
            int channels = 3;

            // System.out.println("width: " + imgWidth + ", height: " + imgHeight);
            // Generate image file to array
            int index = 0;
            float[] fb = new float[imgWidth * imgHeight * channels];
            // Convert image file to multi-dimension array

            for (int row = 0; row < imgHeight; row++)
            {
                for (int column = 0; column < imgWidth; column++)
                {
                    Color pixel = image.GetPixel(column, row);
                    
                    float red = pixel.R;
                    float green = pixel.G;
                    float blue = pixel.B;
                    fb[index++]=red;
                }
            }
            
            return TFTensor.FromBuffer(new TFShape(new long[] {1, imgHeight, imgWidth, channels }), fb, 0, fb.Length);
        }
    }
}

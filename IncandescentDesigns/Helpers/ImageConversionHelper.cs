using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace IncandescentDesigns.Helpers
{
    public class ImageConversionHelper
    {
        public static Image PictureFromByteArray(byte[] input)
        {
            if(input == null)
            {
                return null;
            }
            using (var ms = new MemoryStream(input))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
using MainProject.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
    class CUSTOMPRODUCT : BaseViewModel
    {
        public PRODUCT product;
        public Image Image_product;

        public CUSTOMPRODUCT()
        {
        }

        public CUSTOMPRODUCT (PRODUCT p)
        {
            product = p;
            if (p.Image != null) Image_product = byteArrayToImage(p.Image);
            else Image_product = null;
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }

    }
}

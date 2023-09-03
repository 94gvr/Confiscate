using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confiscate
{
    public class ImageInfo
    {
        public string ImagePath { get; }
        public int Id { get; }

        public ImageInfo(string imagePath, int id)
        {
            ImagePath = imagePath;
            Id = id;
        }
    }
}

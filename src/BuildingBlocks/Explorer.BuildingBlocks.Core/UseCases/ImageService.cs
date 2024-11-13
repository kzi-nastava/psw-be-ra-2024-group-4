using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Core.UseCases;

public abstract class ImageService: IImageService
{
    public ImageService() { }

    public string SaveImage(string folderPath, byte[] imageData)
    {
        var fileName = Guid.NewGuid() + ".png";

        if(!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var filePath = Path.Combine(folderPath, fileName);

        System.IO.File.WriteAllBytes(filePath, imageData);
        return $"images/keypoints/{fileName}";



    }
}

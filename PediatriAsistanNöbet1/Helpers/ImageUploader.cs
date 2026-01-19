using System;
using System.IO;
using System.Web;
using System.Web.Helpers;

namespace PediatriAsistanNöbet1.Helpers
{
    public static class ImageUploader
    {
        // Resmi yükler ve kaydedilen yolunu (path) string olarak döner.
        public static string UploadImage(HttpPostedFileBase file, string serverPath)
        {
            if (file == null) return null;

            // Dosya ismini benzersiz yap (Guid kullan)
            FileInfo imginfo = new FileInfo(file.FileName);
            string uniqueFileName = Guid.NewGuid().ToString() + imginfo.Extension;

            // Resmi işle
            WebImage img = new WebImage(file.InputStream);
            img.Resize(1024, 360); // Boyutlandırma

            // Kaydet
            string savePath = Path.Combine(serverPath, uniqueFileName);
            img.Save(savePath);

            // Veritabanına kaydedilecek yolu dön
            return "/Uploads/Slider/" + uniqueFileName;
        }
        public static void DeleteImage(string imagePath, string serverMapPath)
        {
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(serverMapPath))
            {
                File.Delete(serverMapPath);
            }
        }
    }
}
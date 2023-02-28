namespace Company.PL.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. Get Located Folder Path
            //var folderPath = @"E:\MVCS2\wwwroot\files\" + folderName;
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/files", folderName);
            // 2. Get File Name and Make its Name UNIQUE
            var fileName = $"{Guid.NewGuid()}{Path.GetFileName(file.FileName)}";
            // 3. Get File Path
            var filePath = Path.Combine(folderPath, fileName);
            // 4. Save File as Streams
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }
    }
}

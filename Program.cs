// See https://aka.ms/new-console-template for more information
using ReadMyPc;

WriteLine("Enter the file path");
string directoryPath = ReadLine();

if (string.IsNullOrEmpty(directoryPath))
    directoryPath = "C:\\Users\\User\\Downloads";

string[] files = Directory.GetFiles(directoryPath);
List<CustomFileElement> AllFiles = new();
HashSet<string> possibleExtensions = new();

foreach (var file in files)
{
    AllFiles.Add(new CustomFileElement
    {
        Extension = Path.GetExtension(file),
        Name = Path.GetFileNameWithoutExtension(file)
    });

    possibleExtensions.Add(Path.GetExtension(file).ToLower());
}

foreach (var ext in possibleExtensions)
{
    WriteLine(ext);
    string[] allPossibleExts = GetAllPossibleExts(ext);
    var findCommonExt = AllFiles.Where(x => allPossibleExts.Contains(x.Extension.ToLower())).ToList();

    DumpAllInsideAFolder(findCommonExt, allPossibleExts);
}


Console.WriteLine("Done");

void DumpAllInsideAFolder(List<CustomFileElement> findCommonExt, string[] allPossibleExts)
{
    string path = $"{directoryPath}\\ALL {string.Join(" & ", allPossibleExts.Select(m => m.Substring(1).ToUpper()))}";
    if (!Directory.Exists(path))
    {
        Directory.CreateDirectory(path);
    }

    foreach (var file in findCommonExt)
    {
        if (File.Exists($"{directoryPath}\\{file.Name}{file.Extension}"))
            File.Move($"{directoryPath}\\{file.Name}{file.Extension}", $"{path}\\{file.Name}{file.Extension}");
    }
}

string[] GetAllPossibleExts(string ext)
{
    if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".tif")
        return new string[] { ".png", ".jpeg", ".jpg", ".tif" };

    if (ext == ".csv" || ext == ".xls" || ext == ".xlsx")
        return new string[] { ".csv", ".xls", ".xlsx" };

    if (ext == ".doc" || ext == ".docx" || ext == ".txt" || ext == ".pages")
        return new string[] { ".docx", ".doc", ".txt", ".pages" };


    if (ext == ".zip" || ext == ".rar")
        return new string[] { ".zip", ".rar" };

    return new string[] { ext };
}
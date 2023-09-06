namespace DesktopClean
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderDirPath = @"C:\New folder\Folders";

            string fromPath = @"C:\test";
            string[] files = Directory.GetFiles(fromPath);
            string[] Directories = Directory.GetDirectories(fromPath);
            string[] both = files.Concat(Directories).ToArray();

            Console.Write("Type the extension you want to ignore splited by \", \" \n(Ex: .zip, .exe, .jpeg) \n If you dont have any extension press enter\n");
            string inputExtensionIgnore = Console.ReadLine().ToLower();
            if (inputExtensionIgnore.Length == 0)
                inputExtensionIgnore = @"nothingtoseehere/\./nothingtoseehere";

            Console.Write("Type the files you want to ignore splited by \", \" \n(Ex: test1.zip, game.exe, photo.jpeg) \n If you dont have any files press enter\n");
            string inputFileIgnore = Console.ReadLine().ToLower();
            if (inputFileIgnore.Length == 0)
                inputFileIgnore = @"nothingtoseehere/\./nothingtoseehere";

            string[] filesToIgnore = IgnoredFiles(inputFileIgnore);
            string[] extensionsToIgnore = IgnoredFiles(inputExtensionIgnore);

            Thread.Sleep(2500);

            List<string> fileExtension = new List<string>();
            decimal totalSize = CalculateFolderSize(fromPath);
            int numberOfElements = both.Length;

            int origRow = Console.CursorLeft;
            int origCol = Console.CursorTop;

            int maxNumberWidth = numberOfElements.ToString().Length;
            string message = "Number of elements remained: ";

            Console.SetCursorPosition(origRow, origCol);
            Console.Write(message);

            origRow += message.Length;

            //takes every file name from desktop files, gets the extension only (ex: ".torrent"), creates folders with file extension name
            //and copy file to specified extension folder
            foreach (string file in both)
            {
                string fileCheckForIgnore = file;
                string extensionCheckForIgnore = file;
                if (file.Contains(fromPath + @"\"))
                fileCheckForIgnore = file.Replace(fromPath + @"\", "");


                extensionCheckForIgnore = file.Replace(fromPath + @"\", "");
                extensionCheckForIgnore = GetToThePoint(extensionCheckForIgnore);

                
                if (filesToIgnore.Contains(fileCheckForIgnore) == false && extensionsToIgnore.Contains(extensionCheckForIgnore) == false)
                {
                   //sets cursor and writes number of elements at top left screen
                   WriteAt();
                   

                    string str = file.Replace(fromPath + @"\", "");
                    AddToList(str, str.Length);

                    string dir = @$"C:\New folder\{fileExtension.Last()}_Files";

                    //checks if directory exists at specified path, if not create new directory
                    if (!CheckIfDirectory(file) && !Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    //checks if file exists at the specified path, if not copy the file
                    if (!CheckIfDirectory(file) && !File.Exists(dir + "\\" + str))
                        File.Copy(file, dir + "\\" + str);
                }

                if(filesToIgnore.Contains(fileCheckForIgnore) == false)
                    MoveFolders(file);
            }

            // method that adds extensions (.torrent) to a list
            void AddToList(string input, int nameLength)
            {
                string wordToAdd = string.Empty;
                if (nameLength > 3)
                {
                    wordToAdd = GetToThePoint(input);
                    wordToAdd = wordToAdd.Replace(".", "");
                    fileExtension.Add(wordToAdd);
                }
                else
                {
                    wordToAdd = GetToThePoint(input);
                    wordToAdd = wordToAdd.Replace(".", "");
                    fileExtension.Add(wordToAdd);
                }
            }

            //returns extension -> .torrent
            string GetToThePoint(string input)
            {
                var index = input.LastIndexOf('.');
                string returnString = string.Empty;

                if (index > 0)
                    returnString = input.Substring(index, input.Length - index);
                else
                    returnString = input;

                return returnString;
            }

            decimal sizeMB = totalSize / 1000 / 1000;
            decimal sizeGB = totalSize / (1024 * 1024 * 1024);
            Console.WriteLine("\nTotal size: " + sizeMB.ToString("0.00") + " MB" + "\n\tOR\n" + "Total size: " + sizeGB.ToString("0.00") + " GB\n");

            void WriteAt()
            {
                Console.SetCursorPosition(origRow, origCol);

                // Format the current value with leading zeros to match the width of the widest number
                string formattedValue = (numberOfElements--).ToString().PadLeft(maxNumberWidth, '0');

                // Write the formatted value
                Console.Write(formattedValue);

                // Sleep for a while to display each number and ease a little bit the cpu
                Thread.Sleep(100);
            }

            string[] IgnoredFiles(string input)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    return null; 
                }

                input = input.Trim();
                string[] filezToIgnore = input.Split(", ");

                return filezToIgnore;
            }


            bool CheckIfDirectory(string input)
            {
                FileAttributes attr = File.GetAttributes(input);

                if (attr.HasFlag(FileAttributes.Directory))
                    return true;
                else
                    return false;
            }
            
            //moves folder to specified path
            void MoveFolders(string input)
            {
                if (!Directory.Exists(folderDirPath))
                    Directory.CreateDirectory(folderDirPath);

                if (CheckIfDirectory(input))
                {
                    string[] Directories = Directory.GetDirectories(fromPath);

                    if (!Directory.Exists(folderDirPath))
                    {
                        Directory.CreateDirectory(folderDirPath);

                    }

                    foreach (string dir in Directories)
                    {
                        if (Directory.Exists(dir))
                        {
                            string foldername = Path.GetFileName(dir);
                            try
                            {
                                Directory.Move(dir, folderDirPath + "\\" + foldername);
                            }
                            catch (IOException exp)
                            {
                                Console.WriteLine(exp.Message);
                            }
                        }
                        else
                            Directory.Move(dir, folderDirPath);
                    }
                }
            }
        }
        static long CalculateFolderSize(string folderPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);

            // Calculate the size of all files in the current directory
            long totalSize = directoryInfo.EnumerateFiles().Sum(file => file.Length);

            // Calculate the size of all subdirectories (recursive call)
            totalSize += directoryInfo.EnumerateDirectories().Sum(subdirectory => CalculateFolderSize(subdirectory.FullName));

            return totalSize;
        }
    }
}
namespace DesktopClean
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime today = DateTime.Now;
            int year = today.Year; 
            int month = today.Month; 
            int day = today.Day; 
            int hour = today.Hour; 
            int minute = today.Minute; 
            int second = today.Second;
            string todayDate = today.Year + "-" + today.Month + "-" + today.Day + " " + today.Hour + "-" + today.Minute + "-" + today.Second + " ";

            Console.Write(@"Path to clean ( C:\test ): ");
            string fromPath = @Console.ReadLine();
            Console.Write(@"Path to move everything ( C:\Moveto ): ");
            string toPath = @Console.ReadLine(); 

            decimal totalSize = CalculateFolderSize(fromPath);

            string[] files = Directory.GetFiles(fromPath);
            string[] Directories = Directory.GetDirectories(fromPath);
            string[] both = files.Concat(Directories).ToArray();

            Console.Write("Type the extension you want to ignore splited by \", \" \n(Ex: .zip, .exe, .jpeg) \n If you dont have any extension press enter\n");
            string inputExtensionIgnore = Console.ReadLine().ToLower();
            if (inputExtensionIgnore.Length == 0)
                inputExtensionIgnore = @"nothingtoseehere/\./nothingtoseehere";

            Console.Write("\nType the files you want to ignore splited by \", \" \n(Ex: test1.zip, game.exe, photo.jpeg) \n If you dont have any files press enter\n");
            string inputFileIgnore = Console.ReadLine().ToLower();
            if (inputFileIgnore.Length == 0)
                inputFileIgnore = @"nothingtoseehere/\./nothingtoseehere";

            string[] filesToIgnore = IgnoredFiles(inputFileIgnore);
            string[] extensionsToIgnore = IgnoredFiles(inputExtensionIgnore);
            List<string> ignoreAll = new List<string>();
            ignoreAll.AddRange(filesToIgnore);
            ignoreAll.AddRange(extensionsToIgnore);

            List<string> fileExtension = new List<string>();
           
            int numberOfElements = both.Length;

            int origRow = Console.CursorLeft;
            int origCol = Console.CursorTop;

            int maxNumberWidth = numberOfElements.ToString().Length;
            string message = "Number of elements remained: ";

            Console.WriteLine();
            Console.SetCursorPosition(origRow, origCol);
            Console.Write(message);

            origRow += message.Length;

            //takes every file name from fromPath files, gets the extension only (ex: ".torrent"), creates folders with file extension name
            //and copy file to specified extension folder ********file = C:\test\New WinRAR ZIP archive.zip******
            foreach (string file in both)
            {
                string extensionName = @"NothingToSeeHere/.\";
                string fileName = @"NothingToSeeHere/.\";

                fileName = Path.GetFileName(file);
                
                extensionName = Path.GetExtension(fileName);

                //only for folders
                if (CheckIfDirectory(file) && !ignoreAll.Contains(fileName.Trim().ToLower()))
                {
                    MoveFolders(fileName, file);
                }

                //only for file with extensions
                else if(!CheckIfDirectory(file) && !ignoreAll.Contains(extensionName.Trim().ToLower()) && !ignoreAll.Contains(fileName.Trim().ToLower()))
                {
                    if (!Directory.Exists(toPath + extensionName.Replace(".","") + "_Files"))
                    {
                        Directory.CreateDirectory(toPath + @"\"+extensionName.Replace(".", "") + "_Files");
                        if(File.Exists(toPath + @"\" + extensionName.Replace(".", "") + @"_Files\" + fileName))
                        {
                            File.Move(file, toPath + @"\" + extensionName.Replace(".", "") + @"_Files\" + fileName + " moved on " + todayDate + extensionName);
                        }
                        else
                        {
                        File.Move(file, toPath + @"\" + extensionName.Replace(".", "") + @"_Files\" + fileName);
                        }
                    }
                    else
                    {
                        File.Move(file, toPath +@"\"+ extensionName.Replace(".", "") + @"_Files\"+fileName);
                    }
                }
                WriteAt();
            }

            decimal sizeMB = totalSize / 1000 / 1000;
            decimal sizeGB = totalSize / (1024 * 1024 * 1024);
            Console.WriteLine("\nTotal size: " + sizeMB.ToString("0.00") + " MB" + "\n\tOR\n" + "Total size: " + sizeGB.ToString("0.00") + " GB\n");

            Console.ReadLine();
            void WriteAt()
            {
                Console.SetCursorPosition(origRow, origCol);

                // Format the current value with leading zeros to match the width of the widest number
                string formattedValue = (numberOfElements--).ToString().PadLeft(maxNumberWidth, '0');

                // Write the formatted value
                Console.Write(formattedValue+" ");

                // Sleep for a while to display each number and ease a little bit the cpu
                Thread.Sleep(100);
            }

            string[] IgnoredFiles(string input)
            {
                if (string.IsNullOrWhiteSpace(input))
                    return null; 
                
                input = input.Trim();
                string[] filezToIgnore = input.Split(", ");

                return filezToIgnore;
            }

            bool CheckIfDirectory(string input)
            {
                FileAttributes attr = File.GetAttributes(input);
                return attr.HasFlag(FileAttributes.Directory);
            }
            
            //moves folder to specified path
            void MoveFolders(string fileName, string file)
            {
                if (!Directory.Exists(toPath + @"\" + "Folder"))
                {
                    Directory.CreateDirectory(toPath + @"\" + "Folder");
                }

                if (Directory.Exists(toPath + @"\" + @"Folder\" + fileName))
                {
                    Directory.Move(file, toPath + @"\Folder\" + fileName + " moved on " + todayDate.TrimEnd());
                }
                else
                {
                    Directory.Move(file, toPath + @"\" + @"Folder\" + fileName);
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
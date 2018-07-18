using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeMeter
{
    class Program
    {
        static void Main(string[] args)
        {
            var parentFolder = Environment.CurrentDirectory;

            var directories = Directory.EnumerateDirectories(parentFolder);
            var file = File.CreateText("result.txt");
            
            foreach (var folder in directories) {
                var directory = new DirectoryInfo(folder);
                file.WriteLine($"{directory.Name} - {CalculateFolderSize(folder) / 1000000} Mb");
            }
            file.Close();
        }
        static double CalculateFolderSize(string folder)
        {
            double folderSize = 0.0f;
            try {
                //Checks if the path is valid or not
                if (!Directory.Exists( folder ))
                    return folderSize;
                else {
                    try {
                        foreach (string file in Directory.GetFiles( folder )) {
                            if (File.Exists( file )) {
                                FileInfo finfo = new FileInfo( file );
                                folderSize += finfo.Length;
                            }
                        }

                        foreach (string dir in Directory.GetDirectories( folder ))
                            folderSize += CalculateFolderSize( dir );
                    }
                    catch (NotSupportedException e) {
                        Console.WriteLine( "Unable to calculate folder size: {0}", e.Message );
                    }
                }
            }
            catch (UnauthorizedAccessException e) {
                Console.WriteLine( "Unable to calculate folder size: {0}", e.Message );
            }
            return folderSize;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace FinalTask
{
    namespace FinalTask
    {
        public class Students
        {
            public string Name { get; private set; }
            public string Group { get; private set; }
            public DateTime DateOfBirth { get; private set; }

            public Students(string name, string group, DateTime dateOfBirth)
            {
                Name = name;
                Group = group;
                DateOfBirth = dateOfBirth;
            }

            public static void LoadStudentsFromBinaryFile(string binaryFilePath)
            {
                if (!File.Exists(binaryFilePath))
                {
                    Console.WriteLine("Бинарный файл не найден.");
                    return;
                }

                string studentsDirectoryPath = "C:\\Users\\Stanislav\\Desktop\\Students";

                Directory.CreateDirectory(studentsDirectoryPath);

                using (BinaryReader reader = new BinaryReader(File.Open(binaryFilePath, FileMode.Open)))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        string name = reader.ReadString();
                        if (reader.BaseStream.Position >= reader.BaseStream.Length)
                        {
                            break; // Выход из цикла, если достигнут конец потока
                        }
                        string group = reader.ReadString();
                        if (reader.BaseStream.Position >= reader.BaseStream.Length)
                        {
                            break;
                        }
                        long dateBinary = reader.ReadInt64();
                        DateTime dateOfBirth = DateTime.FromBinary(dateBinary);

                        string groupFilePath = Path.Combine(studentsDirectoryPath, group + ".txt");

                        // Записываем данные студента в файл группы
                        FileInfo fileInfo = new FileInfo(groupFilePath);
                        using (StreamWriter groupWriter = fileInfo.CreateText())
                        {
                            groupWriter.WriteLine($"{name}, {dateOfBirth}");
                        }
                    }
                }
            }
            public static void CreateSampleBinaryFile(string binaryFilePath)
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(binaryFilePath, FileMode.Create)))
                {
 
                    AddStudent(writer, "John", "GroupA", new DateTime(1995, 5, 10));
                    AddStudent(writer, "Alice", "GroupB", new DateTime(1998, 8, 15));
                    AddStudent(writer, "Bob", "GroupF", new DateTime(1997, 3, 20));
                }
            }

            private static void AddStudent(BinaryWriter writer, string name, string group, DateTime dateOfBirth)
            {
                writer.Write(name);
                writer.Write(group);
                writer.Write(dateOfBirth.ToBinary());
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                string binaryFilePath = "C:\\Users\\Stanislav\\Desktop\\Students.dat";
                Students.CreateSampleBinaryFile(binaryFilePath);
                Students.LoadStudentsFromBinaryFile(binaryFilePath);

                Console.WriteLine("Данные успешно загружены в текстовые файлы.");
                Console.ReadKey();
            }
        }
    }
}


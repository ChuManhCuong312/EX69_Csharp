using System;
using System.IO;
using System.Text;

public class Program
{
    public static int CountLines(string filePath)
    {
        return File.ReadAllLines(filePath).Length;
    }

    public static int CountCharacters(string filePath, char character)
    {
        int count = 0;
        string content = File.ReadAllText(filePath);
        foreach (char c in content)
        {
            if (c == character) count++;
        }
        return count;
    }

    public static int CountWords(string filePath)
    {
        string content = File.ReadAllText(filePath);
        string[] words = content.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }

    public static void ReadAndWriteUtf8(string readFile, string writeFile)
    {
        string content = File.ReadAllText(readFile, Encoding.UTF8);
        File.WriteAllText(writeFile, content, Encoding.UTF8);
    }

    public static void ReadAndWriteUtf16(string readFile, string writeFile)
    {
        string content = File.ReadAllText(readFile, Encoding.Unicode);
        File.WriteAllText(writeFile, content, Encoding.Unicode);
    }

    public static void Write2DArrayToBinaryFile(string filePath, double[,] array)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            writer.Write(rows);
            writer.Write(cols);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    writer.Write(array[i, j]);
                }
            }
        }
    }

    public static double[,] Read2DArrayFromBinaryFile(string filePath)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            int rows = reader.ReadInt32();
            int cols = reader.ReadInt32();

            double[,] array = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = reader.ReadDouble();
                }
            }

            return array;
        }
    }

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("Chọn một tùy chọn:");
            Console.WriteLine("a. Tìm số dòng, tính số ký tự 1 ký tự cho trước, tính số từ được phân tách bởi dấu cách của file Program.cs.");
            Console.WriteLine("b. Đọc file UTF-8 và ghi sang một file UTF-8.");
            Console.WriteLine("c. Đọc file UTF-16 và ghi sang một file UTF-16.");
            Console.WriteLine("d. Ghi và đọc mảng 2 chiều vào/from file nhị phân.");
            Console.WriteLine("0. Thoát chương trình.");
            Console.Write("Nhập lựa chọn (a/b/c/d/0): ");
            
            string choice = Console.ReadLine();

            if (choice == "0")
            {
                break;
            }

            switch (choice)
            {
                case "a":
                    Console.Write("Nhập đường dẫn file: ");
                    string filePathA = Console.ReadLine();
                    Console.WriteLine($"Số dòng: {CountLines(filePathA)}");

                    Console.Write("Nhập ký tự để tính: ");
                    char character = Console.ReadLine()[0];
                    Console.WriteLine($"Số lần xuất hiện của ký tự '{character}': {CountCharacters(filePathA, character)}");

                    Console.WriteLine($"Số từ: {CountWords(filePathA)}");
                    break;
                    
                case "b":
                    Console.Write("Nhập đường dẫn file đọc (UTF-8): ");
                    string readFileUtf8 = Console.ReadLine();
                    Console.Write("Nhập đường dẫn file ghi (UTF-8): ");
                    string writeFileUtf8 = Console.ReadLine();
                    ReadAndWriteUtf8(readFileUtf8, writeFileUtf8);
                    Console.WriteLine("Ghi file UTF-8 thành công.");
                    break;
                    
                case "c":
                    Console.Write("Nhập đường dẫn file đọc (UTF-16): ");
                    string readFileUtf16 = Console.ReadLine();
                    Console.Write("Nhập đường dẫn file ghi (UTF-16): ");
                    string writeFileUtf16 = Console.ReadLine();
                    ReadAndWriteUtf16(readFileUtf16, writeFileUtf16);
                    Console.WriteLine("Ghi file UTF-16 thành công.");
                    break;
                    
                case "d":
                    double[,] array = 
                    {
                        { 1.1, 2.2, 3.3 },
                        { 4.4, 5.5, 6.6 },
                        { 7.7, 8.8, 9.9 }
                    };

                    string binaryFilePath = "a2d.dat";
                    Write2DArrayToBinaryFile(binaryFilePath, array);

                    double[,] readArray = Read2DArrayFromBinaryFile(binaryFilePath);

                    Console.WriteLine("Mảng đọc từ file:");
                    for (int i = 0; i < readArray.GetLength(0); i++)
                    {
                        for (int j = 0; j < readArray.GetLength(1); j++)
                        {
                            Console.Write(readArray[i, j] + " ");
                        }
                        Console.WriteLine();
                    }
                    break;
                    
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    break;
            }
        }
    }
}

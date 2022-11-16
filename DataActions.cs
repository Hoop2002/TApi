using Microsoft.EntityFrameworkCore;
using System.Text;

namespace TApi
{
    public class DataActions
    {
        

         internal static void LoadLogs(string text)
         {
            //создайте текстовый файл и укажите путь до него
            string path = @"C:\Users\docto\source\repos\TApi\Logs.txt";

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                 writer.WriteLine(text);
            }
         }
    }
}

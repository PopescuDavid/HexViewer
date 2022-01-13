using System;
using System.IO;

namespace Hexviewer
{
    class Program
    {
        static void Main(string[] args)
        {
            bool done = false;

            while (!done) 
            {
                try
                {
                    Console.Write(" Introduceti calea fisierului pentru a-l vizualiza prin HexViewer:\n ");
                    string path = Console.ReadLine();
                    Console.WriteLine();


                    Console.Write(" Introduceti cati octeti vreti sa fie pe o singura linie:\n ");
                    int nrOcteti = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    char[] caractereDeEliminat = new char[] { ' ', '"' };
                    path = path.Trim(caractereDeEliminat); 


                    if (!File.Exists(path))
                        throw new FileNotFoundException("Path gresit.");

                    FileStream file = new FileStream(path, FileMode.Open);

                    byte[] byteBlock = new byte[nrOcteti]; 
                    int idx = 0;    

                    int actual;

                    while ((actual = file.Read(byteBlock, 0, nrOcteti)) > 0)    
                    {
                        string hex = BitConverter.ToString(byteBlock, 0, actual); 

                        //hex = hex.Replace("-", " ");  // bitconverter separa fiecare byte prin '-', iar noi il eliminam pentru lizibilitate

                        string text = "";           
                        for (int i = 0; i < actual; i++)
                            text += byteBlock[i] < ' ' || byteBlock[i] == 127 ? "." : ((char)byteBlock[i]).ToString();
                        

                        Console.WriteLine($" {idx:X8} : {hex.PadRight(nrOcteti * 3 - 1)}  | {text}");    
                        idx += nrOcteti;

                       
                    }

                    file.Close();

                    done = true; 

                    Console.Write("\n\n ");
                }
                catch (Exception e)
                {
                    Console.WriteLine($" {e.Message}\n\n");
                }
            }
        }
    }
}
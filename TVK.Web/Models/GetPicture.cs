using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace TVK.Web.Models
{
    public class GetPicture
    {
        public static string folder = Environment.CurrentDirectory + "\\wwwroot\\recived";
		public static Int32 port = 6000;
		public static IPAddress localAddr = IPAddress.Parse("192.168.50.229");

		public static TcpListener listener = new TcpListener(localAddr, port);

		public static void Get_Picture()
        {
			
			listener.Start();
			while (true)
			{
				try
				{
					TcpClient client = listener.AcceptTcpClient();
					using (NetworkStream inputStream = client.GetStream())
					{
						using (BinaryReader reader = new BinaryReader(inputStream))
						{
							string filename = reader.ReadString();
							long lenght = reader.ReadInt64();
							using (FileStream outputStream = File.Open(Path.Combine(folder, filename), FileMode.Create))
							{
								long totalBytes = 0;
								int readBytes = 0;
								byte[] buffer = new byte[2048];

								do
								{
									readBytes = inputStream.Read(buffer, 0, buffer.Length);
									outputStream.Write(buffer, 0, readBytes);
									totalBytes += readBytes;
								} while (client.Connected && totalBytes < lenght);
								Console.WriteLine("Принят файл " + filename + " Размер " + totalBytes);
							}
						}

					}
					client.Close();
				}
				catch
				{

				}

			}

		}

        public static void Delet_Picture()
        {
            string[] files = Directory.GetFiles(folder);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

			listener.Stop();

		}



    }
}

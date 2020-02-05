﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TVK.Web.Models
{
    public class GetPicture
    {
        public static void Get_Picture()
        {
			string folder = "recived";
			TcpListener listener = new TcpListener(IPAddress.Any, 20000);
			listener.Start();
			while (true)
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
			}
    }
}
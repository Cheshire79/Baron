using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using Poker;
using UnityEngine;

namespace CustomTools
{
    public class ProblemReportSender
    {
        private static Stream _responseStream;
        private static Stream requestStream;
        private static MemoryStream _stream;

        public static void ReportAProblem(string message)
        {
            SendReportAProblem("", message);
        }

        public static void SendReportAProblem(string fromEmail, string message)
        {
            //in java: problemReportNetworkSender.send(fromEmail, "Problem report (" + getMailSubject() + ")", msg+MyLog.getLastExceptionStackTrace(), MyLog.dump());
            try
            {
                SendData(fromEmail, GetMailSubject(), message);
            }
            catch (Exception e)
            {
                CustomLogger.LogError("ProblemReportController.Exception while SendData() " + e);
            }
        }

        private static void SendData(string fromEmail, string mailSubject, string message)
        {
            var logDump = CustomLogger.DumpToFile();
            string fileName = Application.persistentDataPath + "/" + CustomLogger.LogFilename;

            using (Stream stream = File.OpenWrite(fileName))
            {
                var files = new[] 
                {
                    new UploadFile
                    {
                        Name = "data",
                        Filename = CustomLogger.LogFilename,
                        ContentType = "application/octet-stream",
                        Stream = stream
                    }
                };

                var values = new NameValueCollection
                {
                    { "unity", "unity" },
                    { "subject", mailSubject },
                    { "fromEmail", fromEmail },
                    { "message", message },
                };

                UploadDataToServer(Constants.ProblemReportServerUrl, files, values, logDump);
            }
        }

        public static void UploadDataToServer(string address, IEnumerable<UploadFile> files, NameValueCollection values, string logDump)
        {
            var request = (HttpWebRequest)WebRequest.Create(address);
            request.Method = "POST";
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            boundary = "--" + boundary;

            using (requestStream = request.GetRequestStream())
            {
                // Write the values
                foreach (string name in values.Keys)
                {
                    var buffer = Encoding.UTF8.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", name, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(values[name] + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                // Write the files
                foreach (var file in files)
                {
                    var buffer = Encoding.UTF8.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", file.Name, file.Filename, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(string.Format("Content-Type: {0}{1}{1}", file.ContentType, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(logDump);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                }
                var boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
                requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
            }

            using (var response = request.GetResponse())
            using (_responseStream = response.GetResponseStream())
            using (_stream = new MemoryStream())
            {
                //stream = (MemoryStream)responseStream;//.CopyTo(stream);
                //return stream.ToArray();
            }
        }

        private static string GetMailSubject()
        {
            return "Problem report (" + GetDeviceInfo() + ")";
        }

        private static string GetDeviceInfo()
        {
            return SystemInfo.deviceUniqueIdentifier + " v.2.0.3" + " " + SystemInfo.deviceModel
                   + " (" + Application.platform + ": " + SystemInfo.operatingSystem + ")";
        }
    }

    public class UploadFile
    {
        public UploadFile()
        {
            ContentType = "application/octet-stream";
        }
        public string Name { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public Stream Stream;
    }
}
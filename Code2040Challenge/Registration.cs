using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace Code2040Challenge
{
    class Registration
    {
        public static string Request()
        {
            WebRequest request = WebRequest.Create("http://challenge.code2040.org/api/register");
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            request.ContentType = "text/json";
            string result;           

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                Dictionary<string, string> reqdata = new Dictionary<string, string>()
                {
                    {"email", "fernandojrb@yahoo.com.br"},
                    {"github", "http://github.com/fernandojrb/code2040challenge"}
                };
                string json = JsonConvert.SerializeObject(reqdata, Formatting.Indented);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
                try
                {
                    var httpResponse = (HttpWebResponse)request.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }
                    httpResponse.Close();
                }
                catch(Exception e)
                {
                    string error = "Request Failed: " + e.Message;
                    Console.Error.WriteLine(error);
                    return error;
                }
                
            }
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            values.TryGetValue("result", out result);
            Console.WriteLine("Welcome, your token is: " + result);
            return result;
        }
    }
}
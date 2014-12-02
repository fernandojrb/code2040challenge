using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Code2040Challenge
{
    class ReverseString
    {
        public static string Request(string token)
        {
            WebRequest request = WebRequest.Create("http://challenge.code2040.org/api/getstring");
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            request.ContentType = "text/json";
            string result;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                Dictionary<string, string> reqdata = new Dictionary<string, string>();
                reqdata.Add("token", token);
                string json = JsonConvert.SerializeObject(reqdata, Formatting.Indented);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            values.TryGetValue("result", out result);
            Console.WriteLine("Original String: " + result);
            return result.Reverse();
        }

        public static string Validate(string token, string revStr)
        {
            WebRequest request = WebRequest.Create("http://challenge.code2040.org/api/validatestring");
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            request.ContentType = "text/json";
            string result;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                Dictionary<string, string> reqdata = new Dictionary<string, string>();
                reqdata.Add("token", token);
                reqdata.Add("string", revStr);
                string json = JsonConvert.SerializeObject(reqdata, Formatting.Indented);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            //values.TryGetValue("result", out result);
            return result.Reverse();
        }
    }

    static class StringExtensions
    {
        public static string Reverse(this string input)
        {
            return new string(input.ToCharArray().Reverse().ToArray());
        }
    }
}

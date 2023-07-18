using System;
using System.Net;

internal class Program
{
    private static void Main(string[] args)
    {
        SimpleListenerExample("http://+:4000/");


        static void SimpleListenerExample(string prefix)
        {

            Console.WriteLine($"Listening on {prefix}...");

            var listener = new HttpListener();
            listener.Prefixes.Add(prefix);

            listener.Start();

            // Note: The GetContext method blocks while waiting for a request.
            var context = listener.GetContext();
            var request = context.Request;
            // Obtain a response object.
            var response = context.Response;
            // Construct a response.
            const string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
            listener.Stop();

        }
    }
}
using System.Net.Sockets;


[assembly: Dependency(typeof(MyApp.Droid.Printer))]
namespace MyApp.Droid
{
    public class Printer : MyApp.DSInterfaces.IPrinter
    {
        public void Print(string ipAddress, int port, IList<string> linesToPrint)
        {
            // Create a socket object
            Socket pSocket = new Socket(SocketType.Stream, ProtocolType.IP);
            // Set a timeout for attemps to connect, here set to 1500 miliseconds
            pSocket.SendTimeout = 1500;
            // Connect to the specified ip address and port
            pSocket.Connect(ipAddress, port);

            List<byte> outputList = new List<byte>();
            foreach (string txt in linesToPrint)
            {
                // Convert the strings to list of bytes
                outputList.AddRange(System.Text.Encoding.UTF8.GetBytes(txt));
                // Add ECS/POS Print and line feed command
                outputList.Add(0x0A); ;
            }

            // Send the command to the printer
            pSocket.Send(outputList.ToArray());

            // Close the connection once done
            pSocket.Close();
        }
    }
}
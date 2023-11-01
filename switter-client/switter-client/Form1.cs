using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;


namespace switter_client
{
    public partial class Form1 : Form
    {

        bool terminating = false;
        bool connected = false;
        Socket clientSocket;
        string username = "";

        string publicKey = "";
        string privateKey = "";
        string friendPublicKeyStr = "";

        UTF8Encoding ByteConverter = new UTF8Encoding();

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        // After input username, port and ip, connecting to server
        private void button_connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text == "" ? "localhost" : textBox_ip.Text;
            username = textBox_username.Text;

            int portNum;

            if (username == "")
            {
                logs.AppendText("Username cannot be empty !!\r\n");
            }
            
            else if (Int32.TryParse(textBox_port.Text, out portNum) && IP != "")
            {

                try
                {
                    // trying to connect to server
                    clientSocket.Connect(IP, portNum);
                    button_connect.Enabled = false;
                    button_connectuser.Enabled = true;
                    button_disconnectfromuser.Enabled = false;
                    connected = true;
                    logs.AppendText("Trying to connect the server!\n");

                    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(1024);

                    publicKey = RSA.ToXmlString(false);
                    privateKey = RSA.ToXmlString(true);

                    byte[] buffer = ByteConverter.GetBytes(publicKey + ";" + username);
                    clientSocket.Send(buffer);

                    Console.WriteLine("sent username");

                    // connected and started to listen server
                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();

                }
                catch
                {
                    logs.AppendText("Could not connect to the server.\r\n");
                }
            }
            else
            {
                logs.AppendText("Check the port and IP number.\n");
            }

        }
        // Receives messages from the server
        private void Receive()
        {
            while (connected)
            {
                try
                {
                    // buffer saves incoming messages as bytes
                    byte[] buffer = new byte[10240];
                    clientSocket.Receive(buffer);

                    // bytes to string incoming message
                    string incomingMessage = ByteConverter.GetString(buffer);
                    //buffer = null;
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    // if login is failed
                    if (incomingMessage.Equals("This user does not exist.") || incomingMessage.Equals("This user is already connected."))
                    {   // If the user is not in the server's userlist or already connected to the server

                        logs.AppendText("Server: " + incomingMessage + "\n");
                        connected = false;
                        logs.AppendText("The server has disconnected\n");
                        button_connect.Enabled = true;
                        textBox_message.Enabled = false;
                        button_send.Enabled = false;
                        textBox_finduser.Enabled = false;
                        button_connectuser.Enabled = false;
                    }

                    else if (incomingMessage.StartsWith("msg:"))
                    {
                        //logs.AppendText("---------------------------\nTHE MESSAGES ARE STARTED TO BE TAKEN!\n");
                        string incoming = incomingMessage.Substring(4);
                        string friendUsername = incoming.Substring(0, incoming.IndexOf(";"));
                        string messageReceived = incoming.Substring(incoming.IndexOf(";")+1);
                        //logs.AppendText("MSG: " + messageReceived + "\n");

                        byte[] encrypted = Convert.FromBase64String(messageReceived); //ByteConverter.GetBytes(messageReceived);
                        File.WriteAllBytes("./enc_friend_byte.txt", encrypted);
                        
                        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(1024);
                        // Import your own key for decrypting messages coming for you encrpyted with your public key
                        RSA.FromXmlString(privateKey);
                        File.WriteAllText("./enc_friend.txt", messageReceived);
                        string msgStr = RSADecrypt(encrypted, RSA.ExportParameters(true), false);
                        
                        try
                        {
                            File.WriteAllText("./dec.txt", msgStr);
                        }
                        catch
                        {
                            logs.AppendText("+!!!!! Error decrypted saving file.\n");
                        }
                        
                        // Show the message coming from sender
                        logs.AppendText(friendUsername + ": " + msgStr + "\n");
                    }

                    else if (incomingMessage.StartsWith("key:"))
                    {
                        friendPublicKeyStr = incomingMessage.Substring(4);
                        //logs.AppendText("Your friend's key: " + friendPublicKeyStr + "\n");
                    }

                    // if login successful, fix the buttons and continue listening
                    else if (incomingMessage.Equals("Connected successfully.")){
                        logs.AppendText("Server: " + incomingMessage + "\n");
                        button_disconnect.Enabled = true;
                        textBox_message.Enabled = true;
                        textBox_finduser.Enabled = true;
                        button_connectuser.Enabled = true;
                        button_connect.Enabled = false;

                    }

                    // If this username is already connected
                    else if (incomingMessage.StartsWith("This user is already connected") || incomingMessage.IndexOf("wants to send you a message") != -1)  
                    {
                        logs.AppendText("Server: " + incomingMessage + "\n");
                    }

                    // If incoming message is not empty
                    else if (incomingMessage != "" && incomingMessage.IndexOf("wants to send you a message") == -1)
                    {
                        logs.AppendText("Server: " + incomingMessage + "\n");
                        button_connectuser.Enabled = true;
                        button_disconnectfromuser.Enabled = false;
                    }
                }
                catch
                {
                    // any problem with server
                    if (!terminating)
                    {
                        logs.AppendText("You are disconnected from the server.\n");
                        byte[] buffer = ByteConverter.GetBytes("disconnect");
                        clientSocket.Send(buffer);
                        button_connect.Enabled = true;
                        textBox_message.Enabled = false;
                        button_send.Enabled = false;
                        button_disconnect.Enabled = false;
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

      
        // posting sweet to server
        private void button_send_Click(object sender, EventArgs e)
        {
            string message = textBox_message.Text;

            
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(1024);

            // Upload friends public key to encrypt with their keys
            RSA.FromXmlString(friendPublicKeyStr);
            byte[] dataToEncrypt = ByteConverter.GetBytes(message);
            string enc_str = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);
            File.WriteAllText("./enc_me.txt", enc_str);

            //logs.AppendText("str: " + enc_str + "\n");

            // checks message
            if (message != "")
            {
                byte[] buffer = ByteConverter.GetBytes("msg:" + enc_str);
                clientSocket.Send(buffer);
                logs.AppendText("Me: " + textBox_message.Text + "\n");
                textBox_message.Text = "";
            }
        }

        // disconnect from the server as logged in user
        private void button_disconnect_Click(object sender, EventArgs e)
        {
            try
            {
                // sending the disconnect request
                byte[] buffer = ByteConverter.GetBytes("disconnect");
                clientSocket.Send(buffer);
                
            }
            catch
            {
                logs.AppendText("Error trying to disconnect from the server\n");
            }
            finally
            {
                // disconnects and fixes the button's enableness
                logs.AppendText("Disconnected from the server\n");
                connected = false;
                button_connect.Enabled = true;
                textBox_message.Enabled = false;
                textBox_finduser.Enabled = false;
                button_send.Enabled = false;
                button_connectuser.Enabled = false;
                button_disconnect.Enabled = false;
                clientSocket.Close();
            }
           
        }

        private void button_connectuser_Click(object sender, EventArgs e)
        {
            byte[] buffer = ByteConverter.GetBytes("finduser:" + textBox_finduser.Text);
            clientSocket.Send(buffer);
            textBox_finduser.Text = "";
            button_disconnectfromuser.Enabled = true;
            button_connectuser.Enabled = false;
            textBox_message.Enabled = true;
            button_send.Enabled = true;
        }

        private void button_disconnectfromuser_Click(object sender, EventArgs e)
        {
            button_disconnectfromuser.Enabled = false;
            button_connectuser.Enabled = true;
            logs.AppendText("-------------------------------------------------------------\n");
        }

        public static string RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs
                    //to include the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return Convert.ToBase64String(encryptedData);
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }
        public static string RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return Encoding.UTF8.GetString(decryptedData);
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }
        }
    }
}

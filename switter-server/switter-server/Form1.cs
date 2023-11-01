using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace switter_server
{
    public partial class Form1 : Form
    {
        // for people find themselves in the system, they will search for usernames to find the socket and send messages
        struct userSocket
        {
            public string username;
            public string publicKey;
            public Socket socket;
            public string friendPublicKey;
            public Socket friendSocket;

            public userSocket(string usern, string key, Socket sckt, string friendkey, Socket friendsckt)
            {
                username = usern;
                publicKey = key;
                socket = sckt;
                friendPublicKey = friendkey;
                friendSocket = friendsckt;
            }
        }

        UTF8Encoding ByteConverter = new UTF8Encoding();
        bool terminating = false;   
        bool listening = false;
        List<string> connectedUsers = new List<string>();   // List of all connected users

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();        // all sockets connected
        List<userSocket> userSockets = new List<userSocket>();  // all users and their sockets
        // The socket of person that the user wants to send message

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        // initialize the server
        private void button_start_Click(object sender, EventArgs e)
        {
            int serverPort;

            // if port is fine
            if (Int32.TryParse(textbox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                button_start.Enabled = false;

                // started to accepting users
                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                richtextbox_log.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                richtextbox_log.AppendText("Please check port number. \n");
            }
        }


        // listening for users (clients)
        private void Accept()
        {
            while (listening)
            {
                try
                {
                    // if new user is came
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);


                    // get the username as a message first
                    byte[] buffer = new byte[10240];
                    newClient.Receive(buffer);
                    string incomingMessage = ByteConverter.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    string publicKey = incomingMessage.Split(';')[0];
                    string username = incomingMessage.Split(';')[1];

                    richtextbox_log.AppendText(username + " is trying to connect.\n");

                    if (connectedUsers.Exists(x => x.Equals(username)))     // If the same username tries to connect
                    {
                        richtextbox_log.AppendText("User is already connected.\n");
                        byte[] sweetsBuffer = ByteConverter.GetBytes("This user is already connected.");
                        newClient.Send(sweetsBuffer);
                        newClient.Close();
                        clientSockets.Remove(newClient);
                    }

                    //  SUCCESFUL FIRST TIME LOGIN
                    else
                    {
                        // user is now successfully logged in
                        connectedUsers.Add(username);

                        // we added new socket for communicating among users
                        userSocket newSocket = new userSocket();
                        newSocket.username = username;
                        newSocket.socket = newClient;
                        newSocket.publicKey = publicKey;
                        userSockets.Add(newSocket);

                        richtextbox_log.AppendText(username + " connected successfully.\n");
                        byte[] sweetsBuffer = ByteConverter.GetBytes("Connected successfully.");
                        newClient.Send(sweetsBuffer);
                        Thread receiveThread = new Thread(() => Receive(newClient, username)); // updated
                        receiveThread.Start();
                    }
                    
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        richtextbox_log.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        // listening for new commands from users
        private void Receive(Socket thisClient, string user) // updated
        {
            bool connected = true;
            userSocket thisUser = new userSocket();

            while (connected && !terminating) 
            {
                try
                {
                    byte[] buffer = new byte[10240];
                    thisClient.Receive(buffer);

                    string incomingMessage = ByteConverter.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    if (incomingMessage.StartsWith("finduser:"))       
                    {   // if "request" message comes from client(user), print all the sweets except user's sweets
                        bool userFound = false;
                        string userToMessage = incomingMessage.Substring(9);
                        foreach(userSocket sckt in userSockets)
                        {   
                            if (sckt.username == userToMessage)
                            {   // sckt is the friend's userSocket
                                richtextbox_log.AppendText(user +" wants to message to " + userToMessage + ".\n");
                                byte[] msg = ByteConverter.GetBytes(user + " wants to send you a message.");
                                sckt.socket.Send(msg);

                                byte[] publickey = ByteConverter.GetBytes("key:" + sckt.publicKey);
                                thisClient.Send(publickey);
                                for (int i = 0; i < userSockets.Count; i++)
                                {
                                    if (userSockets[i].socket == thisClient)
                                    {
                                        userSocket newsckt = new userSocket(
                                            userSockets[i].username, userSockets[i].publicKey, userSockets[i].socket,
                                            sckt.publicKey, sckt.socket);
                                        userSockets[i] = newsckt;
                                        i = userSockets.Count;
                                        thisUser = newsckt;
                                    }
                                }
                                userFound = true;
                                break;
                            }
                        }
                        if (!userFound)
                        {
                            byte[] msg = ByteConverter.GetBytes("Username \"" + userToMessage + "\" is not found.");
                            thisClient.Send(msg);
                        }
                    }

                    else if (incomingMessage.StartsWith("msg:")) {
                        string msg = incomingMessage.Substring(4);
                        richtextbox_log.AppendText("+ Message is being sent to receiver.\n");
                        byte[] message = ByteConverter.GetBytes("msg:" + thisUser.username + ";" + msg);
                        byte[] m = ByteConverter.GetBytes(msg);
                        try
                        {
                            File.WriteAllBytes("./enc_server.txt", m);
                        }
                        catch
                        {
                            richtextbox_log.AppendText("+!! Error saving file.\n");
                        }
                        thisUser.friendSocket.Send(message);

                        richtextbox_log.AppendText("--\n" + user + " sent an encryped message " + msg + "\n--\n");
                    }

                    // if user sends disconnect command, removes from connectedUsers
                    else if(incomingMessage == "disconnect")
                    {
                        thisClient.Close();
                        connectedUsers.Remove(user);
                        clientSockets.Remove(thisClient);
                        connected = false;
                        userSockets.Remove(thisUser);
                        richtextbox_log.AppendText(user + " disconnected\n");
                    }

                    else
                        richtextbox_log.AppendText("Invalid request!\n");

                }
                catch
                {
                    // if user disconnects by closing the program without clicking disconnect
                    if (!terminating)
                    {
                        richtextbox_log.AppendText(user + " has disconnected\n");
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connectedUsers.Remove(user);
                    userSockets.Remove(thisUser);
                    connected = false;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }
    }
}

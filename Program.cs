using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

class Logger
{
    private string logFileName;

    public Logger(string fileName)
    {
        logFileName = fileName;
    }

    public void Log(string logMessage)
    {
        string logEntry = $"{DateTime.Now}: {logMessage}";
        File.AppendAllText(logFileName, logEntry + Environment.NewLine);
    }
}

class User
{
    private string name;
    private Logger logger;

    public User(string userName, Logger userLogger)
    {
        name = userName;
        logger = userLogger;
    }

    public void MakeDirectory(string directoryName)
    {
        logger.Log($"{name} created directory '{directoryName}'.");
    }

    public void EditFile(string fileName)
    {
        logger.Log($"{name} edited file '{fileName}'.");
    }

    public void DeleteFile(string fileName)
    {
        logger.Log($"{name} deleted file '{fileName}'.");
    }

    public void CreateFile(string fileName)
    {
        logger.Log($"{name} created file '{fileName}'.");
    }
}

class Server
{
    private List<User> users = new List<User>();
    private Logger serverLogger;

    public Server(Logger logger)
    {
        serverLogger = logger;
    }

    public void AddUser(User user)
    {
        users.Add(user);
    }

    public void Logger(string logMessage)
    {
        serverLogger.Log(logMessage);
    }
}

class Program
{
    static void Main()
    {
        Logger userLogger = new Logger("log.txt");
        Logger serverLogger = new Logger("server_log.txt");

        Server server = new Server(serverLogger);

        User user1 = new User("User1", userLogger);
        User user2 = new User("User2", userLogger);

        server.AddUser(user1);
        server.AddUser(user2);

        user1.MakeDirectory("Documents");
        user2.EditFile("Report.txt");
        user1.DeleteFile("Photo.jpg");
        user2.CreateFile("Notes.txt");

        server.Logger($"{DateTime.Now}: Server log initialized.");

        Console.WriteLine("Actions performed. Check log files.");


        Process.Start("log.txt");
    }
}
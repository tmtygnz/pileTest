using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace pileTest.Logging
{
	public class Logger
	{
		private static Queue<Message> logQueue;
		private static StreamWriter logWriter;
		private static Dictionary<String, Thread> threadDictionary = new Dictionary<string, Thread>();
		private static Thread logThread;
		public static void initializeLogger()
		{
			logQueue = new Queue<Message>();
			logWriter = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "log.txt"));
			logThread = new Thread(logLoop);
			logThread.Name = "logThread";
			logThread.Start();
			threadDictionary.Add("logThread", logThread);
		}
		public static void logLoop()
		{
			while (true)
			{
				if (logQueue.Count != 0)
				{
					writeDisk(logQueue.Peek());
				}
				Thread.Sleep(100);
			}
		}	
		public static void log(Severity severity, String message)
		{
			Message msg = new Message()
			{
				message = message,
				severity = severity,
				logTime = DateTime.UtcNow
			};
			logQueue.Enqueue(msg);
		}
		public static void writeDisk(Message message)
		{
			string log = $"[{message.severity.ToString().ToUpper()}] [{message.logTime}] {message.message}\n";
			logWriter.Write(log);
			logQueue.Dequeue();
		}
		public static void killAll()
		{
			logWriter.Flush();
			logWriter.Close();
			logThread.Abort();
		}
	}
}

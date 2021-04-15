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
		private static StreamWriter logWriter;
		private static Thread loggingThread;
		public static Queue<Message> logQueue;
		private static Dictionary<string, Thread> threadDictionary = new Dictionary<string, Thread>();
		public static void initializeLogger()
		{
			logQueue = new Queue<Message>();
			logWriter = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),"log.txt"));
			loggingThread = new Thread(new ThreadStart(logLoop));
			loggingThread.Name = "logLoopThread";
			loggingThread.Start();
			threadDictionary.Add("logLoopThread", loggingThread);
		}
		private static void logLoop()
		{
			while (true) {
				if (logQueue.Count != 0)
				{
					writeDisk(logQueue.Peek());
				}
				Thread.Sleep(100);
			}
		}
		public static void log(Severity severity, string message)
		{
			Message msg = new Message()
			{
				message = message,
				logTime = DateTime.Now,
				severity = severity
			};
			logQueue.Enqueue(msg);
		}
		public static void writeDisk(Message message)
		{
			string log = $"[{message.severity.ToString().ToUpper()}] | {message.logTime} | {message.message} \n";
			logWriter.Write(log);
			Console.Beep(100,1000);
			logQueue.Dequeue();
		}
		public static void flushAll()
		{
			logWriter.Close();
			loggingThread.Abort();
		}
	}
}

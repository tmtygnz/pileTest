using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pileTest.Logging
{
	public struct Message
	{
		public DateTime logTime;
		public string message;
		public Severity severity;
	}
}

using System;

namespace Kaedei.AcPlay.Redirector
{
	[Serializable]
	public class RequestNotHandledException : Exception
	{
		public RequestNotHandledException() { }
		public RequestNotHandledException(string message) : base(message) { }
		public RequestNotHandledException(string message, Exception inner) : base(message, inner) { }
		protected RequestNotHandledException(
		 System.Runtime.Serialization.SerializationInfo info,
		 System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}

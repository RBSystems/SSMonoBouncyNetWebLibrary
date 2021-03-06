//
// System.Net.WebAsyncResult
//
// Authors:
//	Gonzalo Paniagua Javier (gonzalo@ximian.com)
//
// (C) 2003 Ximian, Inc (http://www.ximian.com)
//

// Copyright (c) 2018 Nivloc Enterprises Ltd

//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;

#if SSHARP
using Crestron.SimplSharp.CrestronIO;
using IAsyncResult = Crestron.SimplSharp.CrestronIO.IAsyncResult;
using AsyncCallback = Crestron.SimplSharp.CrestronIO.AsyncCallback;
#else
using System.IO;
using System.Threading;
#endif

#if SSHARP
namespace SSMono.Net
#else
namespace System.Net
#endif
	{
	internal class WebAsyncResult : SimpleAsyncResult
		{
		private int nbytes;
		private IAsyncResult innerAsyncResult;
		private HttpWebResponse response;
		private Stream writeStream;
		private byte[] buffer;
		private int offset;
		private int size;
		public bool EndCalled;
		public bool AsyncWriteAll;

		public WebAsyncResult (AsyncCallback cb, object state)
			: base (cb, state)
			{
			}

		public WebAsyncResult (HttpWebRequest request, AsyncCallback cb, object state)
			: base (cb, state)
			{
			}

		public WebAsyncResult (AsyncCallback cb, object state, byte[] buffer, int offset, int size)
			: base (cb, state)
			{
			this.buffer = buffer;
			this.offset = offset;
			this.size = size;
			}

		internal void Reset ()
			{
			this.nbytes = 0;
			this.response = null;
			this.buffer = null;
			this.offset = 0;
			this.size = 0;
			Reset_internal ();
			}

		internal void SetCompleted (bool synch, int nbytes)
			{
			this.nbytes = nbytes;
			SetCompleted_internal (synch);
			}

		internal void SetCompleted (bool synch, Stream writeStream)
			{
			this.writeStream = writeStream;
			SetCompleted_internal (synch);
			}

		internal void SetCompleted (bool synch, HttpWebResponse response)
			{
			this.response = response;
			SetCompleted_internal (synch);
			}

		internal void DoCallback ()
			{
			DoCallback_internal ();
			}

		internal int NBytes
			{
			get { return nbytes; }
			set { nbytes = value; }
			}

		internal IAsyncResult InnerAsyncResult
			{
			get { return innerAsyncResult; }
			set { innerAsyncResult = value; }
			}

		internal Stream WriteStream
			{
			get { return writeStream; }
			}

		internal HttpWebResponse Response
			{
			get { return response; }
			}

		internal byte[] Buffer
			{
			get { return buffer; }
			}

		internal int Offset
			{
			get { return offset; }
			}

		internal int Size
			{
			get { return size; }
			}
		}

	internal class ReadWebAsyncResult : WebAsyncResult
		{
		public ReadWebAsyncResult (AsyncCallback cb, object state)
			: base (cb, state)
			{
			}

		public ReadWebAsyncResult (HttpWebRequest request, AsyncCallback cb, object state)
			: base (request, cb, state)
			{
			}

		public ReadWebAsyncResult (AsyncCallback cb, object state, byte[] buffer, int offset, int size)
			: base (cb, state, buffer, offset, size)
			{
			}
		}

	internal class WriteWebAsyncResult : WebAsyncResult
		{
		public WriteWebAsyncResult (AsyncCallback cb, object state)
			: base (cb, state)
			{
			}

		public WriteWebAsyncResult (HttpWebRequest request, AsyncCallback cb, object state)
			: base (request, cb, state)
			{
			}

		public WriteWebAsyncResult (AsyncCallback cb, object state, byte[] buffer, int offset, int size)
			: base (cb, state, buffer, offset, size)
			{
			}
		}

	internal class RequestWebAsyncResult : WebAsyncResult
		{
		public RequestWebAsyncResult (AsyncCallback cb, object state)
			: base (cb, state)
			{
			}

		public RequestWebAsyncResult (HttpWebRequest request, AsyncCallback cb, object state)
			: base (request, cb, state)
			{
			}

		public RequestWebAsyncResult (AsyncCallback cb, object state, byte[] buffer, int offset, int size)
			: base (cb, state, buffer, offset, size)
			{
			}
		}

	internal class ResponseWebAsyncResult : WebAsyncResult
		{
		public ResponseWebAsyncResult (AsyncCallback cb, object state)
			: base (cb, state)
			{
			}

		public ResponseWebAsyncResult (HttpWebRequest request, AsyncCallback cb, object state)
			: base (request, cb, state)
			{
			}

		public ResponseWebAsyncResult (AsyncCallback cb, object state, byte[] buffer, int offset, int size)
			: base (cb, state, buffer, offset, size)
			{
			}
		}
	}
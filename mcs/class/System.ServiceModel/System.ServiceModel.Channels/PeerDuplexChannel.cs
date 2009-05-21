//
// PeerDuplexChannel.cs
//
// Author:
//	Atsushi Enomoto <atsushi@ximian.com>
//
// Copyright (C) 2009 Novell, Inc.  http://www.novell.com
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
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Threading;

namespace System.ServiceModel.Channels
{
	internal class PeerDuplexChannel : DuplexChannelBase
	{
		PeerChannelFactory<IDuplexChannel> factory;
		PeerChannelListener<IDuplexChannel> listener;
		EndpointAddress local_address;
		PeerResolver resolver;
		PeerNode node;

		public PeerDuplexChannel (PeerChannelFactory<IDuplexChannel> factory, EndpointAddress address, Uri via, PeerResolver resolver)
			: base (factory, address, via)
		{
			this.factory = factory;
			this.resolver = resolver;

			string mesh = RemoteAddress.Uri.Host;
			// It could be opened even with empty list of PeerNodeAddresses.
			// So, do not create PeerNode per PeerNodeAddress, but do it with PeerNodeAddress[].
			node = new PeerNodeImpl (resolver, mesh, factory.Source.Port);
		}

		// FIXME: receive local_address too
		public PeerDuplexChannel (PeerChannelListener<IDuplexChannel> listener)
			: base (listener)
		{
			this.listener = listener;

			// FIXME: set resolver and node.
		}

		public override EndpointAddress LocalAddress {
			get { return local_address; }
		}

		public override T GetProperty<T> ()
		{
			if (typeof (T).IsInstanceOfType (node))
				return (T) (object) node;
			return base.GetProperty<T> ();
		}

		// DuplexChannelBase

		public override void Send (Message message, TimeSpan timeout)
		{
			throw new NotImplementedException ();
		}

		public override Message Receive (TimeSpan timeout)
		{
			throw new NotImplementedException ();
		}

		public override IAsyncResult BeginTryReceive (TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException ();
		}

		public override bool EndTryReceive (IAsyncResult result, out Message message)
		{
			throw new NotImplementedException ();
		}

		public override bool WaitForMessage (TimeSpan timeout)
		{
			throw new NotImplementedException ();
		}
		
		// CommunicationObject
		
		[MonoTODO]
		protected override void OnAbort ()
		{
			throw new NotImplementedException ();
		}

		[MonoTODO]
		protected override IAsyncResult OnBeginClose (TimeSpan timeout,
			AsyncCallback callback, object state)
		{
			throw new NotImplementedException ();
		}

		[MonoTODO]
		protected override IAsyncResult OnBeginOpen (TimeSpan timeout,
			AsyncCallback callback, object state)
		{
			throw new NotImplementedException ();
		}

		[MonoTODO]
		protected override void OnClose (TimeSpan timeout)
		{
			throw new NotImplementedException ();
		}
		
		[MonoTODO]
		protected override void OnEndClose (IAsyncResult result)
		{
			throw new NotImplementedException ();
		}

		[MonoTODO]
		protected override void OnEndOpen (IAsyncResult result)
		{
			throw new NotImplementedException ();
		}

		// At some stage I should unify this class with PeerOutputChannel (and probably PeerInputChannel). Too much duplicate.
		protected override void OnOpen (TimeSpan timeout)
		{
			node.Open (timeout);
		}
	}
}

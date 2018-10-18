using System.Diagnostics;
using System.Web.Services;
using System.ComponentModel;
using System.Web.Services.Protocols;
using System;
using System.Xml.Serialization;
namespace ws_OperacionesFactura.Produccion
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [WebServiceBindingAttribute(Name = "CrSeedSoapBinding", Namespace = "https://palena.sii.cl/DTEWS/CrSeed.jws")]
    public partial class CrSeedService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback getSeedOperationCompleted;

        /// <remarks/>
        public CrSeedService()
        {
            this.Url = "https://palena.sii.cl/DTEWS/CrSeed.jws";
        }

        /// <remarks/>
        public event getSeedCompletedEventHandler getSeedCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "https://palena.sii.cl/DTEWS/CrSeed.jws", ResponseNamespace = "https://maullin.sii.cl/DTEWS/CrSeed.jws")]
        [return: System.Xml.Serialization.SoapElementAttribute("getSeedReturn")]
        public string getSeed()
        {
            object[] results = this.Invoke("getSeed", new object[0]);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegingetSeed(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getSeed", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public string EndgetSeed(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void getSeedAsync()
        {
            this.getSeedAsync(null);
        }

        /// <remarks/>
        public void getSeedAsync(object userState)
        {
            if ((this.getSeedOperationCompleted == null))
            {
                this.getSeedOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetSeedOperationCompleted);
            }
            this.InvokeAsync("getSeed", new object[0], this.getSeedOperationCompleted, userState);
        }

        private void OngetSeedOperationCompleted(object arg)
        {
            if ((this.getSeedCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getSeedCompleted(this, new getSeedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void getSeedCompletedEventHandler(object sender, getSeedCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getSeedCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal getSeedCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "GetTokenFromSeedSoapBinding", Namespace = "https://palena.sii.cl/DTEWS/GetTokenFromSeed.jws")]
    public partial class GetTokenFromSeedService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback getVersionOperationCompleted;

        private System.Threading.SendOrPostCallback getTokenOperationCompleted;

        /// <remarks/>
        public GetTokenFromSeedService()
        {
            this.Url = "https://palena.sii.cl/DTEWS/GetTokenFromSeed.jws";
        }

        /// <remarks/>
        public event getVersionCompletedEventHandler getVersionCompleted;

        /// <remarks/>
        public event getTokenCompletedEventHandler getTokenCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "https://palena.sii.cl/DTEWS/GetTokenFromSeed.jws", ResponseNamespace = "https://maullin.sii.cl/DTEWS/GetTokenFromSeed.jws")]
        [return: System.Xml.Serialization.SoapElementAttribute("getVersionReturn")]
        public string getVersion()
        {
            object[] results = this.Invoke("getVersion", new object[0]);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegingetVersion(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getVersion", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public string EndgetVersion(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void getVersionAsync()
        {
            this.getVersionAsync(null);
        }

        /// <remarks/>
        public void getVersionAsync(object userState)
        {
            if ((this.getVersionOperationCompleted == null))
            {
                this.getVersionOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetVersionOperationCompleted);
            }
            this.InvokeAsync("getVersion", new object[0], this.getVersionOperationCompleted, userState);
        }

        private void OngetVersionOperationCompleted(object arg)
        {
            if ((this.getVersionCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getVersionCompleted(this, new getVersionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "https://palena.sii.cl/DTEWS/GetTokenFromSeed.jws", ResponseNamespace = "https://maullin.sii.cl/DTEWS/GetTokenFromSeed.jws")]
        [return: System.Xml.Serialization.SoapElementAttribute("getTokenReturn")]
        public string getToken(string pszXml)
        {
            object[] results = this.Invoke("getToken", new object[] {
                            pszXml});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegingetToken(string pszXml, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getToken", new object[] {
                            pszXml}, callback, asyncState);
        }

        /// <remarks/>
        public string EndgetToken(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void getTokenAsync(string pszXml)
        {
            this.getTokenAsync(pszXml, null);
        }

        /// <remarks/>
        public void getTokenAsync(string pszXml, object userState)
        {
            if ((this.getTokenOperationCompleted == null))
            {
                this.getTokenOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetTokenOperationCompleted);
            }
            this.InvokeAsync("getToken", new object[] {
                            pszXml}, this.getTokenOperationCompleted, userState);
        }

        private void OngetTokenOperationCompleted(object arg)
        {
            if ((this.getTokenCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getTokenCompleted(this, new getTokenCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void getVersionCompletedEventHandler(object sender, getVersionCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getVersionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal getVersionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void getTokenCompletedEventHandler(object sender, getTokenCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getTokenCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal getTokenCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }



}
namespace ws_OperacionesFactura.Certificacion
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [WebServiceBindingAttribute(Name = "CrSeedSoapBinding", Namespace = "https://maullin.sii.cl/DTEWS/CrSeed.jws")]
    public partial class CrSeedService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback getSeedOperationCompleted;

        /// <remarks/>
        public CrSeedService()
        {
            this.Url = "https://maullin.sii.cl/DTEWS/CrSeed.jws";
        }

        /// <remarks/>
        public event getSeedCompletedEventHandler getSeedCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "https://maullin.sii.cl/DTEWS/CrSeed.jws", ResponseNamespace = "https://maullin.sii.cl/DTEWS/CrSeed.jws")]
        [return: System.Xml.Serialization.SoapElementAttribute("getSeedReturn")]
        public string getSeed()
        {
            object[] results = this.Invoke("getSeed", new object[0]);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegingetSeed(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getSeed", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public string EndgetSeed(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void getSeedAsync()
        {
            this.getSeedAsync(null);
        }

        /// <remarks/>
        public void getSeedAsync(object userState)
        {
            if ((this.getSeedOperationCompleted == null))
            {
                this.getSeedOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetSeedOperationCompleted);
            }
            this.InvokeAsync("getSeed", new object[0], this.getSeedOperationCompleted, userState);
        }

        private void OngetSeedOperationCompleted(object arg)
        {
            if ((this.getSeedCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getSeedCompleted(this, new getSeedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void getSeedCompletedEventHandler(object sender, getSeedCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getSeedCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal getSeedCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "GetTokenFromSeedSoapBinding", Namespace = "https://maullin.sii.cl/DTEWS/GetTokenFromSeed.jws")]
    public partial class GetTokenFromSeedService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback getVersionOperationCompleted;

        private System.Threading.SendOrPostCallback getTokenOperationCompleted;

        /// <remarks/>
        public GetTokenFromSeedService()
        {
            this.Url = "https://maullin.sii.cl/DTEWS/GetTokenFromSeed.jws";
        }

        /// <remarks/>
        public event getVersionCompletedEventHandler getVersionCompleted;

        /// <remarks/>
        public event getTokenCompletedEventHandler getTokenCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "https://maullin.sii.cl/DTEWS/GetTokenFromSeed.jws", ResponseNamespace = "https://maullin.sii.cl/DTEWS/GetTokenFromSeed.jws")]
        [return: System.Xml.Serialization.SoapElementAttribute("getVersionReturn")]
        public string getVersion()
        {
            object[] results = this.Invoke("getVersion", new object[0]);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegingetVersion(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getVersion", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public string EndgetVersion(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void getVersionAsync()
        {
            this.getVersionAsync(null);
        }

        /// <remarks/>
        public void getVersionAsync(object userState)
        {
            if ((this.getVersionOperationCompleted == null))
            {
                this.getVersionOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetVersionOperationCompleted);
            }
            this.InvokeAsync("getVersion", new object[0], this.getVersionOperationCompleted, userState);
        }

        private void OngetVersionOperationCompleted(object arg)
        {
            if ((this.getVersionCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getVersionCompleted(this, new getVersionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "https://maullin.sii.cl/DTEWS/GetTokenFromSeed.jws", ResponseNamespace = "https://maullin.sii.cl/DTEWS/GetTokenFromSeed.jws")]
        [return: System.Xml.Serialization.SoapElementAttribute("getTokenReturn")]
        public string getToken(string pszXml)
        {
            object[] results = this.Invoke("getToken", new object[] {
                            pszXml});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegingetToken(string pszXml, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getToken", new object[] {
                            pszXml}, callback, asyncState);
        }

        /// <remarks/>
        public string EndgetToken(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void getTokenAsync(string pszXml)
        {
            this.getTokenAsync(pszXml, null);
        }

        /// <remarks/>
        public void getTokenAsync(string pszXml, object userState)
        {
            if ((this.getTokenOperationCompleted == null))
            {
                this.getTokenOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetTokenOperationCompleted);
            }
            this.InvokeAsync("getToken", new object[] {
                            pszXml}, this.getTokenOperationCompleted, userState);
        }

        private void OngetTokenOperationCompleted(object arg)
        {
            if ((this.getTokenCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getTokenCompleted(this, new getTokenCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void getVersionCompletedEventHandler(object sender, getVersionCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getVersionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal getVersionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void getTokenCompletedEventHandler(object sender, getTokenCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getTokenCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal getTokenCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }



}
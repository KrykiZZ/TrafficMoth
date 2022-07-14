using Avalonia.Controls;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;
using TrafficMoth.Models;
using TrafficMoth.ViewModels;

namespace TrafficMoth.Views
{
    public partial class MainWindow : Window
    {
        private ProxyServer _server;

        public MainWindow()
        {
            InitializeComponent();

            Opened += OnOpened;

            _server = new ProxyServer();
            //_server.CertificateManager.CertificateEngine = Titanium.Web.Proxy.Network.CertificateEngine.DefaultWindows;
            //_server.CertificateManager.CreateRootCertificate();
            //_server.CertificateManager.TrustRootCertificate();

            //_server.BeforeRequest += OnRequest;
            //_server.BeforeResponse += OnResponse;

            //var endpoint = new ExplicitProxyEndPoint(IPAddress.Any, 8000, true);

            //_server.AddEndPoint(endpoint);
            //_server.Start();

            //_server.SetAsSystemHttpProxy(endpoint);
            //_server.SetAsSystemHttpsProxy(endpoint);

            Closing += OnClosing;
        }

        private async void OnOpened(object? sender, EventArgs e)
        {
            var context = await Dispatcher.UIThread.InvokeAsync(() => DataContext);
            if (!(context is MainWindowViewModel vm))
                return;

            vm.Requests.Add(new CapturedRequest
            {
                Url = "URL Here",
                RequestHeaders = new Dictionary<string, string>
                {
                    { "Request", "Header" }
                },
                ResponseHeaders = new Dictionary<string, string>
                {
                    { "Response", "Header" }
                }
            });
        }

        private void OnClosing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _server.DisableAllSystemProxies();
        }

        private async Task OnResponse(object sender, SessionEventArgs e)
        {
            var context = await Dispatcher.UIThread.InvokeAsync(() => DataContext);
            if (!(context is MainWindowViewModel vm))
                return;

            var req = e.HttpClient.Request.Headers.Select(x => KeyValuePair.Create(x.Name, x.Value)).ToDictionary(x => x.Key, x => x.Value);
            var res = e.HttpClient.Response.Headers.Select(x => KeyValuePair.Create(x.Name, x.Value)).ToDictionary(x => x.Key, x => x.Value);

            vm.Requests.Add(new CapturedRequest
            {
                Url = e.HttpClient.Request.Url,
                RequestHeaders = req,
                ResponseHeaders = res
            });
        }

        private async Task OnRequest(object sender, SessionEventArgs e)
        {
        }
    }
}

using RealDebrid.constant;
using RealDebrid.model;
using RealDebrid.service;
using RealDebrid.util;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace RealDebrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string finalToken = "";
        private Thread torrentRefreshThread;

        public ObservableCollection<TorrentModel> torrentList { get; set; } = [];
        public ObservableCollection<DownloadModel> downloadList { get; set; } = [];


        public MainWindow()
        {
            InitializeComponent();
            String cacheToken = CacheService.retrieve("token");
            if (cacheToken != null)
            {
                token.Text = cacheToken;
                finalToken = cacheToken;
            }
            else
            {
                finalToken = "";
            }

            torrentRefreshThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                               torrentList.Clear()
                        );
                        Task<TorrentResponse[]> torrents = new HttpService<TorrentResponse[]>(Constant.TORRENTS, HttpMethod.Get, HttpUtil.generateDefaultHeader(finalToken), default).jsonResponseAsync();
                        if (torrents != null && torrents.Result != null)
                        {
                            foreach (TorrentResponse torrent in torrents.Result)
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    torrentList.Add(new TorrentModel(torrent));
                                });
                            }
                        }
                        Thread.Sleep(4000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            });
            torrentRefreshThread.Start();

            this.DataContext = this;
        }

        private void salvaToken(object sender, RoutedEventArgs e)
        {
            CacheService.save("token", token.Text);
            finalToken = token.Text;
        }

        private void controllaLink(object sender, RoutedEventArgs e)
        {
            String finalLink = link.Text;
            downloadList.Clear();
            new Thread(() =>
            {
                Task<CheckUrlResponse> checkUrlResponse = new HttpService<CheckUrlResponse>(Constant.CHECK_DOWNLOAD_URL, HttpMethod.Post, HttpUtil.generateDefaultHeader(finalToken), HttpUtil.generateBody(Constant.LINK, finalLink)).jsonResponseAsync();
                if (checkUrlResponse != null && checkUrlResponse.Result.isSupported())
                {
                    Task<DownloadResponse> taskDownloadResponse = new HttpService<DownloadResponse>(Constant.DOWNLOAD_URL, HttpMethod.Post, HttpUtil.generateDefaultHeader(finalToken), HttpUtil.generateBody(Constant.LINK, finalLink)).jsonResponseAsync();
                    if (taskDownloadResponse != null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            DownloadResponse downloadResponse = taskDownloadResponse.Result;
                            downloadList.Add(new DownloadModel(downloadResponse));
                            if (downloadResponse.getAlternative() != null)
                            {
                                foreach (var item in downloadResponse.getAlternative())
                                {
                                    downloadList.Add(new DownloadModel(downloadResponse, item));
                                }
                            }

                        });
                    }
                }
            }).Start();
        }



        private void aggiungiMagnet(object sender, RoutedEventArgs e)
        {
            String finalMagnet = magnet.Text;
            new Thread(() =>
            {
                Task<TorrentCreateResponse> torrentCreateResponse = new HttpService<TorrentCreateResponse>(Constant.ADD_MAGNET, HttpMethod.Post, HttpUtil.generateDefaultHeader(finalToken), HttpUtil.generateBody(Constant.MAGNET, finalMagnet)).jsonResponseAsync();
                if (torrentCreateResponse != null)
                {
                    Task<TorrentResponse> torrentByIdResponse = new HttpService<TorrentResponse>(Constant.TORRENTS_INFO + torrentCreateResponse.Result.getId(), HttpMethod.Get, HttpUtil.generateDefaultHeader(finalToken), default).jsonResponseAsync();
                    if (torrentByIdResponse != null)
                    {
                        new HttpService<byte[]>(Constant.TORRENTS_SELECT_FILES + torrentByIdResponse.Result.getId(), HttpMethod.Post, HttpUtil.generateDefaultHeader(finalToken), HttpUtil.generateBody(Constant.FILES, "all")).bodyResponseAsync();
                    }
                }
            }).Start();
        }

        private void downloadTorrent(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var currentItem = button?.Tag;
            if (currentItem is TorrentModel item)
            {
                new Thread(() =>
                {
                    Task<CheckUrlResponse> taskCheckUrl = new HttpService<CheckUrlResponse>(Constant.CHECK_DOWNLOAD_URL, HttpMethod.Post, HttpUtil.generateDefaultHeader(finalToken), HttpUtil.generateBody(Constant.LINK, item.links[0])).jsonResponseAsync();
                    if (taskCheckUrl != null && taskCheckUrl.Result.isSupported())
                    {
                        Task<DownloadResponse> taskDownloadResponse = new HttpService<DownloadResponse>(Constant.DOWNLOAD_URL, HttpMethod.Post, HttpUtil.generateDefaultHeader(finalToken), HttpUtil.generateBody(Constant.LINK, item.links[0])).jsonResponseAsync();
                        if (taskDownloadResponse != null)
                        {
                            DownloadResponse downloadResponse = taskDownloadResponse.Result;
                            Task<Stream> content = new HttpService<Stream>(downloadResponse.getDownload(), HttpMethod.Get, HttpUtil.generateDefaultHeader(finalToken), default).bodyResponseAsync();
                            SaveService.saveFile(content, downloadResponse.getFilename());
                        }
                    }
                }).Start();
            }
        }

        private void deleteTorrent(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var currentItem = button?.Tag;
            if (currentItem is TorrentModel item)
            {
                new Thread(() =>
                    new HttpService<CheckUrlResponse>(Constant.DELETE_TORRENT + item.id, HttpMethod.Delete, HttpUtil.generateDefaultHeader(finalToken), default).bodyResponseAsync()
                ).Start();
            }
        }


        private void downloadLink(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var currentItem = button?.Tag;
            if (currentItem is DownloadModel item)
            {
                new Thread(() =>
                {
                    Task<Stream> taskContent = new HttpService<Stream>(item.download, HttpMethod.Get, HttpUtil.generateDefaultHeader(finalToken), default).bodyResponseAsync();
                    SaveService.saveFile(taskContent, item.filename);

                }).Start();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (torrentRefreshThread != null)
            {
                torrentRefreshThread.Interrupt();
            }

            Application.Current.Shutdown();
        }

    }
}
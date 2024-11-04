using Microsoft.Win32;
using System.IO;
using System.Net.Http;
using System.Windows;

namespace RealDebrid.service
{
    public class SaveService
    {

        public static async void saveFile(Task<HttpContent> contentTask, String filename)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = filename,
                Title = "Seleziona dove salvare il file",
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream fileStream = saveFileDialog.OpenFile())
                {
                    using (BinaryWriter writer = new BinaryWriter(fileStream))
                    {
                        try
                        {
                            using HttpContent content = await contentTask;
                            long totalBytes = content.Headers.ContentLength.Value;

                            using Stream responseStream = await content.ReadAsStreamAsync();
                            byte[] buffer = new byte[2048];
                            int bytesRead;
                            long totalBytesRead = 0;

                            ProgressWindow progressWindow = null;

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                progressWindow = new ProgressWindow();
                                progressWindow.Owner = Application.Current.MainWindow;
                                progressWindow.Show();
                            });

                            while ((bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            {
                                writer.Write(buffer, 0, bytesRead);

                                totalBytesRead += bytesRead;

                                // Calcola la percentuale di avanzamento
                                int progressPercentage = (int)((double)totalBytesRead / totalBytes * 100);
                                Application.Current.Dispatcher.Invoke(() =>

                                    progressWindow.UpdateProgress(progressPercentage)
                                );
                            }

                            Application.Current.Dispatcher.Invoke(() => progressWindow.Close());
                        }
                        catch (EndOfStreamException)
                        {
                            Console.WriteLine("Fine del flusso raggiunta.");
                        }
                    }
                    MessageBox.Show("File Scaricato con successo");
                }
            }
        }
    }
}

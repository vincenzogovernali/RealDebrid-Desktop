using Microsoft.Win32;
using System.IO;

using System.Windows;

namespace RealDebrid.service
{
    public class SaveService
    {

        public static async void saveFile(Task<Stream> content, String filename)
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
                            using (Stream responseStream = await content)
                            {
                                // Leggi dallo stream sorgente e scrivi nel file di destinazione in blocchi
                                byte[] buffer = new byte[2048];
                                int bytesRead;
                                while ((bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                                {
                                    writer.Write(buffer, 0, bytesRead);
                                }
                            }
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

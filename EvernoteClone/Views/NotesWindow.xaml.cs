using Azure.Storage.Blobs;
using EvernoteClone.Helpers;
using EvernoteClone.ViewModels;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace EvernoteClone.Views
{
    /// <summary>
    /// Lógica interna para NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        private NotesViewModel? _notesViewModel;

        public NotesWindow()
        {
            InitializeComponent();

            _notesViewModel = Resources["notevm"] as NotesViewModel;
            _notesViewModel!.SelectedNoteChanged += NotesViewModel_SelectedNoteChanged;

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fontFamilyComboBox.ItemsSource = fontFamilies;

            System.Collections.Generic.List<double> fontSizes = new() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            fontSizeComboBox.ItemsSource = fontSizes;
        }

        //Executado a cada ativação da janela
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (string.IsNullOrEmpty(App.UserId))
            {
                LoginWindow loginWindow = new();
                loginWindow.ShowDialog();
                _notesViewModel!.GetNotebooks();
            }
        }

        private async void NotesViewModel_SelectedNoteChanged(object? sender, EventArgs e)
        {
            contentRichTexBox.Document.Blocks.Clear();
            if (_notesViewModel is not null && _notesViewModel.SelectedNote is not null 
                && !string.IsNullOrEmpty(_notesViewModel!.SelectedNote.FileLocation))
            {
                string downloadPath = Path.Combine(Environment.CurrentDirectory, $"{_notesViewModel.SelectedNote.Id}.rtf");
                await new BlobClient(new Uri(_notesViewModel.SelectedNote.FileLocation)).DownloadToAsync(downloadPath);
                using FileStream fileStream = new(downloadPath, FileMode.Open);
                TextRange textRange = new(contentRichTexBox.Document.ContentStart, contentRichTexBox.Document.ContentEnd);
                textRange.Load(fileStream, DataFormats.Rtf);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) 
            => Application.Current.Shutdown();

        private void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            //Utilizar o Azure Speech service para converter o texto em voz
            string region = "brazilsouth";
            string key = "SUA-CHAVE-DE-ACESSO-AQUI";
            var speechConfig = SpeechConfig.FromSubscription(key, region);
            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var synthesizer = new SpeechRecognizer(speechConfig, audioConfig);
            var result = synthesizer.RecognizeOnceAsync().Result;
            contentRichTexBox.Document.Blocks.Add(new Paragraph(new Run(result.Text)));
        }

        private void ContentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ammountCharacteres = new TextRange(contentRichTexBox.Document.ContentStart, contentRichTexBox.Document.ContentEnd).Text.Length;

            statusTextBox.Text = $"Document length: {ammountCharacteres} characters";
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = (sender as ToggleButton)!.IsChecked ?? false;

            if (isChecked)
                contentRichTexBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            else
                contentRichTexBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
        }

        private void ContentRichTexBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedWeight = contentRichTexBox.Selection.GetPropertyValue(FontWeightProperty);

            boldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) 
                                    && (selectedWeight is FontWeight weight)
                                    && weight == FontWeights.Bold;

            var selectedStyle = contentRichTexBox.Selection.GetPropertyValue(FontStyleProperty);
            italicButton.IsChecked = (selectedStyle != DependencyProperty.UnsetValue)
                                    && (selectedStyle is FontStyle style)
                                    && style == FontStyles.Italic;

            var selectedDecoration = contentRichTexBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            underlineButton.IsChecked = (selectedDecoration != DependencyProperty.UnsetValue)
                                    && (selectedDecoration is TextDecorationCollection decoration)
                                    && decoration == TextDecorations.Underline;

            fontFamilyComboBox.SelectedItem = contentRichTexBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            fontSizeComboBox.Text = contentRichTexBox.Selection.GetPropertyValue(Inline.FontSizeProperty).ToString();
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = (sender as ToggleButton)!.IsChecked ?? false;

            if (isChecked)
                contentRichTexBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
            {
                TextDecorationCollection textDecorations = null!;
                (contentRichTexBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection)?
                    .TryRemove(TextDecorations.Underline,out textDecorations);
                contentRichTexBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = (sender as ToggleButton)!.IsChecked ?? false;

            if (isChecked)
                contentRichTexBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            else
                contentRichTexBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontStyles.Normal);
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(fontFamilyComboBox.SelectedItem != null)
                contentRichTexBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontFamilyComboBox.SelectedItem);
        }

        private void FontSizeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            contentRichTexBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeComboBox.Text);
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(_notesViewModel is not null && _notesViewModel!.SelectedNote is null)
            {
                MessageBox.Show("Please select a note to save", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string fileName = $"{_notesViewModel!.SelectedNote.Title}.rtf";
            string rtfFile = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);

            using FileStream fileStream = new(rtfFile, FileMode.Create);
            TextRange textRange = new(contentRichTexBox.Document.ContentStart, contentRichTexBox.Document.ContentEnd);
            textRange.Save(fileStream, DataFormats.Rtf);

            _notesViewModel.SelectedNote.FileLocation = await UploadFileToStorage(rtfFile, fileName);
            DatabaseHelper.Update(_notesViewModel.SelectedNote);
            MessageBox.Show("Note saved successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async Task<string> UploadFileToStorage(string rtfFilePath, string fileName)
        {
            string connectionString = "SUA-CONEXAO-AQUI";
            string containerName = "notes";
            string blobName = fileName;
            BlobContainerClient containerClient = new(connectionString, containerName);
            await containerClient.CreateIfNotExistsAsync();
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(rtfFilePath, true);
            return $"{blobClient.Uri.AbsoluteUri}/{fileName}";
        }
    }
}

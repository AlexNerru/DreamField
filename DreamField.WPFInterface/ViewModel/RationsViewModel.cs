using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DreamField.ServiceLayer;
using DreamField.ServiceLayer.Dto;
using DreamField.WPFInterface.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;
using Microsoft.Office.Interop.Word;
using Task = System.Threading.Tasks.Task;

namespace DreamField.WPFInterface.ViewModel
{
    public class RationsViewModel : ViewModelBase
    {
        private readonly IRationService _rationService;
        private readonly ICustomFrameNavigationService _navigationService;
        private readonly IUserService _userService;
        private ObservableCollection<RationInfoDto> _rations;


        public ISnackbarMessageQueue MessageQueue { get; set; }

        public RelayCommand AddRationCommand { get; private set; }
        public RelayCommand<int> DeleteRationCommand { get; private set; }
        public RelayCommand<RationInfoDto> PrintRationCommand { get; private set; }
        public ObservableCollection<RationInfoDto> Rations
        {
            get => _rations;
            set
            {
                _rations = value;
                RaisePropertyChanged("Rations");
            }
        }

        public RationsViewModel(ICustomFrameNavigationService navigationService,
            IRationService rationService, IUserService userService)
        {
            _navigationService = navigationService;
            _rationService = rationService;
            _userService = userService;
            Task.Factory.StartNew(LoadRations);
            AddRationCommand = new RelayCommand(AddRation);
            DeleteRationCommand = new RelayCommand<int>(DeleteRation);
            PrintRationCommand = new RelayCommand<RationInfoDto>(PrintRation);
            MessageQueue = new SnackbarMessageQueue();
            MessengerInstance.Register<UpdateRationsMessage>(this, RationCreatedMessageHandler);
        }

        private void AddRation() => _navigationService.NavigateTo("CreateRation");

        private void DeleteRation(int rationId)
        {
            _rationService.DeleteRation(new RationDeleteDto { Id = rationId });
            Rations.Remove(Rations.FirstOrDefault(ration => ration.Id == rationId));
        }

        //Переделать сообщение на перенос типа Ration
        private void RationCreatedMessageHandler(UpdateRationsMessage message)
            => Rations = new ObservableCollection<RationInfoDto>(_rationService.GetAllRations(_userService.LoggedUser.Id));

        private void PrintRation(RationInfoDto dto)
        {
            Task.Factory.StartNew(() => MessageQueue.Enqueue("Ваш рацион сохраняется в папку с программой"));

            AnimalTypesToStringConverter converter = new AnimalTypesToStringConverter();

            object missing = Missing.Value;

            Document document = WordInizialiser.Word.Documents.Add();

            foreach (Section section in document.Sections)
            {
                Range headerRange = section.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                headerRange.Fields.Add(headerRange, WdFieldType.wdFieldPage);
                headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                headerRange.Font.ColorIndex = WdColorIndex.wdBlack;
                headerRange.Font.Size = 14;
                headerRange.Text = $"{dto.Comment}";
            }

            
            Paragraph para1 = document.Content.Paragraphs.Add();
            para1.Range.Font.Size = 14;
            para1.Range.ParagraphFormat.Alignment  = WdParagraphAlignment.wdAlignParagraphCenter;
            para1.Range.Text += "Общая информация";
            para1.Range.InsertParagraphAfter();

            Paragraph para4 = document.Content.Paragraphs.Add();
            para4.Range.Font.Size = 12;
            para4.Range.Text += $"Хозяйство: {dto.FarmName}" + Environment.NewLine;
            para4.Range.Text += $"Тип животного: {converter.Convert(dto.Animal, null, null, null)}" + Environment.NewLine;

            Paragraph para2 = document.Content.Paragraphs.Add();
            para2.Range.Font.Size = 14;
            para2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            para2.Range.Text += "Структура рациона";
            para2.Range.InsertParagraphAfter();

            Paragraph para5 = document.Content.Paragraphs.Add();
            para5.Range.Font.Size = 12;
            para5.Range.Text += $"Грубые корма: {dto.RoughPercent}" + Environment.NewLine;
            para5.Range.Text += $"Сочные корма: {dto.JuicyPercent}" + Environment.NewLine;
            para5.Range.Text += $"Концентраты: {dto.Consentrates}" + Environment.NewLine;

            Paragraph para3 = document.Content.Paragraphs.Add();
            para3.Range.Font.Size = 14;
            para3.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            para3.Range.Text += "Корма";
            para3.Range.InsertParagraphAfter();

            Paragraph para6 = document.Content.Paragraphs.Add();
            para6.Range.Font.Size = 12;
            foreach (RationFeedsDto rationFeedsDto in dto.Feeds)
                para6.Range.Text += $"{rationFeedsDto.Feed}: {rationFeedsDto.Amount:F} кг\n";

            //Save the document
            object filename = Directory.GetCurrentDirectory() + @"\" + dto.Comment + ".docx";
            Console.WriteLine(Directory.GetCurrentDirectory());
            try
            {
                document.SaveAs(ref filename);
                document.Close();
                document = null;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Task.Factory.StartNew(() => MessageQueue.Enqueue("Ошибка сохранения"));
            }
            Task.Factory.StartNew(() => MessageQueue.Enqueue("Рацион успешно сохранен"));

        }

        private void LoadRations()
            => Rations = new ObservableCollection<RationInfoDto>(_rationService.GetAllRations(_userService.LoggedUser.Id));

    }
}

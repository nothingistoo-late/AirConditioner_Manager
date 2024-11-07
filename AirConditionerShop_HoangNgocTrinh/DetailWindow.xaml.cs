using AirConditionerShop.BLL.Services;
using AirConditionerShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AirConditionerShop_HoangNgocTrinh
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {

        // màn hình này cần 2 service, Airconservice để crud table aircon
        //supplier service là thằng lo cái combobox, hộp xổ xuống để chọn ncc

        private AirConService _AirConService = new();
        private SupplierService _SupplierService = new();

        // ta chế thêm trong class detail này 1 prop để hứng cái selected aircon từ bên main window chuyển sang ở mode edit 
        // tức là mode edit là ta cần edit infor của 1 object/ aircon nào đó, cho nên ở class này phải có biển để lưu,, trỏ cái máy lạnh thằng edit
        // còn màn hình này, class này mở, dc new ở chế độ create mode, kh cần biến này, prop này vì create mode là màn hình trắng trơn
        // prop này đóng vai trò biên flag để quyết định mode của class này là mode create hay update nhờ có mode/flag ta biết dc nút save khi nhấn sẽ gọi hàm insert in to hay hàm update from của service, vì 1 màn hình vừa xài create vừa update

        public AirConditioner EditedOne { get; set; }
        // new class này mà kh nói năng thì cả thì nó null
        // nếu nó dc .EditedOne = selected từ bên main nghĩa là chế độ edit 
        // ta xài editedOne. để lấy các infor của máy lạnh để fill vào các ô nhập 


        public DetailWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // 1. load fill cái combo box nhà cung cấp để sẵn cho mọi người lựa chọn lúc tạo mới máy lạnh

            // 2. nếu tạo mới để màn hình trắng cho user nhập mới 1 máy lạnh và chọn từ danh sách ncc nào
            // 3. nếu là edit thì phải show full infor của thằng selected để edit, đổ info của thằng selected bên main vào các ô trên màn hình này
            // 3.1 đặt biệt khi edit, phải nhảy đến ncc mà thằng selected đang thuộc về
            // vd: mình là sinh viên SE, thì edit sv SE thì phải xổ cái combo sẵn mang chữ SE trong danh sách 10 chuyên ngành của trường
            // đổ combo hoy

            SupplierIdComboBox.ItemsSource = _SupplierService.GetAllNCC();
            SupplierIdComboBox.DisplayMemberPath = "SupplierName";
            SupplierIdComboBox.SelectedValuePath = "SupplierId";

            if (EditedOne != null)

            { // là đang ở edited mode
                // đổi data của object vào GUI
                DetailWindowModeLabel.Content = "Update Air Conditioner";
                AirConditionerIdTextBox.IsEnabled = false;
                AirConditionerIdTextBox.Text = EditedOne.AirConditionerId.ToString();
                AirConditionerNameTextBox.Text = EditedOne?.AirConditionerName;
                WarrantyTextBox.Text = EditedOne?.Warranty;
                SoundPressureLevelTextBox.Text = EditedOne?.SoundPressureLevel;
                FeatureFunctionTextBox.Text = EditedOne?.FeatureFunction;
                QuantityTextBox.Text = EditedOne?.Quantity.ToString();
                DollarPriceTextBox.Text = EditedOne?.DollarPrice.ToString();
                SupplierIdComboBox.SelectedValue = EditedOne.SupplierId;

            }
            else DetailWindowModeLabel.Content = "Create Air Conditioner";

        }

        private void SupplierIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            // nút save xài chung cho edited mode và create mode nên ta cần phân biệt khi nào gọi create hay edit, may quá ta có biến EditedOne
            // kiểu gì thì kiểu, vẫn phải cbi 1 object để đưa 2 hàm của service
            if (!CheckVar())
                return;
            AirConditioner obj = new(); // kh chơi object initialization vì nó dài 
            obj.AirConditionerId = int.Parse(AirConditionerIdTextBox.Text);
            obj.AirConditionerName = AirConditionerNameTextBox.Text;
            obj.Warranty = WarrantyTextBox.Text;
            obj.FeatureFunction = FeatureFunctionTextBox.Text;
            obj.SoundPressureLevel = SoundPressureLevelTextBox.Text;
            obj.Quantity = int.Parse(QuantityTextBox.Text);
            obj.DollarPrice = double.Parse(DollarPriceTextBox.Text);
            obj.SupplierId = SupplierIdComboBox.SelectedValue.ToString();

            if (EditedOne != null)
            {
                _AirConService.Update(obj);
                DetailWindowModeLabel.Content = "Update Air Conditioner";
            }
            else
            {
                DetailWindowModeLabel.Content = "Create Air Conditioner";
                _AirConService.Add(obj);
            }
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool CheckVar()
        {
            if (string.IsNullOrWhiteSpace(AirConditionerNameTextBox.Text))
            {
                MessageBox.Show("AirConName Is Required!!", "Require Information", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (AirConditionerNameTextBox.Text.Trim().Length <= 5 || AirConditionerNameTextBox.Text.Trim().Length >= 90)
            {
                MessageBox.Show("AirConName Length Must Be >=5 and <= 90!!", "Require Information", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            TextInfo textInfor = CultureInfo.CurrentCulture.TextInfo;

            string airConName = AirConditionerNameTextBox.Text.Trim();

            airConName = textInfor.ToTitleCase(airConName.ToLower());
            AirConditionerNameTextBox.Text = airConName;

            if (string.IsNullOrWhiteSpace(WarrantyTextBox.Text))
            {
                MessageBox.Show("Warranty Is Required!!", "Require Information", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(SoundPressureLevelTextBox.Text))
            {
                MessageBox.Show("Sound Pressure Level Is Required!!", "Require Information", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(FeatureFunctionTextBox.Text))
            {
                MessageBox.Show("Feature Function Is Required!!", "Require Information", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(QuantityTextBox.Text))
            {
                MessageBox.Show("Quantity Is Required!!", "Require Information", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            bool convertStatus = int.TryParse(QuantityTextBox.Text, out int quantity);

            if (!convertStatus)
            {
                MessageBox.Show("Quantity Must Be A Number!!", "Require Information", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (quantity < 50 || quantity > 100)
            {
                MessageBox.Show("Quantity Must Between 50 and 100!!", "Require Information", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(DollarPriceTextBox.Text))
            {
                MessageBox.Show("Dollar Price Is Required!!", "Require Information", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            convertStatus = double.TryParse(DollarPriceTextBox.Text, out double dollarPrice);

            if (!convertStatus)
            {
                MessageBox.Show("Dollar Price Must Be A Number!!", "Require Information", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (SupplierIdComboBox.SelectedValue == null)
            {
                MessageBox.Show("Supplier Is Required!!", "Require Information", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}


using AirConditionerShop.BLL.Services;
using System;
using System.Collections.Generic;
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
        }

        private void SupplierIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // in thử coi 
            MessageBox.Show("Ban Da Chon Nha Cung Cap :" + SupplierIdComboBox.SelectedValue.ToString());
        }
    }
}

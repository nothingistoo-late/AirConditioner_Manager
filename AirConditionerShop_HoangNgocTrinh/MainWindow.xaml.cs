using AirConditionerShop.BLL.Services;
using AirConditionerShop.DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirConditionerShop_HoangNgocTrinh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AirConService _AirConService = new();

        public StaffMember CurruntAccount { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // nhấn nút create thì mở cửa detail trắng trơn, để cho user gõ thông tin máy lạnh mới - AirConditoner Class
            // mỗi cửa sổ bản chất là 1 class nên ta sẽ tạo new
            DetailWindow d = new();
            d.ShowDialog();// làm xong rồi mới quay về MainWindow
            FillDataGrid(_AirConService.GetAllAirCons());

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AirCondDataGrid.ItemsSource = _AirConService.GetAllAirCons();
            if (CurruntAccount == null)
                return;
            if ( CurruntAccount.Role == 2 )
            {
                UpdateButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                CreateButton.IsEnabled = false;
            }

            HelloMsgLabel.Content = "Hello " + CurruntAccount.FullName;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // phải check khi user nhấn nút update, user đã chọn 1 dòng nào chưa, chưa chọn thì thông báo, kh chọn edit như nào???
            // làm sao biết user đã chọn 1 dòng???
            // khi chọn 1 dòng tử tế(kh chọn dòng trắng ở đáy) thì cái data grid (DG) nó dc thiết kế để lắng nghe user làm gì với nó, nếu user click 1 dòng khác trắng, thì nó ngay lập tức set 1 cái PROP tên là SELECTEDITEMS = trỏ đến cái object dc chọn mà ngay đang show trên màn hình
            //SelectedItem -> chính là cái máy lạnh trong RAM dc forcus
            // nếu chọn dòng trắng hay chưa chọn gì cả thì 
            // .SelectedItems == null;
            // đó là căn cứ để ta chửi hay chuyển hướng cái object này sang màn hình Detail
            AirConditioner? selected = AirCondDataGrid.SelectedItem as AirConditioner;//as : ép kiểu từ object sang máy lạnh
                                                                                      // (máy lạnh) ép kiểu này dễ bị exception
                                                                                      // as : ép không dc thì gán null
            if (selected == null)
            {
                // chửi việc kh có chịu bấm 1 dòng mà bày đặt đòi edit
                MessageBox.Show("Please select an AirCon Before Updated", "Select one AirCon to update!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;// kh chọn 1 dòng, thoát, không làm gì cả
            }

            // đoạn này là xử lý update
            // bắt dc cái 1 caí máy lạnh cần sửa rồi, gửi nó sang hình detail để show lên
            // từ từ, check thử, in thử xem đúng máy lạnh vừa chọn không
            //MessageBox.Show("May lanh edit := " + selected.AirConditionerId + " | " + selected.AirConditionerName);
            DetailWindow d = new();
            // todo: hết sức cẩn thận, phải gửi thằng selected qua màn hình detail
            // để detail có object mà show ra khi edit
            d.EditedOne = selected; // 2 chàng 1 nàng
            // chốt hạ: 2 biến object mà gán bằng nhau, nghĩa là trỏ cùng nghĩa là truyền tham chiếu, trỏ cùng 1 chỗ, vì nếu tạo mới object thì phải có toán tử new dc đem ra dùng
            d.ShowDialog();
            FillDataGrid(_AirConService.GetAllAirCons());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            AirConditioner? selected = AirCondDataGrid.SelectedItem as AirConditioner;//as : ép kiểu từ object sang máy lạnh
                                                                                      // (máy lạnh) ép kiểu này dễ bị exception
                                                                                      // as : ép không dc thì gán null
            if (selected == null)
            {
                // chửi việc kh có chịu bấm 1 dòng mà bày đặt đòi edit
                MessageBox.Show("Please select an AirCon Before Deleting", "Select one AirCon to deleting!!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;// kh chọn 1 dòng, thoát, không làm gì cả
            }

            // chính thức xóa vì đã chọn 1 dòng
            // gọi service thoiii
            // confirm 0.75 điểm nha iem

            MessageBoxResult answer = MessageBox.Show("Are You Sure Delete???", "Confirm Delete???", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
                return;
            _AirConService.Delete(selected);
            // xóa xong, phải f5 lại cái grid, vì grid chơi với ram, đâu có đồng bộ với dtb sau khi bị xóa
            // cực kì quan trọng: tạo mới, update, delete đều phải f5 cái grid, chưa kể mở màn hình này cũng phải load grid, trong giang hồ, cái nào lặp đi lặp lạo thì ta tách hàm
            // hàm này trợ giúp các hàm khác, hàm helper
            FillDataGrid(_AirConService.GetAllAirCons());

        }

        // ta viết hàm helper giúp f5 cái grid

        private void FillDataGrid(List<AirConditioner> list)
        {
            // xóa lưới cũ, đổ lưới mới
            AirCondDataGrid.ItemsSource = null; // xóa data cũ
            AirCondDataGrid.ItemsSource = list;
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are You Sure Want To Exit!!!", "Exit!!!!", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if ((!QuantityTextBox.Text.IsNullOrEmpty()) && (int.TryParse(QuantityTextBox.Text, out int result) == false)) // check if quantity is not emty, and itsnt number
            {
                MessageBox.Show("Quantity Must Be Number", "Invalid Vallue", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //quantity or function has value
            if (!FeatureFunctionTextBox.Text.IsNullOrEmpty() && QuantityTextBox.Text.IsNullOrEmpty())
            {
                FillDataGrid(_AirConService.SearchAirConByFeatureAndQuantity(FeatureFunctionTextBox.Text, null));
                return;
            }
            if (FeatureFunctionTextBox.Text.IsNullOrEmpty() && !QuantityTextBox.Text.IsNullOrEmpty())
            {
                {
                    FillDataGrid(_AirConService.SearchAirConByFeatureAndQuantity(null, int.Parse(QuantityTextBox.Text)));
                    return;
                }
            }
            if (FeatureFunctionTextBox.Text.IsNullOrEmpty () && QuantityTextBox.Text.IsNullOrEmpty())
            {
                FillDataGrid(_AirConService.SearchAirConByFeatureAndQuantity(null, null));
                return;
            }    
            // both of them has value
            FillDataGrid(_AirConService.SearchAirConByFeatureAndQuantity(FeatureFunctionTextBox.Text, int.Parse(QuantityTextBox.Text)));
        }
    }
}
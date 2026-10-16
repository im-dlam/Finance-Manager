using BankAccountManagement.Forms;
using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement
{
    public partial class MainForm : Form
    {

        /// <summary>
        /// Khởi tạo MainForm với user đã đăng nhập.
        /// </summary>
        /// <param name="user">User hiện tại (bắt buộc, không được null).</param>
        public MainForm(User user)
        {
            InitializeComponent();
            if (CurrentSession.IsMember)
            {
                LoadMultiForm(new DashboardUserForm());
            }
            else
            {
                LoadMultiForm(new DashboardForm());
            }
            ApplyRolePermissions();
        }

        /// <summary>
        /// Hiển thị Form con bên trong pnMain.
        /// Nếu pnMain đang chứa Form khác, Form cũ sẽ được xóa trước.
        /// </summary>
        /// <param name="form">
        /// Form cần hiển thị bên trong pnMain.
        /// </param>
        // <summary>
        /// Hiển thị Form con bên trong pnMain.
        /// Dispose form cũ trước khi thêm form mới.
        /// 
        /// ⚠ Dùng try/catch để tránh crash nếu Dispose lỗi
        /// và đảm bảo không gây CallbackOnCollectedDelegate.
        /// </summary>
        public void LoadMultiForm(Form form)
        {
            if (form == null) return;

            // ⭐ Gộp: dispose TẤT CẢ form cũ trong pnMain
            DisposeCurrentForm();

            // Setup form mới
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            pnMain.Controls.Add(form);
            pnMain.Tag = form;

            form.Show();
        }

        /// <summary>
        /// Dispose form hiện tại trong pnMain (nếu có).
        /// An toàn với try/catch — không throw ra ngoài.
        /// </summary>
        private void DisposeCurrentForm()
        {
            if (pnMain.Controls.Count == 0) return;

            // Copy ra list trước khi Clear (tránh modify collection khi đang duyệt)
            var oldControls = new System.Collections.Generic.List<Control>();
            foreach (Control c in pnMain.Controls)
                oldControls.Add(c);

            pnMain.Controls.Clear();

            foreach (var ctrl in oldControls)
            {
                if (ctrl.IsDisposed) continue;

                try
                {
                    if (ctrl is StatisticsForm sf)
                    {
                        sf.CleanupResources();
                    }
                    if (ctrl is Form f)
                    {
                        f.Close();
                    }
                    ctrl.Dispose();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"[LoadMultiForm] Dispose error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Phân quyền dựa trên vai trò của người dùng hiện tại (Admin/Member).
        /// Nếu là Member, ẩn các menu không được phép truy cập (Quản trị khách hàng, Ngân hàng)
        /// và kéo các nút chức năng bên dưới lên liền mạch, không để lại khoảng trống.
        /// </summary>
        private void ApplyRolePermissions()
        {
            Console.WriteLine($"CurrentSession.IsAdmin: {CurrentSession.IsAdmin}");

            if (CurrentSession.IsAdmin)
            {
                btnCustomers.Visible = true;
                btnBanks.Visible = true;
                ArrangeSidebarMenu(isAdmin: true);
                return;
            }

            // Member – ẩn menu quản trị khách hàng và ngân hàng
            btnCustomers.Visible = false;
            btnCustomers.Height = 0;

            btnBanks.Visible = false;
            btnBanks.Height = 0;

            // Kéo các nút chức năng ở dưới lên liền mạch
            ArrangeSidebarMenu(isAdmin: false);
        }

        /// <summary>
        /// Sắp xếp vị trí các nút menu bên trái theo thứ tự và khoảng cách chuẩn (46px),
        /// loại bỏ hoàn toàn khoảng cách trống khi ẩn các mục quản trị ở phía Member.
        /// </summary>
        /// <param name="isAdmin">True nếu là tài khoản Quản trị viên, False nếu là Khách hàng (Member).</param>
        private void ArrangeSidebarMenu(bool isAdmin)
        {
            int currentTop = 106;
            const int buttonHeight = 40;
            const int spacing = 6;
            const int buttonWidth = 154;
            const int buttonLeft = 16;
            int step = buttonHeight + spacing; // 46px

            Button[] menuButtons = isAdmin
                ? new Button[]
                {
                    btnDashboards,
                    btnAccounts,
                    btnTransactions,
                    btnCustomers,
                    btnBanks,
                    btnCreditCard,
                    btnShop,
                    btnBorrowing,
                    btnSaveMoney,
                    btnStatistic
                }
                : new Button[]
                {
                    btnDashboards,
                    btnAccounts,
                    btnTransactions,
                    btnCreditCard,
                    btnShop,
                    btnBorrowing,
                    btnSaveMoney,
                    btnStatistic
                };

            foreach (var btn in menuButtons)
            {
                btn.Left = buttonLeft;
                btn.Top = currentTop;
                btn.Width = buttonWidth;
                btn.Height = buttonHeight;
                btn.Visible = true;
                currentTop += step;
            }
        }

        private void btnDashboards_Click(object sender, EventArgs e)
        {
            if (CurrentSession.IsMember)
            {
                LoadMultiForm(new DashboardUserForm());
            }
            else
            {
                LoadMultiForm(new DashboardForm());
            }
        }

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            OpenFormWithCustomerCheck(() => new AccountForm()); // Hiển thị Form AccountForm bên trong pnMain

        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            //LoadMultiForm(new TransactionForm()); // Hiển thị Form TransactionForm bên trong pnMain
            OpenFormWithCustomerCheck(() => new TransactionForm());
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            if (!CurrentSession.IsAdmin) return;
            LoadMultiForm(new CustomerForm()); // Hiển thị Form CustomerForm bên trong pnMain
        }

        private void btnBanks_Click(object sender, EventArgs e)
        {
            if (!CurrentSession.IsAdmin) return;
            LoadMultiForm(new BankForm()); // Hiển thị Form BankForm bên trong pnMain
        }

        private void btnStatistic_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new StatisticsForm()); // Hiển thị Form StatisticForm bên trong pnMain
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new SettingsForm());
        }

        private void pnMain_Paint(object sender, PaintEventArgs e)
        {
        }
        
        /// <summary>
        /// Dọn dẹp form con trong pnMain TRƯỚC KHI MainForm đóng.
        /// 
        /// Không làm bước này -> form con vẫn giữ delegate WndProc 
        /// -> khi form bị GC sẽ gây lỗi CallbackOnCollectedDelegate.
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DisposeChildForms();
            base.OnFormClosed(e);
        }

        /// <summary>
        /// Dispose tất cả form con đang nhúng trong pnMain.
        /// Gọi từ OnFormClosed để đảm bảo giải phóng sạch trước khi app thoát.
        /// </summary>
        private void DisposeChildForms()
        {
            if (pnMain == null || pnMain.IsDisposed)
                return;

            if (pnMain.Controls.Count == 0)
                return;

            // Copy ra mảng trước khi Clear để tránh lỗi collection bị modify khi duyệt
            var children = new System.Collections.Generic.List<Control>();
            foreach (Control c in pnMain.Controls)
                children.Add(c);

            pnMain.Controls.Clear();

            foreach (var ctrl in children)
            {
                try { ctrl.Dispose(); }
                catch { /* bỏ qua nếu đã dispose */ }
            }
        }

        public void OpenFormWithCustomerCheck(Func<Form> formFactory)
        {
            if (CurrentSession.IsAdmin)
            {
                LoadMultiForm(formFactory());
                return;
            }

            if (CurrentSession.HasCustomerInfo)
            {
                LoadMultiForm(formFactory());
                return;
            }

            var infoForm = new CustomerInfoForm();

            // Khi user save thành công → tự mở form đích
            infoForm.SaveSuccess += (s, args) =>
            {
                System.Diagnostics.Debug.WriteLine(
                    "[MainForm] SaveSuccess → mở form đích");

                LoadMultiForm(formFactory());
            };

            LoadMultiForm(infoForm);
        }

        private void btnCreditCard_Click(object sender, EventArgs e)
        {
            if (CurrentSession.IsAdmin)
            {
                LoadMultiForm(new CreditCardForm()); // Hiển thị Form quản lý thẻ Admin
            }
            else
            {
                OpenFormWithCustomerCheck(() => new CreditCardUserForm()); // Hiển thị Form thẻ ATM của User
            }
        }

        private void btnShop_Click(object sender, EventArgs e)
        {
            if (CurrentSession.IsAdmin)
            {
                LoadMultiForm(new ShopAdminForm()); // Giao diện quản lý sản phẩm Admin
            }
            else
            {
                OpenFormWithCustomerCheck(() => new ShopUserForm()); // Giao diện mua sắm Khách hàng
            }
        }

        private void btnBorrowing_Click(object sender, EventArgs e)
        {
            if (CurrentSession.IsAdmin)
            {
                LoadMultiForm(new BorrowingAdminForm()); // Giao diện thẩm định & phê duyệt khoản vay Admin
            }
            else
            {
                OpenFormWithCustomerCheck(() => new BorrowingUserForm()); // Giao diện đăng ký vay vốn Khách hàng
            }
        }

        private void btnSaveMoney_Click(object sender, EventArgs e)
        {
            if (CurrentSession.IsAdmin)
            {
                LoadMultiForm(new SaveMoneyAdminForm()); // Giao diện quản lý tiết kiệm Admin
            }
            else
            {
                OpenFormWithCustomerCheck(() => new SaveMoneyUserForm()); // Giao diện gửi tiết kiệm Khách hàng
            }
        }
    }
}


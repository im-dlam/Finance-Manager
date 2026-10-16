using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    public partial class ShopForm : Form
    {
        public ShopForm()
        {
            InitializeComponent();
            WireEvents();
        }
        private void WireEvents()
        {
            btnCart.Click += (s, e) => MessageBox.Show("Mở giỏ hàng");
            btnCart1.Click += (s, e) => AddToCart("iPhone 13");
            btnBuy1.Click += (s, e) => Buy("iPhone 13");
            btnCart2.Click += (s, e) => AddToCart("MacBook Air M1");
            btnBuy2.Click += (s, e) => Buy("MacBook Air M1");
            btnCart3.Click += (s, e) => AddToCart("Tai nghe Bluetooth");
            btnBuy3.Click += (s, e) => Buy("Tai nghe Bluetooth");
            btnCart4.Click += (s, e) => AddToCart("Apple Watch S8");
            btnBuy4.Click += (s, e) => Buy("Apple Watch S8");
            btnCart5.Click += (s, e) => AddToCart("AirPods Pro 2");
            btnBuy5.Click += (s, e) => Buy("AirPods Pro 2");
            btnCart6.Click += (s, e) => AddToCart("Samsung S23 Ultra");
            btnBuy6.Click += (s, e) => Buy("Samsung S23 Ultra");
        }

        private void AddToCart(string product)
            => MessageBox.Show($"Đã thêm \"{product}\" vào giỏ hàng!", "Thông báo");

        private void Buy(string product)
            => MessageBox.Show($"Mua \"{product}\"?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    }
}

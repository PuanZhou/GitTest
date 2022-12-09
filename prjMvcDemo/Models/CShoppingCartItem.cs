using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace prjMvcDemo.Models
{
    public class CShoppingCartItem
    {
        public int productId { get; set; }
        [DisplayName("購買量")]
        public int count { get; set; }
        public decimal price { get; set; }
        public decimal 小計 { get { return this.count * this.price; }  }
        public tProduct product { get; set; }
    }
}
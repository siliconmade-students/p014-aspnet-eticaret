using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eticaret.Web.Mvc.Models
{
    public class CheckoutModel
    {
        public List<CartSessionModel> Cart { get; set; }
        public List<SelectListItem> UserAddresses { get; set; }
        public int SelectedUserAddress { get; set; }
        public CreditCartModel CreditCard { get; set; }
    }
}

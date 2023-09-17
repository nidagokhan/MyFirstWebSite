using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Data.Models
{
    public class Order
    {
        [BindNever] //değişiklik yapılmasını istediğimiz datalar için kullanırız
        public int OrderID { get; set; }

        public List<OrderDetail> OrderLines { get; set; }

        [Required(ErrorMessage ="Please enter your first name!")]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name!")]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your address!")]
        [Display(Name = "Address Line1")]
        [StringLength(100)]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line2")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Please enter your zip code!")]
        [Display(Name = "Zip Code")]
        [StringLength(10, MinimumLength =4)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Please enter your city")]
        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter your country")]
        [StringLength(100)]
        public string Country { get; set; }

        [Required(ErrorMessage ="Please enter your phone number!")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="Please enter your email address!")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email address is not entered in a correct format")]
        public string Email { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public decimal OrderTotal { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderPlaced { get; set; } //order date

       
        /// [BindNever]: BindNever özelliği ile işaretlenen bir model özelliği, gelen HTTP isteğindeki veriyle eşleştirilmez. Yani, bu özellikle işaretlenmiş bir özellik, HTTP isteğinden gelen veriyle bağlanmaz ve bu özelliğe değer atanmaz. Bu, güvenlik veya veri girişine yönelik koruma sağlamak için kullanılabilir.
        /// *** *** ***
        /// [ScaffoldColumn(false)]: Bu öznitelik, genellikle Razor sayfalarında kullanılır. Bu özelliği işaretlenen bir model özelliği, otomatik oluşturulan formlarda veya diğer görüntüleme mekanizmalarında gösterilmez. Yani, bu özellikle işaretlenmiş bir özellik, otomatik olarak oluşturulan formlarda veya görünümlerde gözükmez.
       
    }
}

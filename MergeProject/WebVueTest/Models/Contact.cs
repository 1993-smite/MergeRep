using System.ComponentModel.DataAnnotations;

namespace WebVueTest.Models
{
    public class Contact
    {
        public enum ContactStatus
        {
            Active=1,
            Delete=9
        }

        public int Id { get; set; }

        [Display(Name = "pName")]
        public string Name { get; set; }
        [Display(Name = "pPhone")]
        public string Phone { get; set; }
        public ContactStatus Status { get; set; }
    }

    public class ContactValidate : Contact
    {
        [Display(Name = "pName")]
        [Required (ErrorMessage= "pErrorNameRequired")]
        [StringLength(100, ErrorMessage = "pErrorNameStringLength", MinimumLength = 6)]
        public string NameValid => Name;

        [Display(Name = "pPhone")]
        [Required (ErrorMessage= "pErrorPhoneRequired")]
        [StringLength(100, ErrorMessage = "pErrorPhoneStringLength", MinimumLength = 4)]
        public string PhoneValid => Phone;

        public ContactValidate()
        {
        }
    }
}

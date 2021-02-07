using System.ComponentModel.DataAnnotations;

namespace Raat.Web.ViewModels
{
    public class RandomConnection
    {
        [Required]
        [MaxLength(100)]
        public string Id { get; set; } = string.Empty;
    }
}

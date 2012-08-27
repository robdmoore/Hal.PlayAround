using System.ComponentModel.DataAnnotations;

namespace Hal.PlayAround.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
namespace Epica.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        [MaxLength(15), MinLength(3)]
        public string Username { get; set; }

        [Required]
        [MaxLength(20), MinLength(5)]
        public string Password { get; set; }

        [Required]
        [MaxLength(30), MinLength(8)]
        public string Email { get; set; }

        public virtual Hero Hero { get; set; }
        public Guid HeroId { get; set; }

        public virtual Crafter Crafter { get; set; }
        public Guid CrafterId { get; set; }

        public virtual Gatherer Gatherer { get; set; }
        public Guid GathererId { get; set; }
    }
}

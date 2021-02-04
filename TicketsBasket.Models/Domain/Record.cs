using System;
using System.ComponentModel.DataAnnotations;

namespace TicketsBasket.Models.Domain
{
    public class Record
    {
        [Key]
        public string Id { get; set; } 

        public Record( )
        {
            Id = Guid.NewGuid().ToString();
        }      
    }
}
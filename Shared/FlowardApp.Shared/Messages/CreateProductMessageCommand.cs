using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowardApp.Shared.Messages
{
    public class CreateProductMessageCommand
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Cost { get; set; }

        public string Image { get; set; }
    }
}

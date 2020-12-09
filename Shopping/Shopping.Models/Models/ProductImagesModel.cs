using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Models.Models
{
    public class ProductImagesModel: BaseModel
    {
        public long ProductId { get; set; }
        public string Image { get; set; }
    }
}

using Shopping.Models.Enums;
using Shopping.Models.Models;
using Shopping.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopping.Services
{
    public class ProductsService : IProductsService
    {
        /// <summary>
        /// Gets the available products list by category name.
        /// </summary>
        /// <param name="categoryName">The target category Name</param>
        /// <returns>The found prodycts.</returns>
        public IEnumerable<ProductModel> GetProductsByGategoryName(string categoryName)
        {
            ProductsEnum productCategory;
            List<ProductModel> mockList = new List<ProductModel>
            {
                new ProductModel{ID = 1, Category = ProductsEnum.Pants, Name = "High rise skinny jeans", Price = 50, Thumbnail = "https://m1.jeans-industry.fr/201863-thickbox/jeans-bleu-delave-taille-haute.jpg" },
                new ProductModel{ID = 2, Category = ProductsEnum.Pants, Name = "High waist destroyed jeans", Price = 50, Thumbnail = "https://m1.jeans-industry.fr/210026-thickbox/jeans-taille-haute-destroy.jpg" },
                new ProductModel{ID = 3, Category = ProductsEnum.Dresses, Name = "Long dress", Price = 50, Thumbnail = "https://m1.jeans-industry.fr/195837-thickbox/robe-longue.jpg" },
                new ProductModel{ID = 4, Category = ProductsEnum.Dresses, Name = "Ribbed belt long dress", Price = 50, Thumbnail = "https://m3.jeans-industry.fr/190595-thickbox/robe-longue-cotele-a-ceinture.jpg" },
                new ProductModel{ID = 5, Category = ProductsEnum.Dresses, Name = "Embroidery and lace dress", Price = 50, Thumbnail = "https://m3.jeans-industry.fr/181489-thickbox/robe-a-broderies-et-dentelles.jpg" },
                new ProductModel{ID = 6, Category = ProductsEnum.Dresses, Name = "Ribbed gathered dress", Price = 50, Thumbnail = "https://m2.jeans-industry.fr/227238-thickbox/robe-cotelee-froncee.jpg" },
            };

            productCategory = (ProductsEnum)Enum.Parse(typeof(ProductsEnum), categoryName);
            return mockList.Where(x => x.Category == productCategory);
        }

        /// <summary>
        /// Gets product images by the product ID.
        /// </summary>
        /// <param name="productId">The target product ID.</param>
        /// <returns>The method returns thhe found list of products.</returns>
        public IEnumerable<string> GetProductsImages(long productId)
        {
            List<ProductImagesModel> images = new List<ProductImagesModel>
            {
                new ProductImagesModel{ID = 1, ProductId = 1, Image="https://m1.jeans-industry.fr/201863-thickbox/jeans-bleu-delave-taille-haute.jpg"},
                new ProductImagesModel{ID = 2, ProductId = 1, Image="https://m1.jeans-industry.fr/201864-thickbox/jeans-bleu-delave-taille-haute.jpg"},
                new ProductImagesModel{ID = 3, ProductId = 1, Image="https://m1.jeans-industry.fr/201865-thickbox/jeans-bleu-delave-taille-haute.jpg"},
                new ProductImagesModel{ID = 4, ProductId = 2, Image="https://m1.jeans-industry.fr/210026-thickbox/jeans-taille-haute-destroy.jpg"},
                new ProductImagesModel{ID = 5, ProductId = 2, Image="https://m1.jeans-industry.fr/210027-thickbox/jeans-taille-haute-destroy.jpg"},
                new ProductImagesModel{ID = 6, ProductId = 2, Image="https://m1.jeans-industry.fr/210028-thickbox/jeans-taille-haute-destroy.jpg"},
                new ProductImagesModel{ID = 7, ProductId = 3, Image="https://m1.jeans-industry.fr/195837-thickbox/robe-longue.jpg"},
                new ProductImagesModel{ID = 8, ProductId = 3, Image="https://m1.jeans-industry.fr/195838-thickbox/robe-longue.jpg"},
                new ProductImagesModel{ID = 9, ProductId = 3, Image="https://m1.jeans-industry.fr/195839-thickbox/robe-longue.jpg"},
                new ProductImagesModel{ID = 10, ProductId = 4, Image="https://m3.jeans-industry.fr/190595-thickbox/robe-longue-cotele-a-ceinture.jpg"},
                new ProductImagesModel{ID = 11, ProductId = 4, Image="https://m3.jeans-industry.fr/190594-thickbox/robe-longue-cotele-a-ceinture.jpg"},
                new ProductImagesModel{ID = 12, ProductId = 4, Image="https://m3.jeans-industry.fr/190596-thickbox/robe-longue-cotele-a-ceinture.jpg"},
                new ProductImagesModel{ID = 13, ProductId = 4, Image="https://m3.jeans-industry.fr/190597-thickbox/robe-longue-cotele-a-ceinture.jpg"},
                new ProductImagesModel{ID = 14, ProductId = 4, Image="https://m3.jeans-industry.fr/190598-thickbox/robe-longue-cotele-a-ceinture.jpg"},
                new ProductImagesModel{ID = 15, ProductId = 5, Image="https://m3.jeans-industry.fr/181488-thickbox/robe-a-broderies-et-dentelles.jpg"},
                new ProductImagesModel{ID = 16, ProductId = 5, Image="https://m3.jeans-industry.fr/181489-thickbox/robe-a-broderies-et-dentelles.jpg"},
                new ProductImagesModel{ID = 17, ProductId = 5, Image="https://m3.jeans-industry.fr/181490-thickbox/robe-a-broderies-et-dentelles.jpg"},
                new ProductImagesModel{ID = 18, ProductId = 6, Image="https://m2.jeans-industry.fr/227235-thickbox/robe-cotelee-froncee.jpg"},
                new ProductImagesModel{ID = 19, ProductId = 6, Image="https://m2.jeans-industry.fr/227236-thickbox/robe-cotelee-froncee.jpg"},
                new ProductImagesModel{ID = 20, ProductId = 6, Image="https://m2.jeans-industry.fr/227237-thickbox/robe-cotelee-froncee.jpg"},
                new ProductImagesModel{ID = 21, ProductId = 6, Image="https://m2.jeans-industry.fr/227238-thickbox/robe-cotelee-froncee.jpg"},
            };

            return images.Where(x => x.ProductId == productId).Select(x=>x.Image);
        }
    }
}

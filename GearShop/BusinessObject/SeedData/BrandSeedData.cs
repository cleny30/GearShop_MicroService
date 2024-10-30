using BusinessObject.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.SeedData
{
    public static class BrandSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasData(
                new Brand
                {
                    BrandId = 1,
                    BrandName = "Asus",
                    BrandLogo = "https://res.cloudinary.com/dklkzeill/image/upload/v1717914957/Brands/dkrlpjps52vsacomynik.png",
                    IsAvailable = false
                },
                new Brand
                {
                    BrandId = 2,
                    BrandName = "Logitech",
                    BrandLogo = "https://res.cloudinary.com/dklkzeill/image/upload/v1717914988/Brands/ocvp75lvpkobdl7tsluu.png",
                    IsAvailable = true
                },
                new Brand
                {
                    BrandId = 3,
                    BrandName = "Corsair",
                    BrandLogo = "https://res.cloudinary.com/dklkzeill/image/upload/v1717915043/Brands/u15iyz2vnl9szxsiuee0.png",
                    IsAvailable = true
                },
                new Brand
                {
                    BrandId = 4,
                    BrandName = "DareU",
                    BrandLogo = "https://res.cloudinary.com/dklkzeill/image/upload/v1717915068/Brands/mkm8jxdozqvkyp6unk0k.jpg",
                    IsAvailable = true
                },
                new Brand
                {
                    BrandId = 5,
                    BrandName = "Akko",
                    BrandLogo = "https://res.cloudinary.com/dklkzeill/image/upload/v1717915086/Brands/y8ejiik9cch4ooenuv5b.webp",
                    IsAvailable = true
                },
                new Brand
                {
                    BrandId = 6,
                    BrandName = "Razer",
                    BrandLogo = "https://res.cloudinary.com/dklkzeill/image/upload/v1717915101/Brands/fgxosmijxov7wpfm1c8x.png",
                    IsAvailable = true
                },
                new Brand
                {
                    BrandId = 7,
                    BrandName = "Steelseries",
                    BrandLogo = "https://res.cloudinary.com/dklkzeill/image/upload/v1717915116/Brands/i1mkyisiddlvx0tlj41n.jpg",
                    IsAvailable = true
                },
                new Brand
                {
                    BrandId = 8,
                    BrandName = "HyperX",
                    BrandLogo = "https://res.cloudinary.com/dklkzeill/image/upload/v1717915138/Brands/rzed9esfc4alzf2u1rjb.png",
                    IsAvailable = true
                }
            );
        }
    }
}

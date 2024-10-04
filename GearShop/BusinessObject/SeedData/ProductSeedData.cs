using BusinessObject.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.SeedData
{
    public static class ProductSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProId = "CT001",
                    CateId = 5,
                    BrandId = 6,
                    ProName = "Razer Raiju Ultimate1",
                    ProQuan = 15,
                    ProPrice = 250,
                    ProDes = "Razer Raiju Ultimate is one of the products that supports gamers from one of the world''s leading gaming gear manufacturers in addition to the product lines of  mechanical keyboards , gaming mice and gaming headsets. Designed specifically for gamers: With a height and shape for your thumb to adapt to, and a choice of different D-pad layouts, use the Razer Raiju Ultimate to choose the one that''s most comfortable for your hand. . A handle with a reasonable layout will help you hold the key to avoid pressing it",
                    Discount = 20,
                    IsAvailable = true,
                },
                new Product
                {
                    ProId = "CT002",
                    CateId = 5,
                    BrandId = 4,
                    ProName = "DareU H101X Wireless Pink",
                    ProQuan = 10,
                    ProPrice = 40,
                    ProDes = "DareU H101X Wireless Pink', 10, 40, N'For console gamers, one of the indispensable and widely used gaming accessories today is the game controller. And to bring a breath of fresh air in this product segment, DareU has launched a product from its own production line with a model called DareU H101X Wireless Pink . Let''s learn more about this product with GEARVN! Gentle and feminine design Complete with a symmetrical - ergonomic design, DareU H101X Wireless Pink provides comfort when using a gaming controller , suitable and suitable for all hand shapes. The grip and grip area of ??the DareU H101X Wireless Pink is smooth to create a comfortable feeling when used for long periods of time. Coming to the stylish pink version, DareU H101X Wireless Pink brings gentleness and prominence to female gamers or \"strong\" entertainment corners, through extremely personal details and button icons.",
                    Discount = 0,
                    IsAvailable = true,
                },
                new Product
                {
                    ProId = "CT003",
                    CateId = 5,
                    BrandId = 4,
                    ProName = "DareU H1056X Wireless",
                    ProQuan = 10,
                    ProPrice = 70,
                    ProDes = "DareU H1056X Wireless ', 10, 70, N'For console gamers, one of the indispensable and widely used gaming accessories today is the game controller. And to bring a breath of fresh air in this product segment, DareU has launched a product from its own production line with a model called DareU H101X Wireless Black . Let''s learn more about this product with GEARSHOP!",
                    Discount = 20,
                    IsAvailable = true,
                }, new Product
                {
                    ProId = "CT004",
                    CateId = 5,
                    BrandId = 1,
                    ProName = "Asus ROG Raikiri Pro",
                    ProQuan = 5,
                    ProPrice = 180,
                    ProDes = "For console gaming setups, game controllers are indispensable devices for gamers. Always among the top gaming consoles, Xbox from Microsoft is always the top choice for all gamers, especially the controller product line. And here GEARVN will introduce to you the latest Asus ROG Raikiri Pro with the greatest improvements.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "GC001",
                    CateId = 4,
                    BrandId = 3,
                    ProName = "Corsair TC100 Leatherette Chair",
                    ProQuan = 5,
                    ProPrice = 200,
                    ProDes = "Corsair TC100 Leatherette Black CF-9010050-WW gaming chair line has a unique design that adds inspiration when playing games and working. At the same time, it is made from high-quality materials to bring a feeling of comfort all day long.",
                    Discount = 20,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "GC002",
                    CateId = 4,
                    BrandId = 1,
                    ProName = "ASUS ROG Chariot Core Chair",
                    ProQuan = 5,
                    ProPrice = 450,
                    ProDes = "Crafted for comfort. The ROG Chariot Core gaming chair evokes the style and feel of a high-end racing car. With a high-density foam headrest, memory foam lumbar support, breathable PU leather seat, 4D adjustable armrests, lockable tilt mechanism and highly durable components, the Chariot Core giving you safe, comfortable style - and allowing you to express your own personality in any gaming arena.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "GC003",
                    CateId = 4,
                    BrandId = 6,
                    ProName = "Razer ISKUR X Chair",
                    ProQuan = 0,
                    ProPrice = 300,
                    ProDes = "If you are a gamer or regular computer user, the name gaming chair is no longer strange to you, right? To be able to mention one of the top names in the gaming chair industry, Razer will always be named on our list. The brand with the three beams logo has launched a new groundbreaking gaming chair called Razer ISKUR X. Let's learn more details about this product with GEARVN right here!",
                    Discount = 20,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "GC004",
                    CateId = 4,
                    BrandId = 3,
                    ProName = "Corsair TC200 Leatherette",
                    ProQuan = 5,
                    ProPrice = 200,
                    ProDes = "Not only does it possess an aggressive design through meticulously crafted angular lines, the Corsair TC200 with its luxurious Light Gray/White color tone and Leatherette leather material will highlight every corner of the camera.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "HP001",
                    CateId = 3,
                    BrandId = 1,
                    ProName = "Asus ROG Delta S Core",
                    ProQuan = 0,
                    ProPrice = 90,
                    ProDes = "The Asus ROG Delta S Core headset is the lightest gaming headset model currently in the Asus Delta series with a weight of only 270 grams. Designed for unmatched performance and comfort, the Delta S Core features convenient and modern D-shaped ear cups. It fits the ear shape of most users perfectly, reducing unwanted contact area by up to 20% for a better fit and comfort. Exclusive 50mm ASUS Essence drivers with virtual 7.1 surround sound and boom microphone to deliver the ultimate gaming experience certified by Discord and TeamSpeak.",
                    Discount = 20,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "HP002",
                    CateId = 3,
                    BrandId = 1,
                    ProName = "ASUS TUF GAMING H1",
                    ProQuan = 10,
                    ProPrice = 50,
                    ProDes = "Always known for its extremely high-quality products, TUF Gaming also scores points in the eyes of users thanks to its affordable prices. From monitors, laptops to VGA, TUF Gaming always tries to bring the best experience and meet all the needs of the most demanding users. And now, the 'child' from ASUS will enter the equally pleasant market, which is headphones with the ASUS TUF Gaming H1. Let's learn more about the product with GEARVN right here!",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "HP003",
                    CateId = 3,
                    BrandId = 2,
                    ProName = "Logitech G733 LIGHTSPEED",
                    ProQuan = 20,
                    ProPrice = 120,
                    ProDes = "Logitech G733 LIGHTSPEED Wireless White line of computer headsets are designed to bring comfort to gamers. This wireless headset is equipped with the stereophonic sound, audio filters and advanced lighting features you need to see, speak and play in style like never before. Eye-catching design, super light weight. Designed in the shape of an Over-ear headphone with a weight of only 278 grams, a little more than half a pound (250g). It is very lightweight and the elastic straps are designed to reduce and distribute weight.",
                    Discount = 20,
                    IsAvailable = false
                },
                new Product
                {
                    ProId = "HP004",
                    CateId = 3,
                    BrandId = 2,
                    ProName = "Logitech G431 Headset",
                    ProQuan = 50,
                    ProPrice = 70,
                    ProDes = "Loud and clear sound: The over-the-ear headset has a built-in 6mm enhanced large mic to ensure your teammates can hear you. The mic boom can be folded away to mute when you don't want your voice to be heard. DTS HEADPHONE:X 2.0 technology: New generation DTS Headphone:X 2.0 surround sound makes Logitech's computer headset more unique, using Logitech's G HUB software, allowing you to hear enemies hiding behind, possible signals Special features and immersive environments - all around you. Experience 3D audio that goes beyond 7.1 channels to make you feel like you're in the action.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "HP005",
                    CateId = 3,
                    BrandId = 8,
                    ProName = "HyperX Cloud III Headset",
                    ProQuan = 54,
                    ProPrice = 100,
                    ProDes = "HyperX Cloud III Wireless headset is the new gaming headset from Hyper. The Cloud Core series always leaves a mark in the hearts of users because of its excellent sound quality, players will be immersed in clear, vivid sounds. HyperX Cloud Core Wireless can be said to be one of the most worthy wireless headsets for gamers at the moment. Beautiful HyperX Cloud Core design: The HyperX Cloud Core computer headset has a simple earcup design with the signature HyperX logo right on the ear cup. The combination of black and red tones creates an extremely harmonious, luxurious and clean overall.",
                    Discount = 20,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "HP006",
                    CateId = 3,
                    BrandId = 6,
                    ProName = "Razer Shark V2 Pro Black",
                    ProQuan = 2,
                    ProPrice = 220,
                    ProDes = "Black Shark V2 Pro, the latest line of gaming headphones from Razer, is integrated with many modern technologies to bring players vivid, powerful sound quality. It doesn't stop there, thanks to the super soft Flowknit cushioning, it will bring a comfortable and airy feeling when used all day long.",
                    Discount = 20,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "HP007",
                    CateId = 3,
                    BrandId = 7,
                    ProName = "Steelseries Arctis 7 Plus",
                    ProQuan = 6,
                    ProPrice = 175,
                    ProDes = "Steelseries is known as a manufacturer of Gaming Gear devices that possess impressive performance and luxurious design. That is shown through the latest product line from the company, Arctis 7 Plus is a line of bluetooth gaming headsets equipped with many modern features, easily compatible with all systems from USB-C to PS5.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "HP008",
                    CateId = 3,
                    BrandId = 7,
                    ProName = "Steelseries Arctis Prime",
                    ProQuan = 3,
                    ProPrice = 120,
                    ProDes = "Computer headset with high-precision audio drivers inherited from Arctis Pro, fine-tuned for maximum accuracy for the most intense gaming sessions.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "HP009",
                    CateId = 3,
                    BrandId = 3,
                    ProName = "Test01",
                    ProQuan = 0,
                    ProPrice = 100,
                    ProDes = "edadwa",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "KB001",
                    CateId = 1,
                    BrandId = 1,
                    ProName = "ASUS ROG Strix Flare II",
                    ProQuan = 34,
                    ProPrice = 160,
                    ProDes = "Detailed review of the ASUS ROG Strix Flare II Nx Blue Switch mechanical keyboard. Not only outstanding in PC component product lines, ASUS is also known as one of the brands that specializes in bringing extremely high-class gaming gear peripherals in terms of design and performance, from headset lines. gaming, multi-connected computer mice from wired to wireless and even gaming keyboards.",
                    Discount = 20,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "KB002",
                    CateId = 1,
                    BrandId = 2,
                    ProName = "Logitech G Pro X",
                    ProQuan = 2,
                    ProPrice = 140,
                    ProDes = "Logitech GX Switch. Logitech's exclusive GX switch version delivers high performance and long-lasting durability over time. One of the logitech g pro combo mechanical keyboard product lines has an integrated hot swap feature that allows you to replace another switch to innovate the experience during use. The vast Logitech G Hub ecosystem provides you with not only the ability to customize each key RGB LED effect on the Logitech G Pro Gaming computer mice, headphones, etc. in this ecosystem. In addition, you can also use this software to assign extremely convenient macro tasks.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "KB003",
                    CateId = 1,
                    BrandId = 3,
                    ProName = "Corsair K60 Pro Red",
                    ProQuan = 43,
                    ProPrice = 130,
                    ProDes = "Detailed review of the Corsair K60 Pro Red Led gaming keyboard. Beauty and roughness: The cheap mechanical keyboard Corsair K60 Pro Red Led has a minimalist design, combining many angular lines to create an extremely elegant looking keyboard. Viola Switch comes from Cherry: Corsair has equipped the Corsair K60 Pro Red Led with a new type of switch that no brand has used before, which is Cherry Viola. This type of switch provides fast, linear key press speed (almost like Cherry Red), simple, durable switch structure and key travel is still 4mm and the active point is 2mm. Overall, this will be a switch that is very easy to get used to and suitable for gaming needs.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "KB004",
                    CateId = 1,
                    BrandId = 4,
                    ProName = "DareU EK520",
                    ProQuan = 50,
                    ProPrice = 35,
                    ProDes = "Detailed review of DareU EK520 Optical mechanical keyboard. The DareU EK520 keyboard is manufactured by DareU, a company specializing in providing peripheral devices for gamers and office workers. Products from Dareu are of high quality and affordable for consumers. Good material, cheap price: The DareU EK520 keyboard is designed with a frame made from sturdy, durable, and smooth material. Along with the keycaps made from high-quality plastic, it ensures a long life for the device, without the keys fading after a long time.",
                    Discount = 20,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "KB005",
                    CateId = 1,
                    BrandId = 5,
                    ProName = "AKKO 3098 RF Dracula",
                    ProQuan = 30,
                    ProPrice = 70,
                    ProDes = "Detailed review of the AKKO 3098 RF Dracula Castle keyboard. AKKO 3098 RF Dracula Castle is a cheap mechanical keyboard line with a color scheme inspired by Count Dracula, giving players a lot of creative inspiration and excitement when playing recreational games. In particular, thanks to the Fullsize Layout and exclusive AKKO V3 switches, this promises to be the computer keyboard line you are looking for!",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "KB006",
                    CateId = 1,
                    BrandId = 6,
                    ProName = "Razer Blackwidow V3 Pro",
                    ProQuan = 7,
                    ProPrice = 230,
                    ProDes = "The world's first and most iconic Razer Blackwidow V3 Pro mechanical keyboard makes a landmark development. Enter a new wireless meta with the Razer BlackWidow V3 Pro—with 3 connection modes that deliver unparalleled versatility and a satisfying gaming experience including best-in-class switches and full-height keys. Razer Blackwidow V3 Pro Green Switch - a mechanical bluetooth keyboard equipped with our most advanced wireless technology for low-latency gaming and ultra-responsive input—delivered via an optimized data protocol optimized, ultra-fast radio frequency and seamless frequency switching in the noisiest, data-saturated environments.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "KB007",
                    CateId = 1,
                    BrandId = 7,
                    ProName = "Steelseries Apex 3",
                    ProQuan = 15,
                    ProPrice = 70,
                    ProDes = "Apex 3 series mechanical keyboards are certified to be water and dust resistant according to IP32 standards (IP32 is an industry standard for water and dust resistance). Waterproof and dustproof keeps your keyboard clean and has a long life. You don't have to worry about accidentally getting your keyboard wet. With the IP32 standard, dirt and debris will have a harder time penetrating the keyboard, making the internal components safe for long-term use.",
                    Discount = 20,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "KB008",
                    CateId = 1,
                    BrandId = 6,
                    ProName = "Razer DeathStalker V2",
                    ProQuan = 6,
                    ProPrice = 120,
                    ProDes = "Razer DeathStalker V2 Pro TKL possesses a compact, thin and extremely luxurious design. In the segment, the TKL gaming keyboard DeathStalker V2 Pro TK will be one of the choices you should not miss.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "KB009",
                    CateId = 1,
                    BrandId = 5,
                    ProName = "AKKO 3098S World Tour London",
                    ProQuan = 6,
                    ProPrice = 120,
                    ProDes = "AKKO 3098S World Tour London is a product line that continues the success of the 'World Tour Tokyo' series with impressive and unique color combinations. Along with performance upgrades, GEARVN is confident that the AKKO 3098S World Tour London will be a mechanical keyboard line that you should not miss.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "KB010",
                    CateId = 1,
                    BrandId = 3,
                    ProName = "Test",
                    ProQuan = 10,
                    ProPrice = 100,
                    ProDes = "dadaw",
                    Discount = 1,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "MS001",
                    CateId = 2,
                    BrandId = 1,
                    ProName = "Asus ROG Harpe Ace",
                    ProQuan = 7,
                    ProPrice = 115,
                    ProDes = "ASUS - The brand is extremely famous for its technology products, from laptops to computer components. And when it comes to gaming gear, ASUS still owns a high-end gaming mouse product line called ASUS ROG Harpe Ace. Let's welcome the latest version of the product lineup with the ASUS ROG Harpe Ace Aim Lab Edition model. Join GEARVN to learn more about this mouse!",
                    Discount = 20,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "MS002",
                    CateId = 2,
                    BrandId = 1,
                    ProName = "Asus ROG Chakram X",
                    ProQuan = 43,
                    ProPrice = 130,
                    ProDes = "ASUS - The brand is extremely famous for its technology products, from laptops to computer components. And when it comes to gaming gear, ASUS still owns a high-end gaming mouse product line called Asus ROG Chakram X. Let's welcome the latest version of the product lineup with the Asus ROG Chakram X. Beautiful with Aura Sync RGB lighting. The ROG logo on Asus ROG Chakram X adorns the mouse, affirming the name of the famous gaming laptop brand. Combined with the brilliant ARGB effects on Aura Sync RGB, this will be a great decoration for PC systems and entertainment and work corners.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "MS004",
                    CateId = 2,
                    BrandId = 1,
                    ProName = "Asus ROG Strix Impact II",
                    ProQuan = 7,
                    ProPrice = 50,
                    ProDes = "Accurate and high performance. Asus collaborated with professional gamers in designing ROG Strix Impact II, delivering an ambidextrous ergonomic design optimized for performance play and comfortable grip, with a weight of just 79g. The 6,200 dpi sensor tracks at up to 220 ips and with a 1000 Hz polling rate, so you're guaranteed high accuracy, quick response and precise control - and all without a hint of lag. Impact II even includes five programmable buttons, allowing you to customize controls for your game or play style.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "MS005",
                    CateId = 2,
                    BrandId = 2,
                    ProName = "Logitech G502 X",
                    ProQuan = 6,
                    ProPrice = 150,
                    ProDes = "Logitech G502 X PLUS Mouse is the latest product of the popular G502 series. Redesigned and enhanced with modern gaming technology, including the first hybrid optical-mechanical Lightforce switch, Wireless Lightspeed, LIGHT SYNC RGB and Hero 25K optical sensor, the rugged Logitech G502 X PLUS is one of the most worth buying gaming gear for gamers in the near future.",
                    Discount = 20,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "MS006",
                    CateId = 2,
                    BrandId = 2,
                    ProName = "Logitech G703 HERO",
                    ProQuan = 4,
                    ProPrice = 75,
                    ProDes = "Logitech G703 HERO Lightspeed Wireless Mouse. Logitech G703 is one of the computer mouse lines that possesses an ergonomic design that hugs the palm of the hand to support each mouse movement more firmly. G703 is one of the latest Logitech wireless mouse product lines to join the game with the new generation 16K HERO sensor. Logitech's most advanced sensor to date, with 1:1 tracking, 400+ IPS, and 100-16,000 maximum DPI sensitivity — plus smoothing, filtering, and acceleration. Logitech G703 is one of the few wireless gaming computer mice with a comfortable design and light weight, with a rubber handle to help increase mouse movement. In addition, during use, users can customize the weight of the mouse with the 10g option to help you be more flexible in use.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "MS007",
                    CateId = 2,
                    BrandId = 6,
                    ProName = "Razer Cobra",
                    ProQuan = 9,
                    ProPrice = 50,
                    ProDes = "Detailed review of the Razer Cobra mouse. The Razer Cobra line of gaming mice has a symmetrical design that makes mouse movements more secure. At the same time, equipped with up to 6 buttons intelligently arranged along the mouse body to help make operations more effective. This promises to be one of the gaming computer mouse lines worth owning from Razer!",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "MS008",
                    CateId = 2,
                    BrandId = 6,
                    ProName = "Razer DeathAdder V3",
                    ProQuan = 9,
                    ProPrice = 140,
                    ProDes = "Victory takes on a new shape with the Razer DeathAdder V3 Pro. Refined and reforged with the aid of top esports pros, its iconic ergonomic form is now more than 25% lighter than its predecessor, backed by a set of cutting-edge upgrades to push the limits of competitive play. Razer DeathAdder V3 Pro's ergonomic design and wireless connection give you a comfortable feeling when used for long periods of time. Perfectly suited for palm grip, and also works well with fingertip swipes.",
                    Discount = 20,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "MS009",
                    CateId = 2,
                    BrandId = 7,
                    ProName = "Steelseries Rival 5",
                    ProQuan = 7,
                    ProPrice = 35,
                    ProDes = "Detailed review of the Steelseries Rival 5 mouse product. The Steelseries Rival 5 product is a gaming computer mouse with high precision and flexibility. Players can dominate any game with 9 customizable buttons and lightning-fast toggles. Freely program 9 custom buttons. Players have the right to adjust important keys for convenience in use. The side of the mouse comes with 5 buttons designed for thumb use. Increase the speed of performing many operations so players can have great game experience moments.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "MS010",
                    CateId = 2,
                    BrandId = 4,
                    ProName = "Dare-U EM908 RGB",
                    ProQuan = 7,
                    ProPrice = 25,
                    ProDes = "DareU EM908 RGB has a compact, beautiful design. One of the gaming mouse lines that possesses a palm-sized appearance and size with an ergonomic design that is convenient for the user. Brings a sense of comfort to gamers and users who have to work for long periods of time without feeling tired when using DAREU EM908 for long periods of time. In addition, to synchronize with the computer system DareU has equipped the EM908 mouse line with a beautiful RGB LED strip of 16.7 million colors around the edge of the mouse.",
                    Discount = 20,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "MS011",
                    CateId = 2,
                    BrandId = 5,
                    ProName = "Akko Hamster Plus",
                    ProQuan = 6,
                    ProPrice = 20,
                    ProDes = "Akko Hamster Wireless is designed to be small, more suitable for women's hands, but if you are used to holding claw-grip or Fingertip mice, you will definitely be satisfied with the 'fat' body of these hamsters. This hamster. Akko Hamster is finished with pre-colored ABS plastic, not painted on the surface, so the color will definitely last over time. Combined with a set of extremely chill pastel colors, it will definitely please the ladies, and even the guys with gentle, peaceful personalities.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "MS012",
                    CateId = 2,
                    BrandId = 3,
                    ProName = "Corsair M65 RGB",
                    ProQuan = 7,
                    ProPrice = 65,
                    ProDes = "Detailed review of Corsair M65 RGB ULTRA Black mouse. Corsair, a brand that is extremely popular with gamers with its PC components, streaming accessories,... Especially gaming gear products, are always perfected with a bold gaming design and special features. Those will be the strengths that Corsair M65 RGB ULTRA Black will possess. So let's see with GEARVN what else the gaming mouse from Corsair can bring to gamers!",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "MS013",
                    CateId = 2,
                    BrandId = 8,
                    ProName = "HYPERX Pulsefire Haste II",
                    ProQuan = 4,
                    ProPrice = 60,
                    ProDes = "Detailed review of the HP HYPERX Pulsefire Haste mouse. HyperX Pulsefire Haste 2 lines of gaming computer mice have a symmetrical design that hugs the palm of the hand, making all operations more secure. Integrating many modern technologies from HyperX, this promises to be one of the gaming mouse lines that offers the outstanding physical control you are looking for. Ergonomic design, hugs the palm. Possessing an ergonomic mouse design with a symmetrical grip form that hugs the entire palm of the hand, making each mouse move more secure and reducing pressure on the wrist when used for long periods of time. HYPERX Pulsefire Haste 2 is finished with black as the main color tone, the rounded details are meticulously machined to create an extremely beautiful overall. It is promised that when placed next to a mechanical keyboard, a gaming computer headset will create an extremely high-quality, beautiful gaming space.",
                    Discount = 0,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "MS014",
                    CateId = 2,
                    BrandId = 1,
                    ProName = "ASUS ROG Keris",
                    ProQuan = 4,
                    ProPrice = 65,
                    ProDes = "ASUS ROG Keris is equipped with a specially tuned 16,000 dpi sensor. It features ROG's exclusive push-fit switch sockets and ROG Micro Switches, along with left and right PBT polymer buttons, Asus ROG Omni mouse feet, ROG Paracord lighting, and Aura Sync RGB.",
                    Discount = 20,
                    IsAvailable = true
                },
                new Product
                {
                    ProId = "MS015",
                    CateId = 2,
                    BrandId = 1,
                    ProName = "Asus Gladius II",
                    ProQuan = 4,
                    ProPrice = 70,
                    ProDes = "Asus Gladius II is designed for people who feeling sad in love, with powerful design, this mouse can make you feel good, never feel sad, like when you eat coconut, drink watermelon, take a bath in beach and swim in bathroom.",
                    Discount = 20,
                    IsAvailable = true
                }
            );
        }
    }
}

﻿using BusinessObject.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BusinessObject.SeedData
{
    internal class ProductAttributeSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAttribute>().HasData(
                new ProductAttribute { ProId = "MS007", Feature = "Trademark", Description = "Razer" },
                new ProductAttribute { ProId = "MS007", Feature = "Connection type", Description = "Wired" },
                new ProductAttribute { ProId = "MS007", Feature = "Sensitivity (DPI)", Description = "8500" },
                new ProductAttribute { ProId = "MS007", Feature = "Sensor", Description = "8500 DPI Optical Sensor" },
                new ProductAttribute { ProId = "MS007", Feature = "Switch", Description = "Optical Mouse Switches Gen-3" },
                new ProductAttribute { ProId = "MS007", Feature = "Size", Description = "119.6 mm / 4.71 in" },
                new ProductAttribute { ProId = "KB001", Feature = "Trademark", Description = "Asus" },
                new ProductAttribute { ProId = "KB001", Feature = "Connect", Description = "Wired (USB 2.0)" },
                new ProductAttribute { ProId = "KB001", Feature = "Keyboard style", Description = "Full size 100%" },
                new ProductAttribute { ProId = "KB001", Feature = "Led", Description = "Led" },
                new ProductAttribute { ProId = "KB001", Feature = "AURA Sync", Description = "AURA Sync" },
                new ProductAttribute { ProId = "KB001", Feature = "Macro keys", Description = "All keys are programmable" },
                new ProductAttribute { ProId = "KB001", Feature = "Weight", Description = "1113g" },
                new ProductAttribute { ProId = "MS001", Feature = "Trademark", Description = "Asus" },
                new ProductAttribute { ProId = "MS001", Feature = "Designs", Description = "Symmetry" },
                new ProductAttribute { ProId = "MS001", Feature = "Connect", Description = "Wired / Bluetooth 5.1 / Wireless 2.4GHz" },
                new ProductAttribute { ProId = "MS001", Feature = "DPI", Description = "36000" },
                new ProductAttribute { ProId = "MS001", Feature = "IPS", Description = "650" },
                new ProductAttribute { ProId = "MS001", Feature = "Weight", Description = "54G" },
                new ProductAttribute { ProId = "MS008", Feature = "Trademark", Description = "Razer" },
                new ProductAttribute { ProId = "MS008", Feature = "Connect", Description = "Wireless" },
                new ProductAttribute { ProId = "MS010", Feature = "Trademark", Description = "DareU" },
                new ProductAttribute { ProId = "MS010", Feature = "Connection", Description = "Wired" },
                new ProductAttribute { ProId = "MS010", Feature = "Led light", Description = "RGB" },
                new ProductAttribute { ProId = "MS005", Feature = "Trademark", Description = "Logitech" },
                new ProductAttribute { ProId = "MS005", Feature = "Connect", Description = "LIGHTSPEED wireless technology" },
                new ProductAttribute { ProId = "MS005", Feature = "Charging port", Description = "USB-C" },
                new ProductAttribute { ProId = "MS005", Feature = "Battery life", Description = "Up to 130 hours (37 hours with RGB enabled)" },
                new ProductAttribute { ProId = "MS005", Feature = "Maximum acceleration", Description = "> 40G2" },
                new ProductAttribute { ProId = "MS005", Feature = "Max speed", Description = "> 400 IPS" },
                new ProductAttribute { ProId = "MS005", Feature = "Size", Description = "131.4 mm x 41.1 mm x 79.2 mm" },
                new ProductAttribute { ProId = "MS010", Feature = "Resolution (CPI/DPI)", Description = "6000DPI" },
                new ProductAttribute { ProId = "MS010", Feature = "Sensor", Description = "BRAVO ATG4090" },
                new ProductAttribute { ProId = "MS010", Feature = "Size", Description = "Size" },
                new ProductAttribute { ProId = "MS011", Feature = "Trademark", Description = "Akko" },
                new ProductAttribute { ProId = "MS011", Feature = "Sensor", Description = "Optical" },
                new ProductAttribute { ProId = "MS011", Feature = "Pixels per inch (DPI)", Description = "1200" },
                new ProductAttribute { ProId = "MS011", Feature = "Connect", Description = "Wireless Wireless" },
                new ProductAttribute { ProId = "MS011", Feature = "Weight (gr)", Description = "84 (including battery)" },
                new ProductAttribute { ProId = "MS011", Feature = "Battery life", Description = "Up to 6 months" },
                new ProductAttribute { ProId = "MS012", Feature = "Trademark", Description = "Corsair" },
                new ProductAttribute { ProId = "MS012", Feature = "Node number", Description = "8" },
                new ProductAttribute { ProId = "MS012", Feature = "DPI", Description = "26,000" },
                new ProductAttribute { ProId = "MS012", Feature = "Sensor", Description = "MARKSMAN 26K" },
                new ProductAttribute { ProId = "MS012", Feature = "Led", Description = "2 RGB Zones" },
                new ProductAttribute { ProId = "MS012", Feature = "Connect", Description = "Wired" },
                new ProductAttribute { ProId = "MS012", Feature = "Weight", Description = "97g" },
                new ProductAttribute { ProId = "MS013", Feature = "Trademark", Description = "HyperX" },
                new ProductAttribute { ProId = "MS013", Feature = "Connection type", Description = "Wired" },
                new ProductAttribute { ProId = "MS013", Feature = "Sensitivity (DPI)", Description = "Maximum 26,000" },
                new ProductAttribute { ProId = "MS013", Feature = "Sensor", Description = "HyperX 26K" },
                new ProductAttribute { ProId = "MS013", Feature = "Switch", Description = "up to 100 million clicks" },
                new ProductAttribute { ProId = "CT002", Feature = "Trademark", Description = "DareU" },
                new ProductAttribute { ProId = "CT002", Feature = "Connect", Description = "Wireless" },
                new ProductAttribute { ProId = "CT002", Feature = "Size", Description = "160mm x 105mm x 62mm" },
                new ProductAttribute { ProId = "CT002", Feature = "Weight", Description = "˜220g" },
                new ProductAttribute { ProId = "CT002", Feature = "Wire length", Description = "1.8M" },
                new ProductAttribute { ProId = "CT002", Feature = "Compatible", Description = "PC, Switch, Android TV & phone" },
                new ProductAttribute { ProId = "CT002", Feature = "The battery", Description = "650mAh" },
                new ProductAttribute { ProId = "GC001", Feature = "Trademark", Description = "Corsair" },
                new ProductAttribute { ProId = "KB002", Feature = "Trademark", Description = "Logitech" },
                new ProductAttribute { ProId = "KB002", Feature = "Connect", Description = "Wired" },
                new ProductAttribute { ProId = "KB002", Feature = "Size", Description = "361 x 153 x 34 (mm)" },
                new ProductAttribute { ProId = "KB002", Feature = "Switch", Description = "Logitech GX Switch Clicky" },
                new ProductAttribute { ProId = "KB002", Feature = "Weight", Description = "1kg" },
                new ProductAttribute { ProId = "KB005", Feature = "Trademark", Description = "Akko" },
                new ProductAttribute { ProId = "KB005", Feature = "Design", Description = "98 Keys" },
                new ProductAttribute { ProId = "KB005", Feature = "Connect", Description = "USB Type-C to Type-A (detachable cord)" },
                new ProductAttribute { ProId = "KB005", Feature = "The battery", Description = "1300mah" },
                new ProductAttribute { ProId = "KB005", Feature = "Size", Description = "382 x 134 x 40 mm" },
                new ProductAttribute { ProId = "KB005", Feature = "Weight", Description = "382 x 134 x 40 mm" },
                new ProductAttribute { ProId = "KB005", Feature = "Switch", Description = "Akko switch v3 (Cream Blue Pro/ Cream Yellow Pro)" },
                new ProductAttribute { ProId = "KB007", Feature = "Trademark", Description = "Steelseries" },
                new ProductAttribute { ProId = "KB007", Feature = "Connection", Description = "Wired keyboard" },
                new ProductAttribute { ProId = "KB007", Feature = "Switch", Description = "SteelSeries Whisper-Quiet Switches" },
                new ProductAttribute { ProId = "KB007", Feature = "Keycaps material", Description = "ABS" },
                new ProductAttribute { ProId = "KB007", Feature = "Size", Description = "Length 444.7 x Width 151.62 x Height 39.69 mm" },
                new ProductAttribute { ProId = "KB007", Feature = "Weight", Description = "816 grams" },
                new ProductAttribute { ProId = "MS002", Feature = "Trademark", Description = "Asus" },
                new ProductAttribute { ProId = "MS002", Feature = "Connection", Description = "Wired / Wireless RF 2.4GHz / Bluetooth" },
                new ProductAttribute { ProId = "MS002", Feature = "Switch", Description = "70 million clicks" },
                new ProductAttribute { ProId = "MS002", Feature = "Sensitivity (DPI)", Description = "100 ~ 36000" },
                new ProductAttribute { ProId = "MS002", Feature = "Battery life", Description = "Up to 150h" },
                new ProductAttribute { ProId = "MS004", Feature = "Trademark", Description = "Asus" },
                new ProductAttribute { ProId = "MS004", Feature = "Connect", Description = "USB 2.0" },
                new ProductAttribute { ProId = "MS004", Feature = "Sensor", Description = "Pixart3327" },
                new ProductAttribute { ProId = "MS006", Feature = "Trademark", Description = "Logitech" },
                new ProductAttribute { ProId = "MS006", Feature = "Sensor", Description = "16K HERO" },
                new ProductAttribute { ProId = "MS006", Feature = "DPI sensitivity", Description = "100-16,000 maximum" },
                new ProductAttribute { ProId = "MS006", Feature = "IPS", Description = "400+" },
                new ProductAttribute { ProId = "MS006", Feature = "Connect", Description = "Wireless" },
                new ProductAttribute { ProId = "MS009", Feature = "Trademark", Description = "Steelseries" },
                new ProductAttribute { ProId = "MS009", Feature = "Sensor Type", Description = "optics" },
                new ProductAttribute { ProId = "MS009", Feature = "CPI", Description = "18000 CPI" },
                new ProductAttribute { ProId = "MS009", Feature = "IPS", Description = "400, on SteelSeries QcK surface" },
                new ProductAttribute { ProId = "MS009", Feature = "Size", Description = "128.8 x 63.35 x 28.2 (mm)" },
                new ProductAttribute { ProId = "MS009", Feature = "Switch Type", Description = "SteelSeries IP54 mechanical switches" },
                new ProductAttribute { ProId = "MS009", Feature = "Connect", Description = "Wired" },
                new ProductAttribute { ProId = "KB003", Feature = "Trademark", Description = "Corsair" },
                new ProductAttribute { ProId = "KB003", Feature = "Led", Description = "RED" },
                new ProductAttribute { ProId = "KB003", Feature = "Size", Description = "FullSize" },
                new ProductAttribute { ProId = "KB003", Feature = "Switch", Description = "CHERRY VIOLA" },
                new ProductAttribute { ProId = "KB003", Feature = "CHERRY® VIOLA", Description = "ABS Doubleshot" },
                new ProductAttribute { ProId = "KB003", Feature = "Connect", Description = "Wired, USB 2.0 Type A" },
                new ProductAttribute { ProId = "KB003", Feature = "Weight", Description = "Weight" },
                new ProductAttribute { ProId = "MS008", Feature = "Sensor", Description = "Focus Pro 30K optical sensor" },
                new ProductAttribute { ProId = "MS008", Feature = "DPI", Description = "30000" },
                new ProductAttribute { ProId = "MS008", Feature = "IPS", Description = "750" },
                new ProductAttribute { ProId = "MS008", Feature = "Switch", Description = "Optical Gen-3" },
                new ProductAttribute { ProId = "KB006", Feature = "Trademark", Description = "Razer" },
                new ProductAttribute { ProId = "KB006", Feature = "Connect", Description = "USB, Bluetooth, 2.4 Ghz Wireless" },
                new ProductAttribute { ProId = "KB006", Feature = "Designs", Description = "Designs" },
                new ProductAttribute { ProId = "KB006", Feature = "Switch", Description = "Razer Mechanical Green/Yellow" },
                new ProductAttribute { ProId = "KB006", Feature = "Keycaps", Description = "Keycap ABS Doubleshot" },
                new ProductAttribute { ProId = "KB004", Feature = "Trademark", Description = "DareU" },
                new ProductAttribute { ProId = "KB004", Feature = "Connect", Description = "USB cord" },
                new ProductAttribute { ProId = "KB004", Feature = "LED", Description = "Multi" },
                new ProductAttribute { ProId = "KB004", Feature = "Switch", Description = "Optical" },
                new ProductAttribute { ProId = "HP001", Feature = "Trademark", Description = "Asus" },
                new ProductAttribute { ProId = "HP001", Feature = "Model", Description = "Asus ROG Delta S Core" },
                new ProductAttribute { ProId = "HP001", Feature = "Connect", Description = "Has standard 3.5mm wire" },
                new ProductAttribute { ProId = "HP001", Feature = "Headphone style", Description = "Over-ear" },
                new ProductAttribute { ProId = "HP001", Feature = "Driver", Description = "50mm" },
                new ProductAttribute { ProId = "HP001", Feature = "Sensitivity", Description = "-40 dB" },
                new ProductAttribute { ProId = "HP001", Feature = "Impedance", Description = "32 Ohms" },
                new ProductAttribute { ProId = "HP001", Feature = "Feature", Description = "7.1 virtual surround sound system powered by Windows Sonic" },
                new ProductAttribute { ProId = "HP002", Feature = "Trademark", Description = "Asus" },
                new ProductAttribute { ProId = "HP002", Feature = "Connect", Description = "Has a 3.5mm cable" },
                new ProductAttribute { ProId = "HP002", Feature = "Microphone sensitivity", Description = "-45dB" },
                new ProductAttribute { ProId = "HP002", Feature = "Driver size", Description = "40mm" },
                new ProductAttribute { ProId = "HP002", Feature = "Impedance", Description = "60 ohms" },
                new ProductAttribute { ProId = "HP002", Feature = "Response frequency", Description = "20Hz - 20Khz" },
                new ProductAttribute { ProId = "HP002", Feature = "Microphone frequency", Description = "100Hz - 10Khz" },
                new ProductAttribute { ProId = "HP002", Feature = "Weight", Description = "287 gr" },
                new ProductAttribute { ProId = "HP006", Feature = "Trademark", Description = "Razer" },
                new ProductAttribute { ProId = "HP006", Feature = "Headphone type", Description = "Over-ear" },
                new ProductAttribute { ProId = "HP006", Feature = "Response frequency", Description = "12 Hz - 28 kHz" },
                new ProductAttribute { ProId = "HP006", Feature = "Impedance", Description = "32 Ohms" },
                new ProductAttribute { ProId = "HP006", Feature = "Sensitivity", Description = "100 dBSPL/mW @ 1 kHz using HATS" },
                new ProductAttribute { ProId = "HP006", Feature = "Drivers", Description = "50 mm" },
                new ProductAttribute { ProId = "HP006", Feature = "Headphone cushion material", Description = "Breathable foam mattress" },
                new ProductAttribute { ProId = "HP006", Feature = "Battery life", Description = "Up to 70 hours" },
                new ProductAttribute { ProId = "HP006", Feature = "Noise cancellation", Description = "Advanced passive noise cancellation" },
                new ProductAttribute { ProId = "HP006", Feature = "Connection type", Description = "Type A wireless (2.4 GHz), Bluetooth 5.2" },
                new ProductAttribute { ProId = "HP006", Feature = "Weight", Description = "320 g" },
                new ProductAttribute { ProId = "CT001", Feature = "Trademark", Description = "Razer" },
                new ProductAttribute { ProId = "CT001", Feature = "Connect", Description = "Bluetooth/Wired" },
                new ProductAttribute { ProId = "CT001", Feature = "Compatible", Description = "PC/PS4" },
                new ProductAttribute { ProId = "CT001", Feature = "Software that can be used", Description = "Raiju application (IOS/ANDROID)" },
                new ProductAttribute { ProId = "CT001", Feature = "Switch Type", Description = "RAZER MECHA-TACTILE" },
                new ProductAttribute { ProId = "CT001", Feature = "Used Time", Description = "11 hours (Bluetooth)" },
                new ProductAttribute { ProId = "CT001", Feature = "Cable length", Description = "3m" },
                new ProductAttribute { ProId = "CT001", Feature = "Size", Description = "106 x 155 x 66 ( mm )" },
                new ProductAttribute { ProId = "CT001", Feature = "Weight", Description = "370 g" },
                new ProductAttribute { ProId = "GC001", Feature = "Material", Description = "PU leather" },
                new ProductAttribute { ProId = "GC001", Feature = "Maximum height", Description = "188 cm" },
                new ProductAttribute { ProId = "GC001", Feature = "Minimum height", Description = "45 – 55 cm" },
                new ProductAttribute { ProId = "GC001", Feature = "Chair arm height", Description = "45 – 52" },
                new ProductAttribute { ProId = "GC001", Feature = "Backrest height", Description = "81 cm" },
                new ProductAttribute { ProId = "GC001", Feature = "Backrest width", Description = "33 cm" },
                new ProductAttribute { ProId = "GC001", Feature = "Maximum load", Description = "120 kg" },
                new ProductAttribute { ProId = "GC001", Feature = "Lumbar pillow", Description = "Have" },
                new ProductAttribute { ProId = "GC001", Feature = "Neck pillow", Description = "Have" },
                new ProductAttribute { ProId = "GC001", Feature = "Seat width", Description = "54 cm" },
                new ProductAttribute { ProId = "GC001", Feature = "Handrail type", Description = "2D" },
                new ProductAttribute { ProId = "GC001", Feature = "Number of wheels", Description = "5" },
                new ProductAttribute { ProId = "GC001", Feature = "Wheel material", Description = "High quality plastic" },
                new ProductAttribute { ProId = "GC001", Feature = "Weight", Description = "18.30kg" },
                new ProductAttribute { ProId = "GC003", Feature = "Trademark", Description = "Razer" },
                new ProductAttribute { ProId = "GC003", Feature = "Material", Description = "PVC leather" },
                new ProductAttribute { ProId = "GC003", Feature = "Armrest", Description = "2D" },
                new ProductAttribute { ProId = "GC003", Feature = "Chair cover color", Description = "Black, blue" },
                new ProductAttribute { ProId = "GC003", Feature = "Reclining angle", Description = "90°-139°" },
                new ProductAttribute { ProId = "GC003", Feature = "Air lift layer", Description = "4" },
                new ProductAttribute { ProId = "GC003", Feature = "Wheel", Description = "Caster 6 cm" },
                new ProductAttribute { ProId = "GC003", Feature = "Chair frame material", Description = "Metal & Plywood" },
                new ProductAttribute { ProId = "GC003", Feature = "Maximum load (kg)", Description = "136" },
                new ProductAttribute { ProId = "GC003", Feature = "Chair weight (kg)", Description = "25" },
                new ProductAttribute { ProId = "HP003", Feature = "Trademark", Description = "Logitech" },
                new ProductAttribute { ProId = "HP003", Feature = "Connect", Description = "Wireless" },
                new ProductAttribute { ProId = "HP003", Feature = "Connection standard", Description = "Receiver USB type A" },
                new ProductAttribute { ProId = "HP003", Feature = "Headphone type", Description = "Over-ear" },
                new ProductAttribute { ProId = "HP003", Feature = "Impedance", Description = "1kHz 32Ohm" },
                new ProductAttribute { ProId = "HP003", Feature = "Frequency", Description = "20Hz - 20KHz" },
                new ProductAttribute { ProId = "HP003", Feature = "Headphone cushion material", Description = "Airy fabric" },
                new ProductAttribute { ProId = "HP003", Feature = "Compatible", Description = "Windows 7 or later / MacOS X 10.12 or later / PlayStation 4" },
                new ProductAttribute { ProId = "HP004", Feature = "Trademark", Description = "Logitech" },
                new ProductAttribute { ProId = "HP004", Feature = "Headphone type", Description = "Over-ear" },
                new ProductAttribute { ProId = "HP004", Feature = "Connection typ", Description = "Wired" },
                new ProductAttribute { ProId = "HP004", Feature = "Connection standard", Description = "3.5mm / USB type A" },
                new ProductAttribute { ProId = "HP004", Feature = "Microphone", Description = "Can be folded when not in use" },
                new ProductAttribute { ProId = "HP004", Feature = "Impedance", Description = "1 kHz 32 Ohm" },
                new ProductAttribute { ProId = "HP004", Feature = "Frequency", Description = "20Hz - 20KHz" },
                new ProductAttribute { ProId = "HP004", Feature = "Frame material", Description = "Alloy" },
                new ProductAttribute { ProId = "HP004", Feature = "Compatible", Description = "Windows / MacOS / PlayStation 4 / Nintendo Switch / Smartphone" },
                new ProductAttribute { ProId = "HP005", Feature = "Trademark", Description = "HyperX" },
                new ProductAttribute { ProId = "HP005", Feature = "Response frequency", Description = "10Hz-21kHz" },
                new ProductAttribute { ProId = "HP005", Feature = "Cable Length", Description = "Headphone cable 1.2m, USB dongle cable 1m3" },
                new ProductAttribute { ProId = "HP005", Feature = "Diaphragm", Description = "53mm" },
                new ProductAttribute { ProId = "HP005", Feature = "Frame Type", Description = "Aluminum" },
                new ProductAttribute { ProId = "HP005", Feature = "Sensitivity", Description = "-42dBV (0dB = 1V/Pa at 1kHz)" },
                new ProductAttribute { ProId = "HP005", Feature = "Connection via jack", Description = "3.5mm, USB" },
                new ProductAttribute { ProId = "HP005", Feature = "Included accessories", Description = "Microphone, USB soundcard" },
                new ProductAttribute { ProId = "GC002", Feature = "Trademark", Description = "Asus" },
                new ProductAttribute { ProId = "GC002", Feature = "Material", Description = "High quality PU leather" },
                new ProductAttribute { ProId = "GC002", Feature = "Armrest", Description = "4D (Up/Down, Left/Right, Front/Back, Swivel)" },
                new ProductAttribute { ProId = "GC002", Feature = "Reclining angle", Description = "85° ~ 165°" },
                new ProductAttribute { ProId = "GC002", Feature = "Chair size", Description = "134 – 142 x 73 x 73 (cm) (Height x Length x Width)" },
                new ProductAttribute { ProId = "GC002", Feature = "Chair back height", Description = "85cm" },
                new ProductAttribute { ProId = "GC002", Feature = "Seat back width", Description = "73cm" },
                new ProductAttribute { ProId = "GC002", Feature = "Wheel Size", Description = "65mm" },
                new ProductAttribute { ProId = "GC002", Feature = "Chair frame material", Description = "Metal" },
                new ProductAttribute { ProId = "GC002", Feature = "Weight", Description = "120kg" },
                new ProductAttribute { ProId = "KB008", Feature = "Connect", Description = "Wired / Wireless (2.4 Ghz) / Bluetooth" },
                new ProductAttribute { ProId = "KB008", Feature = "Media keys", Description = "Have" },
                new ProductAttribute { ProId = "KB008", Feature = "LED", Description = "Razer Chroma™ RGB with 16.8 million colors" },
                new ProductAttribute { ProId = "KB008", Feature = "Switch", Description = "Razer™ Low-Profile Optical (Linear)" },
                new ProductAttribute { ProId = "MS015", Feature = "Trademark", Description = "Asus" },
                new ProductAttribute { ProId = "MS015", Feature = "Connect", Description = "USB 2.0" },
                new ProductAttribute { ProId = "MS015", Feature = "Connection type", Description = "Wired mouse" },
                new ProductAttribute { ProId = "MS015", Feature = "Color", Description = "Black" },
                new ProductAttribute { ProId = "MS015", Feature = "Switch", Description = "Omron" },
                new ProductAttribute { ProId = "MS015", Feature = "Resolution (CPI/DPI)", Description = "12000DPI" },
                new ProductAttribute { ProId = "CT003", Feature = "Trademark", Description = "DareU" },
                new ProductAttribute { ProId = "CT003", Feature = "Connect", Description = "Wireless" },
                new ProductAttribute { ProId = "CT003", Feature = "Size", Description = "160mm x 105mm x 62mm" },
                new ProductAttribute { ProId = "CT003", Feature = "Weight", Description = "˜220g" },
                new ProductAttribute { ProId = "CT003", Feature = "Wire length", Description = "1.8M" },
                new ProductAttribute { ProId = "CT003", Feature = "Compatible", Description = "PC, Switch, Android TV & phone" },
                new ProductAttribute { ProId = "CT003", Feature = "The battery", Description = "650mAh" },
                new ProductAttribute { ProId = "CT004", Feature = "Trademark", Description = "Asus" },
                new ProductAttribute { ProId = "CT004", Feature = "Connect", Description = "Wired / Wireless (2.4 Ghz) / Bluetooth" },
                new ProductAttribute { ProId = "CT004", Feature = "Battery life", Description = "Battery life up to 48 hours of use (no lights on, vibration off)" },
                new ProductAttribute { ProId = "CT004", Feature = "Screen", Description = "1.3inch with 128x40 resolution, 2 gray levels" },
                new ProductAttribute { ProId = "KB009", Feature = "Trademark", Description = "Akko" },
                new ProductAttribute { ProId = "KB009", Feature = "Connect", Description = "USB Type-C, detachable" },
                new ProductAttribute { ProId = "KB009", Feature = "Number of keys", Description = "98 Keys" },
                new ProductAttribute { ProId = "KB009", Feature = "LED", Description = "RGB background (Backlit, SMT bottom without switch) with many modes" },
                new ProductAttribute { ProId = "KB009", Feature = "Size", Description = "382 x 134 x 40 mm" },
                new ProductAttribute { ProId = "KB009", Feature = "Weight", Description = "1.1kg" },
                new ProductAttribute { ProId = "KB009", Feature = "Switch", Description = "AKKO CS Silver" }
            );
        }
    }
}


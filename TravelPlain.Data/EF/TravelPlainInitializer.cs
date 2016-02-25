using System;
using System.Data.Entity;
using TravelPlain.Data.Models;

namespace TravelPlain.Data.EF
{
    public class TravelPlainInitializer : CreateDatabaseIfNotExists<TravelPlainContext>
    {
        protected override void Seed(TravelPlainContext context)
        {
            InitializeBusinessValues(context);
            InitializeTours(context);

            base.Seed(context);
        }

        private void InitializeBusinessValues(TravelPlainContext context)
        {
            context.BusinessValues.AddRange(
                new BusinessValue[]
                {
                    new BusinessValue
                    {
                        DiscountPercentCap = 10,
                        DiscountPercentIncrement = 1,
                        TimeSet = DateTime.Now.AddDays(-1)
                    },
                    new BusinessValue
                    {
                        DiscountPercentCap = 15,
                        DiscountPercentIncrement = 2,
                        TimeSet = DateTime.Now
                    }
                });
            context.SaveChanges();
        }

        private void InitializeTours(TravelPlainContext context)
        {
            context.Tours.AddRange(
                new Tour[]
                {
                    new Tour
                    {
                        Title = "Yosemite National Park",
                        Description = "Yosemite National Park lies in the heart of California. With its 'hanging' valleys, many waterfalls, cirque lakes, polished domes, moraines and U-shaped valleys, it provides an excellent overview of all kinds of granite relief fashioned by glaciation. At 600–4,000 m, a great variety of flora and fauna can also be found here.",
                        PeopleNumber = 1,
                        Price = 50.00m,
                        HotelType = Enums.HotelType.NotAvailable,
                        TourType = Enums.TourType.Nature,
                        TransferType = Enums.TransferType.Bus,
                        IsAvailable = true,
                        IsHot = false,
                        ImageName = "yosemite.jpg"
                    },
                    new Tour
                    {
                        Title = "Chernobyl",
                        Description = "The Chernobyl accident attracted a great deal of interest. Because of the distrust that many people (both within and outside the USSR) had in the Soviet authorities, a great deal of debate about the situation at the site occurred in the First World during the early days of the event.",
                        PeopleNumber = 1,
                        Price = 1000.00m,
                        HotelType = Enums.HotelType.NotAvailable,
                        TourType = Enums.TourType.Extreme,
                        TransferType = Enums.TransferType.Car,
                        IsAvailable = true,
                        IsHot = true,
                        ImageName = "chernobyl.jpg"
                    },
                    new Tour
                    {
                        Title = "Prague",
                        Description = "Prague is home to a number of famous cultural attractions, many of which survived the violence and destruction of 20th-century Europe. Main attractions include the Prague Castle, the Charles Bridge, Old Town Square with the Prague astronomical clock.",
                        PeopleNumber = 2,
                        Price = 200.00m,
                        HotelType = Enums.HotelType.Four,
                        TourType = Enums.TourType.Romantic,
                        TransferType = Enums.TransferType.Plane,
                        IsAvailable = true,
                        IsHot = false,
                        ImageName = "prague.jpg"
                    },
                    new Tour
                    {
                        Title = "Cape Tarkhankut",
                        Description = "Cape Tarkhankut is a south-western cape of the Tarkhankut Peninsula, Crimea.",
                        PeopleNumber = 4,
                        Price = 150.00m,
                        HotelType = Enums.HotelType.Two,
                        TourType = Enums.TourType.Family,
                        TransferType = Enums.TransferType.Train,
                        IsAvailable = false,
                        IsHot = true,
                        ImageName = "tarkhankut.jpg"
                    },
                    new Tour
                    {
                        Title = "Beautiful Alaska",
                        Description = "Alaska is the largest U.S. State - in fact it is larger than all but 18 countries of the world, and about 1/5 of the total land area of the 48 contiguous states. Since the Aleutian Islands cross over the 180-degree longitude line, Alaska is in fact the westernmost, northernmost, and easternmost state!",
                        PeopleNumber = 2,
                        Price = 200.00m,
                        HotelType = Enums.HotelType.Three,
                        TourType = Enums.TourType.Extreme,
                        TransferType = Enums.TransferType.Bus,
                        IsAvailable = true,
                        IsHot = true,
                        ImageName = "alaska.jpg"
                    },
                    new Tour
                    {
                        Title = "Zürich",
                        Description = "Zürich is a leading global city and among the world's largest financial centres despite a relatively low population.[9] The city is home to a large number of financial institutions and banking giants. Most of Switzerland's research and development centres are concentrated in Zürich and the low tax rates attract overseas companies to set up their headquarters there.",
                        PeopleNumber = 1,
                        Price = 400.00m,
                        HotelType = Enums.HotelType.Five,
                        TourType = Enums.TourType.Business,
                        TransferType = Enums.TransferType.Plane,
                        IsAvailable = true,
                        IsHot = false,
                        ImageName = "zurich.jpg"
                    }
                });
            context.SaveChanges();
        }
    }
}

using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private BoothRepository boothsrepository;

        public Controller()
        {
            this.boothsrepository = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            int boothId = boothsrepository.Models.Count + 1;
            Booth booth = new Booth(boothId, capacity);
            boothsrepository.AddModel(booth);

            return $"Added booth number {boothId} with capacity {capacity} in the pastry shop!";
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            IBooth booth = boothsrepository.Models.FirstOrDefault(b => b.BoothId == boothId);

            if (cocktailTypeName != nameof(Hibernation) && cocktailTypeName != nameof(MulledWine))
            {
                return $"Cocktail type {cocktailTypeName} is not supported in our application!";
            }

            if (size != "Small" && size != "Miidle" && size != "Large")
            {
                return $"{size} is not recognized as valid cocktail size!";
            }

            ICocktail cocktail = booth.CocktailMenu.Models.FirstOrDefault(b => b.Name == cocktailName
            && b.Size == size);

            if (cocktail == null)
            {
                return $"{size} {cocktailName} is already added in the pastry shop!";
            }

            if (cocktailTypeName == nameof(Hibernation))
            {
                ICocktail hibernation = new Hibernation(cocktailName, size);
                booth.CocktailMenu.AddModel(hibernation);
            }
            else if (cocktailTypeName == nameof(MulledWine))
            {
                ICocktail mulledWine = new MulledWine(cocktailName, size);
                booth.CocktailMenu.AddModel(mulledWine);
            }

            return $"{size} {cocktailName} {cocktailTypeName} added to the pastry shop!";
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            IBooth booth = boothsrepository.Models.FirstOrDefault(x => x.BoothId == boothId);

            if (delicacyTypeName == nameof(Gingerbread))
            {
                IDelicacy delicacy = booth.DelicacyMenu.Models
                    .FirstOrDefault(a => a.Name == delicacyName);

                if (delicacy != null)
                {
                    return $"{delicacyName} is already added in the pastry shop!";
                }

                IDelicacy gingerbread = new Gingerbread(delicacyName);
                booth.DelicacyMenu.AddModel(gingerbread);
            }
            else if (delicacyTypeName == nameof(Stolen))
            {
                IDelicacy delicacy = booth.DelicacyMenu.Models
                    .FirstOrDefault(a => a.Name == delicacyName);

                if (delicacy != null)
                {
                    return $"{delicacyName} is already added in the pastry shop!";
                }

                IDelicacy stolen = new Stolen(delicacyName);
                booth.DelicacyMenu.AddModel(stolen);
            }
            else
            {
                return $"Delicacy type {delicacyTypeName} is not supported in our application!";
            }

            return $"{delicacyTypeName} {delicacyName} added to the pastry shop!";
        }

        public string BoothReport(int boothId)
        {
            throw new NotImplementedException();
        }

        public string LeaveBooth(int boothId)
        {
            throw new NotImplementedException();
        }

        public string ReserveBooth(int countOfPeople)
        {
            throw new NotImplementedException();
        }

        public string TryOrder(int boothId, string order)
        {
            throw new NotImplementedException();
        }
    }
}

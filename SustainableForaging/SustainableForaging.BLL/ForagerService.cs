using SustainableForaging.Core.Models;
using SustainableForaging.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SustainableForaging.BLL
{
    public class ForagerService
    {
        private readonly IForagerRepository repository;

        public ForagerService(IForagerRepository repository)
        {
            this.repository = repository;
        }

        public List<Forager> FindByState(string stateAbbr)
        {
            return repository.FindByState(stateAbbr);
        }

        public List<Forager> FindByLastName(string prefix)
        {
            return repository.FindAll()
                    .Where(i => i.LastName.StartsWith(prefix))
                    .ToList();
        }

        private Result<Forager> ValidateNulls(Forager forager)
        {
            var result = new Result<Forager>();

            if (forager == null)
            {
                result.AddMessage("Nothing to save.");
                return result;
            }

            if (string.IsNullOrEmpty(forager.FirstName))
            {
                result.AddMessage("First name is required.");
            }

            if (string.IsNullOrEmpty(forager.LastName))
            {
                result.AddMessage("First name is required.");
            }

            if (string.IsNullOrEmpty(forager.State))
            {
                result.AddMessage("First name is required.");
            }

            return result;
        }

        private void ValidateFields(Forager forager, Result<Forager> result)
        {
            // No future dates.
            if (forager.State.Length != 2)
            {
                result.AddMessage("State data must be a Two-Letter Abbreviation");
            }
        }

        private void ValidateNotDuplicate(Forager forager, Result<Forager> result) 
        {
            if (repository.FindAll().Any(f => 
                f.FirstName == forager.FirstName
                && f.LastName == forager.LastName
                && f.State == forager.State))
            {
                result.AddMessage("That forager already exists");
            }
        }

        private Result<Forager> Validate(Forager forager)
        {
            Result<Forager> result = ValidateNulls(forager);
            if (!result.Success)
            {
                return result;
            }

            ValidateFields(forager, result);
            if (!result.Success)
            {
                return result;
            }

            ValidateNotDuplicate(forager, result);
            if(!result.Success){
                return result;
            }

            return result;
        }

        public Result<Forager> Add(Forager forager)
        {
            Result<Forager> result = Validate(forager);
            if (!result.Success)
            {
                return result;
            }

            result.Value = repository.Add(forager);

            return result;
        }
    }
}

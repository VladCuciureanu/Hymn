using System.Collections.Generic;
using System.Linq;
using AutoMapper.Internal;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hymn.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private readonly ICollection<string> _errors = new List<string>();
        private readonly ICollection<ValidationFailure> _validationFailures = new List<ValidationFailure>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsOperationValid()) return Ok(result);

            var problemDetails = new ValidationProblemDetails(new Dictionary<string, string[]>());

            if (_errors.Any())
                problemDetails.Errors.Add("Messages", _errors.ToArray());

            _validationFailures.ForAll(e =>
            {
                if (!problemDetails.Errors.ContainsKey(e.PropertyName))
                {
                    problemDetails.Errors.Add(e.PropertyName, new[] {e.ErrorMessage});
                }
                else
                {
                    var errors = problemDetails.Errors[e.PropertyName].ToList();
                    errors.Add(e.ErrorMessage);
                    
                    problemDetails.Errors[e.PropertyName] = errors.ToArray();
                }
            });

            return BadRequest(problemDetails);
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            
            foreach (var error in errors) AddError(error.ErrorMessage);
            
            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors) AddError(error);

            return CustomResponse();
        }

        protected bool IsOperationValid()
        {
            return !_errors.Any() && !_validationFailures.Any();
        }

        protected void AddError(string error)
        {
            _errors.Add(error);
        }

        protected void AddError(ValidationFailure validationFailure)
        {
            _validationFailures.Add(validationFailure);
        }

        protected void ClearErrors()
        {
            _errors.Clear();
        }
    }
}
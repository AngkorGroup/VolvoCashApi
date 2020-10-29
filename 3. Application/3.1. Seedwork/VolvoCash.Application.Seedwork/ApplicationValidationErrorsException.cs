using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using VolvoCash.CrossCutting.Localization;

namespace VolvoCash.Application.Seedwork
{
    [Serializable]
    /// <summary>
	/// The custom exception for validation errors
	/// </summary>
	public class ApplicationValidationErrorsException : Exception , ISerializable
    {
        #region Properties
        [NonSerialized]
        /// <summary>
        /// Get or set the validation errors messages
        /// </summary>
        public readonly IEnumerable<string> ValidationErrors;
        #endregion

        #region Constructor
        /// <summary>
        /// Create new instance of Application validation errors exception
        /// </summary>
        /// <param name="validationErrors">The collection of validation errors</param>        
        protected ApplicationValidationErrorsException(SerializationInfo info, StreamingContext context) :base (info,context)
        {
        }

        /// <summary>
        /// Create new instance of Application validation errors exception
        /// </summary>
        /// <param name="validationErrors">The collection of validation errors</param>  
        public ApplicationValidationErrorsException(IEnumerable<string> validationErrors)
            : base(LocalizationFactory.CreateLocalResources().GetStringResource(LocalizationKeys.Application.validation_Exception))
        {
            ValidationErrors = validationErrors;
        }
        #endregion
    }
}

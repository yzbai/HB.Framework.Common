using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HB.Framework.Common
{
    /// <summary>
    /// 基础领域模型
    /// 内建验证机制。
    /// </summary>
    public class ValidatableObject : ISupportValidate
    {
        #region Validation

        private IList<ValidationResult>? _validateResults;

        public bool IsValid()
        {
            return PerformValidate();
        }

        public IList<ValidationResult> GetValidateResults()
        {
            if (_validateResults == null)
            {
                PerformValidate();
            }
            return _validateResults!;
        }

        public string GetValidateErrorMessage()
        {
            if (_validateResults == null)
            {
                PerformValidate();
            }

            StringBuilder builder = new StringBuilder();

            foreach (ValidationResult result in _validateResults!)
            {
                builder.AppendLine(result.ErrorMessage);
            }

            return builder.ToString();
        }

        private bool PerformValidate()
        {
            _validateResults = new List<ValidationResult>();
            ValidationContext vContext = new ValidationContext(this);
            return Validator.TryValidateObject(this, vContext, _validateResults, true);
        }

        #endregion
    }
}

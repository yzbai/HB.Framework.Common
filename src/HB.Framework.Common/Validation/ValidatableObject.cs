﻿#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HB.Framework.Common
{
    //TODO: 考虑验证嵌套类， 和 集合类
    //asp.net core model binding 可以验证嵌套类，但无法验证集合类
    /// <summary>
    /// 基础领域模型
    /// 内建验证机制。
    /// 不能应对嵌套的类的验证
    /// </summary>
    public class ValidatableObject : ISupportValidate
    {
        #region Validation

        private IList<ValidationResult>? _validateResults;
        private ValidationContext? _validationContext;

        public bool IsValid()
        {
            return PerformValidate();
        }

        public IList<ValidationResult> GetValidateResults(bool rePerformValidate = false)
        {
            if (_validateResults == null || rePerformValidate)
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

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public bool PerformValidate(string? propertyName = null)
        {
            try
            {
                _validateResults = new List<ValidationResult>();

                if (_validationContext == null)
                {
                    _validationContext = new ValidationContext(this);
                }

                if (!string.IsNullOrEmpty(propertyName))
                {
                    _validationContext.MemberName = propertyName;

                    object propertyValue = this.GetType().GetProperty(propertyName).GetValue(this);

                    return Validator.TryValidateProperty(propertyValue, _validationContext, _validateResults);
                }
                else
                {
                    bool result = Validator.TryValidateObject(this, _validationContext, _validateResults, true);

                    return result;
                }
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}

#nullable restore
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Messages;
using Core.Utilities.Interceptors;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect:MethodInterception
    {
        private readonly Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception(AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);//reflection ile bir instance üretiyor.ProductValidator oluşturuyor boş ctor olduğu için validationRule'lar içinde
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];//validatorType'In base'inin 0. generic argümanı
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);//örnek product tipindeki entityleri getir o methoda giden
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator,entity);
            }
        }
    }
}

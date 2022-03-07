using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            //RuleFor(p => p.ProductName).NotEmpty().Length(2,30);
            //RuleFor(p => p.ProductName).NotEmpty().WithMessage();with message tanımlamassak default mesaj döndürür 19dil tanımlı türkçe'de dahil // Static magic string bölümüne bunla ilgili class açılıp içine eklenebilir

            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).Length(2, 30);
           // RuleFor(p => p.ProductName).Must(StartWithA);


            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1);//unit price 1 den büyük veya eşit olmalı
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);//categoryId 1 olduğundan minumun 10 kabul et


        }

        private bool StartWithA(string arg)
        {//custom böyle yapıabilir ama burda business tanımlanmaz nesneye girilen değerlerle alakalı olmalı
            return arg.StartsWith("A");
        }
    }
}
